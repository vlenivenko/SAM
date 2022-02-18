using System.Net;
using SAM.Core.CQRS.Validation;
using SAM.Repository.Repositories.Interfaces;
using SAM.Search.SearchEngines;

namespace SAM.Search.Services.Commands.CreateSearch
{
    /// <summary>
    /// Validates create search request
    /// </summary>
    public class CreateSearchValidator : BaseValidator<CreateSearchRequest, CreateSearchResponse>
    {
        private readonly IRepository _repository;
        private readonly ISearchEngineFactory _searchEngineFactory;

        public const string PatientDoesNotExistErrorMessage = "Patient with provided id could not be found";
        public const string SearchEngineDoesNotExistErrorMessage = "Search engine with provided id could not be found";

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSearchValidator"/> class.
        /// </summary>
        public CreateSearchValidator(
            IRepository repository,
            ISearchEngineFactory searchEngineFactory)
        {
            _repository = repository;
            _searchEngineFactory = searchEngineFactory;

            SetupValidationSteps();
        }

        private void SetupValidationSteps()
        {
            Must(DoesPatientExist)
                .WithErrorMessage(PatientDoesNotExistErrorMessage)
                .WithStatusCode((int)HttpStatusCode.BadRequest);

            Must(DoesSearchEngineExist)
                .WithErrorMessage(SearchEngineDoesNotExistErrorMessage)
                .WithStatusCode((int)HttpStatusCode.BadRequest);
        }

        private bool DoesPatientExist(CreateSearchRequest request)
        {
            var patient = _repository.GetByIdAsync<SAM.Repository.Models.Patient>(request.PatientId).Result;

            return patient != null;
        }

        private bool DoesSearchEngineExist(CreateSearchRequest request)
        {
            try
            {
                var searchEngine = _searchEngineFactory.GetClient(request.MatchEngineId);
                return searchEngine != null;
            }
            catch (NotImplementedException)
            {
                return false;
            }
        }
    }
}