using System;

using Icomon.PushNotification.Domain.Entities;
using Icomon.PushNotification.Domain.Interfaces.Repository;
using Icomon.PushNotification.Infra.Data.Repositories.Dapper.Generic;

namespace Icomon.PushNotification.Infra.Data.Repositories.Dapper
{
    public class UserFCMRepository : IUserFCMRepository
    {
        private string cn;
        public UserFCMRepository()
        {
            cn = System.Configuration.ConfigurationManager.ConnectionStrings["API.PushNotification"].ConnectionString;
        }

        public int Add(UserFCM userFCM)
        {
            int retorno = 0;

            try
            {
                string query = string.Format(@"INSERT INTO UserFCM
                                               VALUES ({0}, '{1}', '{2}', GETDATE(), NULL, NULL)", userFCM.IdApp, userFCM.Re, userFCM.Token);

                var dapper = new DapperHelper(cn);
                retorno = dapper.QueryInsert(query, userFCM);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao criar usuário FCM! ErrorMessage: " + e.Message);
            }

            return retorno;
        }
        public void Update(UserFCM userFCM)
        {
            try
            {
                string query = string.Format(@"UPDATE UserFCM SET Token = '{0}', DataAlteracao = GETDATE()
                                               WHERE IdApp = {1} AND Re = '{2}'"
                                               , userFCM.Token, userFCM.IdApp, userFCM.Re);

                var dapper = new DapperHelper(cn);
                dapper.QueryUpdate(query, userFCM);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao atualizar o usuário FCM! ErrorMessage: " + e.Message);
            }
        }
        public UserFCM Find(int idApp, string re)
        {
            UserFCM userFCM = new UserFCM();

            try
            {
                string query = string.Format(@"SELECT * FROM UserFCM WHERE IdApp = {0} AND Re = '{1}'", idApp, re);

                var dapper = new DapperHelper(cn);
                userFCM = (UserFCM)dapper.QueryFirstOrDefault<UserFCM>(query);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao encontrar o usuário FCM! ErrorMessage: " + e.Message);
            }

            return userFCM;
        }
        public UserFCM Find(int id)
        {
            UserFCM userFCM = new UserFCM();

            try
            {
                string query = string.Format(@"SELECT * FROM UserFCM WHERE Id = {0}", id);

                var dapper = new DapperHelper(cn);
                userFCM = (UserFCM)dapper.QueryFirstOrDefault<UserFCM>(query);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao encontrar o usuário FCM! ErrorMessage: " + e.Message);
            }

            return userFCM;
        }
    }
}
