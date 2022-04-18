using ApiProject.ApiCustomResponse;
using ApiProject.Helpers;
using DataAccessLayer;
using EntityLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ApiProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ServiceFilter(typeof(PermissionFilter))]
    public class LogController : ControllerBase
    {
        #region /*IoC*/ 
        private readonly IUnitOfWork<Log> _unitOfWorkLog;
        #endregion

        #region /*ctor*/
        public LogController(IUnitOfWork<Log> unitOfWorkLog)
        {
            _unitOfWorkLog = unitOfWorkLog;
        }
        #endregion

        #region /*Get*/
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(new CustomOk(true, "Success", await _unitOfWorkLog.RepositoryLog.GetAllAsync()));
            }
            catch (Exception ex)
            {
                Log log = new()
                {
                    LogDescription = "try-catch " + ex.Message,
                    LogType = "try-catch",
                    TableID = "api/Log/Get",
                    TableName = "Log",
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
