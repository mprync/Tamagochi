using Tamagotchi.API.Actions;
using Tamagotchi.DataAccess.Responses;
using Tamagotchi.DataAccess.Responses.Errors;

namespace Tamagotchi.API.Helpers
{
    /// <summary>
    /// Query helper
    /// </summary>
    public static class QueryHelper
    {
        /// <summary>
        /// Execute query
        /// </summary>
        /// <param name="query"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async Task<HttpActionResult<T>> ExecuteQuery<T>(Func<Task<HttpActionResult<T>>> query)
        {
            try
            {
                return await query.Invoke();
            }
            catch (Exception e)
            {
                return new HttpActionResult<T>(
                    new ApiResponse<T>
                    {
                        Status = StatusCodes.Status500InternalServerError,
                        Errors = new[]
                        {
                            new ErrorResponse
                            {
#if DEBUG
                                Detail = e.InnerException?.Message ?? e.Message,
#else
                                Detail = $"An error has occured on the server.",
#endif
                            }
                        }
                    });
            }
        }
    }
}