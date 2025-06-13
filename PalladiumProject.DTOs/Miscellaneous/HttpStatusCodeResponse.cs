using Microsoft.AspNetCore.Mvc;
using System.Net;
using static PalladiumPayroll.Helper.Constants.AppConstants;

namespace PalladiumPayroll.DTOs.Miscellaneous
{
    public static class HttpStatusCodeResponse
    {
        public static JsonResult GenerateResponse<T>(bool result, HttpStatusCode statusCode, string message, T data)
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
            return GenerateResponse(
                true,
                HttpStatusCode.OK,
                message,
                data
            );
        }

        public static JsonResult NotFoundResponse(string notFoundMessage)
        {
            return GenerateResponse(
                false,
                HttpStatusCode.NotFound,
                string.Format(ResponseMessages.NotFound, notFoundMessage),
                string.Empty
            );
        }

        public static JsonResult BadRequestResponse()
        {
            return GenerateResponse(
                false,
                HttpStatusCode.BadRequest,
                ResponseMessages.UnexpectedError,
                string.Empty
            );
        }

        public static JsonResult InternalServerErrorResponse(string message)
        {
            return GenerateResponse(
                false,
                HttpStatusCode.InternalServerError,
                message,
                string.Empty
            );
        }

        public static JsonResult UnAuthorizedResponse()
        {
            return GenerateResponse(
                false,
                HttpStatusCode.Unauthorized,
                ResponseMessages.UnAuthorized,
                string.Empty
            );
        }

    }
}
