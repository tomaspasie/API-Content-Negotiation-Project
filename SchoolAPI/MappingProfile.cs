using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace SchoolAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Organization, OrganizationDto>()
                .ForMember(c => c.FullAddress,
                    opt => opt.MapFrom(x => string.Join(' ', x.City, x.Country)));
            CreateMap<OrganizationForCreationDto, Organization>();
            CreateMap<OrganizationForUpdateDto, Organization>();

            CreateMap<User, UserDto>()
                .ForMember(c => c.Name,
                    opt => opt.MapFrom(x => x.Name))
                .ForMember(c => c.Age,
                opt => opt.MapFrom(x => x.Age));

            CreateMap<UserForCreationDto, User>();
            CreateMap<UserForUpdateDto, User>();

            CreateMap<Section, SectionDto>()
                .ForMember(c => c.Name,
                    opt => opt.MapFrom(x => x.Name));
            CreateMap<SectionForCreationDto, Section>();
            CreateMap<SectionForUpdateDto, Section>();

            CreateMap<Course, CourseDto>()
                .ForMember(c => c.Name,
                    opt => opt.MapFrom(x => x.Name));
            CreateMap<CourseForCreationDto, Course>();
            CreateMap<CourseForUpdateDto, Course>();

            CreateMap<Assignment, AssignmentDto>()
                .ForMember(c => c.Name,
                    opt => opt.MapFrom(x => x.Name))
                .ForMember(c => c.Score,
                    opt => opt.MapFrom(x => x.Score));

            CreateMap<AssignmentForCreationDto, Assignment>();
            CreateMap<AssignmentForUpdateDto, Assignment>();

            CreateMap<CreateItem, User>();
            CreateMap<NameString, User>();
            CreateMap<CreateItem, Section>();
            CreateMap<NameString, Section>();
            CreateMap<CreateItem, Course>();
            CreateMap<NameString, Course>();
            CreateMap<CreateItem, Assignment>();
            CreateMap<NameString, Assignment>();

        }
    }
}