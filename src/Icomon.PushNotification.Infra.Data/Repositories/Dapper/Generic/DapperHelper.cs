using System;
using Dapper;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Icomon.PushNotification.Infra.Data.Repositories.Dapper.Generic
{
    public class DapperHelper
    {
        private readonly string _connString;

        public DapperHelper(string connString) { _connString = connString; }

        /// <summary>
        /// Executar SQL parametrizado
        /// </summary>
        public void QueryUpdate(string queryString, object command)
        {
            try
            {
                using (var con = new SqlConnection(_connString))
                {
                    try
                    {
                        con.Open();

                        con.Execute(queryString, command);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Executar SQL parametrizado
        /// </summary
        public void QueryUpdate(string queryString, int Id)
        {
            try
            {
                using (var con = new SqlConnection(_connString))
                {
                    try
                    {
                        con.Open();

                        con.Execute(queryString, new { Id });
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Executa uma consulta. Retorna o último valor de coluna de identidade
        /// </summary>
        public int QueryInsert(string queryString, object command)
        {
            try
            {
                int retorno = 0;

                using (var con = new SqlConnection(_connString))
                {
                    try
                    {
                        con.Open();

                        retorno = con.Query<int>(queryString + ";SELECT CAST(SCOPE_IDENTITY() as INT);", command, commandType: CommandType.Text).Single();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        con.Close();
                    }
                }

                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Executa uma consulta, retornando os dados digitados conforme T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public List<T> QueryList<T>(string queryString)
        {
            try
            {
                List<T> Lista = new List<T>();

                using (var con = new SqlConnection(_connString))
                {
                    try
                    {
                        con.Open();

                        var obj = con.Query<T>(queryString).ToList();

                        foreach (var item in obj)
                        {
                            Lista.Add(item);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        con.Close();
                    }
                }

                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Executa uma consulta, retornando os dados digitados conforme T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryString"></param>
        /// <param name="Re"></param>
        /// <returns></returns>
        public List<T> QueryList<T>(string queryString, string Re)
        {
            try
            {
                List<T> Lista = new List<T>();

                using (var con = new SqlConnection(_connString))
                {
                    try
                    {
                        con.Open();

                        var obj = con.Query<T>(queryString, new { @Re = Re }).ToList();

                        foreach (var item in obj)
                        {
                            Lista.Add(item);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        con.Close();
                    }
                }

                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Executa uma consulta, retornando os dados digitados conforme T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public object QueryFirstOrDefault<T>(string queryString, int Id)
        {
            try
            {
                var retorno = new object();

                using (var con = new SqlConnection(_connString))
                {
                    try
                    {
                        con.Open();

                        var obj = con.Query<T>(queryString, new { Id }).FirstOrDefault();

                        retorno = obj;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        con.Close();
                    }
                }

                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object QueryFirstOrDefault<T>(string queryString)
        {
            try
            {
                var retorno = new object();

                using (var con = new SqlConnection(_connString))
                {
                    try
                    {
                        con.Open();

                        var obj = con.Query<T>(queryString).FirstOrDefault();

                        retorno = obj;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        con.Close();
                    }
                }

                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
