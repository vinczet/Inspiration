namespace Inspiration.Contract;
public class NPUCreateRequestDto
{
    public const int MinDesctiptionLength = 5;
    public const int MaxDesctiptionLength = 2000;

    public const int MinPartsLength = 3;
    public const int MaxPartsLength = 100;
    public string Description { get; set; } = "";
    public List<string> Parts { get; set; } = new List<string>();
}
