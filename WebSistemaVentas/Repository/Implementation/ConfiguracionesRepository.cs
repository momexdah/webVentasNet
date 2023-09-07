using System.Data;
using MySqlConnector;
using WebSistemaVentas.Core;
using WebSistemaVentas.Models.Configuraciones.Perfiles;
using WebSistemaVentas.Models.Configuraciones.Usuarios;
using WebSistemaVentas.Repository.Interfaces;

namespace WebSistemaVentas.Repository.Implementation;

public class ConfiguracionesRepository: IConfiguracionesRepository
{
    private string _connectionString;
    public ConfiguracionesRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    #region USUARIOS
    public async Task<List<Get1_Usuarios>> GetUsuarios()
    {
        List<Get1_Usuarios> usuarios = new List<Get1_Usuarios>();
        Get1_Usuarios usuario = new Get1_Usuarios();
        try
        {
            using (var cnx = new MySqlConnection(_connectionString))
            {
                cnx.Open();
                using (var cmd = cnx.CreateCommand())
                {
                    cmd.CommandText = @"uspget_usuarios";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("p_opcion", MySqlDbType.Int32).Value = 1;
                    cmd.Parameters.Add("p_filtroINT1", MySqlDbType.Int32).Value = 0;
                    cmd.Parameters.Add("p_filtroTXT1", MySqlDbType.String).Value = "";

                    MySqlDataReader reader = await cmd.ExecuteReaderAsync(CommandBehavior.Default);

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            usuario = new Get1_Usuarios();

                            usuario.id = Convert.ToInt32(reader["id"]);
                            usuario.nombre = reader["nombre"].ToString();
                            usuario.correo = reader["correo"].ToString();
                            usuario.estado = Convert.ToBoolean(reader["estado"]);
                            usuario.auditoria_creacion = reader["auditoria_creacion"].ToString();
                            
                            usuarios.Add(usuario);
                        }
                    }
                }
            }
        }
        catch (Exception)
        {
            throw;
        }

        return usuarios;
    }

    public Task<Get2_UsuarioXId> GetUsuarioById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<int> GetUsuarioValidExists(string correo)
    {
        int response = 0;
        try
        {
            using (var cnx = new MySqlConnection(_connectionString))
            {
                cnx.Open();
                using (var cmd = cnx.CreateCommand())
                {
                    cmd.CommandText = @"uspget_usuarios";
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    cmd.Parameters.Add("p_opcion", MySqlDbType.Int32).Value = 3;
                    cmd.Parameters.Add("p_filtroINT1", MySqlDbType.Int32).Value = 0;
                    cmd.Parameters.Add("p_filtroTXT1", MySqlDbType.String).Value = correo;

                    MySqlDataReader reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow);

                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            response = Convert.ToInt32(reader["cantidad_registros"]);
                        }
                    }
                }
            }
        }
        catch (Exception)
        {
            throw;
        }

        return response;
    }

    public async Task<Response> SetUsuario(SetUsuarioParametros parametros)
    {
        Response respuesta = new Response();

        try
        {
            using (var cnx = new MySqlConnection(_connectionString))
            {
                cnx.Open();
                using (var cmd = cnx.CreateCommand())
                {
                    cmd.CommandText = @"uspset_usuario";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("p_opcion", MySqlDbType.String).Value = parametros.p_opcion;
                    cmd.Parameters.Add("p_id", MySqlDbType.Int32).Value = parametros.p_id;
                    cmd.Parameters.Add("p_id_perfil", MySqlDbType.Int32).Value = parametros.p_id_perfil;
                    cmd.Parameters.Add("p_nombre", MySqlDbType.String).Value = parametros.p_nombre;
                    cmd.Parameters.Add("p_correo", MySqlDbType.String).Value = parametros.p_correo;
                    cmd.Parameters.Add("p_clave", MySqlDbType.String).Value = parametros.p_clave;
                    cmd.Parameters.Add("p_estado", MySqlDbType.Bool).Value = parametros.p_estado;
                    cmd.Parameters.Add("p_id_usuario", MySqlDbType.Int32).Value = parametros.p_id_usuario;

                    MySqlDataReader reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow);

                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            respuesta.statusprocess = Convert.ToBoolean(reader["statusprocess"]);
                            respuesta.messagetype = reader["messagetype"].ToString();
                            respuesta.responsemessage = reader["responsemessage"].ToString();
                            respuesta.responsevalue = Convert.ToInt32(reader["responsevalue"]);
                        }
                    }
                }
            }
        }
        catch (Exception e)
        {
            respuesta.messagetype = "ERROR";
            respuesta.responsemessage = e.Message;
        }

        return respuesta;
    }
    #endregion

    #region PERFILES
    public async Task<List<Get1_Perfiles>> GetPerfiles()
    {
        List<Get1_Perfiles> perfiles = new List<Get1_Perfiles>();
        Get1_Perfiles perfil = new Get1_Perfiles();

        try
        {
            using (var cnx = new MySqlConnection(_connectionString))
            {
                cnx.Open();
                using (var cmd = cnx.CreateCommand())
                {
                    cmd.CommandText = @"uspget_perfiles";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("p_opcion", MySqlDbType.String).Value = 1;
                    cmd.Parameters.Add("p_filtroINT1", MySqlDbType.Int32).Value = 0;
                    cmd.Parameters.Add("p_filtroTXT1", MySqlDbType.String).Value = "";

                    MySqlDataReader reader = await cmd.ExecuteReaderAsync(CommandBehavior.Default);

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            perfil = new Get1_Perfiles();

                            perfil.id = Convert.ToInt32(reader["id"]);
                            perfil.nombre_perfil = reader["nombre_perfil"].ToString();
                            perfil.descripcion = reader["descripcion"].ToString();
                            perfil.estado = Convert.ToBoolean(reader["estado"]);
                            perfil.auditoria_creacion = reader["auditoria_creacion"].ToString();
                            
                            perfiles.Add(perfil);
                        }
                    }
                }
            }
        }
        catch (Exception)
        {
            throw;
        }

        return perfiles;
    }

    public Task<Get2_PerfilXId> GetPerfilById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<int> GetPerfilValidExists(string nombre_perfil)
    {
        int response = 0;
        try
        {
            using (var cnx = new MySqlConnection(_connectionString))
            {
                cnx.Open();

                using (var cmd = cnx.CreateCommand())
                {
                    cmd.CommandText = @"uspget_perfiles";
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    cmd.Parameters.Add("p_opcion", MySqlDbType.String).Value = 3;
                    cmd.Parameters.Add("p_filtroINT1", MySqlDbType.Int32).Value = 0;
                    cmd.Parameters.Add("p_filtroTXT1", MySqlDbType.String).Value = nombre_perfil;

                    MySqlDataReader reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow);

                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            response = Convert.ToInt32(reader["cantidad_registros"]);
                        }
                    }

                }
            }
        }
        catch (Exception)
        {
            throw;
        }

        return response;
    }

    public async Task<Response> SetPerfil(SetPerfilParametros parametros)
    {
        Response respuesta = new Response();
        try
        {
            using (var cnx = new MySqlConnection(_connectionString))
            {
                cnx.Open();

                using (var cmd = cnx.CreateCommand())
                {
                    cmd.CommandText = @"uspset_perfil";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("p_opcion", MySqlDbType.String).Value = parametros.p_opcion;
                    cmd.Parameters.Add("p_id", MySqlDbType.Int32).Value = parametros.p_id;
                    cmd.Parameters.Add("p_nombre_perfil", MySqlDbType.String).Value = parametros.p_nombre_perfil;
                    cmd.Parameters.Add("p_descripcion", MySqlDbType.String).Value = parametros.p_descripcion;
                    cmd.Parameters.Add("p_estado", MySqlDbType.Bool).Value = parametros.p_estado;
                    cmd.Parameters.Add("p_id_usuario", MySqlDbType.Int32).Value = parametros.p_id_usuario;

                    MySqlDataReader reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow);

                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            respuesta.statusprocess = Convert.ToBoolean(reader["statusprocess"]);
                            respuesta.messagetype = reader["messagetype"].ToString();
                            respuesta.responsemessage = reader["responsemessage"].ToString();
                            respuesta.responsevalue = Convert.ToInt32(reader["responsevalue"]);
                        }
                    }
                }
            }
        }
        catch (Exception e)
        {
            respuesta.messagetype = "ERROR";
            respuesta.responsemessage = e.Message;
        }

        return respuesta;
    }
    #endregion
}