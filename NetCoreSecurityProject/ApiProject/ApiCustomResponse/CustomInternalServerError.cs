using System.Net;

namespace ApiProject.ApiCustomResponse
{
    public class CustomInternalServerError : ApiCustomResponse
    {

        public CustomInternalServerError() : base(500, HttpStatusCode.InternalServerError.ToString())
        {
        }


        public CustomInternalServerError(bool isSuccess, string message, object source)
            : base(500, HttpStatusCode.InternalServerError.ToString(), isSuccess, message, source)
        {
        }
    }
}
