namespace Tamagotchi.DataAccess.Responses.Errors
{
    /// <summary>
    /// An error response used for all API calls that generate errors
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// A human-readable explanation specific to this occurrence of the problem.
        /// Like title, this field’s value can be localized.
        /// </summary>
        public string Detail { get; set; } = null;
    }
}