using Com.Danliris.Service.Packing.Inventory.Data.Models.Inventory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.InventorySKU
{
    public interface IInventorySKUService
    {
        Task<int> AddDocument(FormDto form);
        void AddMovement(ProductSKUInventoryMovementModel model);
        DocumentDto GetDocumentById(int id);
        DocumentIndexDto GetDocumentIndex(IndexQueryParam query);
        MovementIndexDto GetMovementIndex(int storageId, int productSKUId, string type, DateTime startDate, DateTime endDate);
        SummaryIndexDto GetSummaryIndex(int storageId, int productSKUId);
    }
}
