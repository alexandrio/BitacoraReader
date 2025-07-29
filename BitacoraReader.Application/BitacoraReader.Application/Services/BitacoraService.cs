using AutoMapper;
using BitacoraReader.Application.DTOs;
using BitacoraReader.Application.Services;
using BitacoraReader.Domain.Common;
using BitacoraReader.Domain.Interfaces;
using BitacoraReader.Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace BitacoraReader.Application.Services
{
    public class BitacoraService : IBitacoraService
    {
        private readonly IBitacoraRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<BitacoraService> _logger;

        public BitacoraService(
            IBitacoraRepository repository,
            IMapper mapper,
            ILogger<BitacoraService> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<PagedResult<BitacoraDto>> GetBitacorasAsync(
            BitacoraFilterDto filterDto,
            CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("Obteniendo bitácoras con filtros: {@Filter}", filterDto);

                var filter = _mapper.Map<BitacoraFilter>(filterDto);
                var result = await _repository.GetBitacorasAsync(filter, cancellationToken);

                var dtoItems = _mapper.Map<IEnumerable<BitacoraDto>>(result.Items);

                return new PagedResult<BitacoraDto>(
                    dtoItems,
                    result.TotalCount,
                    result.PageNumber,
                    result.PageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener bitácoras");
                throw;
            }
        }

        public async Task<BitacoraDto?> GetBitacoraByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("Obteniendo bitácora por ID: {Id}", id);

                var bitacora = await _repository.GetByIdAsync(id, cancellationToken);
                return bitacora != null ? _mapper.Map<BitacoraDto>(bitacora) : null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener bitácora por ID: {Id}", id);
                throw;
            }
        }

        public async Task<IEnumerable<string>> GetDistinctResultadosAsync( CancellationToken cancellationToken = default)
        {
            try
            {
                //return await _repository.GetDistinctResultadosAsync(cancellationToken);
                return await _repository.GetDistinctResultadosAsyncSub(null, "Error: No se pudo encontrar el archivo", cancellationToken);
                
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener resultados distintos");
                throw;
            }
        }

        public async Task<IEnumerable<int>> GetDistinctJobsAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await _repository.GetDistinctJobsAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener jobs distintos");
                throw;
            }
        }

        public async Task<bool> TestConnectionAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await _repository.TestConnectionAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al probar conexión");
                return false;
            }
        }

        public async Task<int> GetTotalCountAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await _repository.GetTotalCountAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener conteo total");
                throw;
            }
        }
    }
}