using BitacoraReader.Domain.Common;
using BitacoraReader.Domain.Entities;
using BitacoraReader.Domain.Interfaces;
using BitacoraReader.Domain.ValueObjects;
using BitacoraReader.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BitacoraReader.Infrastructure.Repositories
{
    public class BitacoraRepository : IBitacoraRepository
    {
        private readonly BitacoraDbContext _context;
        private readonly ILogger<BitacoraRepository> _logger;

        public BitacoraRepository(BitacoraDbContext context, ILogger<BitacoraRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<PagedResult<Bitacora>> GetBitacorasAsync(
            BitacoraFilter filter,
            CancellationToken cancellationToken = default)
        {
            var query = _context.Bitacoras.AsQueryable();

            // Aplicar filtros
            if (filter.IdJob.HasValue)
                query = query.Where(b => b.IdJob == filter.IdJob.Value);

            if (filter.Dia.HasValue)
                query = query.Where(b => b.Dia == filter.Dia.Value);

            if (filter.FechaInicio.HasValue)
            {
                // Asegura que la hora sea 00:00:00 para el inicio del rango
                var startDate = filter.FechaInicio.Value.Date;
                query = query.Where(b => b.FechaInicio >= startDate);
            }

            if (filter.FechaTermino.HasValue)
            {
                // Asegura que la hora sea 00:00:00 y luego suma un día para cubrir todo el día
                // Ejemplo: Si filter.FechaInicioHasta es '2025-07-28 10:00:00',
                // endDate se convierte en '2025-07-29 00:00:00'.
                var endDate = filter.FechaTermino.Value.Date.AddDays(1);
                query = query.Where(b => b.FechaTermino <= endDate); // Usamos '<' para incluir todo el día de filter.FechaInicioHasta
            }


            if (!string.IsNullOrEmpty(filter.Resultado))
                query = query.Where(b => b.Resultado.Contains(filter.Resultado));

            if (!string.IsNullOrEmpty(filter.NombreArchivo))
                query = query.Where(b => b.NombreArchivo.Contains(filter.NombreArchivo));

            // Contar total
            var totalCount = 0;
            IReadOnlyList<Bitacora> items = new List<Bitacora>();
            try
            {
                 totalCount = await query.CountAsync(cancellationToken);

                // Aplicar paginación
                  items = await query
                    .OrderByDescending(b => b.FechaInicio)
                    .Skip((filter.PageNumber - 1) * filter.PageSize)
                    .Take(filter.PageSize)
                    .ToListAsync(cancellationToken);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            



            return new PagedResult<Bitacora>(items, totalCount, filter.PageNumber, filter.PageSize);
        }

        public async Task<Bitacora?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return await _context.Bitacoras
                .FirstOrDefaultAsync(b => b.IdBitacora == id, cancellationToken);
        }

        public async Task<IEnumerable<string>> GetDistinctResultadosAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Bitacoras
                .Where(b => !string.IsNullOrEmpty(b.Resultado))
                .Select(b => b.Resultado)
                .Distinct()
                .OrderBy(r => r)
                .ToListAsync(cancellationToken);
        }


        public async Task<IEnumerable<string>> GetDistinctResultadosAsyncSub(
            string? filtroSubcadena = null,
            string? excluirQueComiecenCon = null,
            CancellationToken cancellationToken = default)
        {
            var query = _context.Bitacoras
                .Where(b => !string.IsNullOrEmpty(b.Resultado));

            // Incluir solo los que contengan la subcadena
            if (!string.IsNullOrEmpty(filtroSubcadena))
            {
                query = query.Where(b => !b.Resultado.Contains(filtroSubcadena));
            }

            // Excluir los que comiencen con la subcadena especificada
            if (!string.IsNullOrEmpty(excluirQueComiecenCon))
            {
                query = query.Where(b => !b.Resultado.StartsWith(excluirQueComiecenCon));
            }

            return await query
                .Select(b => b.Resultado)
                .Distinct()
                .OrderBy(r => r)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<int>> GetDistinctJobsAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Bitacoras
                .Select(b => b.IdJob)
                .Distinct()
                .OrderBy(j => j)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> TestConnectionAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await _context.Database.CanConnectAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al probar conexión a la base de datos");
                return false;
            }
        }

        public async Task<int> GetTotalCountAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Bitacoras.CountAsync(cancellationToken);
        }


        public async Task<IEnumerable<string>> GetDistinctResultadosByMultiplesSubcadenasAsync(
    IEnumerable<string>? subcadenas = null,
    IEnumerable<string>? excluirQueComiecenCon = null,
    bool todasLasSubcadenas = true, // true = AND, false = OR
    bool ignorarMayusculas = true,
    CancellationToken cancellationToken = default)
        {
            var query = _context.Bitacoras
                .Where(b => !string.IsNullOrEmpty(b.Resultado));

            // Aplicar filtros de inclusión por subcadenas
            if (subcadenas != null && subcadenas.Any())
            {
                var subcadenasValidas = subcadenas.Where(s => !string.IsNullOrEmpty(s)).ToList();

                if (subcadenasValidas.Any())
                {
                    if (todasLasSubcadenas)
                    {
                        // Debe contener TODAS las subcadenas (AND)
                        foreach (var subcadena in subcadenasValidas)
                        {
                            var subcadenaLocal = subcadena; // Para captura en lambda
                            if (ignorarMayusculas)
                            {
                                query = query.Where(b => b.Resultado.ToLower().Contains(subcadenaLocal.ToLower()));
                            }
                            else
                            {
                                query = query.Where(b => b.Resultado.Contains(subcadenaLocal));
                            }
                        }
                    }
                    else
                    {
                        // Debe contener AL MENOS UNA subcadena (OR)
                        if (ignorarMayusculas)
                        {
                            var subcadenasLower = subcadenasValidas.Select(s => s.ToLower()).ToList();
                            query = query.Where(b => subcadenasLower.Any(s => b.Resultado.ToLower().Contains(s)));
                        }
                        else
                        {
                            query = query.Where(b => subcadenasValidas.Any(s => b.Resultado.Contains(s)));
                        }
                    }
                }
            }

            // Aplicar filtros de exclusión
            if (excluirQueComiecenCon != null && excluirQueComiecenCon.Any())
            {
                var exclusionesValidas = excluirQueComiecenCon.Where(s => !string.IsNullOrEmpty(s)).ToList();

                if (exclusionesValidas.Any())
                {
                    foreach (var exclusion in exclusionesValidas)
                    {
                        var exclusionLocal = exclusion; // Para captura en lambda
                        if (ignorarMayusculas)
                        {
                            query = query.Where(b => !b.Resultado.ToLower().StartsWith(exclusionLocal.ToLower()));
                        }
                        else
                        {
                            query = query.Where(b => !b.Resultado.StartsWith(exclusionLocal));
                        }
                    }
                }
            }

            return await query
                .Select(b => b.Resultado)
                .Distinct()
                .OrderBy(r => r)
                .ToListAsync(cancellationToken);
        }

    }
}