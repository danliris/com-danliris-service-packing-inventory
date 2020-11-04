using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities
{
    public class AzureStorageService
    {
        protected IServiceProvider ServiceProvider { get; private set; }
        protected CloudStorageAccount StorageAccount { get; private set; }
        protected CloudBlobContainer StorageContainer { get; private set; }

        public AzureStorageService(IServiceProvider serviceProvider)
        {

            string storageAccountName = ApplicationSetting.StorageAccountName;
            string storageAccountKey = ApplicationSetting.StorageAccountKey;
            string storageContainer = "packing-inventory";

            ServiceProvider = serviceProvider;
            StorageAccount = new CloudStorageAccount(new StorageCredentials(storageAccountName, storageAccountKey), useHttps: true);
            StorageContainer = Configure(storageContainer).GetAwaiter().GetResult();
        }

        private async Task<CloudBlobContainer> Configure(string storageContainer)
        {
            CloudBlobClient cloudBlobClient = StorageAccount.CreateCloudBlobClient();

            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(storageContainer);
            await cloudBlobContainer.CreateIfNotExistsAsync();

            BlobContainerPermissions permissions = SetContainerPermission(true);
            await cloudBlobContainer.SetPermissionsAsync(permissions);

            return cloudBlobContainer;
        }

        private BlobContainerPermissions SetContainerPermission(bool isPublic)
        {
            BlobContainerPermissions permissions = new BlobContainerPermissions();
            if (isPublic)
                permissions.PublicAccess = BlobContainerPublicAccessType.Container;
            else
                permissions.PublicAccess = BlobContainerPublicAccessType.Off;
            return permissions;
        }
    }
}
