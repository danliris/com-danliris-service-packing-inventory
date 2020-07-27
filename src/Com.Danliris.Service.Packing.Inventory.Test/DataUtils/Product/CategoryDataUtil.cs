using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.Product
{
     public  class CategoryDataUtil : BaseDataUtil<CategoryRepository, CategoryModel>
    {
        public CategoryDataUtil(CategoryRepository repository) : base(repository)
        {
        }

        public override CategoryModel GetModel()
        {
            return new CategoryModel("name", "code");
        }
    }
}
