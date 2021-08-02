using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Inventory;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;
using iTextSharp.text;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using OfficeOpenXml.FormulaParsing.ExcelUtilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Com.Danliris.Service.Packing.Inventory.Application.InventorySKU
{
    public class DocumentDto
    {
        public DocumentDto(ProductSKUInventoryDocumentModel document, List<ProductSKUInventoryMovementModel> items, List<ProductSKUModel> products, List<UnitOfMeasurementModel> uoms, List<CategoryModel> categories)
        {
            DocumentNo = document.DocumentNo;
            Date = document.Date;
            ReferenceNo = document.ReferenceNo;
            ReferenceType = document.ReferenceType;
            Type = document.Type;
            Storage = new Storage()
            {
                Code = document.StorageCode,
                Id = document.StorageId,
                Name = document.StorageName
            };
            Remark = document.Remark;
            Items = items.Select(item =>
            {
                var product = products.FirstOrDefault(element => element.Id == item.ProductSKUId);
                var uom = uoms.FirstOrDefault(element => element.Id == item.UOMId);
                var category = categories.FirstOrDefault(element => element.Id == product.CategoryId);
                return new DocumentItemDto(item, product, uom, category);
            }).ToList();
        }

        public string DocumentNo { get; }
        public DateTimeOffset Date { get; }
        public string ReferenceNo { get; }
        public string ReferenceType { get; }
        public string Type { get; }
        public Storage Storage { get; }
        public string Remark { get; }
        public List<DocumentItemDto> Items { get; }
    }
}