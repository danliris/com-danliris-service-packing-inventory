using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList
{
    public class GarmentPackingListDraftService : GarmentPackingListService, IGarmentPackingListDraftService
    {
        private const string UserAgent = "GarmentPackingListDraftService";

        public GarmentPackingListDraftService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
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

            GarmentPackingListModel model = MapToModel(viewModel);

            var modelToUpdate = _packingListRepository.Query.FirstOrDefault(s => s.Id == id);

            modelToUpdate.SetDate(model.Date, _identityProvider.Username, UserAgent);
            modelToUpdate.SetDestination(model.Destination, _identityProvider.Username, UserAgent);

            modelToUpdate.SetGrossWeight(model.GrossWeight, _identityProvider.Username, UserAgent);
            modelToUpdate.SetNettWeight(model.NettWeight, _identityProvider.Username, UserAgent);
            modelToUpdate.SetTotalCartons(model.TotalCartons, _identityProvider.Username, UserAgent);
            modelToUpdate.SetSayUnit(model.SayUnit, _identityProvider.Username, UserAgent);

            modelToUpdate.SetShippingMark(model.ShippingMark, _identityProvider.Username, UserAgent);
            modelToUpdate.SetSideMark(model.SideMark, _identityProvider.Username, UserAgent);
            modelToUpdate.SetRemark(model.Remark, _identityProvider.Username, UserAgent);

            modelToUpdate.SetShippingMarkImagePath(model.ShippingMarkImagePath, _identityProvider.Username, UserAgent);
            modelToUpdate.SetSideMarkImagePath(model.SideMarkImagePath, _identityProvider.Username, UserAgent);
            modelToUpdate.SetRemarkImagePath(model.RemarkImagePath, _identityProvider.Username, UserAgent);

            modelToUpdate.SetShippingStaff(model.ShippingStaffId, model.ShippingStaffName, _identityProvider.Username, UserAgent);

            return await _packingListRepository.SaveChanges();
        }

        public override async Task<MemoryStreamResult> ReadPdfById(int id)
        {
            var data = await _packingListRepository.ReadByIdAsync(id);

            var PdfTemplate = new GarmentPackingListDraftPdfTemplate(_identityProvider);

            var viewModel = MapToViewModel(data);
            viewModel.ShippingMarkImageFile = await _azureImageService.DownloadImage(IMG_DIR, viewModel.ShippingMarkImagePath);
            viewModel.SideMarkImageFile = await _azureImageService.DownloadImage(IMG_DIR, viewModel.SideMarkImagePath);
            viewModel.RemarkImageFile = await _azureImageService.DownloadImage(IMG_DIR, viewModel.RemarkImagePath);

            var stream = PdfTemplate.GeneratePdfTemplate(viewModel);

            return new MemoryStreamResult(stream, "Draft Packing List " + data.InvoiceNo + ".pdf");
        }
    }
}
