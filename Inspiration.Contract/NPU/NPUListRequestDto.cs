namespace Inspiration.Contract;
public class NPUListRequestDto
{
    public int? PageSize { get; set; }
    public int? PageNumber { get; set; }
    public String? SearchInDescription { get; set; }
    public String? UserName { get; set; }
    public float? AboveAverageCreativityRating { get; set; }
    public float? BelowAverageCreativityRating { get; set; }
    public float? AboveAverageUniqunessRating { get; set; }
    public float? BelowAverageUniqunessRating { get; set; }
    public bool? IsEnabled { get; set; }
    public List<string>? Parts { get; set; }
}
