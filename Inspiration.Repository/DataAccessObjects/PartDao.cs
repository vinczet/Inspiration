namespace Inspiration.Repository.DataAccessObjects;

public class PartDao
{
    public Guid Id { get; set; }
    public Guid NPUId { get; set; }
    public string Part { get; set; }

    public PartDao(Guid id, Guid nPUId, string part)
    {
        Id = id;
        NPUId = nPUId;
        Part = part;
    }
}
