using WebSistemaVentas.Core;
using WebSistemaVentas.Models.Configuraciones.Perfiles;
using WebSistemaVentas.Models.Configuraciones.Usuarios;

namespace WebSistemaVentas.BLL;

public interface IConfiguracionesBL
{

    #region USUARIOS
    Task<Response> GetUsuarios();
    Task<Response> GetUsuarioById(int id);
    Task<Response> SetUsuario(SetUsuarioParametros parametros);

    #endregion

    Task<Response> GetPerfiles();
    Task<Response> GetPerfilById(int id);
    Task<Response> SetPerfil(SetPerfilParametros parametros);

}