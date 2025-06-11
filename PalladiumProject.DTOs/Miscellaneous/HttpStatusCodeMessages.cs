using System.ComponentModel;

namespace PalladiumPayroll.DTOs.Miscellaneous
{
    public class HttpStatusCodeMessages
    {
        public enum HttpStatus
        {
            [Description("Success")]
            Success = 200,

            [Description("Internal server error")]
            InternalServerError = 500,

            [Description("Bad Request, issue in client request")]
            BadRequest = 400,

            [Description("Not Found")]
            NotFound = 404,

            [Description("Already Exist")]
            Conflict = 409,

            [Description("Unauthorized, Invalid Token")]
            UnAuthorized = 401
        }


    }
}
