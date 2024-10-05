using AutoMapper;
using RskAnalysis.API.DTOs;
using RskAnalysis.CORE.Models;


namespace RskAnalysis.API.Mapping
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<Businesses, BusinessesDto>();
            CreateMap<BusinessesDto, Businesses>();
            
            CreateMap<Cities, CitiesDto>();
            CreateMap<CitiesDto, Cities>();

            CreateMap<Contracts, ContractsDto>();
            CreateMap<ContractsDto, Contracts>();

            CreateMap<Partners, PartnersDto>();
            CreateMap<PartnersDto, Partners>();

           

            CreateMap<Sectors, SectorsDto>();
            CreateMap<SectorsDto, Sectors>();
        }
    }
}
