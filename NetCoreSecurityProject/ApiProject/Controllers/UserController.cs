using ApiProject.ApiCustomResponse;
using DataAccessLayer;
using EntityLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ApiProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        #region /*IoC*/
        private readonly IUnitOfWork<User> _unitOfWorkUser;
        private readonly IUnitOfWork<Log> _unitOfWorkLog;
        #endregion

        #region /*ctor*/
        public UserController(IUnitOfWork<User> unitOfWorkUser,
            IUnitOfWork<Log> unitOfWorkLog)
        {
            _unitOfWorkUser = unitOfWorkUser;
            _unitOfWorkLog = unitOfWorkLog;
        }
        #endregion

        #region /*Get*/
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(new CustomOk(true, "Success", await _unitOfWorkUser.RepositoryUser.GetAllAsync()));
            }
            catch (Exception ex)
            {
                Log log = new()
                {
                    LogDescription = "try-catch " + ex.Message,
                    LogType = "try-catch",
                    TableID = "api/Users/Get",
                    TableName = "User",
                    CreatedTime = DateTime.Now
                };
                await _unitOfWorkLog.RepositoryLog.CreateAsync(log);
                await _unitOfWorkLog.CompleteAsync();
                return NotFound(new CustomInternalServerError(false, "try-catch " + ex.Message, "nullObject"));
            }
        }
        #endregion

        #region /*GetByID*/
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            try
            {
                var isUserExist = await _unitOfWorkUser.RepositoryUser.IsUserExist(id);
                if (isUserExist.Count != 0)
                {
                    return Ok(new CustomOk(true, "Success", await _unitOfWorkUser.RepositoryUser.GetByIDAsync(id)));
                }
                else
                {
                    return BadRequest(new CustomBadRequest(false, "There is no such a user like that!", "nullObject"));
                }
            }
            catch(Exception ex)
            {
                Log log = new()
                {
                    LogDescription = "try-catch " + ex.Message,
                    LogType = "try-catch",
                    TableID = "api/Users/Get/"+id.ToString(),
                    TableName = "User",
                    CreatedTime = DateTime.Now
                };
                await _unitOfWorkLog.RepositoryLog.CreateAsync(log);
                await _unitOfWorkLog.CompleteAsync();
                return NotFound(new CustomInternalServerError(false, "try-catch " + ex.Message, "nullObject"));
            }
        }
        #endregion

        #region /*Post*/
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] User user)
        {
            try
            {
                await _unitOfWorkUser.RepositoryUser.CreateAsync(user);
                await _unitOfWorkUser.CompleteAsync();
                return Ok(new CustomOk(true, "Success", "nullObject"));
            }
            catch (System.Exception ex)
            {
                Log log = new()
                {
                    LogDescription = "try-catch " + ex.Message,
                    LogType = "try-catch",
                    TableID = "api/Users/Post",
                    TableName = "User",
                    CreatedTime = DateTime.Now
                };
                await _unitOfWorkLog.RepositoryLog.CreateAsync(log);
                await _unitOfWorkLog.CompleteAsync();
                return NotFound(new CustomInternalServerError(false, "try-catch " + ex.Message, "nullObject"));
            }
        }
        #endregion

        #region /*Update*/
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] User user)
        {
            try
            {
                await _unitOfWorkUser.RepositoryUser.UpdateAsync(user);
                await _unitOfWorkUser.CompleteAsync();
                return Ok(new CustomOk(true, "Success", "nullObject"));
            }
            catch (System.Exception ex)
            {
                Log log = new()
                {
                    LogDescription = "try-catch " + ex.Message,
                    LogType = "try-catch",
                    TableID = "api/Users/Update",
                    TableName = "User",
                    CreatedTime = DateTime.Now
                };
                await _unitOfWorkLog.RepositoryLog.CreateAsync(log);
                await _unitOfWorkLog.CompleteAsync();
                return NotFound(new CustomInternalServerError(false, "try-catch " + ex.Message, "nullObject"));
            }
        }
        #endregion
    }
}
