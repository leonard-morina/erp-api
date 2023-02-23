namespace Erp.Api.Files;

public interface IFileUploader
{
    Task<string> UploadFileAsync(IFormFile file, string folder,CancellationToken cancellationToken = default);
    string GetFileUrl(string key, string folder);
    Task<bool> FileExists(string folder, string filePrefix, CancellationToken cancellationToken = default);
    Task<Stream> GetFileAsync(string key, CancellationToken cancellationToken = default);
}
