using System;

using Icomon.PushNotification.Domain.Entities;
using Icomon.PushNotification.Domain.Interfaces.Repository;
using Icomon.PushNotification.Infra.Data.Repositories.Dapper.Generic;

namespace Icomon.PushNotification.Infra.Data.Repositories.Dapper
{
    public class NotificationRepository : INotificationRepository
    {
        private string cn;
        public NotificationRepository()
        {
            cn = System.Configuration.ConfigurationManager.ConnectionStrings["API.PushNotification"].ConnectionString;
        }

        public int Add(Notification notification)
        {
            int retorno = 0;

            try
            {
                string query = string.Format(@"INSERT INTO Notification
                                               VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', GETDATE(), NULL, NULL, '{9}', 0)"
                                             , notification.Titulo , notification.Corpo , notification.IdFCM
                                             , notification.Icone, notification.Som, notification.Cor
                                             , notification.ClickAction, notification.Tag, notification.Link
                                             , Base64Encode(notification.JsonData));

                var dapper = new DapperHelper(cn);
                retorno = dapper.QueryInsert(query, notification);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao cadastrar a notificação! ErrorMessage: " + e.Message);
            }

            return retorno;
        }

        public static string Base64Encode(string plainText)
        {
            if (!string.IsNullOrEmpty(plainText))
            {
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
                return System.Convert.ToBase64String(plainTextBytes);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string Base64Decode(string base64EncodedData)
        {
            if (!string.IsNullOrEmpty(base64EncodedData))
            {
                var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
                return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            }
            else
            {
                return string.Empty;
            }
        }

        public void Update(Notification notification)
        {
            try
            {
                string query = string.Format(@"UPDATE Notification SET
                                                    Titulo = '{0}' , Corpo = '{1}' , IdFCM = '{2}'
                                                    , Recebido = '{3}' , Icone = '{4}' , Som = '{5}'
                                                    , Cor = '{6}' , DataAlteracao = GETDATE()
                                               WHERE Id = {7}"
                                               , notification.Titulo , notification.Corpo , notification.IdFCM
                                               , notification.Recebido , notification.Icone , notification.Som
                                               , notification.Cor , notification.Id);

                var dapper = new DapperHelper(cn);
                dapper.QueryUpdate(query, notification);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao marcar a notificação como entregue! ErrorMessage: " + e.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                string query = string.Format(@"UPDATE Notification SET DataExclusao = GETDATE() WHERE Id = @Id");

                var dapper = new DapperHelper(cn);
                dapper.QueryUpdate(query, id);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao excluir a notificação! ErrorMessage: " + e.Message);
            }
        }

        public Notification Find(int id)
        {
            Notification notification = new Notification();

            try
            {
                string query = string.Format(@"SELECT * FROM Notification WHERE Id = {0} AND DataExclusao IS NULL", id);

                var dapper = new DapperHelper(cn);
                notification = (Notification)dapper.QueryFirstOrDefault<Notification>(query);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao buscar a notificação! ErrorMessage: " + e.Message);
            }

            return notification;
        }
    }
}
