using AutoMapper;

namespace DrivingSchoolApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // TbShift → ShiftDto
          //  CreateMap<TbShift, ShiftDto>().ReverseMap();

            // Example: if you also have TbShiftRule → ShiftRuleDto
            // CreateMap<TbShiftRule, ShiftRuleDto>().ReverseMap();
        }
    }
}
