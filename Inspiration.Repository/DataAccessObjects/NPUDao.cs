namespace Inspiration.Repository.DataAccessObjects;

public class NPUDao
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Description { get; set; }
    public string ImageCode { get; set; }

    /// <summary>
    /// An Admin set this to true, so it will show up in listings
    /// </summary>
    public bool IsEnabled { get; set; }
    public DateTime CreationDateTime { get; set; }
    public DateTime UpdateDateTime { get; set; }

    public NPUDao(Guid id, Guid userId, string description, string imageCode, bool isEnabled, DateTime creationDateTime, DateTime updateDateTime)
    {
        Id = id;
        UserId = userId;
        Description = description;
        ImageCode = imageCode;
        IsEnabled = isEnabled;
        CreationDateTime = creationDateTime;
        UpdateDateTime = updateDateTime;
    }
}
