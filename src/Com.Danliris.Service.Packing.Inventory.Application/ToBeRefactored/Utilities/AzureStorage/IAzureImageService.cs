using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities
{
    public interface IAzureImageService
    {
        string GetFileNameFromPath(string imagePath);
        Task<string> DownloadImage(string directoryName, string imagePath);
        Task<string> UploadImage(string directoryName, string imageName, string imageBase64);
        Task RemoveImage(string directoryName, string imagePath);
    }
}
