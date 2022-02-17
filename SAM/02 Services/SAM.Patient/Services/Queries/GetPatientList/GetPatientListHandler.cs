using SAM.Core.CQRS.Handlers;
using SAM.Core.CQRS.Responses;
using SAM.Core.CQRS.Validation.Interfaces;

namespace SAM.Patient.Services.Queries.GetPatientList
{
    /// <summary>
    /// Handles get patient list request
    /// </summary>
    public class GetPatientListHandler : ValidatedHandler<GetPatientListRequest, GetPatientListResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetPatientListHandler"/> class.
        /// </summary>
        /// <param name="validator"></param>
        public GetPatientListHandler(
            IRequestValidator<GetPatientListRequest, GetPatientListResponse> validator)
            : base(validator)
        {
        }

        /// <inheritdoc/>
        public override async Task<HandlerResponse<GetPatientListResponse>> HandleValidatedRequestAsync(GetPatientListRequest request)
        {
            return await Task.FromResult<HandlerResponse<GetPatientListResponse>>(null);
        }
    }
}