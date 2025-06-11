
using Microsoft.AspNetCore.Mvc;
using static PalladiumPayroll.Helper.Constants.AppConstants;

namespace PalladiumPayroll.DTOs.Miscellaneous
{
    public class HttpStatusCodeResponse
    {
        public static JsonResult GenerateResponse<T>(bool result, int statusCode, string message, T data)
        {
            return new JsonResult(new HttpApiResponse<T>
            {
                Result = result,
                StatusCode = statusCode,
                Message = message,
                Data = data
            });
        }

        public static JsonResult SuccessResponse<T>(T data, string message)
        {
            return GenerateResponse<T>(
                true,
                (int)HttpStatusCodeMessages.HttpStatus.Success,
                message,
                data
            );
        }

        public static JsonResult NotFoundResponse(string notFoundMessage)
        {
            return GenerateResponse<string>(
                false,
                (int)HttpStatusCodeMessages.HttpStatus.NotFound,
                string.Format(ResponseMessages.NotFound, notFoundMessage),
                string.Empty
            );
        }

        public static JsonResult BadRequestResponse()
        {
            return GenerateResponse<string>(
                false,
                (int)HttpStatusCodeMessages.HttpStatus.BadRequest,
                ResponseMessages.UnexpectedError,
                string.Empty
            );
        }

        public static JsonResult InternalServerErrorResponse(string message)
        {
            return GenerateResponse<string>(
                false,
                (int)HttpStatusCodeMessages.HttpStatus.InternalServerError,
                message,
                string.Empty
            );
        }

        public static JsonResult UnAuthorizedResponse()
        {
            return GenerateResponse<string>(
                false,
                (int)HttpStatusCodeMessages.HttpStatus.UnAuthorized,
                ResponseMessages.UnAuthorized,
                string.Empty
            );
        }

    }
}
