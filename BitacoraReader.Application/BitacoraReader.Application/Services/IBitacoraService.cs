using BitacoraReader.Application.DTOs;
using BitacoraReader.Domain.Common;

namespace BitacoraReader.Application.Services
{
    public interface IBitacoraService
    {
        Task<PagedResult<BitacoraDto>> GetBitacorasAsync(BitacoraFilterDto filter, CancellationToken cancellationToken = default);
        Task<BitacoraDto?> GetBitacoraByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<IEnumerable<string>> GetDistinctResultadosAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<int>> GetDistinctJobsAsync(CancellationToken cancellationToken = default);
        Task<bool> TestConnectionAsync(CancellationToken cancellationToken = default);
        Task<int> GetTotalCountAsync(CancellationToken cancellationToken = default);
    }
}