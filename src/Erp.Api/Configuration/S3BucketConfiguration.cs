namespace Erp.Api.Configuration;

public class S3BucketConfiguration
{
    public string BucketName { get; set; }
    public string AccessKey { get; set; }
    public string SecretKey { get; set; }
    public string Region { get; set; }
    public string Url { get; set; }
    public FoldersConfiguration Folders { get; set; }
}