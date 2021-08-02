using Com.Moonlay.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Product
{
    public class ProductSKUModel : StandardEntity
    {
        public ProductSKUModel()
        {

        }

        public ProductSKUModel(
            string code,
            string name,
            int uomId,
            int categoryId,
            string description
            )
        {
            Code = code;
            Name = name;
            UOMId = uomId;
            CategoryId = categoryId;
            Description = description;
        }

        [MaxLength(64)]
        public string Code { get; private set; }
        [MaxLength(512)]
        public string Name { get; private set; }
        public int UOMId { get; private set; }
        public int CategoryId { get; private set; }
        public string Description { get; private set; }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetUOM(int uomId)
        {
            UOMId = uomId;
        }

        public void SetCategory(int categoryId)
        {
            CategoryId = categoryId;
        }

        public void SetDescription(string description)
        {
            Description = description;
        }

        public void SetCode(string code)
        {
            Code = code;
        }
    }
}
