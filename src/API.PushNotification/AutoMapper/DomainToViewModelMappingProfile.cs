using AutoMapper;

using API.PushNotification.Models;
using Icomon.PushNotification.Domain.Entities;

namespace API.PushNotification.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<UserFCM, UserFCMRequest>();
            Mapper.CreateMap<UserFCM, UserFCMResponse>();
        }
    }
}