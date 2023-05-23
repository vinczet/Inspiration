using Inspiration.Repository.DataAccessObjects;
using Inspiration.Repository.Interfaces;

namespace Inspiration.Repository;
public class NPURepository : INPURepository
{
    public void Create(NPUDao dao)
    {
        MockDB.NPUs.Add(dao);
    }

    public NPUDao? Get(Guid id)
    {
        return MockDB.NPUs.Where(x => x.Id == id).FirstOrDefault();
    }
    public NPUDao? GetEnabled(Guid id)
    {
        return MockDB.NPUs.Where(x => x.Id == id && x.IsEnabled).FirstOrDefault();
    }

    public (List<NPUListItemDao> NPUs, int TotalCount) GetList(
        int? pageSize,
        int? pageNumber,
        string? searchInDescription,
        string? userName,
        float? AboveAverageCreativityRating,
        float? BelowAverageCreativityRating,
        float? AboveAverageUniqunessRating,
        float? BelowAverageUniqunessRating,
        bool? isEnabled,
        List<string>? parts)
    {
        var averageUniqeness = MockDB.UniquenessRatings.GroupBy(i => i.NPUId).Select(g => new { NPUId = g.Key, Average = g.Average(i => i.Rating), Count = g.Count() });
        var averageCreativity = MockDB.CreativityRatings.GroupBy(i => i.NPUId).Select(g => new { NPUId = g.Key, Average = g.Average(i => i.Rating), Count = g.Count() });
        var partsList = MockDB.Parts.GroupBy(i => i.NPUId).Select(g => new { NPUId = g.Key, Parts = g.Select(i => i.Part).ToList() });

        //TODO Handle order by, till that it is CreatinDateTime

        var query = from a in MockDB.NPUs
                    join u in MockDB.Users on a.UserId equals u.Id
                    join uniqR in averageUniqeness on a.Id equals uniqR.NPUId into leftUniq
                    from lu in leftUniq.DefaultIfEmpty()
                    join creatR in averageCreativity on a.Id equals creatR.NPUId into leftCreat
                    from lc in leftCreat.DefaultIfEmpty()
                    join p in partsList on a.Id equals p.NPUId into leftParts
                    from lp in leftParts.DefaultIfEmpty()

                    where
                        (string.IsNullOrEmpty(searchInDescription) || a.Description.Contains(searchInDescription)) &&
                        (string.IsNullOrEmpty(userName) || u.Name.Equals(userName)) &&
                        (isEnabled == null || a.IsEnabled.Equals(isEnabled)) &&
                        (parts == null || parts.Count == 0 || (lp != null && !parts.Except(lp.Parts).Any())) &&
                        (AboveAverageCreativityRating == null || (lc != null && AboveAverageCreativityRating < lc.Average)) &&
                        (BelowAverageCreativityRating == null || (lc != null && BelowAverageCreativityRating > lc.Average)) &&
                        (AboveAverageUniqunessRating == null || (lu != null && AboveAverageUniqunessRating < lu.Average)) &&
                        (BelowAverageUniqunessRating == null || (lu != null && BelowAverageUniqunessRating > lu.Average))

                    orderby a.CreationDateTime
                    select new NPUListItemDao()
                    {
                        Id = a.Id,
                        Description = a.Description,
                        UserId = a.UserId,
                        UserName = u.Name,
                        IsEnabled = a.IsEnabled,
                        CreationDateTime = a.CreationDateTime,
                        UpdateDateTime = a.UpdateDateTime,
                        AverageCreativityRating = lc == null ? null : (float)lc.Average,
                        AverageUniquenessRating= lu == null ? null : (float)lu.Average,
                        CreativityRatings = lc == null ? 0 : lc.Count,
                        UniquenessRatings = lu == null ? 0 : lu.Count,
                        //This is only a code, it has to be changed to URI
                        ImageUrl = a.ImageCode,
                        Parts = lp == null ? new List<string>() : lp.Parts
                    };
        var totalCount = query.Count();

        if (pageSize != null && pageNumber != null)
        {
            query = query.Skip(pageSize.Value * (pageNumber.Value - 1)).Take(pageSize.Value);
        }
        return (query.ToList(), totalCount);
    }
}
