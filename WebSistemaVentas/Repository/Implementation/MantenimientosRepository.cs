using System.Data;
using Dapper;
using MySqlConnector;
using WebSistemaVentas.Core;
using WebSistemaVentas.Models.Mantenimientos.Categorias;
using WebSistemaVentas.Repository.Interfaces;

namespace WebSistemaVentas.Repository.Implementation;

public class MantenimientosRepository: IMantenimientosRepository
{
    private string _connectionString;

    public MantenimientosRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    #region Categorias

    public async Task<IEnumerable<Get1_Categorias>> GetCategorias()
    {
        using (var cnx = new MySqlConnection(_connectionString))
        {
            var storeProcedureName = "uspget_categorias";
            var values = new
            {
                p_opcion = 1,
                p_filtroTXT1 = ""
            };
            return await cnx.QueryAsync <
                   Get1_Categorias>(storeProcedureName, values, commandType: CommandType.StoredProcedure);
        }
    }
    public async Task<Get2_CategoriasXCodigo> GetCategoriaByCodigo(string codigo)
    {
        Get2_CategoriasXCodigo categoria = new Get2_CategoriasXCodigo();
        try
        {
            using (var cnx = new MySqlConnection(_connectionString))
            {
                var storeProcedureName = "uspget_categorias";
                var parameters = new
                {
                    p_opcion = 2,
                    p_filtroTXT1 = codigo
                };

                var result = await cnx.QueryFirstOrDefaultAsync<Get2_CategoriasXCodigo>(storeProcedureName, parameters, commandType: CommandType.StoredProcedure);

                if (result != null)
                {
                    categoria = result;
                }
            }
        }
        catch (Exception)
        {
            throw;
        }

        return categoria;
    }

    public async Task<int> GetCategoriaValidExists(string nombre)
    {
        int response = 0;
        try
        {
            using (var cnx = new MySqlConnection(_connectionString))
            {
                var storeProcedureName = "uspget_categorias";
                var parameters = new
                {
                    p_opcion = 3,
                    p_filtroTXT1 = nombre
                };

                var result = await cnx.QueryFirstOrDefaultAsync(storeProcedureName, parameters, commandType: CommandType.StoredProcedure);

                if (result != null)
                {
                    response = Convert.ToInt32(result.cantidad_registros);
                }
            }
        }
        catch (Exception)
        {
            throw;
        }

        return response;
    }
    public async Task<Response> SetCategoria(SetCategoriaParametros parametro)
    {
        Response respuesta = new Response();

        try
        {
            using (var cnx = new MySqlConnection(_connectionString))
            {
                var storeProcedureName = "uspset_categoria";

                var parameters = new
                {
                    p_opcion = parametro.p_opcion,
                    p_codigo = parametro.p_codigo,
                    p_nombre_categoria = parametro.p_nombre_categoria,
                    p_descripcion = parametro.p_descripcion,
                    p_estado = parametro.p_estado,
                    p_id_usuario = parametro.p_id_usuario
                };

                var result = await cnx.QueryFirstOrDefaultAsync(storeProcedureName, parameters, commandType: CommandType.StoredProcedure);

                if (result != null)
                {
                    respuesta.statusprocess = Convert.ToBoolean(result.statusprocess);
                    respuesta.messagetype = result.messagetype;
                    respuesta.responsemessage = result.responsemessage;
                    respuesta.responsevalue = Convert.ToInt32(result.responsevalue);
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