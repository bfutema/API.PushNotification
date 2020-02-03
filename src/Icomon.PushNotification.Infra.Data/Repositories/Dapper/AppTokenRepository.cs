using System;

using Icomon.PushNotification.Domain.Entities;
using Icomon.PushNotification.Domain.Interfaces.Repository;
using Icomon.PushNotification.Infra.Data.Repositories.Dapper.Generic;

namespace Icomon.PushNotification.Infra.Data.Repositories.Dapper
{
    public class AppTokenRepository : IAppTokenRepository
    {
        private string cn;
        public AppTokenRepository()
        {
            cn = System.Configuration.ConfigurationManager.ConnectionStrings["API.PushNotification"].ConnectionString;
        }

        public int Add(AppToken appToken)
        {
            int retorno = 0;

            try
            {
                string query = string.Format(@"INSERT INTO AppToken
                                               VALUES ('{0}', '{1}', GETDATE(), NULL, NULL)"
                                             , appToken.IdApp, appToken.AuthorizationFCM);

                var dapper = new DapperHelper(cn);
                retorno = dapper.QueryInsert(query, appToken);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao cadastrar a aplicação! ErrorMessage: " + e.Message);
            }

            return retorno;
        }

        public void Update(AppToken appToken)
        {
            try
            {
                string query = string.Format(@"UPDATE AppToken SET
                                                    IdApp = '{0}' , AuthorizationFCM = '{1}' , DataAlteracao = GETDATE()
                                               WHERE Id = {0}"
                                               , appToken.Id);

                var dapper = new DapperHelper(cn);
                dapper.QueryUpdate(query, appToken);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao atualizar a aplicação! ErrorMessage: " + e.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                string query = string.Format(@"UPDATE AppToken SET DataExclusao = GETDATE() WHERE Id = @Id");

                var dapper = new DapperHelper(cn);
                dapper.QueryUpdate(query, id);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao excluir a notificação! ErrorMessage: " + e.Message);
            }
        }

        public AppToken Find(int id)
        {
            AppToken appToken = new AppToken();

            try
            {
                string query = string.Format(@"SELECT * FROM AppToken WHERE Id = {0} AND DataExclusao IS NULL", id);

                var dapper = new DapperHelper(cn);
                appToken = (AppToken)dapper.QueryFirstOrDefault<AppToken>(query);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao buscar a aplicação! ErrorMessage: " + e.Message);
            }

            return appToken;
        }
    }
}
