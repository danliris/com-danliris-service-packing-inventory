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

        public override Task<string> Create(GarmentPackingListViewModel viewModel)
        {
            viewModel.ShippingMarkImagePath = "";
            viewModel.SideMarkImagePath = "";
            viewModel.RemarkImagePath = "";
            viewModel.Status = GarmentPackingListStatusEnum.DRAFT.ToString();
            return base.Create(viewModel);
        }

        public async Task PostBooking(int id)
        {
            var status = GarmentPackingListStatusEnum.DRAFT_POSTED;
            var model = _packingListRepository.Query.Single(m => m.Id == id);
            model.SetStatus(status, _identityProvider.Username, UserAgent);
            model.StatusActivities.Add(new GarmentPackingListStatusActivityModel(_identityProvider.Username, UserAgent, status));

            await _packingListRepository.SaveChanges();
        }
    }
}
