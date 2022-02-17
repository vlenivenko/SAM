using AutoMapper;
using SAM.Patient.Services.Queries.GetPatientList;

namespace SAM.Patient.MappingProfiles
{
    public class PatientMappingProfile : Profile
    {
        public PatientMappingProfile()
        {
            CreateMap<List<SAM.Repository.Models.Patient>, List<GetPatientListResponse.Patient>>();
        }
    }
}
