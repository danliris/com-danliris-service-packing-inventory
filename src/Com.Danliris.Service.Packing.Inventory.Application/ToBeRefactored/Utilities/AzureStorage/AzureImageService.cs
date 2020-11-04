using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities
{
    public class AzureImageService : AzureStorageService, IAzureImageService
    {
        public AzureImageService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        private string getBase64File(string encoded)
        {
            return encoded.Substring(encoded.IndexOf(',') + 1);
        }

        private string getBase64Type(string encoded)
        {
            Regex regex = new Regex(@"data:([a-zA-Z0-9]+\/[a-zA-Z0-9-.+]+).*,.*");
            string match = regex.Match(encoded).Groups[1].Value;

            return match == null && match == string.Empty ? "image/jpeg" : match;
        }

        public string GetFileNameFromPath(string imagePath)
        {
            string[] filePath = imagePath.Split('/');
            return filePath[filePath.Length - 1];
        }

        public async Task<string> DownloadImage(string directoryName, string imagePath)
        {
            if (!string.IsNullOrWhiteSpace(imagePath))
            {
                string imageName = GetFileNameFromPath(imagePath);
                return await DownloadBase64Image(directoryName, imageName);
            }
            return null;
        }

        private async Task<string> DownloadBase64Image(string directoryName, string imageName)
        {
            string imageSrc = string.Empty;

            try
            {
                CloudBlobContainer container = this.StorageContainer;
                CloudBlobDirectory dir = container.GetDirectoryReference(directoryName);

                CloudBlockBlob blob = dir.GetBlockBlobReference(imageName);
                await blob.FetchAttributesAsync();

                byte[] imageBytes = new byte[blob.Properties.Length];
                await blob.DownloadToByteArrayAsync(imageBytes, 0);

                string imageBase64 = Convert.ToBase64String(imageBytes);
                imageSrc = "data:" + blob.Properties.ContentType + ";base64," + imageBase64;
            }
            catch (Exception ex)
            {
                if (!(ex is StorageException))
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
            }

            return imageSrc;
        }

        public async Task<string> UploadImage(string directoryName, string imageName, string imageBase64)
        {
            return await UploadBase64Image(directoryName, imageName, imageBase64);
        }

        private async Task<string> UploadBase64Image(string directoryName, string imageName, string imageBase64)
        {
            string path = null;

            try
            {
                string imageFile = getBase64File(imageBase64);
                string imageType = getBase64Type(imageBase64);
                byte[] imageBytes = Convert.FromBase64String(imageFile);
                if (imageBytes != null)
                {
                    CloudBlobContainer container = StorageContainer;
                    CloudBlobDirectory dir = container.GetDirectoryReference(directoryName);

                    CloudBlockBlob blob = dir.GetBlockBlobReference(imageName);
                    blob.Properties.ContentType = imageType;
                    await blob.UploadFromByteArrayAsync(imageBytes, 0, imageBytes.Length);
                    path = "/" + StorageContainer.Name + "/" + directoryName + "/" + imageName;
                }
            }
            catch (Exception ex)
            {
                if (!(ex is ArgumentNullException) && !(ex is FormatException))
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
            }

            return path;
        }

        public async Task RemoveImage(string directoryName, string imagePath)
        {
            if (imagePath != null)
            {
                string fileName = GetFileNameFromPath(imagePath);
                await RemoveBase64Image(directoryName, fileName);
            }
        }

        private async Task RemoveBase64Image(string directoryName, string fileName)
        {
            CloudBlobContainer container = StorageContainer;
            CloudBlobDirectory dir = container.GetDirectoryReference(directoryName);

            CloudBlockBlob blob = dir.GetBlockBlobReference(fileName);
            await blob.DeleteIfExistsAsync();
        }
    }
}
