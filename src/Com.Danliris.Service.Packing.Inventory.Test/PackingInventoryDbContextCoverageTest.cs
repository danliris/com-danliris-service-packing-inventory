using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test
{
    public class PackingInventoryDbContextCoverageTest
    {
        private string _entity = "PackingInventoryDbContext";

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected string GetCurrentMethod()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);

            return string.Concat(sf.GetMethod().Name, "_", _entity);
        }

        protected PackingInventoryDbContext DbContext(string testName)
        {
            DbContextOptionsBuilder<PackingInventoryDbContext> optionsBuilder = new DbContextOptionsBuilder<PackingInventoryDbContext>();
            optionsBuilder
                .UseInMemoryDatabase(testName)
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));

            PackingInventoryDbContext dbContext = new PackingInventoryDbContext(optionsBuilder.Options);

            return dbContext;
        }

        [Fact]
        public void CheckTableCollections()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            Assert.Empty(dbContext.InventoryDocumentPackingItems);
            Assert.Empty(dbContext.InventoryDocumentPackings);
            Assert.Empty(dbContext.InventoryDocumentSKUItems);
            Assert.Empty(dbContext.InventoryDocumentSKUs);
            Assert.Empty(dbContext.ProductSKUs);
            Assert.Empty(dbContext.ProductPackings);
            Assert.Empty(dbContext.NewFabricQualityControls);
            Assert.Empty(dbContext.NewFabricGradeTests);
            Assert.Empty(dbContext.NewCriterias);
            Assert.Empty(dbContext.DyeingPrintingAreaInputProductionOrders);
            Assert.Empty(dbContext.DyeingPrintingAreaInputs);
            Assert.Empty(dbContext.DyeingPrintingAreaMovements);
            Assert.Empty(dbContext.DyeingPrintingAreaOutputProductionOrders);
            Assert.Empty(dbContext.DyeingPrintingAreaOutputs);
            Assert.Empty(dbContext.DyeingPrintingAreaSummaries);
            Assert.Empty(dbContext.PackagingStock);
            //Assert.Empty(dbContext.DyeingPrintingAreaOutputAvalItems);
            Assert.Empty(dbContext.IPCategories);
            Assert.Empty(dbContext.IPPackings);
            Assert.Empty(dbContext.IPProducts);
            Assert.Empty(dbContext.IPUnitOfMeasurements);

            Assert.Empty(dbContext.GarmentPackingLists);
            Assert.Empty(dbContext.GarmentPackingListItems);
            Assert.Empty(dbContext.GarmentPackingListDetails);
            Assert.Empty(dbContext.GarmentPackingListDetailSizes);
            Assert.Empty(dbContext.GarmentPackingListMeasurements);

            Assert.Empty(dbContext.GarmentShippingCoverLetters);
            Assert.Empty(dbContext.GarmentShippingInstructions);

			Assert.Empty(dbContext.GarmentShippingInvoices);
			Assert.Empty(dbContext.GarmentShippingInvoiceItems);
			Assert.Empty(dbContext.GarmentShippingInvoiceAdjustments);

            Assert.Empty(dbContext.MaterialDeliveryNote);
            Assert.Empty(dbContext.Items);
            Assert.Empty(dbContext.MaterialDeliveryNoteWeaving);
            Assert.Empty(dbContext.ItemsMaterialDeliveryNoteWeaving);
            Assert.Empty(dbContext.IPWeftTypes);
            Assert.Empty(dbContext.IPWarpTypes);
            Assert.Empty(dbContext.IPMaterialConstructions);
            Assert.Empty(dbContext.IPGrades);
            Assert.Empty(dbContext.IPWovenType);
            Assert.Empty(dbContext.IPYarnType);
            Assert.Empty(dbContext.IPProcessType);
            Assert.Empty(dbContext.IPWidthType);

            Assert.Empty(dbContext.GarmentShippingLetterOfCredits);
            Assert.Empty(dbContext.GarmentShippingNotes);
            Assert.Empty(dbContext.GarmentShippingNoteItems);

            Assert.Empty(dbContext.GarmentShippingAmendLetterOfCredits);
            Assert.Empty(dbContext.GarmentShippingExportSalesDOs);
            Assert.Empty(dbContext.GarmentShippingExportSalesDOItems);
            Assert.Empty(dbContext.GarmentShippingLocalSalesNotes);
            Assert.Empty(dbContext.GarmentShippingLocalSalesNoteItems);

            Assert.Empty(dbContext.GarmentShippingLocalCoverLetters);
            Assert.Empty(dbContext.GarmentShippingLocalSalesDOs);
            Assert.Empty(dbContext.GarmentShippingLocalSalesDOItems);

            Assert.Empty(dbContext.GarmentShippingLocalPriceCorrectionNotes);
            Assert.Empty(dbContext.GarmentShippingLocalPriceCorrectionNoteItems);
        }
    }
}
