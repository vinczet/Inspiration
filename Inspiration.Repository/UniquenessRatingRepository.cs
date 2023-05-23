using Inspiration.Repository.DataAccessObjects;
using Inspiration.Repository.Interfaces;

namespace Inspiration.Repository;
public class UniquenessRatingRepository : IUniquenessRatingRepository
{
    public void CreateRating(Guid npuId, Guid userId, int rating)
    {
        //First remove all, then add
        MockDB.UniquenessRatings.RemoveAll(x => x.NPUId.Equals(npuId) && x.UserId.Equals(userId));
        MockDB.UniquenessRatings.Add(new UniquenessRatingDao(Guid.NewGuid(), npuId, userId, rating));
    }

    public (float? averageRating, int ratingCount) GetRatingsForNpu(Guid npuId)
    {
        var ret = MockDB.UniquenessRatings.Where(x => x.NPUId.Equals(npuId)).GroupBy(i => 1).Select(g => new { Count = g.Count(), Average = g.Average(i => i.Rating) }).FirstOrDefault();
        if (ret == null)
        {
            return (null, 0);
        }
        return ((float)ret.Average, ret.Count);
    }
}
