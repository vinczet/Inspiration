namespace Inspiration.Repository.Interfaces;

public interface IPartRepository
{
    public void CreateBatch(List<string> parts, Guid npuId);
    public List<string> GetForNPU(Guid npuId);
}
