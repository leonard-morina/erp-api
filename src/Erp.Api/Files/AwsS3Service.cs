using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Erp.Api.Configuration;

namespace Erp.Api.Files;

public class AwsS3Service : IFileUploader
{
    private readonly S3BucketConfiguration _s3BucketConfig;
    private readonly IAmazonS3 _s3Client;

    public AwsS3Service(S3BucketConfiguration s3BucketConfig)
    {
        _s3BucketConfig = s3BucketConfig;
        var region = RegionEndpoint.GetBySystemName(s3BucketConfig.Region);

        _s3Client = new AmazonS3Client(new BasicAWSCredentials(s3BucketConfig.AccessKey, s3BucketConfig.SecretKey),
            new AmazonS3Config
            {
                RegionEndpoint = region
            });
    }

    public async Task<string> UploadFileAsync(IFormFile file, string folder,
        CancellationToken cancellationToken = default)
    {
        if (file == null || file.Length == 0)
        {
            throw new ArgumentException("File is missing or empty.");
        }
        
        var key = $"{Guid.NewGuid().ToString()}_{file.FileName}";
        try
        {
            var fileTransferUtility = new TransferUtility(_s3Client);

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);

            await fileTransferUtility.UploadAsync(memoryStream, _s3BucketConfig.BucketName, $"{folder}/{key}", cancellationToken);
        }
        catch (AmazonS3Exception ex)
        {
            //TODO: Add logger
            // _logger.LogError(ex, "Error uploading file to S3 bucket");
            throw;
        }

        return key;
    }

    public string GetFileUrl(string key, string folder) => $"{_s3BucketConfig.Url}/{folder}/{key}";

    public async Task<Stream> GetFileAsync(string key, CancellationToken cancellationToken = default)
    {
        var getObjectRequest = new GetObjectRequest
        {
            BucketName = _s3BucketConfig.BucketName,
            Key = key
        };

        var response = await _s3Client.GetObjectAsync(getObjectRequest, cancellationToken);
        return response.ResponseStream;
    }

    /// <summary>
    /// Determines whether a file exists within the specified bucket
    /// </summary>
    /// <param name="filePrefix">Match files that begin with this prefix</param>
    /// <returns>True if the file exists</returns>
    public async Task<bool> FileExists(string folder, string filePrefix, CancellationToken cancellationToken = default)
    {
        var request = new ListObjectsRequest
        {
            BucketName = _s3BucketConfig.BucketName,
            Prefix = $"{folder}/{filePrefix}",
            MaxKeys = 1
        };

        var response = await _s3Client.ListObjectsAsync(request, cancellationToken);

        return response.S3Objects.Any();
    }
    
    
    public async Task DeleteFileAsync(string fileName, string folder, CancellationToken cancellationToken = default)
    {
        var deleteObjectRequest = new DeleteObjectRequest
        {
            BucketName = _s3BucketConfig.BucketName,
            Key = $"{folder}/{fileName}",
        };

        try
        {
            await _s3Client.DeleteObjectAsync(deleteObjectRequest, cancellationToken);
        }
        catch (AmazonS3Exception ex)
        {
            //TODO: Add logger
            // _logger.LogError(ex, "Error uploading file to S3 bucket");
            throw;
        }
    }
}