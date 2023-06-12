using Com.Danliris.Service.Packing.Inventory.Application;
using Com.Danliris.Service.Packing.Inventory.Application.DTOs;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.helper
{
    public class BarcodeInfoTest
    {
        [Fact]
        public void shouldSuccessInstantiate()
        {

            ProductSKUModel product = new ProductSKUModel("Code","Name",1,1,"Description", true)
            {
                Id =1,
                
            };

            UnitOfMeasurementModel uom = new UnitOfMeasurementModel()
            {
                Id = 1
            };

            CategoryModel category = new CategoryModel("Name","Code")
            {
                
            };

            ProductSKUDto productSKUDto = new ProductSKUDto(product, uom, category)
            {
                
                Category = new CategoryDto(new CategoryModel("Name","Code"))

            };
           

            ProductPackingModel productPackingModel = new ProductPackingModel(1,1,1,"Code","Name","description")
            {
                
            };

            ProductSKUModel productSKUModel = new ProductSKUModel("Code","Name",1,1,"Description", true)
            {

            };

            UnitOfMeasurementModel uomModel = new UnitOfMeasurementModel("Unit")
            {

            };
            UnitOfMeasurementModel skuUOM = new UnitOfMeasurementModel();
            CategoryModel skuCategory = new CategoryModel();
            ProductPackingDto productPackingDto = new ProductPackingDto(productPackingModel, productSKUModel, uomModel, skuUOM, skuCategory);
            

            BarcodeInfo barcode = new BarcodeInfo(productSKUDto, productPackingDto);
            Assert.NotNull(barcode);
            Assert.Equal("Code", barcode.SKUCode);
            Assert.Equal("Name", barcode.SKUName);
            Assert.Equal("Code", barcode.PackingCode);
            Assert.Equal(1, barcode.PackingSize);
            Assert.Equal("Unit", barcode.PackingType);
        }
    }
}
