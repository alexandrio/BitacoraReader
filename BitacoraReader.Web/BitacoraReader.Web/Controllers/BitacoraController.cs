using BitacoraReader.Application.DTOs;
using BitacoraReader.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BitacoraReader.Web.Controllers
{
    public class BitacoraController : Controller
    {
        private readonly IBitacoraService _bitacoraService;
        private readonly ILogger<BitacoraController> _logger;

        public BitacoraController(IBitacoraService bitacoraService, ILogger<BitacoraController> logger)
        {
            _bitacoraService = bitacoraService ?? throw new ArgumentNullException(nameof(bitacoraService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> Index(BitacoraFilterDto filter)
        {
            try
            {
                var result = await _bitacoraService.GetBitacorasAsync(filter);

                ViewBag.DistinctJobs = await _bitacoraService.GetDistinctJobsAsync();
                ViewBag.DistinctResultados = await _bitacoraService.GetDistinctResultadosAsync();
                ViewBag.CurrentFilter = filter;

                return View(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cargar la página de bitácoras");
                TempData["Error"] = "Error al cargar los datos de la bitácora";
                return View();
            }
        }

        public async Task<IActionResult> Details(long id)
        {
            try
            {
                var bitacora = await _bitacoraService.GetBitacoraByIdAsync(id);

                if (bitacora == null)
                {
                    return NotFound();
                }

                return View(bitacora);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cargar detalles de bitácora con ID: {Id}", id);
                TempData["Error"] = "Error al cargar los detalles de la bitácora";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> TestConnection()
        {
            try
            {
                var isConnected = await _bitacoraService.TestConnectionAsync();
                return Json(new { success = isConnected, message = isConnected ? "Conexión exitosa" : "Error de conexión" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al probar conexión");
                return Json(new { success = false, message = "Error al probar la conexión" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetStats()
        {
            try
            {
                var totalCount = await _bitacoraService.GetTotalCountAsync();
                var distinctJobs = await _bitacoraService.GetDistinctJobsAsync();
                var distinctResultados = await _bitacoraService.GetDistinctResultadosAsync();

                return Json(new
                {
                    totalRecords = totalCount,
                    totalJobs = distinctJobs.Count(),
                    totalResultados = distinctResultados.Count()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener estadísticas");
                return Json(new { error = "Error al obtener estadísticas" });
            }
        }
    }
}