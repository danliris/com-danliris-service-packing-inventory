using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.Master.ProductRFID
{
    public interface IProductRFIDService
    {
        Task<ProductRFIDIndex> GetIndex(IndexQueryParam filter);
        Task<ProductRFIDDto> GetById(int id);
    }
}
