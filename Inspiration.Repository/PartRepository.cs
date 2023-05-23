using Inspiration.Repository.DataAccessObjects;
using Inspiration.Repository.Interfaces;
namespace Inspiration.Repository;
public class PartRepository : IPartRepository
{
    public void CreateBatch(List<string> parts, Guid npuId)
    {
        //First remove all, then add
        MockDB.Parts.RemoveAll(x=>x.NPUId.Equals(npuId));
        foreach (var part in parts)
        {
            MockDB.Parts.Add(new PartDao(Guid.NewGuid(), npuId, part.ToLower()));
        }        
    }

    public List<string> GetForNPU(Guid npuId)
    {                
        return MockDB.Parts.Where(x => x.NPUId == npuId).Select(y => y.Part).ToList();
    }
}
