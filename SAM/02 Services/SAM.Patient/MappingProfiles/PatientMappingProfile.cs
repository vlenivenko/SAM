using AutoMapper;
using SAM.Patient.Services.Commands.CreatePatient;
using SAM.Patient.Services.Queries.GetPatientList;

namespace SAM.Patient.MappingProfiles
{
    /// <summary>
    /// Contains information about patient types mapping
    /// </summary>
    public class PatientMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PatientMappingProfile"/> class.
        /// </summary>
        public PatientMappingProfile()
        {
            // get patient list
            CreateMap<SAM.Repository.Models.Patient, GetPatientListResponse.Patient>();

            // create patient
            CreateMap<CreatePatientRequest, SAM.Repository.Models.Patient>();
        }
    }
}
