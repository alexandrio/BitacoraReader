using BitacoraReader.Domain.Common;
using BitacoraReader.Domain.Entities;
using BitacoraReader.Domain.ValueObjects;

namespace BitacoraReader.Domain.Interfaces
{
    public interface IBitacoraRepository
    {
        Task<PagedResult<Bitacora>> GetBitacorasAsync(BitacoraFilter filter, CancellationToken cancellationToken = default);
        Task<Bitacora?> GetByIdAsync(long id, CancellationToken cancellationToken = default);

        Task<IEnumerable<string>> GetDistinctResultadosAsyncSub(  string? filtroSubcadena,   string? excluirQueComiecenCon,                  CancellationToken cancellationToken = default);
        Task<IEnumerable<string>> GetDistinctResultadosAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<int>> GetDistinctJobsAsync(CancellationToken cancellationToken = default);
        Task<bool> TestConnectionAsync(CancellationToken cancellationToken = default);
        Task<int> GetTotalCountAsync(CancellationToken cancellationToken = default);


        Task<IEnumerable<string>> GetDistinctResultadosByMultiplesSubcadenasAsync(
            IEnumerable<string>? subcadenas,
            IEnumerable<string>? excluirQueComiecenCon,
            bool todasLasSubcadenas, // true = AND, false = OR
            bool ignorarMayusculas,
            CancellationToken cancellationToken = default);
    }
}