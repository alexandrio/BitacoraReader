using Microsoft.AspNetCore.Mvc;
using BitacoraReader.Web.Models;
using System.Diagnostics;
using BitacoraReader.Application.Services;

namespace BitacoraReader.Web.Controllers
{
    // Quita [ApiController] y [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBitacoraService _bitacoraService;

        public HomeController(ILogger<HomeController> logger, IBitacoraService bitacoraService)
        {
            _logger = logger;
            _bitacoraService = bitacoraService;
        }

        // Esta acción, por convención, manejará la ruta base "/" o "/Home"
        public async Task<IActionResult> Index()
        {
            try
            {
                var isConnected = await _bitacoraService.TestConnectionAsync();
                ViewBag.IsConnected = isConnected;

                if (isConnected)
                {
                    ViewBag.TotalRecords = await _bitacoraService.GetTotalCountAsync();
                }

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cargar la página principal");
                ViewBag.IsConnected = false;
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // Esta acción será invocada por el middleware de errores, no directamente por una ruta.
        // No necesita un atributo [Route] específico a menos que quieras que la gente la acceda directamente.
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}