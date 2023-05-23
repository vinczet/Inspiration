namespace Inspiration.Repository.DataAccessObjects;

public class CreativityRatingDao
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid NPUId { get; set; }
    public int Rating { get; set; }

    public CreativityRatingDao(Guid id, Guid nPUId, Guid userId, int rating)
    {
        Id = id;
        UserId = userId;
        NPUId = nPUId;
        Rating = rating;
    }
}
