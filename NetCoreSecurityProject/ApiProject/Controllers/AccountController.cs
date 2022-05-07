using ApiProject.ApiCustomResponse;
using ApiProject.Helpers;
using ApiProject.Models;
using DataAccessLayer;
using Entities_HBKSOFTWARE.JwtModels;
using EntityLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [RequestSizeLimit(5242880)]//We are limiting the body size as kb
    public class AccountController : ControllerBase
    {
        #region /*IoC*/
        private readonly IUnitOfWork<User> _unitOfWorkUser;
        private readonly IUnitOfWork<RefreshToken> _unitOfWorkRefreshToken;
        private readonly IUnitOfWork<Log> _unitOfWorkLog;
        private readonly IUnitOfWork<UserRole> _unitOfWorkUserRole;
        private readonly IBiggerToLower _biggerToLower;
        private readonly ICipherService _cipherService;

        private readonly JWTSettings _jwtSettings;

        #endregion

        #region /*ctor*/
        public AccountController(IUnitOfWork<User> unitOfWorkUser,
            IUnitOfWork<RefreshToken> unitOfWorkRefreshToken,
            IUnitOfWork<Log> unitOfWorkLog,
            IUnitOfWork<UserRole> unitOfWorkUserRole,
            IBiggerToLower biggerToLower,
            IOptions<JWTSettings> jwtSettings,
            ICipherService cipherService)
        {
            _unitOfWorkUser = unitOfWorkUser;
            _unitOfWorkRefreshToken = unitOfWorkRefreshToken;
            _unitOfWorkLog = unitOfWorkLog;
            _unitOfWorkUserRole = unitOfWorkUserRole;
            _jwtSettings = jwtSettings.Value;
            _biggerToLower = biggerToLower;
            _cipherService = cipherService;
        }
        #endregion

        #region /*Login*/
        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] LoginViewModel loginViewModel)
        {
            try
            {
                var user = await _unitOfWorkUser.RepositoryUser.GetUserForLogin(loginViewModel.UserEMail);
                if (user != null)
                {
                    var encryptedPassword = _cipherService.Decrypt(user.UserPassword);
                    if (encryptedPassword == loginViewModel.UserPassword)
                    {
                        RefreshToken refreshToken = GenerateRefreshToken();
                        refreshToken.UserID = user.UserID;
                        await _unitOfWorkRefreshToken.RepositoryRefreshToken.CreateAsync(refreshToken);
                        await _unitOfWorkUser.CompleteAsync();

                        UserWithToken userWithToken = new();
                        userWithToken.AccessToken = GenerateAccessToken(user.UserID);
                        userWithToken.RefreshToken = refreshToken.Token;
                        userWithToken.UserGuidID = user.UserGuidID;
                        return Ok(new CustomOk(true, "No Error!", userWithToken));
                    }
                    else
                    {
                        return Ok(new CustomOk(true, "No Error!", "nullObject"));
                    }
                }
                else
                {
                    return Ok(new CustomOk(false, "Your E-mail address or password is incorrect!", "nullObject"));
                }
            }
            catch (Exception ex)
            {
                Log log = new()
                {
                    LogDescription = "try-catch " + ex.Message,
                    LogType = "try-catch",
                    TableID = "api/Account/Login",
                    TableName = "User",
                    CreatedTime = DateTime.Now
                };
                await _unitOfWorkLog.RepositoryLog.CreateAsync(log);
                await _unitOfWorkLog.CompleteAsync();
                return NotFound(new CustomInternalServerError(false, "try-catch " + ex.Message, "nullObject"));
            }
        }
        #endregion

        #region /*Register*/
        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] RegisterViewModel registerViewModel)
        {
            try
            {
                registerViewModel.User.UserEmail = _biggerToLower.CharacterReplacementBiggerToLower(registerViewModel.User.UserEmail);
                var user = await _unitOfWorkUser.RepositoryUser.GetAllUsersByEmailAndPhone(registerViewModel.User.UserEmail);
                if (user != null)
                {
                    return Ok(new CustomOk(false, "It has already been registered by someone with the E-Mail Address or Telephone Number you have entered! Please try again.", "nullObject"));
                }
                else
                {
                    if (registerViewModel.User.UserPassword != registerViewModel.UserAgainPassword)
                    {
                        return Ok(new CustomOk(false, "The passwords you entered do not match! Please try again.", "nullObject"));
                    }
                    else
                    {
                        registerViewModel.User.UserPassword = _cipherService.Encrypt(registerViewModel.User.UserPassword);
                        registerViewModel.User.UserName = registerViewModel.User.UserName.ToUpper();
                        registerViewModel.User.UserSurname = registerViewModel.User.UserSurname.ToUpper();
                        await _unitOfWorkUser.RepositoryUser.CreateAsync(registerViewModel.User);
                        await _unitOfWorkUser.CompleteAsync();

                        UserRole userRole = new()
                        {
                            UserID = registerViewModel.User.UserID,
                            RoleGroupID = 1,
                            Roles = 3
                        };
                        await _unitOfWorkUserRole.RepositoryUserRole.CreateAsync(userRole);
                        await _unitOfWorkUserRole.CompleteAsync();
                        return Ok(new CustomOk(true, "Your registration has been successfully received.", "nullObject"));
                    }
                }
            }
            catch (Exception ex)
            {
                Log log = new()
                {
                    LogDescription = "try-catch " + ex.Message,
                    LogType = "try-catch",
                    TableID = "api/Account/Register",
                    TableName = "User",
                    CreatedTime = DateTime.Now
                };
                await _unitOfWorkLog.RepositoryLog.CreateAsync(log);
                await _unitOfWorkLog.CompleteAsync();
                return NotFound(new CustomInternalServerError(false, "try-catch " + ex.Message, "nullObject"));
            }
        }
        #endregion

        #region /*RefreshToken*/
        [HttpPost("RefreshToken")]
        public async Task<ActionResult<UserWithToken>> RefreshToken([FromBody] RefreshRequest refreshRequest)
        {
            User user = await GetUserFromAccessToken(refreshRequest.AccessToken);//Kullanıcının mevcut token'ı üzerinden token'ı kullanan kullanıcı bulunuyor.

            if (user != null && ValidateRefreshToken(user, refreshRequest.RefreshToken))//User'ın barındırdığı refresh token kontrol ediliyor
            {
                UserWithToken userWithToken = new UserWithToken();
                userWithToken.AccessToken = GenerateAccessToken(user.UserID);
                userWithToken.RefreshToken = refreshRequest.RefreshToken;
                return userWithToken;// Kontroller sağlanırsa yeni bir AccessToken üretilip kullanıcıya aktarılıyor.
            }
            return null;
        }
        #endregion

        #region /*GetUserFromAccessToken*/
        private async Task<User> GetUserFromAccessToken(string accessToken)//AccessToken üzerinden kullanıcı bilgileri çağırılıyor.
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                var principle = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out SecurityToken securityToken);

                if (securityToken is JwtSecurityToken jwtSecurityToken && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    string userId = principle.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                    return await _unitOfWorkUser.RepositoryUser.GetUserFromAccessToken(Convert.ToInt32(userId));
                }
                else
                {
                    return new User();
                }
            }
            catch (Exception ex)
            {
                Log log = new()
                {
                    LogDescription = "try-catch " + ex.Message,
                    LogType = "try-catch",
                    TableID = "/GetUserFromAccessToken",
                    TableName = "User",
                };
                await _unitOfWorkLog.RepositoryLog.CreateAsync(log);
                await _unitOfWorkLog.CompleteAsync();
                return new User();
            }
        }
        #endregion

        #region /*ValidateRefreshToken*/
        private bool ValidateRefreshToken(User user, string refreshToken)//RefreshToken ve userID üzerinden kontrol sağlanıyor.
        {

            RefreshToken refreshTokenUser = _unitOfWorkRefreshToken.RepositoryRefreshToken.ValidateRefreshToken(refreshToken);

            if (refreshTokenUser != null && refreshTokenUser.UserID == user.UserID
                && refreshTokenUser.ExpiryDate > DateTime.UtcNow)
            {
                return true;
            }

            return false;
        }
        #endregion

        #region /*GenerateRefreshToken*/
        private RefreshToken GenerateRefreshToken()//RefreshToken burada üretilir.
        {
            RefreshToken refreshToken = new();
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                refreshToken.Token = Convert.ToBase64String(randomNumber);
            }
            refreshToken.ExpiryDate = DateTime.UtcNow.AddMonths(6);

            return refreshToken;
        }
        #endregion

        #region /*GenerateAccessToken*/
        private string GenerateAccessToken(int userId)//AccessToken burada üretilir.
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, Convert.ToString(userId))
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        #endregion
    }
}
