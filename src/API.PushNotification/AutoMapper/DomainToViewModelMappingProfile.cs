using AutoMapper;

using API.PushNotification.Models.Request;
using API.PushNotification.Models.Response;
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

            Mapper.CreateMap<Notification, NotificationRequest>();
            Mapper.CreateMap<Notification, NotificationResponse>();
        }
    }
}