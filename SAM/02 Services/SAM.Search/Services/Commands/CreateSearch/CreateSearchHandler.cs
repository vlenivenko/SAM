using System.Net;
using AutoMapper;
using SAM.Core.CQRS.Handlers;
using SAM.Core.CQRS.Responses;
using SAM.Core.CQRS.Validation.Interfaces;
using SAM.Repository.Repositories.Interfaces;
using SAM.Search.SearchEngines;

namespace SAM.Search.Services.Commands.CreateSearch
{
    /// <summary>
    /// Handles create search request
    /// </summary>
    public class CreateSearchHandler : ValidatedHandler<CreateSearchRequest, CreateSearchResponse>
    {
        private readonly IRepository _repository;
        private readonly ISearchEngineFactory _searchEngineFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSearchHandler"/> class.
        /// </summary>
        /// <param name="validator"></param>
        public CreateSearchHandler(
            IRepository repository,
            ISearchEngineFactory searchEngineFactory,
            IRequestValidator<CreateSearchRequest, CreateSearchResponse> validator)
            : base(validator)
        {
            _repository = repository;
            _searchEngineFactory = searchEngineFactory;
        }

        /// <inheritdoc/>
        public override async Task<HandlerResponse<CreateSearchResponse>> HandleValidatedRequestAsync(CreateSearchRequest request)
        {
            var patient = await _repository.GetByIdAsync<SAM.Repository.Models.Patient>(request.PatientId);
            if (patient == null)
            {
                throw new ArgumentException(nameof(request.PatientId));
            }

            var searchEngineClient = _searchEngineFactory.GetClient(request.MatchEngineId);
            await searchEngineClient.SearchAsync(patient);

            return new OkHandlerResponse<CreateSearchResponse>(HttpStatusCode.Created, new CreateSearchResponse());
        }
    }
}