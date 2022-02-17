using Microsoft.AspNetCore.Mvc;
using SAM.Core.CQRS.Handlers.Interfaces;
using SAM.Patient.Services.Queries.GetPatientList;

namespace SAM.API.Controllers
{
    /// <summary>
    /// Patient controller
    /// </summary>
    [Route("/api/patient")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly ILogger<PatientController> _logger;

        private readonly IRequestHandler<GetPatientListRequest, GetPatientListResponse> _getPatientListHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientController"/> class.
        /// </summary>
        public PatientController(
            ILogger<PatientController> logger,
            IRequestHandler<GetPatientListRequest, GetPatientListResponse> getPatientListHandler)
        {
            _logger = logger;

            _getPatientListHandler = getPatientListHandler;
        }

        /// <summary>
        /// Returns patient list
        /// </summary>
        /// <returns>List of patients data</returns>
        /// <response code="200">Request successful</response>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(GetPatientListResponse), 200)]
        public async Task<IActionResult> Get()
        {
            var request = new GetPatientListRequest();

            var response = await _getPatientListHandler.HandleAsync(request);

            if (response.IsSuccessStatusCode())
            {
                return Ok(response.SuccessResponse);
            }
            else
            {
                var message = response.ErrorResponse.Error.Message;
                _logger.LogWarning($"Failed getting patient list. Details - {message}.");

                return StatusCode((int)response.HttpStatusCode, message);
            }
        }
    }
}