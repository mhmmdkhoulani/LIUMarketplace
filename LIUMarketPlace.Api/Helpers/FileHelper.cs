using Azure.Storage.Blobs;

namespace LIUMarketPlace.Api.Helpers
{
    public static class FileHelper
    {
        public static async Task<string> ImageUpload(IFormFile file)
        {
            string connectionString = @"DefaultEndpointsProtocol=https;AccountName=liumrketpxlace;AccountKey=xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
            string containerName = "productcover";

            BlobContainerClient blobContainerClient = new BlobContainerClient(connectionString, containerName);
            BlobClient blobClient = blobContainerClient.GetBlobClient($"{Guid.NewGuid().ToString()}-{file.FileName}");
            var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            await blobClient.UploadAsync(memoryStream);

            return blobClient.Uri.AbsoluteUri;
        }
        //public static async Task<string> AudioUpload(IFormFile file)
        //{
        //    string connectionString = @"DefaultEndpointsProtocol=https;AccountName=mkmusicapistorage;AccountKey=PSYsdBtWIV7QuDxXign1XEc7axt2Nkr6waUiJMTwhJnGVS5tKS63JHlsszNj5tn2SWtS4WCBJicyTarRv2dEWQ==;EndpointSuffix=core.windows.net";
        //    string containerName = "songs";

        //    BlobContainerClient blobContainerClient = new BlobContainerClient(connectionString, containerName);
        //    BlobClient blobClient = blobContainerClient.GetBlobClient(file.FileName);
        //    var memoryStream = new MemoryStream();
        //    await file.CopyToAsync(memoryStream);
        //    memoryStream.Position = 0;
        //    await blobClient.UploadAsync(memoryStream);

        //    return blobClient.Uri.AbsoluteUri;
        //}
    }
}
