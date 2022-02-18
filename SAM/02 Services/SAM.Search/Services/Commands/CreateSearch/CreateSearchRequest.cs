using SAM.Search.Enums;

namespace SAM.Search.Services.Commands.CreateSearch
{
    /// <summary>
    /// Create search request
    /// </summary>
    public class CreateSearchRequest
    {
        /// <summary>
        /// Patient Id
        /// </summary>
        public int PatientId { get; set; }

        /// <summary>
        /// Search engine id
        /// </summary>
        public MatchEngineType MatchEngineId { get; set; }
    }
}
