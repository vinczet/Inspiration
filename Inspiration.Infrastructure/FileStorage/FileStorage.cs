namespace Inspiration.Infrastructure.FileStorage;

public class FileStorage : IFileStorage
{
    private const string IMAGEPATH = @"C:\Temp\InspirationImages\";
    private string _ImageBaseUrl = "https://localhost:7188/";
    public string GetUri(string imageCode)
    {
        return $"{_ImageBaseUrl}{imageCode}";
    }

    public async Task<string> SaveAsync(MemoryStream ms, string extension, CancellationToken cancellationToken = default)
    {
        var fileName = Guid.NewGuid().ToString() + extension;
        var fullPath = Path.Combine(IMAGEPATH, fileName);
        using (FileStream file = new FileStream(fullPath, FileMode.Create, System.IO.FileAccess.Write))
            await ms.CopyToAsync(file, cancellationToken);
        return fileName;
    }
}
