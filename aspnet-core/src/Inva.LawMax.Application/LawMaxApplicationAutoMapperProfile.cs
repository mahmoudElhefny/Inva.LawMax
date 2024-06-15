using AutoMapper;
using Inva.LawMax.Cases;
using Inva.LawMax.Hearings;
using Inva.LawMax.LawyerCases;
using Inva.LawMax.Lawyers;

namespace Inva.LawMax;

public class LawMaxApplicationAutoMapperProfile : Profile
{
    public LawMaxApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<Case, CaseDto>().ReverseMap();
        CreateMap<Case, CreateUpdateCaseDto>().ReverseMap();

        // Lawyer Mapper
        CreateMap<Lawyer, LawyerDTO>().ReverseMap();
        CreateMap<Lawyer, CreateUpdateLawyerDTO>().ReverseMap();

        // Lawyer Mapper
        CreateMap<LawyerCase, LawyerCaseDTO>().ReverseMap();
        CreateMap<LawyerCase, CreateUpdateLawyerCase>().ReverseMap();

        // Lawyer Mapper
        CreateMap<Hearing, HearingDTO>().ReverseMap();
        CreateMap<Hearing, CreateUpdateHearingDTO>().ReverseMap();
    }
    
}
