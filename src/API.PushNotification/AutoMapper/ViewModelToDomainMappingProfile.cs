using AutoMapper;

using API.PushNotification.Models.Request;
using API.PushNotification.Models.Response;
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

            Mapper.CreateMap<NotificationRequest, Notification>();
            Mapper.CreateMap<NotificationResponse, Notification>();
        }
    }
}