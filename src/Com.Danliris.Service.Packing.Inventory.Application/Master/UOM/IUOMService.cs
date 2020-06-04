using Com.Danliris.Service.Packing.Inventory.Application.DTOs;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.Master.UOM
{
    public interface IUOMService
    {
        Task<int> Create(FormDto form);
        Task<int> Update(int id, FormDto form);
        Task<int> Delete(int id);
        Task<UnitOfMeasurementDto> GetById(int id);
        Task<UOMIndex> GetIndex(IndexQueryParam filter);
    }
}
