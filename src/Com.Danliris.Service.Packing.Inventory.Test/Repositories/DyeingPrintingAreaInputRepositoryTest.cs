using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories
{
    public class DyeingPrintingAreaInputRepositoryTest
        : BaseRepositoryTest<PackingInventoryDbContext, DyeingPrintingAreaInputRepository, DyeingPrintingAreaInputModel, DyeingPrintingAreaInputDataUtil>
    {
        private const string ENTITY = "DyeingPrintingAreaInput";
        public DyeingPrintingAreaInputRepositoryTest() : base(ENTITY)
        {
        }

        [Fact]
        public async Task Should_Success_GetDbSet()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var repo = new DyeingPrintingAreaInputRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var data = await DataUtil(repo, dbContext).GetTestData();
            var result = repo.GetDbSet();

            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task Should_Success_ReadAllIgnoreQueryFilter()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var repo = new DyeingPrintingAreaInputRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var data = await DataUtil(repo, dbContext).GetTestData();
            var result = repo.ReadAllIgnoreQueryFilter();

            Assert.NotEmpty(result);
        }

        [Fact]
        public virtual async Task Should_Success_Update_2()
        {
            string testName = GetCurrentMethod() + "Update2";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new DyeingPrintingAreaInputRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var repo2 = new DyeingPrintingAreaInputRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var emptyData = DataUtil(repo, dbContext).GetEmptyModel();
            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();
            var model = DataUtil(repo, dbContext).GetModel();

            int index = 0;
            foreach (var item in model.DyeingPrintingAreaInputProductionOrders)
            {
                var spp = data.DyeingPrintingAreaInputProductionOrders.ElementAtOrDefault(index++);
                item.DyeingPrintingAreaInputId = data.Id;
                item.Id = spp.Id;
            }

            var result = await repo2.UpdateAsync(data.Id, model);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_Update_3()
        {
            string testName = GetCurrentMethod() + "Update3";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new DyeingPrintingAreaInputRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var repo2 = new DyeingPrintingAreaInputRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var emptyData = DataUtil(repo, dbContext).GetEmptyuAvalTransformModel();
            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();
            var model = DataUtil(repo, dbContext).GetAvalTransformModel();

            int index = 0;
            foreach (var item in model.DyeingPrintingAreaInputProductionOrders)
            {
                var spp = data.DyeingPrintingAreaInputProductionOrders.ElementAtOrDefault(index++);
                item.DyeingPrintingAreaInputId = data.Id;
                if (spp != null)
                {
                    item.Id = spp.Id;
                }
            }

            var result = await repo2.UpdateAsync(data.Id, model);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_DeleteIMArea()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new DyeingPrintingAreaInputRepository(dbContext, serviceProvider);
            var data = await DataUtil(repo, dbContext).GetTestData();
            var result = await repo.DeleteIMArea(data);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_UpdateIMArea()
        {
            string testName = GetCurrentMethod() + "UpdateIMArea";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new DyeingPrintingAreaInputRepository(dbContext, serviceProvider);
            var repo2 = new DyeingPrintingAreaInputRepository(dbContext, serviceProvider);
            var emptyData = DataUtil(repo, dbContext).GetEmptyModel();
            foreach (var item in emptyData.DyeingPrintingAreaInputProductionOrders)
            {
                item.SetHasOutputDocument(false, "", "");
            }
            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();
            var dbModel = await repo.ReadByIdAsync(data.Id);
            var model = DataUtil(repo, dbContext).GetModel();
            var result = await repo2.UpdateIMArea(data.Id, model, dbModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_UpdateIMArea_2()
        {
            string testName = GetCurrentMethod() + "UpdateIMArea_2";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new DyeingPrintingAreaInputRepository(dbContext, serviceProvider);
            var repo2 = new DyeingPrintingAreaInputRepository(dbContext, serviceProvider);
            var emptyData = DataUtil(repo, dbContext).GetEmptyModel();
            foreach (var item in emptyData.DyeingPrintingAreaInputProductionOrders)
            {
                item.SetHasOutputDocument(false, "", "");
            }
            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();
            var dbModel = await repo.ReadByIdAsync(data.Id);
            var model = DataUtil(repo, dbContext).GetModel();

            int index = 0;
            foreach (var item in model.DyeingPrintingAreaInputProductionOrders)
            {
                var spp = dbModel.DyeingPrintingAreaInputProductionOrders.ElementAtOrDefault(index++);
                item.DyeingPrintingAreaInputId = data.Id;
                item.Id = spp.Id;
            }

            var result = await repo2.UpdateIMArea(data.Id, model, dbModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_DeleteTransitArea()
        {
            string testName = GetCurrentMethod() + "DeleteTransitArea";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaOutputProductionOrderRepository> outputSPPMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            outputSPPMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<IEnumerable<int>>(), It.IsAny<bool>(), It.IsAny<string>()))
                .ReturnsAsync(1);
            outputSPPMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new DyeingPrintingAreaOutputProductionOrderModel());

            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            inputSPPMock.Setup(s => s.UpdateFromNextAreaInputAsync(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<decimal>()))
                .ReturnsAsync(1);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputSPPMock.Object);

            var repo = new DyeingPrintingAreaInputRepository(dbContext, serviceProvider.Object);
            var data = await DataUtil(repo, dbContext).GetTestData();
            var result = await repo.DeleteTransitArea(data);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_UpdateTransitArea()
        {
            string testName = GetCurrentMethod() + "UpdateTransitArea";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaOutputProductionOrderRepository> outputSPPMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            outputSPPMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<IEnumerable<int>>(), It.IsAny<bool>()))
                .ReturnsAsync(1);
            outputSPPMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new DyeingPrintingAreaOutputProductionOrderModel());

            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            inputSPPMock.Setup(s => s.UpdateFromNextAreaInputAsync(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<decimal>()))
                .ReturnsAsync(1);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputSPPMock.Object);

            var repo = new DyeingPrintingAreaInputRepository(dbContext, serviceProvider.Object);
            var repo2 = new DyeingPrintingAreaInputRepository(dbContext, serviceProvider.Object);
            var emptyData = DataUtil(repo, dbContext).GetEmptyModel();
            foreach (var item in emptyData.DyeingPrintingAreaInputProductionOrders)
            {
                item.SetHasOutputDocument(false, "", "");
            }
            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();
            var dbModel = await repo.ReadByIdAsync(data.Id);
            var model = DataUtil(repo, dbContext).GetModel();
            model.DyeingPrintingAreaInputProductionOrders.Take(model.DyeingPrintingAreaInputProductionOrders.Count - 1);
            var result = await repo2.UpdateTransitArea(data.Id, model, dbModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_DeleteShippingArea()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaOutputProductionOrderRepository> outputSPPMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            outputSPPMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<IEnumerable<int>>(), It.IsAny<bool>()))
                .ReturnsAsync(1);
            outputSPPMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new DyeingPrintingAreaOutputProductionOrderModel());

            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            inputSPPMock.Setup(s => s.UpdateFromNextAreaInputAsync(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<decimal>()))
                .ReturnsAsync(1);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputSPPMock.Object);

            var repo = new DyeingPrintingAreaInputRepository(dbContext, serviceProvider.Object);
            var data = await DataUtil(repo, dbContext).GetTestData();
            var result = await repo.DeleteShippingArea(data);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_UpdateShippingArea()
        {
            string testName = GetCurrentMethod() + "UpdateShippingArea";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaOutputProductionOrderRepository> outputSPPMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            outputSPPMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<IEnumerable<int>>(), It.IsAny<bool>()))
                .ReturnsAsync(1);
            outputSPPMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new DyeingPrintingAreaOutputProductionOrderModel());

            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            inputSPPMock.Setup(s => s.UpdateFromNextAreaInputAsync(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<decimal>()))
                .ReturnsAsync(1);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputSPPMock.Object);

            var repo = new DyeingPrintingAreaInputRepository(dbContext, serviceProvider.Object);
            var repo2 = new DyeingPrintingAreaInputRepository(dbContext, serviceProvider.Object);
            var emptyData = DataUtil(repo, dbContext).GetEmptyModel();
            foreach (var item in emptyData.DyeingPrintingAreaInputProductionOrders)
            {
                item.SetHasOutputDocument(false, "", "");
            }
            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();
            var dbModel = await repo.ReadByIdAsync(data.Id);
            var model = DataUtil(repo, dbContext).GetModel();
            model.DyeingPrintingAreaInputProductionOrders.Take(model.DyeingPrintingAreaInputProductionOrders.Count - 1);
            var result = await repo2.UpdateShippingArea(data.Id, model, dbModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_DeleteAvalTransformationArea()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaOutputProductionOrderRepository> outputSPPMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            inputSPPMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputSPPMock.Object);

            var repo = new DyeingPrintingAreaInputRepository(dbContext, serviceProvider.Object);
            var data = await DataUtil(repo, dbContext).GetTestData();
            var result = await repo.DeleteAvalTransformationArea(data);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_UpdateAvalTransforArea()
        {
            string testName = GetCurrentMethod() + "UpdateAvalTransforArea";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaOutputProductionOrderRepository> outputSPPMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            outputSPPMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<IEnumerable<int>>(), It.IsAny<bool>()))
                .ReturnsAsync(1);
            outputSPPMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new DyeingPrintingAreaOutputProductionOrderModel());

            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            inputSPPMock.Setup(s => s.UpdateFromNextAreaInputAsync(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<decimal>()))
                .ReturnsAsync(1);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputSPPMock.Object);

            var repo = new DyeingPrintingAreaInputRepository(dbContext, serviceProvider.Object);
            var repo2 = new DyeingPrintingAreaInputRepository(dbContext, serviceProvider.Object);
            var emptyData = DataUtil(repo, dbContext).GetEmptyuAvalTransformModel();
            foreach (var item in emptyData.DyeingPrintingAreaInputProductionOrders)
            {
                item.SetHasOutputDocument(false, "", "");
            }
            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();
            var dbModel = await repo.ReadByIdAsync(data.Id);
            var model = DataUtil(repo, dbContext).GetAvalTransformModel();
            model.DyeingPrintingAreaInputProductionOrders.Take(model.DyeingPrintingAreaInputProductionOrders.Count - 1);
            var result = await repo2.UpdateAvalTransformationArea(data.Id, model, dbModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_UpdateAvalTransforAreaChangeItem()
        {
            string testName = GetCurrentMethod() + "UpdateAvalTransforAreaChangeItem";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaOutputProductionOrderRepository> outputSPPMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            outputSPPMock.Setup(s => s.UpdateFromInputAsync(It.IsAny<IEnumerable<int>>(), It.IsAny<bool>()))
                .ReturnsAsync(1);
            outputSPPMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new DyeingPrintingAreaOutputProductionOrderModel());

            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            inputSPPMock.Setup(s => s.UpdateFromNextAreaInputAsync(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<decimal>()))
                .ReturnsAsync(1);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputSPPMock.Object);

            var repo = new DyeingPrintingAreaInputRepository(dbContext, serviceProvider.Object);
            var repo2 = new DyeingPrintingAreaInputRepository(dbContext, serviceProvider.Object);
            var emptyData = DataUtil(repo, dbContext).GetEmptyuAvalTransformModel();
            foreach (var item in emptyData.DyeingPrintingAreaInputProductionOrders)
            {
                item.SetHasOutputDocument(false, "", "");
            }
            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();
            var dbModel = await repo.ReadByIdAsync(data.Id);
            var model = DataUtil(repo, dbContext).GetAvalTransformModel();
            foreach (var item in model.DyeingPrintingAreaInputProductionOrders)
            {
                item.Id = dbModel.DyeingPrintingAreaInputProductionOrders.FirstOrDefault().Id;
            }
            var result = await repo2.UpdateAvalTransformationArea(data.Id, model, dbModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_UpdateHeaderAvalTransforArea()
        {
            string testName = GetCurrentMethod() + "_UpdateHeaderAvalTransforArea";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaOutputProductionOrderRepository> outputSPPMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputSPPMock.Object);

            var repo = new DyeingPrintingAreaInputRepository(dbContext, serviceProvider.Object);
            var repo2 = new DyeingPrintingAreaInputRepository(dbContext, serviceProvider.Object);
            var emptyData = DataUtil(repo, dbContext).GetAvalTransformModel();
            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();

            var result = await repo2.UpdateHeaderAvalTransform(emptyData, 10, 10);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_UpdateAvalTrasnfromFromOut()
        {
            string testName = GetCurrentMethod() + "UpdateAvalTrasnfromFromOut";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaOutputProductionOrderRepository> outputSPPMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputSPPMock.Object);

            var repo = new DyeingPrintingAreaInputRepository(dbContext, serviceProvider.Object);
            var repo2 = new DyeingPrintingAreaInputRepository(dbContext, serviceProvider.Object);
            var emptyData = DataUtil(repo, dbContext).GetAvalTransformModel();
            await repo.InsertAsync(emptyData);
            var result = await repo2.UpdateAvalTransformationFromOut(emptyData.AvalType, 5, 5);

            Assert.NotEqual(0, result.Item1);
        }

        [Fact]
        public virtual async Task Should_Success_RestoreAvalTransformation()
        {
            string testName = GetCurrentMethod() + "RestoreAvalTransformation";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext);

            Mock<IDyeingPrintingAreaOutputProductionOrderRepository> outputSPPMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            Mock<IDyeingPrintingAreaInputProductionOrderRepository> inputSPPMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inputSPPMock.Object);

            serviceProvider.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(outputSPPMock.Object);

            var repo = new DyeingPrintingAreaInputRepository(dbContext, serviceProvider.Object);
            var repo2 = new DyeingPrintingAreaInputRepository(dbContext, serviceProvider.Object);
            var emptyData = DataUtil(repo, dbContext).GetAvalTransformModel();
            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();

            var avalData = new AvalData()
            {
                Id = data.Id,
                AvalQuantity = 5,
                AvalQuantityKg = 5
            };

            var result = await repo2.RestoreAvalTransformation(new List<AvalData>() { avalData });

            Assert.NotEqual(0, result);
        }
    }
}
