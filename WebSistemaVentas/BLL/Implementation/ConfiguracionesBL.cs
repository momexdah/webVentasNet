using WebSistemaVentas.Core;
using WebSistemaVentas.Models.Configuraciones.Perfiles;
using WebSistemaVentas.Models.Configuraciones.Usuarios;
using WebSistemaVentas.UOW;

namespace WebSistemaVentas.BLL.Implementation;

public class ConfiguracionesBL : IConfiguracionesBL
{
    private readonly IUOWVentas _uow;

    public ConfiguracionesBL(IUOWVentas uow)
    {
        _uow = uow;
    }

    #region USUARIOS

    public async Task<Response> GetUsuarios()
    {
        Response response = new Response();
        try
        {
            response.responsevalue = await _uow.configuracionesRepository.GetUsuarios();
            response.messagetype = "SUCCESS";
            response.statusprocess = true;
        }
        catch (Exception e)
        {
            response.responsemessage = e.Message;
        }

        return response;
    }

    public Task<Response> GetUsuarioById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Response> SetUsuario(SetUsuarioParametros parametros)
    {
        Response response = new Response();
        bool procesar = true;

        try
        {
            if (parametros.p_opcion == "INSERT")
            {
                var existeUsuario = await _uow.configuracionesRepository.GetUsuarioValidExists(parametros.p_correo);
                if (existeUsuario > 0)
                {
                    procesar = false;
                    response.statusprocess = false;
                    response.messagetype = "WARNING";
                    response.responsemessage = "Ya existe un usuario registrado con este correo";
                }
            }

            if (procesar)
            {
                response = await _uow.configuracionesRepository.SetUsuario(parametros);
            }
        }
        catch (Exception e)
        {
            response.responsemessage = e.Message;
        }

        return response;
    }

    #endregion

    #region PERFILES

    public async Task<Response> GetPerfiles()
    {
        Response response = new Response();
        try
        {
            response.responsevalue = await _uow.configuracionesRepository.GetPerfiles();
            response.messagetype = "SUCCESS";
            response.statusprocess = true;
        }
        catch (Exception e)
        {
            response.responsemessage = e.Message;
        }

        return response;
    }

    public Task<Response> GetPerfilById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Response> SetPerfil(SetPerfilParametros parametros)
    {
        Response response = new Response();
        bool procesar = true;

        try
        {
            if (parametros.p_opcion == "INSERT")
            {
                var existePerfil = await _uow.configuracionesRepository.GetPerfilValidExists(parametros.p_nombre_perfil);
                if (existePerfil > 0)
                {
                    procesar = false;
                    response.statusprocess = false;
                    response.messagetype = "WARNING";
                    response.responsemessage = "Ya existe un perfil con este nombre";
                }
            }

            if (procesar)
            {
                response = await _uow.configuracionesRepository.SetPerfil(parametros);
            }
        }
        catch (Exception e)
        {
            response.responsemessage = e.Message;
        }

        return response;
    }

    #endregion
}