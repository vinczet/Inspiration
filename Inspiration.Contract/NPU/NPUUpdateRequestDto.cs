namespace Inspiration.Contract;

public class NPUUpdateRequestDto
{
    public Guid Id { get; set; }    
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
}
