using ErrorOr;
using Inspiration.Contract;

namespace Inspiration.Domain.Services.Interfaces;

public interface INPUService
{
    public Task<ErrorOr<NPUResponseDto>> CreateAsync(NPUCreateRequestDto npu, Guid userId, MemoryStream ms, string fileExtension, CancellationToken cancellationToken = default);
    /// <summary>
    /// Returns a NRU, for admins even not enabled ones
    /// </summary>
    /// <param name="id"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public ErrorOr<NPUResponseDto> Get(Guid id, Guid userId);
    public ErrorOr<RatingDto> CreateCreativityRating(Guid npuId, Guid userId, int rating);
    public ErrorOr<RatingDto> CreateUniquenessRating(Guid npuId, Guid userId, int rating);
    public ErrorOr<NPUListResponseDto> List(NPUListRequestDto filters, Guid userId);
}
