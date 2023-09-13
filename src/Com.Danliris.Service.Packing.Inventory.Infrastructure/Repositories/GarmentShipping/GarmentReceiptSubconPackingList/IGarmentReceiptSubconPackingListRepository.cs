using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentReceiptSubconPackingList;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentReceiptSubconPackingList
{
    public interface IGarmentReceiptSubconPackingListRepository : IRepository<GarmentReceiptSubconPackingListModel>
    {
        Task<int> SaveChanges();

    }
}
