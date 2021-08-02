using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;
using System;

namespace Com.Danliris.Service.Packing.Inventory.Application.Master.Category
{
    public class CategoryIndexInfo
    {
        public CategoryIndexInfo()
        {
        }

        public CategoryIndexInfo(CategoryModel entity)
        {
            LastModifiedUtc = entity.LastModifiedUtc;
            Name = entity.Name;
            Code = entity.Code;
            Id = entity.Id;
        }

        public DateTime LastModifiedUtc { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Id { get; set; }
    }
}