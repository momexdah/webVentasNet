using Microsoft.AspNetCore.Mvc;
using WebSistemaVentas.BLL;
using WebSistemaVentas.Models.Mantenimientos.Categorias;

namespace WebSistemaVentas.Controllers;

public class MantenimientosController : Controller
{
    // GET
    // public IActionResult Index()
    // {
    //     return View();
    // }
    private IMantenimientosBL _mantenimientosBl;

    public MantenimientosController(IMantenimientosBL mantenimientosBl)
    {
        _mantenimientosBl = mantenimientosBl;
    }
    
    #region Vistas

    public ActionResult RegistrarCategoria()
    {
        return View();
    }

    public ActionResult ListarCategorias()
    {
        return View();
    }
    #endregion

    [HttpPost]
    public async Task<ActionResult> SetCategoria(SetCategoriaParametros parametros)
    {
        return Ok(await _mantenimientosBl.SetCategoria(parametros));
    }

    [HttpGet]
    public async Task<ActionResult> GetCategorias()
    {
        return Ok(await _mantenimientosBl.GetCategorias());
    }

    [HttpGet]
    public async Task<ActionResult> GetCategoriaByCodigo(string codigo)
    {
        return Ok(await _mantenimientosBl.GetCategoriasByCodigo(codigo));
    }
    
}