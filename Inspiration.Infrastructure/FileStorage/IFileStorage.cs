namespace Inspiration.Infrastructure.FileStorage;

public interface IFileStorage
{
    public Task<string> SaveAsync(MemoryStream ms, string extension, CancellationToken cancellationToken = default);
    public string GetUri(string imageCode);
}
