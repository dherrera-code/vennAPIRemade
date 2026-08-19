using Azure.Storage.Blobs;

namespace vennAPIRemade.Services
{
    public class BlobServices
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName;

        public BlobServices(IConfiguration configuration)
        {
            var connectionString = configuration["AzureBlobStorage:ConnectionString"];
            _containerName = configuration["AzureBlobStorage:ContainerName"];
            _blobServiceClient = new BlobServiceClient(connectionString);
        }
        public async Task<string> UploadFileAsync(Stream stream, string fileName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = containerClient.GetBlobClient(fileName);

            await containerClient.CreateIfNotExistsAsync();
            await blobClient.UploadAsync(stream, overwrite: true );

            return blobClient.Uri.ToString();
        }
    }
}