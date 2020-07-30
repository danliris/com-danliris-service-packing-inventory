using Com.Danliris.Service.Packing.Inventory.Application.GoodsWarehouse;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GoodsWarehouse
{
    public class GoodsWarehouseDocumentsViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            GoodsWarehouseDocumentsViewModel viewModel = new GoodsWarehouseDocumentsViewModel()
            {
                Activities = "Activities",
                Balance = "Balance",
                Color = "Color",
                Construction = "Construction",
                Date =DateTimeOffset.Now,
                Grade="A",
                Group = "Group",
                Motif = "Motif",
                Mutation = "Mutation",
                NoOrder = "NoOrder",
                Packaging = "Packaging",
                Qty ="Qty",
                QtyPacking = "QtyPacking",
                Satuan="kg"
            };

            Assert.Equal("Color", viewModel.Color);
            Assert.Equal("Balance", viewModel.Balance);
            Assert.Equal("Activities", viewModel.Activities);
            Assert.Equal("Construction", viewModel.Construction);
            Assert.True(DateTimeOffset.MinValue < viewModel.Date);
            Assert.Equal("A", viewModel.Grade);
            Assert.Equal("Group", viewModel.Group);
            Assert.Equal("Motif", viewModel.Motif);
            Assert.Equal("Mutation", viewModel.Mutation);
            Assert.Equal("NoOrder", viewModel.NoOrder);
            Assert.Equal("Packaging", viewModel.Packaging);
            Assert.Equal("Qty", viewModel.Qty);
            Assert.Equal("QtyPacking", viewModel.QtyPacking);
            Assert.Equal("kg", viewModel.Satuan);
        }
        }
}
