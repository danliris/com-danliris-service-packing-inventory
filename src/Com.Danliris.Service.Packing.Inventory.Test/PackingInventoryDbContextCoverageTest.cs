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
            Assert.Empty(dbContext.DyeingPrintingAreaMovements);
            Assert.Empty(dbContext.DyeingPrintingAreaMovementHistories);
            Assert.Empty(dbContext.NewFabricQualityControls);
            Assert.Empty(dbContext.NewFabricGradeTests);
            Assert.Empty(dbContext.NewCriterias);
        }
    }
}
