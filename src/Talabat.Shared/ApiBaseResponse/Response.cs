using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Talabat.Shared.ApiBaseResponse
{
    public enum ErrorType
    {
        Validation,
        NotFound,
        Unauthorized,
        Forbidden,
        ServerError,
        BadRequest,
        Conflict
    }
    public class Response
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public object? Data { get; set; }
        public string Message { get; set; } = string.Empty;
        public static Task<Response> SuccessAsync(object? data = null, string? message = null)
        {
            return Task.FromResult(new Response
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Data = data,
                Message = message ?? string.Empty
            });
        }
        public static Task<Response> FailureAsync(string message, ErrorType errorType, object? data = null)
        {
            var statusCode = errorType switch
            {
                ErrorType.Validation => HttpStatusCode.BadRequest,
                ErrorType.NotFound => HttpStatusCode.NotFound,
                ErrorType.Unauthorized => HttpStatusCode.Unauthorized,
                ErrorType.Forbidden => HttpStatusCode.Forbidden,
                ErrorType.ServerError => HttpStatusCode.InternalServerError,
                ErrorType.Conflict => HttpStatusCode.Conflict,
                _ => HttpStatusCode.BadRequest
            };
            return Task.FromResult(new Response
            {
                StatusCode = statusCode,
                IsSuccess = false,
                Message = message,
                Data = data
            });
        }
    }
}