using Amazon.S3;
using Amazon.S3.Model;
using Amazon.Runtime;
using Server.DataBase;
using Amazon.S3.Transfer;

public class YandexS3Service
{
    private readonly IAmazonS3 _s3Client;
    private readonly YandexCloudKeys yandexCloudKeys;
    private readonly string BUCKETNAME = "web3pinterest";
    public YandexS3Service(YandexCloudKeys yandexCloudKeys)
    {
        this.yandexCloudKeys = yandexCloudKeys;
        var config = new AmazonS3Config
        {
            ServiceURL = "https://storage.yandexcloud.net",
            ForcePathStyle = true
        };

        var credentials = new BasicAWSCredentials(yandexCloudKeys.ACCESS_KEY, yandexCloudKeys.SECRET_KEY);

        _s3Client = new AmazonS3Client(credentials, config);
    }

    public async Task UploadFileAsync(string filePath, string keyName)
    {
        try
        {
            var fileTransferUtility = new TransferUtility(_s3Client);
            await fileTransferUtility.UploadAsync(filePath, BUCKETNAME, keyName);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    public async Task<string> GetImageUrl(string keyName)
    {
        return $"https://storage.yandexcloud.net/{BUCKETNAME}/{keyName}";
    }
}