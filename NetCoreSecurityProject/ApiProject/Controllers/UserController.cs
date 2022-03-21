using DataAccessLayer;
using EntityLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        #region /*IoC*/
        private readonly IUnitOfWork<User> _unitOfWorkUser;
        #endregion

        #region /*ctor*/
        public UserController(IUnitOfWork<User> unitOfWorkUser)
        {
            _unitOfWorkUser = unitOfWorkUser;
        }
        #endregion

        #region /*Get*/
        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await _unitOfWorkUser.RepositoryUser.GetAllAsync();
        }
        #endregion

        #region /*Post*/
        [HttpPost]
        public async Task<string> Post(User user)
        {
            string responseMessage;
            try
            {
                if (ModelState.IsValid)
                {
                    await _unitOfWorkUser.RepositoryUser.CreateAsync(user);
                    await _unitOfWorkUser.CompleteAsync();
                    return responseMessage = "Success";
                }
                else
                {
                    return responseMessage = "Failed";
                }
            }
            catch (System.Exception ex)
            {
                return responseMessage = ex.Message;
            }
        }
        #endregion
    }
}
