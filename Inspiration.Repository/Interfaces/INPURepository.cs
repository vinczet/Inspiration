using Inspiration.Repository.DataAccessObjects;

namespace Inspiration.Repository.Interfaces;

public interface INPURepository
{
    public void Create(NPUDao dao);
    public NPUDao? Get(Guid id);
    public NPUDao? GetEnabled(Guid id);
    public (List<NPUListItemDao> NPUs, int TotalCount) GetList(int? pageSize,
                          int? pageNumber,
                          string? searchInDescription,
                          string? userName,
                          float? AboveAverageCreativityRating,
                          float? BelowAverageCreativityRating,
                          float? AboveAverageUniqunessRating,
                          float? BelowAverageUniqunessRating,
                          bool? isEnabled,
                          List<string>? parts);
}
    