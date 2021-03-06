﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CourierBA_dsAPIS.Controllers
{
    public class AnularReferenciaController : ApiController
    {
        [HttpGet]
        public int getAnularReferencia(string user, string descripcion, string idReferencia, int referencia)
        {
            int resRef = 0;

            try
            {
                using (var connection = Connection.ConnectionSql.getConnection())
                {
                    using (SqlCommand command = new SqlCommand("PA_Validar_Referencia", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@pAccion", SqlDbType.TinyInt).Value = 1;
                        command.Parameters.Add("@pMensaje", SqlDbType.VarChar).Value = "";
                        command.Parameters.Add("@pResultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                        command.Parameters.Add("@pUserName", SqlDbType.VarChar).Value = user;
                        command.Parameters.Add("@pReferencia", SqlDbType.Int).Value = referencia;
                        command.Parameters.Add("@pReferencia_Id", SqlDbType.VarChar).Value =idReferencia;
                        command.Parameters.Add("@pDescripcion", SqlDbType.VarChar).Value = descripcion;
                        command.Parameters.Add("@pReferencia_Padre", SqlDbType.Int).Value = 0;
                        command.Parameters.Add("@pTipo_Referencia", SqlDbType.TinyInt).Value = 0;

                        connection.Open();
                        command.ExecuteNonQuery();

                        resRef = Convert.ToInt32(command.Parameters["@pResultado"].Value);
                    }
                }

                if (resRef == 1)
                {
                    try
                    {
                        using (var connection = Connection.ConnectionSql.getConnection())
                        {
                            using (SqlCommand command = new SqlCommand("PA_tbl_Referencia", connection))
                            {
                                command.CommandType = CommandType.StoredProcedure;

                                command.Parameters.Add("@TAccion", SqlDbType.TinyInt).Value = 3;
                                command.Parameters.Add("@TOpcion", SqlDbType.TinyInt).Value = 0;
                                command.Parameters.Add("@pReferencia", SqlDbType.Int).Value = referencia;

                                connection.Open();
                                command.ExecuteNonQuery();

                            }
                        }

                        
                    }
                    catch (Exception)
                    {

                       
                    }

                }
                return 1;
            }
            catch (Exception)
            {

                return 0;
            }

           

        }
    }
}
