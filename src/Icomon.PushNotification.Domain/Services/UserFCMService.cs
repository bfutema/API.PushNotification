using System;

using Icomon.PushNotification.Domain.Request;
using Icomon.PushNotification.Domain.Entities;
using Icomon.PushNotification.Domain.Interfaces.Services;
using Icomon.PushNotification.Domain.Interfaces.Repository;

namespace Icomon.PushNotification.Domain.Services
{
    public class UserFCMService : IUserFCMService
    {
        private readonly IUserFCMRepository _userFCMRepository;
        public UserFCMService(IUserFCMRepository userFCMRepository) { _userFCMRepository = userFCMRepository; }

        public Response Add(UserFCM userFCM)
        {
            Response res = new Response();

            try
            {
                int id = _userFCMRepository.Add(userFCM);

                res.success = true;
                res.message = "Usuário FCM criado com sucesso!";
                res.data = id;
            }
            catch (Exception e)
            {
                res.success = false;
                res.message = e.Message;
                res.data = null;
            }

            return res;
        }
        public Response Update(UserFCM userFCM)
        {
            Response res = new Response();

            try
            {
                _userFCMRepository.Update(userFCM);

                res.success = true;
                res.message = "Usuário FCM atualizado com sucesso!";
                res.data = null;
            }
            catch (Exception e)
            {
                res.success = false;
                res.message = e.Message;
                res.data = null;
            }

            return res;
        }
        public Response Find(int idApp, string re)
        {
            Response res = new Response();

            try
            {
                var userFCM = _userFCMRepository.Find(idApp, re);

                res.success = true;
                res.message = "Usuário FCM encontrado!";
                res.data = userFCM;
            }
            catch (Exception e)
            {
                res.success = false;
                res.message = e.Message;
                res.data = null;
            }

            return res;
        }
        public Response Find(int id)
        {
            Response res = new Response();

            try
            {
                var userFCM = _userFCMRepository.Find(id);

                res.success = true;
                res.message = "Usuário FCM encontrado!";
                res.data = userFCM;
            }
            catch (Exception e)
            {
                res.success = false;
                res.message = e.Message;
                res.data = null;
            }

            return res;
        }
    }
}
