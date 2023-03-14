namespace Tamagotchi.DataAccess.Responses
{
    public class Response
    {
        /// <summary>
        /// A human-readable explanation specific to this occurrence of the problem.
        /// Like title, this field’s value can be localized.
        /// </summary>
        public string Detail { get; set; }

        /// <summary>
        /// A short, human-readable summary of the problem that SHOULD NOT change from occurrence to occurrence of the response,
        /// except for purposes of localization.
        /// </summary>
        public string Title { get; set; } = null;
    }
}