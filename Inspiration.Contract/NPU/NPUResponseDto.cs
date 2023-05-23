namespace Inspiration.Contract;

public class NPUResponseDto
{
    public Guid Id { get; set; }
    public UserResponseDto User { get; set; }
    public string Description { get; set; } = "";
    public string ImageUrl { get; set; } = "";

    public bool IsEnabled { get; set; }
    public DateTime CreationDateTime { get; set; }
    public DateTime UpdateDateTime { get; set; }
    public List<string> Parts { get; set; } = new List<string>();
    public float? AverageCreativityRating { get; set; }
    public  int CreativityRatings { get; set; }
    public float? AverageUniquenessRating { get; set; }
    public int UniquenessRatings { get; set; }

    public NPUResponseDto(
        Guid id,
        UserResponseDto user,
        string description,
        string imageUrl,
        bool isEnabled,
        DateTime creationDateTime,
        DateTime updateDateTime,
        List<string> parts,
        float? averageCreativityRating,
        int creativityRatings,
        float? averageUniquenessRating,
        int uniquenessRatings)
    {
        Id = id;
        User = user;
        Description = description;
        ImageUrl = imageUrl;
        IsEnabled = isEnabled;
        CreationDateTime = creationDateTime;
        UpdateDateTime = updateDateTime;
        Parts = parts;
        AverageCreativityRating = averageCreativityRating;
        CreativityRatings = creativityRatings;
        AverageUniquenessRating = averageUniquenessRating;
        UniquenessRatings = uniquenessRatings;
    }
}
