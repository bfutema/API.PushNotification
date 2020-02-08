using System;
using Icomon.PushNotification.Domain.Request;
using Icomon.PushNotification.Domain.Entities;
using Icomon.PushNotification.Domain.Interfaces.Services;
using Icomon.PushNotification.Domain.Interfaces.Repository;

namespace Icomon.PushNotification.Domain.Services
{
    public class AppTokenService : IAppTokenService
    {
        private readonly IAppTokenRepository _appTokenRepository;
        public AppTokenService(IAppTokenRepository appTokenRepository) { _appTokenRepository = appTokenRepository; }

        public Response Add(AppToken appToken)
        {
            Response res = new Response();

            try
            {
                int id = _appTokenRepository.Add(appToken);

                res.success = true;
                res.message = "Aplicação cadastrada com sucesso!";
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

        public Response Update(AppToken appToken)
        {
            Response res = new Response();

            try
            {
                _appTokenRepository.Update(appToken);

                res.success = true;
                res.message = "Aplicação atualizada com sucesso!";
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

        public Response Delete(int id)
        {
            Response res = new Response();

            try
            {
                _appTokenRepository.Delete(id);

                res.success = true;
                res.message = "Aplicação excluída com sucesso!";
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

        public Response Find(int id)
        {
            Response res = new Response();

            try
            {
                var notification = _appTokenRepository.Find(id);

                res.success = true;
                res.message = "Aplicação encontrada!";
                res.data = notification;
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
