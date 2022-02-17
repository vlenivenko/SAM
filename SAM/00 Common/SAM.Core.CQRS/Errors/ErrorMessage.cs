namespace SAM.Core.CQRS.Errors
{
    /// <summary>
    /// Error message object
    /// </summary>
    public class ErrorMessage
    {
        /// <summary>
        /// Error message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorMessage"/> class.
        /// </summary>
        /// <param name="message">Error message</param>
        public ErrorMessage(string message)
        {
            Message = message;
        }
    }
}