using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Microsoft.EntityFrameworkCore;
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

            foreach (var item in garmentPackingListModel.Items)
            {
                foreach (var detail in item.Details)
                {
                    detail.SetNetNetWeight(detail.NetNetWeight == 0 ? 0.9 * detail.NetWeight : detail.NetNetWeight, _identityProvider.Username, UserAgent);
                }
            }
            garmentPackingListModel.StatusActivities.Add(new GarmentPackingListStatusActivityModel(_identityProvider.Username, UserAgent, garmentPackingListModel.Status));

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
            GarmentPackingListModel modelToUpdate;

            if (model.Status == GarmentPackingListStatusEnum.DRAFT_APPROVED_SHIPPING)
            {
                modelToUpdate = _packingListRepository.Query
                    .Include(i => i.Items)
                        .ThenInclude(i => i.Details)
                    .Include(i => i.Measurements)
                    .FirstOrDefault(s => s.Id == id);

                foreach (var itemToUpdate in modelToUpdate.Items)
                {
                    var item = model.Items.First(i => i.Id == itemToUpdate.Id);
                    foreach (var detailToUpdate in itemToUpdate.Details)
                    {
                        var detail = item.Details.Where(d => d.Id == detailToUpdate.Id).First();
                        detailToUpdate.SetCarton1(detail.Carton1, _identityProvider.Username, UserAgent);
                        detailToUpdate.SetCarton2(detail.Carton2, _identityProvider.Username, UserAgent);
                        detailToUpdate.SetCartonQuantity(detail.CartonQuantity, _identityProvider.Username, UserAgent);
                        detailToUpdate.SetTotalQuantity(detail.TotalQuantity, _identityProvider.Username, UserAgent);
                    }
                }

                foreach (var measurementToUpdate in modelToUpdate.Measurements)
                {
                    var measurement = model.Measurements.First(m => m.Id == measurementToUpdate.Id);
                    measurementToUpdate.SetCartonsQuantity(measurement.CartonsQuantity, _identityProvider.Username, UserAgent);
                }
            }
            else
            {
                modelToUpdate = _packingListRepository.Query.FirstOrDefault(s => s.Id == id);
            }

            modelToUpdate.SetDate(model.Date, _identityProvider.Username, UserAgent);
            modelToUpdate.SetDestination(model.Destination, _identityProvider.Username, UserAgent);

            modelToUpdate.SetGrossWeight(model.GrossWeight, _identityProvider.Username, UserAgent);
            modelToUpdate.SetNettWeight(model.NettWeight, _identityProvider.Username, UserAgent);
            modelToUpdate.SetNetNetWeight(model.NetNetWeight, _identityProvider.Username, UserAgent);
            modelToUpdate.SetTotalCartons(model.TotalCartons, _identityProvider.Username, UserAgent);
            modelToUpdate.SetSayUnit(model.SayUnit, _identityProvider.Username, UserAgent);
            modelToUpdate.SetOtherCommodity(model.OtherCommodity, _identityProvider.Username, UserAgent);

            modelToUpdate.SetShippingMark(model.ShippingMark, _identityProvider.Username, UserAgent);
            modelToUpdate.SetSideMark(model.SideMark, _identityProvider.Username, UserAgent);
            modelToUpdate.SetRemark(model.Remark, _identityProvider.Username, UserAgent);

            modelToUpdate.SetShippingMarkImagePath(model.ShippingMarkImagePath, _identityProvider.Username, UserAgent);
            modelToUpdate.SetSideMarkImagePath(model.SideMarkImagePath, _identityProvider.Username, UserAgent);
            modelToUpdate.SetRemarkImagePath(model.RemarkImagePath, _identityProvider.Username, UserAgent);

            modelToUpdate.SetShippingStaff(model.ShippingStaffId, model.ShippingStaffName, _identityProvider.Username, UserAgent);

            modelToUpdate.SetDescription(model.Description, _identityProvider.Username, UserAgent);

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

        public override async Task<MemoryStreamResult> ReadExcelById(int id)
        {
            var data = await _packingListRepository.ReadByIdAsync(id);

            var ExcelTemplate = new GarmentPackingListDraftExcelTemplate(_identityProvider);

            var viewModel = MapToViewModel(data);
            viewModel.ShippingMarkImageFile = await _azureImageService.DownloadImage(IMG_DIR, viewModel.ShippingMarkImagePath);
            viewModel.SideMarkImageFile = await _azureImageService.DownloadImage(IMG_DIR, viewModel.SideMarkImagePath);
            viewModel.RemarkImageFile = await _azureImageService.DownloadImage(IMG_DIR, viewModel.RemarkImagePath);

            var stream = ExcelTemplate.GenerateExcelTemplate(viewModel);

            return new MemoryStreamResult(stream, "Draft Packing List " + data.InvoiceNo + ".xls");
        }
    }
}
