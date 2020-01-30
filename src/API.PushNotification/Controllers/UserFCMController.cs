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

namespace API.PushNotification.Controllers
{
    [System.Web.Http.RoutePrefix("v1")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserFCMController : ApiController
    {
        #region Attributes

        private readonly IUserFCMAppService _userFCMApp;

        #endregion

        #region Constructor

        public UserFCMController(IUserFCMAppService userFCMApp) { _userFCMApp = userFCMApp; }

        #endregion

        [System.Web.Http.HttpPost]
        [ValidateInput(false)]
        [System.Web.Http.Route("usersFCM")]
        public async Task<HttpResponseMessage> AddOrUpdate(UserFCMRequest userFCMRequest)
        {
            var statusCode = HttpStatusCode.Found;
            var message = string.Empty;

            UserFCMResponse userFCMResponse = new UserFCMResponse();

            if (userFCMRequest == null)
            {
                throw new BadRequestException("Model não informada!");
            }

            if (ModelState.IsValid)
            {
                UserFCM checkUserExists = (UserFCM)_userFCMApp.Find(userFCMRequest.IdApp, userFCMRequest.Re).data;

                if (checkUserExists == null)
                {
                    var id = (int)_userFCMApp.Add(Mapper.Map<UserFCMRequest, UserFCM>(userFCMRequest)).data;

                    if (id > 0)
                    {
                        statusCode = HttpStatusCode.Created;

                        userFCMResponse = Mapper.Map<UserFCM, UserFCMResponse>((UserFCM)_userFCMApp.Find(id).data);
                    }
                }
                else
                {
                    if (checkUserExists.Token != userFCMRequest.Token)
                        _userFCMApp.Update(Mapper.Map<UserFCMRequest, UserFCM>(userFCMRequest));

                    statusCode = HttpStatusCode.OK;

                    userFCMResponse = Mapper.Map<UserFCM, UserFCMResponse>((UserFCM)_userFCMApp.Find(userFCMRequest.IdApp, userFCMRequest.Re).data);
                }
            }
            else
            {
                throw new ConflictModelException("Módel inválida!", ModelState);
            }

            return await Task.FromResult(Request.CreateResponse(statusCode, userFCMResponse));
        }
	}
}