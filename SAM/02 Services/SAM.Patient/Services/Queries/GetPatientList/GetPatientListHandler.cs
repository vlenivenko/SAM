using AutoMapper;
using SAM.Core.CQRS.Handlers;
using SAM.Core.CQRS.Responses;
using SAM.Core.CQRS.Validation.Interfaces;
using SAM.Repository.Repositories.Interfaces;

namespace SAM.Patient.Services.Queries.GetPatientList
{
    /// <summary>
    /// Handles get patient list request
    /// </summary>
    public class GetPatientListHandler : ValidatedHandler<GetPatientListRequest, GetPatientListResponse>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPatientListHandler"/> class.
        /// </summary>
        /// <param name="validator"></param>
        public GetPatientListHandler(
            IRepository repository,
            IMapper mapper,
            IRequestValidator<GetPatientListRequest, GetPatientListResponse> validator)
            : base(validator)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public override async Task<HandlerResponse<GetPatientListResponse>> HandleValidatedRequestAsync(GetPatientListRequest request)
        {
            var patientList = await _repository.GetAllAsync<SAM.Repository.Models.Patient>();

            var list = _mapper.Map<List<GetPatientListResponse.Patient>>(patientList);

            var response = new GetPatientListResponse()
            {
                PatientList = _mapper.Map<List<GetPatientListResponse.Patient>>(patientList),
            };

            return new OkHandlerResponse<GetPatientListResponse>(response);
        }
    }
}