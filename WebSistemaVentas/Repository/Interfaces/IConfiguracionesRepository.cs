using WebSistemaVentas.Core;
using WebSistemaVentas.Models.Configuraciones.Perfiles;
using WebSistemaVentas.Models.Configuraciones.Usuarios;

namespace WebSistemaVentas.Repository.Interfaces;

public interface IConfiguracionesRepository
{
    #region USUARIOS
    Task<List<Get1_Usuarios>> GetUsuarios();
    Task<Get2_UsuarioXId> GetUsuarioById(int id);
    Task<int> GetUsuarioValidExists(string correo);
    Task<Response> SetUsuario(SetUsuarioParametros parametros);
    #endregion

    #region PERFILES
    Task<List<Get1_Perfiles>> GetPerfiles();
    Task<Get2_PerfilXId> GetPerfilById(int id);
    Task<int> GetPerfilValidExists(string nombre_perfil);
    Task<Response> SetPerfil(SetPerfilParametros parametros);
    #endregion

}