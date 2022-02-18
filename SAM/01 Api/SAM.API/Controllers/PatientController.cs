using Microsoft.AspNetCore.Mvc;
using SAM.Core.CQRS.Handlers.Interfaces;
using SAM.Patient.Services.Commands.CreatePatient;
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
        private readonly IRequestHandler<CreatePatientRequest, CreatePatientResponse> _createPatientHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientController"/> class.
        /// </summary>
        public PatientController(
            ILogger<PatientController> logger,
            IRequestHandler<GetPatientListRequest, GetPatientListResponse> getPatientListHandler,
            IRequestHandler<CreatePatientRequest, CreatePatientResponse> createPatientHandler)
        {
            _logger = logger;

            _getPatientListHandler = getPatientListHandler;
            _createPatientHandler = createPatientHandler;
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

        /// <summary>
        /// Create patient
        /// </summary>
        /// <returns>Id of created patient</returns>
        /// <response code="201">Request successful</response>
        /// <response code="400">Bad request\Validation failed</response>
        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(CreatePatientResponse), 201)]
        public async Task<IActionResult> Post(CreatePatientRequest request)
        {
            var response = await _createPatientHandler.HandleAsync(request);

            if (response.IsSuccessStatusCode())
            {
                return Ok(response.SuccessResponse);
            }
            else
            {
                var message = response.ErrorResponse.Error.Message;
                _logger.LogWarning($"Failed creating patient. Details - {message}.");

                return StatusCode((int)response.HttpStatusCode, message);
            }
        }
    }
}