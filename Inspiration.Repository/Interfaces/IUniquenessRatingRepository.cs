namespace Inspiration.Repository.Interfaces;

public interface IUniquenessRatingRepository
{
    public void CreateRating(Guid npuId, Guid userId, int rating);
    public (float? averageRating, int ratingCount) GetRatingsForNpu(Guid npuId);

    //TODO: modify existing rating
}
