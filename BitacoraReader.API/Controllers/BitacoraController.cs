using BitacoraReader.Application.DTOs;
using BitacoraReader.Application.Services;
using BitacoraReader.Domain.Common;
using Microsoft.AspNetCore.Mvc;
 
using BitacoraReader.API.Models;

namespace Bitacora.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class BitacoraController : ControllerBase
    {
        private readonly IBitacoraService _bitacoraService;
        private readonly ILogger<BitacoraController> _logger;

        public BitacoraController(
            IBitacoraService bitacoraService,
            ILogger<BitacoraController> logger)
        {
            _bitacoraService = bitacoraService ?? throw new ArgumentNullException(nameof(bitacoraService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Obtiene bitácoras con filtros y paginación
        /// </summary>
        /// <param name="filter">Filtros de búsqueda</param>
        /// <param name="cancellationToken">Token de cancelación</param>
        /// <returns>Lista paginada de bitácoras</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<BitacoraDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<PagedResult<BitacoraDto>>>> GetBitacoras(
            [FromQuery] BitacoraFilterDto filter,
            CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("Solicitando bitácoras con filtros: {@Filter}", filter);

                if (filter.PageSize > 500)
                {
                    return BadRequest(ApiResponse.Error("El tamaño de página no puede ser mayor a 500"));
                }

                if (filter.PageNumber < 1)
                {
                    return BadRequest(ApiResponse.Error("El número de página debe ser mayor a 0"));
                }

                var result = await _bitacoraService.GetBitacorasAsync(filter, cancellationToken);

                return Ok(ApiResponse.Success(result, "Bitácoras obtenidas exitosamente"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener bitácoras");
                return StatusCode(500, ApiResponse.Error("Error interno del servidor"));
            }
        }

        /// <summary>
        /// Obtiene una bitácora específica por ID
        /// </summary>
        /// <param name="id">ID de la bitácora</param>
        /// <param name="cancellationToken">Token de cancelación</param>
        /// <returns>Bitácora encontrada</returns>
        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(ApiResponse<BitacoraDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<BitacoraDto>>> GetBitacoraById(
            long id,
            CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("Solicitando bitácora con ID: {Id}", id);

                var bitacora = await _bitacoraService.GetBitacoraByIdAsync(id, cancellationToken);

                if (bitacora == null)
                {
                    return NotFound(ApiResponse.Error($"Bitácora con ID {id} no encontrada"));
                }

                return Ok(ApiResponse.Success(bitacora, "Bitácora encontrada"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener bitácora por ID: {Id}", id);
                return StatusCode(500, ApiResponse.Error("Error interno del servidor"));
            }
        }

        /// <summary>
        /// Obtiene todos los resultados distintos
        /// </summary>
        /// <param name="cancellationToken">Token de cancelación</param>
        /// <returns>Lista de resultados únicos</returns>
        [HttpGet("resultados")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<string>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<IEnumerable<string>>>> GetDistinctResultados(
            CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("Solicitando resultados distintos");

                var resultados = await _bitacoraService.GetDistinctResultadosAsync(cancellationToken);

                return Ok(ApiResponse.Success(resultados, "Resultados obtenidos exitosamente"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener resultados distintos");
                return StatusCode(500, ApiResponse.Error("Error interno del servidor"));
            }
        }

        /// <summary>
        /// Obtiene todos los jobs distintos
        /// </summary>
        /// <param name="cancellationToken">Token de cancelación</param>
        /// <returns>Lista de jobs únicos</returns>
        [HttpGet("jobs")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<int>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<IEnumerable<int>>>> GetDistinctJobs(
            CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("Solicitando jobs distintos");

                var jobs = await _bitacoraService.GetDistinctJobsAsync(cancellationToken);

                return Ok(ApiResponse.Success(jobs, "Jobs obtenidos exitosamente"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener jobs distintos");
                return StatusCode(500, ApiResponse.Error("Error interno del servidor"));
            }
        }

        /// <summary>
        /// Obtiene el conteo total de bitácoras
        /// </summary>
        /// <param name="cancellationToken">Token de cancelación</param>
        /// <returns>Número total de registros</returns>
        [HttpGet("count")]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<int>>> GetTotalCount(
            CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("Solicitando conteo total");

                var count = await _bitacoraService.GetTotalCountAsync(cancellationToken);

                return Ok(ApiResponse.Success(count, "Conteo obtenido exitosamente"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener conteo total");
                return StatusCode(500, ApiResponse.Error("Error interno del servidor"));
            }
        }

        /// <summary>
        /// Prueba la conexión a la base de datos
        /// </summary>
        /// <param name="cancellationToken">Token de cancelación</param>
        /// <returns>Estado de la conexión</returns>
        [HttpGet("test-connection")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<bool>>> TestConnection(
            CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("Probando conexión a la base de datos");

                var isConnected = await _bitacoraService.TestConnectionAsync(cancellationToken);

                var message = isConnected ? "Conexión exitosa" : "Conexión fallida";
                return Ok(ApiResponse.Success(isConnected, message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al probar conexión");
                return StatusCode(500, ApiResponse.Error("Error interno del servidor"));
            }
        }
    }
}