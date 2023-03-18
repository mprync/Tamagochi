using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Tamagotchi.DataAccess.Models.Pagination;
using Tamagotchi.DataAccess.Responses;
using Tamagotchi.DataAccess.Responses.Errors;

namespace Tamagotchi.API.Actions
{
    /// <summary>
    /// A HTTP Action result object used to return HTTP status codes and results in an intuitive way
    /// </summary>
    public class HttpActionResult<T> : IActionResult
    {
        /// <summary>
        /// The result of the response
        /// </summary>
        public ApiResponse<T> Result { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="result"></param>
        [JsonConstructor]
        public HttpActionResult(ApiResponse<T> result)
        {
            Result = result;
        }
        
        /// <summary>
        /// Create a error instance of <see cref="HttpActionResult{T}"/>
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="detail"></param>
        /// <returns></returns>
        public static async Task<HttpActionResult<T>> Error(int statusCode, string detail)
        {
            return await Task.FromResult(new HttpActionResult<T>(
                new ApiResponse<T>
                {
                    Status = statusCode,
                    Errors = new List<ErrorResponse>
                    {
                        new()
                        {
                            Detail = detail,
                        }
                    }
                }));
        }

        /// <summary>
        /// Create a error instance of <see cref="HttpActionResult{T}"/>
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        public static async Task<HttpActionResult<T>> Error(int statusCode, IEnumerable<ErrorResponse> errors)
        {
            return await Task.FromResult(new HttpActionResult<T>(
                new ApiResponse<T>
                {
                    Status = statusCode,
                    Errors = errors
                }));
        }
        
        /// <summary>
        /// Create a success instance of <see cref="HttpActionResult{T}"/>
        /// </summary>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        public static async Task<HttpActionResult<T>> Success(int statusCode)
        {
            return await Task.FromResult(new HttpActionResult<T>(
                new ApiResponse<T>
                {
                    Status = statusCode
                }));
        }
        
        /// <summary>
        /// Create a success instance of <see cref="HttpActionResult{T}"/>
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static async Task<HttpActionResult<T>> Success(int statusCode, T data = default(T))
        {
            return await Task.FromResult(new HttpActionResult<T>(
                new ApiResponse<T>
                {
                    Status = statusCode,
                    Data = data
                }));
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="result"></param>
        /// <param name="errors"></param>
        public HttpActionResult(int statusCode, 
            T result,
            IEnumerable<ErrorResponse>? errors = null)
        {
            if (result != null && typeof(T) == typeof(ApiResponse<T>))
            {
                Result = result as ApiResponse<T>;
            }
            else
            {
                Result = new ApiResponse<T>
                {
                    Status = statusCode,
                    Errors = errors,
                    Data = result
                };
            }
        }

        /// <summary>
        /// Execute the result, generating a response based on the results data and status code
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task ExecuteResultAsync(ActionContext context)
        {
            if (Result is { Status: StatusCodes.Status204NoContent })
            {
                var objectResult = new NoContentResult();
                await objectResult.ExecuteResultAsync(context);
            }
            else
            {
                var objectResult = new ObjectResult(typeof(T).IsGenericType
                    ? typeof(T).GetGenericTypeDefinition() == typeof(PagedModel<>)
                        ? Result!.Data
                        : Result
                    : Result)
                {
                    StatusCode = Result!.Status
                };
                
                await objectResult.ExecuteResultAsync(context);
            }
        }
    }
}