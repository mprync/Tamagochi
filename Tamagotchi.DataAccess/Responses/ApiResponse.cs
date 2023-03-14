using Tamagotchi.DataAccess.Responses.Errors;

namespace Tamagotchi.DataAccess.Responses
{
    /// <summary>
    /// Base response class
    /// </summary>
    public class ApiResponse<T>
    {
        /// <summary>
        /// Response data
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Gets or sets the HTTP status code, defaults to HTTP Code 200 (OK)
        /// </summary>
        public int Status { get; set; } = 200;

        /// <summary>
        /// Error collection, always present if at least 1 error is present, otherwise null
        /// </summary>
        public IEnumerable<ErrorResponse> Errors { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="errors"></param>
        /// <returns></returns>
        public ApiResponse(IEnumerable<ErrorResponse> errors)
        {
            Errors = errors;
        }

        /// <summary>
        /// Parameterless Constructor
        /// </summary>
        public ApiResponse()
        {
        }
    }
}