using System.Net;
using SAM.Core.CQRS.Validation;
using SAM.Repository.Repositories.Interfaces;

namespace SAM.Search.Services.Commands.CreateSearch
{
    /// <summary>
    /// Validates create search request
    /// </summary>
    public class CreateSearchValidator : BaseValidator<CreateSearchRequest, CreateSearchResponse>
    {
        private readonly IRepository _repository;

        public const string PatientDoesNotExistErrorMessage = "Patient with provided id could not be found";

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSearchValidator"/> class.
        /// </summary>
        public CreateSearchValidator(
            IRepository repository)
        {
            _repository = repository;

            SetupValidationSteps();
        }

        private void SetupValidationSteps()
        {
            Must(DoesPatientExist)
                .WithErrorMessage(PatientDoesNotExistErrorMessage)
                .WithStatusCode((int)HttpStatusCode.BadRequest);
        }

        private bool DoesPatientExist(CreateSearchRequest request)
        {
            var patient = _repository.GetByIdAsync<SAM.Repository.Models.Patient>(request.PatientId).Result;

            return patient != null;
        }
    }
}