using System;
using AutoMapper;
using System.Net;
using System.Web.Mvc;
using System.Web.Http;
using System.Net.Http;
using System.Web.Http.Cors;
using System.Threading.Tasks;

using API.PushNotification.Models.Request;
using API.PushNotification.Models.Response;
using API.PushNotification.Exceptions;
using Icomon.PushNotification.Domain.Entities;
using Icomon.PushNotification.Application.Interfaces;
using Newtonsoft.Json;
using System.Text;
using System.IO;
using System.Collections.Generic;
using API.PushNotification.Models.Response.Generic;

namespace API.PushNotification.Controllers
{
    [System.Web.Http.RoutePrefix("v1")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class NotificationController : ApiController
    {
        #region Attributes

        private readonly IUserFCMAppService _userFCMApp;
        private readonly INotificationAppService _notificationApp;
        private readonly IAppTokenAppService _appTokenApp;

        #endregion

        #region Constructor

        public NotificationController(
            INotificationAppService notificationApp
            , IUserFCMAppService userFCMApp
            , IAppTokenAppService appTokenApp)
        {
            _notificationApp = notificationApp;
            _userFCMApp = userFCMApp;
            _appTokenApp = appTokenApp;
        }

        #endregion

        [System.Web.Http.HttpPost]
        [ValidateInput(false)]
        [System.Web.Http.Route("notifications")]
        public async Task<HttpResponseMessage> Add(PushRequest pushRequest)
        {
            var statusCode = HttpStatusCode.Found;
            var message = string.Empty;

            NotificationResponse notificationResponse = new NotificationResponse();

            if (pushRequest == null)
            {
                throw new BadRequestException("Model não informada!");
            }

            if (ModelState.IsValid)
            {
                var id = (int)_notificationApp.Add(Mapper.Map<NotificationRequest, Notification>(pushRequest.Notification)).data;

                if (id > 0)
                {
                    var userFCM = (UserFCM)_userFCMApp.Find(pushRequest.IdApp, pushRequest.Re).data;
                    var notification = (Notification)_notificationApp.Find(id).data;
                    var authFCM = (AppToken)_appTokenApp.Find(pushRequest.IdApp).data;

                    PushNotificationRequest pushSendRequest = new PushNotificationRequest();

                    if (notification != null && authFCM != null)
                    {
                        pushSendRequest.To = userFCM.Token;
                        pushSendRequest.Notification = new NotificationRequest();
                        pushSendRequest.Notification.Id = id;
                        pushSendRequest.Notification.Titulo = notification.Titulo;
                        pushSendRequest.Notification.Corpo = notification.Corpo;
                        pushSendRequest.Notification.JsonData = pushRequest.Notification.JsonData;

                        pushSendRequest.Notification.Icone = pushRequest.Notification.Icone;
                        pushSendRequest.Notification.Som = pushRequest.Notification.Som;
                        pushSendRequest.Notification.Cor = pushRequest.Notification.Cor;
                        pushSendRequest.Notification.ClickAction = pushRequest.Notification.ClickAction;
                        pushSendRequest.Notification.Tag = pushRequest.Notification.Tag;
                        pushSendRequest.Notification.Link = pushRequest.Notification.Link;
                    }
                    else
                    {
                        if (notification == null) throw new NotFoundException("Notificação não encontrada!");
                        if (authFCM == null) throw new NotFoundException("AuthorizationToken não encontrado!");
                    }

                    var auth = "key=" + authFCM.AuthorizationFCM;

                    var idFCMGoogle = await Push(pushSendRequest, auth);

                    if (string.IsNullOrEmpty(idFCMGoogle))
                    {
                        _notificationApp.Delete(id);

                        throw new InternalServerErrorException("Erro no serviço externo! (FCM)");
                    }
                    else
                    {
                        notification.IdFCM = idFCMGoogle;

                        _notificationApp.Update(notification);

                        statusCode = HttpStatusCode.Created;

                        notificationResponse = Mapper.Map<Notification, NotificationResponse>(notification);
                    }
                }
                else
                {
                    throw new InternalServerErrorException("Não foi cadastrado a notificação!");
                }
            }
            else
            {
                throw new ConflictModelException("Módel inválida!", ModelState);
            }

            return await Task.FromResult(Request.CreateResponse(statusCode, notificationResponse));
        }

        [System.Web.Http.HttpPost]
        [ValidateInput(false)]
        [System.Web.Http.Route("notifications/{id}")]
        public async Task<HttpResponseMessage> Update(int id)
        {
            var statusCode = HttpStatusCode.Found;
            var message = string.Empty;

            NotificationResponse notificationResponse = new NotificationResponse();

            if (id == 0 || id == null)
            {
                throw new BadRequestException("Informar a notificação!");
            }

            var notification = (Notification)_notificationApp.Find(id).data;

            notification.Recebido = true;

            _notificationApp.Update(notification);

            statusCode = HttpStatusCode.OK;

            notificationResponse = Mapper.Map<Notification, NotificationResponse>(notification);

            return await Task.FromResult(Request.CreateResponse(statusCode, notificationResponse));
        }

        private async Task<string> Push(PushNotificationRequest obj, string auth)
        {
            var requisicaoWeb = WebRequest.CreateHttp("https://fcm.googleapis.com/fcm/send");
            requisicaoWeb.Headers["Authorization"] = auth;
            requisicaoWeb.Method = "POST";
            requisicaoWeb.ContentType = "application/json";
            requisicaoWeb.AllowWriteStreamBuffering = true;
            string jsonString = JsonConvert.SerializeObject(obj);
            var json = Encoding.ASCII.GetBytes(jsonString);
            var streamBodyRequest = requisicaoWeb.GetRequestStream();

            streamBodyRequest.Write(json, 0, json.Length);
            streamBodyRequest.Flush();

            try
            {
                using (StreamReader reader = new StreamReader(requisicaoWeb.GetResponse().GetResponseStream()))
                {
                    string objResponse = reader.ReadToEnd();

                    PushNotificationResponse retProt = new PushNotificationResponse();
                    retProt = JsonConvert.DeserializeObject<PushNotificationResponse>(objResponse);
                    reader.Close();

                    return await Task.FromResult(retProt.results[0].message_id);
                }
            }
            catch (Exception e)
            {
                return "";
            }
        }


        [System.Web.Http.HttpPost]
        [ValidateInput(false)]
        [System.Web.Http.Route("notificationsList")]
        public async Task<HttpResponseMessage> AddList(PushRequestList pushRequest)
        {
            List<UserFCMPushResponse> usersSuccessOrFailedList = new List<UserFCMPushResponse>();

            var statusCode = HttpStatusCode.Found;
            var message = string.Empty;

            NotificationResponse notificationResponse = new NotificationResponse();

            if (pushRequest == null)
            {
                throw new BadRequestException("Model não informada!");
            }

            if (pushRequest.Users.Count == 0)
            {
                throw new BadRequestException("Por favor informe ao menos um usuário!");
            }

            if (ModelState.IsValid)
            {
                foreach (var userNotification in pushRequest.Users)
                {
                    // buscar o token de autorização do serviço de aplicação
                    var id = (int)_notificationApp.Add(Mapper.Map<NotificationRequest, Notification>(userNotification.Notification)).data;
                    var userFCM = (UserFCM)_userFCMApp.Find(userNotification.IdApp, userNotification.Re).data;
                    var notification = (Notification)_notificationApp.Find(id).data;
                    var authFCM = (AppToken)_appTokenApp.Find(userNotification.IdApp).data;

                    string idFCMGoogle = string.Empty;

                    if (userFCM != null && authFCM != null)
                    {
                        PushNotificationRequest pushSendRequest = new PushNotificationRequest();

                        pushSendRequest.To = userFCM.Token;
                        pushSendRequest.Notification = new NotificationRequest();
                        pushSendRequest.Notification.Id = id;
                        pushSendRequest.Notification.Titulo = notification.Titulo;
                        pushSendRequest.Notification.Corpo = notification.Corpo;
                        pushSendRequest.Notification.JsonData = userNotification.Notification.JsonData;

                        pushSendRequest.Notification.Icone = userNotification.Notification.Icone;
                        pushSendRequest.Notification.Som = userNotification.Notification.Som;
                        pushSendRequest.Notification.Cor = userNotification.Notification.Cor;
                        pushSendRequest.Notification.ClickAction = userNotification.Notification.ClickAction;
                        pushSendRequest.Notification.Tag = userNotification.Notification.Tag;
                        pushSendRequest.Notification.Link = userNotification.Notification.Link;

                        var auth = "key=" + authFCM.AuthorizationFCM;
                        idFCMGoogle = await Push(pushSendRequest, auth);
                    }

                    if (id > 0 && userFCM != null && !string.IsNullOrEmpty(idFCMGoogle) && authFCM != null)
                    {
                        UserFCMPushResponseSuccess userResponseSuccess = new UserFCMPushResponseSuccess();

                        userResponseSuccess.IdApp = userNotification.IdApp;
                        userResponseSuccess.Re = userNotification.Re;
                        userResponseSuccess.Success = true;
                        userResponseSuccess.Notification = Mapper.Map<Notification, NotificationResponse>(notification);

                        usersSuccessOrFailedList.Add(userResponseSuccess);

                        notification.IdFCM = idFCMGoogle;

                        _notificationApp.Update(notification);

                        statusCode = HttpStatusCode.Created;
                    }
                    else
                    {
                        UserFCMPushResponseFailed userResponseFailed = new UserFCMPushResponseFailed();

                        userResponseFailed.IdApp = userNotification.IdApp;
                        userResponseFailed.Re = userNotification.Re;
                        userResponseFailed.Success = false;

                        userResponseFailed.Errors = new List<string>();

                        if (id == 0)
                        {
                            //throw new InternalServerErrorException("Não foi cadastrado a notificação!");
                            userResponseFailed.Errors.Add("Não foi cadastrado a notificação!");
                        }

                        if (userFCM == null)
                        {
                            //throw new InternalServerErrorException("Erro ao encontrar o usuário!");
                            userResponseFailed.Errors.Add("Erro ao encontrar o usuário!");
                        }

                        if (authFCM == null)
                        {
                            userResponseFailed.Errors.Add("AuthorizationToken não encontrado!");
                        }

                        if (string.IsNullOrEmpty(idFCMGoogle))
                        {
                            _notificationApp.Delete(id);

                            //throw new InternalServerErrorException("Erro no serviço externo! (FCM)");
                            userResponseFailed.Errors.Add("Erro no serviço externo! (FCM)");
                        }

                        usersSuccessOrFailedList.Add(userResponseFailed);

                        //statusCode = HttpStatusCode.PreconditionFailed;
                        statusCode = HttpStatusCode.Created;
                    }
                }
            }
            else
            {
                throw new ConflictModelException("Módel inválida!", ModelState);
            }

            return await Task.FromResult(Request.CreateResponse(statusCode, usersSuccessOrFailedList));
        }
	}
}