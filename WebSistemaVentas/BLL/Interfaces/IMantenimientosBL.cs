using WebSistemaVentas.Core;
using WebSistemaVentas.Models.Mantenimientos.Categorias;

namespace WebSistemaVentas.BLL;

public interface IMantenimientosBL
{
    #region Categorias

    Task<Response> GetCategorias();
    Task<Response> GetCategoriasByCodigo(string codigo);
    Task<Response> SetCategoria(SetCategoriaParametros parametro);

    #endregion
}