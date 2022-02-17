using System.Net;
using SAM.Core.CQRS.Validation;

namespace SAM.Patient.Services.Queries.GetPatientList
{
    /// <summary>
    /// Validates get patient list request
    /// </summary>
    public class GetPatientListValidator : BaseValidator<GetPatientListRequest, GetPatientListResponse>
    {
        public const string ErrorMessage = "Request is not valid";

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPatientListValidator"/> class.
        /// </summary>
        public GetPatientListValidator()
        {
            SetupValidationSteps();
        }

        private void SetupValidationSteps()
        {
            Must(IsValid)
                .WithErrorMessage(ErrorMessage)
                .WithStatusCode((int)HttpStatusCode.BadRequest);
        }

        private bool IsValid(GetPatientListRequest request)
        {
            return request != null;
        }
    }
}