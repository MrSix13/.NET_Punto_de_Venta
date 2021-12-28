﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_Usuario
    {
        public List<Usuario> Listar(){
            List<Usuario> lista = new List<Usuario>();
            using(SqlConnection oconexion = new SqlConnection(Conexion.cadena)){
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("Select u.IdUsuario,u.Documento, u.NombreCompleto, u.Correo, u.Clave, u.Estado, r.IdRol, r.Descripcion from usuario u");
                    query.AppendLine("inner join rol r on r.IdRol = u.IdRol");


                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    using(SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while(dr.Read())
                        {
                            lista.Add(new Usuario()
                            {
                                IdUsaurio = Convert.ToInt32(dr["IdUsuario"]),
                                Documento = dr["documento"].ToString(),
                                NombreCompleto = dr["NombreCompleto"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                Clave = dr["Clave"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"]),
                                oRol = new Rol() { IdRol = Convert.ToInt32(dr["IdRol"]),Descripcion = dr["Descripcion"].ToString() }
                            }); ;
                        }
                    }
                }
                catch(Exception)
                {
                    lista = new List<Usuario>();

                }
            }
            return lista;
        }
    }
}
