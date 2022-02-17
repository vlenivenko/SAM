namespace SAM.Core.CQRS.Errors
{
    /// <summary>
    /// Error response object
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorResponse"/> class.
        /// </summary>
        /// <param name="error">Error message</param>
        public ErrorResponse(string error)
        {
            Error = new ErrorMessage(error);
        }

        /// <summary>
        /// Error object
        /// </summary>
        public ErrorMessage Error { get; set; }

        /// <summary>
        /// Exception that was thrown
        /// </summary>
        public Exception Exception { get; set; }
    }
}