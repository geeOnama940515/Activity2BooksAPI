using Activity2BooksAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Activity2BooksAPI.Services.Helpers
{
    public static class ResponseHelper
    {
        public static IActionResult CreateResponse<T>(this ControllerBase controller, int statusCode, string message, T data)
        {
            var response = new ResponseDTO<T>
            {
                StatusCode = statusCode,
                Message = message,
                Data = data
            };

            return statusCode switch
            {
                200 => controller.Ok(response),
                201 => controller.StatusCode(201, response),
                400 => controller.BadRequest(response),
                404 => controller.NotFound(response),
                _ => controller.StatusCode(statusCode, response)
            };
        }

        public static IActionResult CreateResponse(this ControllerBase controller, int statusCode, string message)
        {
            var response = new ResponseDTO<object>
            {
                StatusCode = statusCode,
                Message = message,
                Data = null
            };

            return statusCode switch
            {
                200 => controller.Ok(response),
                201 => controller.StatusCode(201, response),
                400 => controller.BadRequest(response),
                404 => controller.NotFound(response),
                _ => controller.StatusCode(statusCode, response)
            };
        }
    }
}
