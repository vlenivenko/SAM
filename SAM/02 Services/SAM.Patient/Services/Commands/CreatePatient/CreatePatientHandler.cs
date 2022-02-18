using AutoMapper;
using SAM.Core.CQRS.Handlers;
using SAM.Core.CQRS.Responses;
using SAM.Core.CQRS.Validation.Interfaces;
using SAM.Repository.Repositories.Interfaces;

namespace SAM.Patient.Services.Commands.CreatePatient
{
    /// <summary>
    /// Handles create patient request
    /// </summary>
    public class CreatePatientHandler : ValidatedHandler<CreatePatientRequest, CreatePatientResponse>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePatientHandler"/> class.
        /// </summary>
        /// <param name="validator"></param>
        public CreatePatientHandler(
            IRepository repository,
            IMapper mapper,
            IRequestValidator<CreatePatientRequest, CreatePatientResponse> validator)
            : base(validator)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public override async Task<HandlerResponse<CreatePatientResponse>> HandleValidatedRequestAsync(CreatePatientRequest request)
        {
            // TODO: map request using automapper
            var patient = new SAM.Repository.Models.Patient
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth,
                DiseaseType = request.DiseaseType,
            };

            await _repository.AddAsync(patient);

            await _repository.CommitAsync();

            var response = new CreatePatientResponse
            {
                Id = patient.Id,
            };

            return new OkHandlerResponse<CreatePatientResponse>(response);
        }
    }
}