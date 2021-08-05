using Com.Moonlay.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Product
{
    public class CategoryModel : StandardEntity
    {
        public CategoryModel()
        {

        }

        public CategoryModel(string name, string code)
        {
            Name = name;
            Code = code;
        }

        [MaxLength(64)]
        public string Name { get; private set; }
        [MaxLength(64)]
        public string Code { get; private set; }

        public void SetName(string name)
        {
            Name = name;
        }
    }
}
