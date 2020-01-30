using AutoMapper;

using API.PushNotification.Models;
using Icomon.PushNotification.Domain.Entities;

namespace API.PushNotification.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<UserFCMRequest, UserFCM>();
            Mapper.CreateMap<UserFCMResponse, UserFCM>();
        }
    }
}