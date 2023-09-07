using WebSistemaVentas.Core;
using WebSistemaVentas.Models.Mantenimientos.Categorias;

namespace WebSistemaVentas.Repository.Interfaces;

public interface IMantenimientosRepository
{
    #region Categorias
    // Task<List<Get1_Categorias>> GetCategorias();
    Task<IEnumerable<Get1_Categorias>> GetCategorias();
    Task<Get2_CategoriasXCodigo> GetCategoriaByCodigo(string codigo);
    Task<int> GetCategoriaValidExists(string nombre);
    Task<Response> SetCategoria(SetCategoriaParametros parametro);
    #endregion

}