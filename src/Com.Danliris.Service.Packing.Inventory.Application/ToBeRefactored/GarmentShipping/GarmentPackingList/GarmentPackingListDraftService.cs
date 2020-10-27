using Com.Danliris.Service.Packing.Inventory.Application.Helper;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using System;
using System.Linq;
using System.Threading.Tasks;
using SHA1 = Com.Danliris.Service.Packing.Inventory.Application.Helper.SHA1;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList
{
    public class GarmentPackingListDraftService : GarmentPackingListService, IGarmentPackingListDraftService
    {
        private const string UserAgent = "GarmentPackingListDraftService";

        public GarmentPackingListDraftService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        private string GenerateFileName(int id, DateTime createdUtc, string hash)
        {
            return string.Format("IMG_{0}_{1}_{2}", id, Timestamp.Generate(createdUtc), hash);
        }

        private async Task<string> UploadImage(string imageFile, int id, string imagePath, DateTime createdUtc)
        {
            if (!string.IsNullOrWhiteSpace(imageFile))
            {
                var shippingMarkImageHash = SHA1.Hash(imageFile);

                if (!string.IsNullOrWhiteSpace(imagePath))
                {
                    var fileName = _azureImageService.GetFileNameFromPath(imagePath).Split("_");

                    if (fileName[3] != shippingMarkImageHash)
                    {
                        await _azureImageService.RemoveImage(IMG_DIR, imagePath);
                        imagePath = await _azureImageService.UploadImage(IMG_DIR, GenerateFileName(id, createdUtc, shippingMarkImageHash), imageFile);
                    }
                }
                else
                {
                    imagePath = await _azureImageService.UploadImage(IMG_DIR, GenerateFileName(id, createdUtc, shippingMarkImageHash), imageFile);
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(imagePath))
                {
                    await _azureImageService.RemoveImage(IMG_DIR, imagePath);
                    imagePath = null;
                }
            }

            return imagePath;
        }

        public override async Task<string> Create(GarmentPackingListViewModel viewModel)
        {
            viewModel.Status = GarmentPackingListStatusEnum.DRAFT.ToString();

            GarmentPackingListModel garmentPackingListModel = MapToModel(viewModel);

            await _packingListRepository.InsertAsync(garmentPackingListModel);

            garmentPackingListModel.SetShippingMarkImagePath(await UploadImage(viewModel.ShippingMarkImageFile, garmentPackingListModel.Id, garmentPackingListModel.ShippingMarkImagePath, garmentPackingListModel.CreatedUtc), _identityProvider.Username, UserAgent);
            garmentPackingListModel.SetSideMarkImagePath(await UploadImage(viewModel.SideMarkImageFile, garmentPackingListModel.Id, garmentPackingListModel.SideMarkImagePath, garmentPackingListModel.CreatedUtc), _identityProvider.Username, UserAgent);
            garmentPackingListModel.SetRemarkImagePath(await UploadImage(viewModel.RemarkImageFile, garmentPackingListModel.Id, garmentPackingListModel.RemarkImagePath, garmentPackingListModel.CreatedUtc), _identityProvider.Username, UserAgent);
            await _packingListRepository.SaveChanges();

            return garmentPackingListModel.InvoiceNo;
        }

        public override async Task<int> Update(int id, GarmentPackingListViewModel viewModel)
        {
            viewModel.ShippingMarkImagePath = await UploadImage(viewModel.ShippingMarkImageFile, viewModel.Id, viewModel.ShippingMarkImagePath, viewModel.CreatedUtc);
            viewModel.SideMarkImagePath = await UploadImage(viewModel.SideMarkImageFile, viewModel.Id, viewModel.SideMarkImagePath, viewModel.CreatedUtc);
            viewModel.RemarkImagePath = await UploadImage(viewModel.RemarkImageFile, viewModel.Id, viewModel.RemarkImagePath, viewModel.CreatedUtc);

            return await base.Update(id, viewModel);
        }

        public async Task PostBooking(int id)
        {
            var status = GarmentPackingListStatusEnum.DRAFT_POSTED;
            var model = _packingListRepository.Query.Single(m => m.Id == id);
            model.SetStatus(status, _identityProvider.Username, UserAgent);
            model.StatusActivities.Add(new GarmentPackingListStatusActivityModel(_identityProvider.Username, UserAgent, status));

            await _packingListRepository.SaveChanges();
        }

        public async Task UnpostBooking(int id)
        {
            var status = GarmentPackingListStatusEnum.DRAFT;
            var model = _packingListRepository.Query.Single(m => m.Id == id);
            model.SetStatus(status, _identityProvider.Username, UserAgent);
            model.StatusActivities.Add(new GarmentPackingListStatusActivityModel(_identityProvider.Username, UserAgent, status));

            await _packingListRepository.SaveChanges();
        }
    }
}
