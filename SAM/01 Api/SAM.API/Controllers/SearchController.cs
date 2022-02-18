using Microsoft.AspNetCore.Mvc;
using SAM.Core.CQRS.Handlers.Interfaces;
using SAM.Search.Services.Commands.CreateSearch;

namespace SAM.API.Controllers
{
    /// <summary>
    /// Search controller
    /// </summary>
    [Route("/api/search")]
    [ApiController]
    public class SearchController : Controller
    {
        private readonly ILogger<SearchController> _logger;

        private readonly IRequestHandler<CreateSearchRequest, CreateSearchResponse> _createSearchHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchController"/> class.
        /// </summary>
        public SearchController(
            ILogger<SearchController> logger,
            IRequestHandler<CreateSearchRequest, CreateSearchResponse> createSearchHandler)
        {
            _logger = logger;

            _createSearchHandler = createSearchHandler;
        }

        /// <summary>
        /// Create search
        /// </summary>
        /// <returns></returns>
        /// <response code="201">Request successful</response>
        /// <response code="400">Bad request\Validation failed</response>
        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(CreateSearchResponse), 201)]
        public async Task<IActionResult> Post(CreateSearchRequest request)
        {
            var response = await _createSearchHandler.HandleAsync(request);

            if (response.IsSuccessStatusCode())
            {
                return Ok(response.SuccessResponse);
            }
            else
            {
                var message = response.ErrorResponse.Error.Message;
                _logger.LogWarning($"Failed creating search. Details - {message}.");

                return StatusCode((int)response.HttpStatusCode, message);
            }
        }
    }
}
