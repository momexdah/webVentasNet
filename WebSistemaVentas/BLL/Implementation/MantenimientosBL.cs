using WebSistemaVentas.Core;
using WebSistemaVentas.Models.Mantenimientos.Categorias;
using WebSistemaVentas.UOW;

namespace WebSistemaVentas.BLL.Implementation;

public class MantenimientosBL: IMantenimientosBL
{
    private readonly IUOWVentas _uow;

    public MantenimientosBL(IUOWVentas uow)
    {
        _uow = uow;
    }
    
    #region Categorias

    public async Task<Response> GetCategorias()
    {
        Response response = new Response();
        try
        {
            response.responsevalue = await _uow.mantenimientosRepository.GetCategorias();
            response.messagetype = "SUCCESS";
            response.statusprocess = true;
        }
        catch (Exception e)
        {
            response.responsemessage = e.Message;
        }

        return response;
    }

    public async Task<Response> GetCategoriasByCodigo(string codigo)
    {
        Response response = new Response();
        try
        {
            response.responsevalue = await _uow.mantenimientosRepository.GetCategoriaByCodigo(codigo);
            response.messagetype = "SUCCESS";
            response.statusprocess = true;
        }
        catch (Exception e)
        {
            response.responsemessage = e.Message;
        }

        return response;
    }

    public async Task<Response> SetCategoria(SetCategoriaParametros parametro)
    {
        Response response = new Response();
        bool procesar = true;
        try
        {
            if (parametro.p_opcion == "INSERT")
            {
                var existeCategoria =
                    await _uow.mantenimientosRepository.GetCategoriaValidExists(parametro.p_nombre_categoria);
                if (existeCategoria > 0)
                {
                    procesar = false;

                    response.statusprocess = false;
                    response.messagetype = "WARNING";
                    response.responsemessage = "Ya existe una categoría con este nombre";
                }
            }

            if (procesar)
            {
                response = await _uow.mantenimientosRepository.SetCategoria(parametro);
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