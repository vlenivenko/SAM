using AutoMapper;
using SAM.Search.SearchEngines.Requests;

namespace SAM.Search.MappingProfiles
{
    /// <summary>
    /// Contains information about patient types mapping
    /// </summary>
    public class SearchMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchMappingProfile"/> class.
        /// </summary>
        public SearchMappingProfile()
        {
            // create search engine requests
            CreateMap<SAM.Repository.Models.Patient, SearchEnginesRequest1>()
                .ForPath(d => d.Patient.Forename, opt => opt.Ignore())
                .ForPath(d => d.Patient.Surname, opt => opt.Ignore())
                .ForPath(d => d.Patient.DateOfBirth, opt => opt.MapFrom(s => s.DateOfBirth))
                .ForPath(d => d.Patient.DiseaseType, opt => opt.MapFrom(s => s.DiseaseType));

            CreateMap<SAM.Repository.Models.Patient, SearchEnginesRequest2>()
                .ForPath(d => d.Patient.FirstName, opt => opt.MapFrom(s => s.FirstName))
                .ForPath(d => d.Patient.LastName, opt => opt.MapFrom(s => s.LastName))
                .ForPath(d => d.Patient.DateOfBirth, opt => opt.MapFrom(s => s.DateOfBirth))
                .ForPath(d => d.Patient.DiseaseType, opt => opt.MapFrom(s => s.DiseaseType));
        }
    }
}