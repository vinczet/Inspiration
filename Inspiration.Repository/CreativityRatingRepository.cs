using Inspiration.Repository.DataAccessObjects;
using Inspiration.Repository.Interfaces;
namespace Inspiration.Repository;
public class CreativityRatingRepository : ICreativityRatingRepository
{
    public void CreateRating(Guid npuId, Guid userId, int rating)
    {
        //First remove all, then add
        MockDB.CreativityRatings.RemoveAll(x => x.NPUId.Equals(npuId) && x.UserId.Equals(userId));
        MockDB.CreativityRatings.Add(new CreativityRatingDao(Guid.NewGuid(), npuId, userId, rating));
    }

    public (float? averageRating, int ratingCount) GetRatingsForNpu(Guid npuId)
    {
        var ret = MockDB.CreativityRatings.Where(x => x.NPUId.Equals(npuId)).GroupBy(i => 1).Select(g => new { Count = g.Count(), Average = g.Average(i => i.Rating) }).FirstOrDefault();
        if (ret == null) 
        { 
            return (null, 0);  
        }
        return ((float)ret.Average, ret.Count);
    }
}
