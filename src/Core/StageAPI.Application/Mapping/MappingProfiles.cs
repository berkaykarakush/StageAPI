using AutoMapper;
using StageAPI.Application.DTOs;
using StageAPI.Application.DTOs.Activity;
using StageAPI.Application.Features.Commands.Activity.CreateActivity;
using StageAPI.Domain.Entities;

namespace StageAPI.Application.Mapping
{
    /// <summary>
    /// AutoMapper profiles for mapping between DTOs and entities
    /// </summary>
    public class MappingProfiles : Profile
    {
        /// <summary>
        ///  Constructor for MappingProfiles class
        /// </summary>
        public MappingProfiles()
        {
            // Maps CreateActivityDTO to Activity entity
            CreateMap<CreateActivityDTO, Activity>();
            // Maps UpdateActivityDTO to Activity entity
            CreateMap<UpdateActivityDTO, Activity>();
            // Maps ActivityDTO to Activity entity
            CreateMap<ActivityDTO, Activity>().ReverseMap();
            // Maps CreateActivityCommandRequest to Activity entity
            CreateMap<CreateActivityCommandRequest, Activity>();
        }
    }
}