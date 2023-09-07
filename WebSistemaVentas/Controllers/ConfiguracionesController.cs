using Microsoft.AspNetCore.Mvc;
using WebSistemaVentas.BLL;
using WebSistemaVentas.Models.Configuraciones.Perfiles;

namespace WebSistemaVentas.Controllers;

public class ConfiguracionesController : Controller
{
    private IConfiguracionesBL _configuracionesBl;

    public ConfiguracionesController(IConfiguracionesBL configuracionesBl)
    {
        _configuracionesBl = configuracionesBl;
    }

    // GET
    public IActionResult RegistrarPerfil()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> SetPerfil(SetPerfilParametros parametros)
    {
        return Ok(await _configuracionesBl.SetPerfil(parametros));
    }
}