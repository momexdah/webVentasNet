using Microsoft.AspNetCore.Mvc;
using WebSistemaVentas.DTO.Login;

namespace WebSistemaVentas.Controllers;

public class LoginController : Controller
{
    //private readonly IHttpContextAccessor _context;

    // public LoginController(IHttpContextAccessor context)
    // {
    //     _context = context;
    // }
    // GET
    public IActionResult Index()
    {
        return View();
    }

    // [HttpPost]
    // public JsonResult Login(LoginDTO login)
    // {
    //     bool estado = false;
    //     string mensaje = "";
    //     string url = "";
    //
    //     try
    //     {
    //     }
    //     catch (Exception e)
    //     {
    //         Console.WriteLine(e);
    //         throw;
    //     }
    // }
}