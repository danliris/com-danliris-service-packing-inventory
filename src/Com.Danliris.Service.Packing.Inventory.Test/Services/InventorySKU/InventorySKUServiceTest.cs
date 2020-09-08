using Com.Danliris.Service.Packing.Inventory.Application;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.InventorySKU;
using Com.Danliris.Service.Packing.Inventory.Application.QueueService;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Inventory;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.InventorySKU
{
    public class InventorySKUServiceTest
    {
        public InventorySKUService GetService(IServiceProvider serviceProvider)
        {
            return new InventorySKUService(serviceProvider);
        }

        public Mock<IServiceProvider> GetServiceProvider(IUnitOfWork _unitOfWork,
                                                       IAzureServiceBusSender<ProductSKUInventoryMovementModel> _queueService)
        {
            var spMock = new Mock<IServiceProvider>();

            spMock.Setup(s => s.GetService(typeof(IUnitOfWork)))
                .Returns(_unitOfWork);
            spMock.Setup(s => s.GetService(typeof(IAzureServiceBusSender<ProductSKUInventoryMovementModel>)))
                .Returns(_queueService);

            return spMock;
        }

        private static DbContextOptions<PackingInventoryDbContext> CreateNewContextOptions(string currentMethod)
        {
            // Create a fresh service provider, and therefore a fresh 
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<PackingInventoryDbContext>();
            builder.UseInMemoryDatabase(currentMethod)
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        [Fact]
        public void Should_Success_instantiateProductSKUInventorySummaryModel()
        {
            var model = new ProductSKUInventorySummaryModel(0, 0, "storageCode", "storageName", 1);
            model.SetBalance(4);
            model.AddBalance(1);
            model.AdjustBalance(1);
            model.ReduceBalance(1);
            Assert.NotNull(model);
        }


        [Fact]
        public async Task Should_Success_AddDocument()
        {
            //Arrange
            var dbContext = new PackingInventoryDbContext(CreateNewContextOptions(MethodBase.GetCurrentMethod().ReflectedType.FullName + MethodBase.GetCurrentMethod().Name));
            var serviceProviderMock = new Mock<IServiceProvider>();

            serviceProviderMock
                .Setup(serviceProvider => serviceProvider
                .GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider() { TimezoneOffset = 1, Token = "token", Username = "username" });


            var productSKUInventoryDocument = new ProductSKUInventoryDocumentModel("documentNo",DateTimeOffset.Now, "ReferenceNo", "ReferenceType", 1, "storagename", "storagecode","type","remark");
            dbContext.ProductSKUInventoryDocuments.Add(productSKUInventoryDocument);

            dbContext.SaveChanges();

            var unitOfWork = new UnitOfWork(dbContext, serviceProviderMock.Object);
           
           
            var azureServiceBusSenderMock = new Mock<IAzureServiceBusSender<ProductSKUInventoryMovementModel>>();
            var service = GetService(GetServiceProvider(unitOfWork, azureServiceBusSenderMock.Object).Object);

            var data = new FormDto()
            {
                
                Date = DateTimeOffset.Now,
                ReferenceNo = "ReferenceNo",
                ReferenceType = "ReferenceType",
                Remark = "Remark",
                Storage = new Application.DTOs.StorageDto()
                {
                    _id=1,
                    code = "storagecode",
                    name = "storagename",
                },
                Items = new List<FormItemDto>()
                {
                    new FormItemDto()
                    {
                        ProductSKUId=1,
                        Quantity =1,
                        Remark ="Remark",
                        UOMId =1
                    }
                },
                Type= "Type",
                
            };


            //Act
            var result = await service.AddDocument(data);

            //Assertion
            Assert.True(-1 < result);

        }

        [Fact]
        public void Should_Success_AddMovement()
        {
            //Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var azureServiceBusSenderMock = new Mock<IAzureServiceBusSender<ProductSKUInventoryMovementModel>>();

            unitOfWorkMock
                .Setup(s => s.ProductSKUInventorySummaries.Get(It.IsAny<Expression<Func<ProductSKUInventorySummaryModel, bool>>>(), It.IsAny<Func<IQueryable<ProductSKUInventorySummaryModel>, IOrderedQueryable<ProductSKUInventorySummaryModel>>>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<ProductSKUInventorySummaryModel>()
                {
                    new ProductSKUInventorySummaryModel(0,0,"storageCode","storageName",1)
                });

            unitOfWorkMock
                .Setup(s => s.ProductSKUInventoryMovements
                .Insert(It.IsAny<ProductSKUInventoryMovementModel>())).Verifiable();

            unitOfWorkMock
                .Setup(s => s.ProductSKUInventorySummaries
                .Insert(It.IsAny<ProductSKUInventorySummaryModel>())).Verifiable();

            var service = GetService(GetServiceProvider(unitOfWorkMock.Object, azureServiceBusSenderMock.Object).Object);


            //Act
            var inventoryMovement = new ProductSKUInventoryMovementModel(0, 0, 0, 0, "storageCode", "storageName", 0, "IN", "Remark");
            inventoryMovement.SetPreviousBalance(0);
            inventoryMovement.SetCurrentBalance(1);

            service.AddMovement(inventoryMovement);

        }

        [Fact]
        public void AddMovement_with_existing_data_and_with_Type_IN_return_success()
        {
            //Arrange
            var dbContext = new PackingInventoryDbContext(CreateNewContextOptions(MethodBase.GetCurrentMethod().ReflectedType.FullName + MethodBase.GetCurrentMethod().Name));
            var serviceProviderMock = new Mock<IServiceProvider>();

            serviceProviderMock
                .Setup(serviceProvider => serviceProvider
                .GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider() { TimezoneOffset = 1, Token = "token", Username = "username" });

            var inventoryMovement = new ProductSKUInventoryMovementModel(1, 1, 1, 1, "storageCode", "storageName", 1, "IN", "Remark");

            var unitOfWork = new UnitOfWork(dbContext, serviceProviderMock.Object);

            dbContext
                .ProductSKUInventorySummaries
                .Add(new ProductSKUInventorySummaryModel(1, 1, "storageCode", "storageName", 1));

            dbContext.SaveChanges();
            var azureServiceBusSenderMock = new Mock<IAzureServiceBusSender<ProductSKUInventoryMovementModel>>();
            var service = GetService(GetServiceProvider(unitOfWork, azureServiceBusSenderMock.Object).Object);

            //Act
            service.AddMovement(inventoryMovement);
        }

        [Fact]
        public void AddMovement_with_existing_data_and_with_type_OUT_return_success()
        {
            //Arrange
            var dbContext = new PackingInventoryDbContext(CreateNewContextOptions(MethodBase.GetCurrentMethod().ReflectedType.FullName + MethodBase.GetCurrentMethod().Name));
            var serviceProviderMock = new Mock<IServiceProvider>();

            serviceProviderMock
                .Setup(serviceProvider => serviceProvider
                .GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider() { TimezoneOffset = 1, Token = "token", Username = "username" });

            var inventoryMovement = new ProductSKUInventoryMovementModel(1, 1, 1, 1, "storageCode", "storageName", 1, "OUT", "Remark");

            var unitOfWork = new UnitOfWork(dbContext, serviceProviderMock.Object);

            dbContext
                .ProductSKUInventorySummaries
                .Add(new ProductSKUInventorySummaryModel(1, 1, "storageCode", "storageName", 1));

            dbContext.SaveChanges();
            var azureServiceBusSenderMock = new Mock<IAzureServiceBusSender<ProductSKUInventoryMovementModel>>();
            var service = GetService(GetServiceProvider(unitOfWork, azureServiceBusSenderMock.Object).Object);

            //Act
            service.AddMovement(inventoryMovement);
        }

        [Fact]
        public void AddMovement_with_existing_data_and_with_type_ADJ_return_success()
        {
            //Arrange
            var dbContext = new PackingInventoryDbContext(CreateNewContextOptions(MethodBase.GetCurrentMethod().ReflectedType.FullName + MethodBase.GetCurrentMethod().Name));
            var serviceProviderMock = new Mock<IServiceProvider>();

            serviceProviderMock
                .Setup(serviceProvider => serviceProvider
                .GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider() { TimezoneOffset = 1, Token = "token", Username = "username" });

            var inventoryMovement = new ProductSKUInventoryMovementModel(1, 1, 1, 1, "storageCode", "storageName", 1, "ADJ", "Remark");

            var unitOfWork = new UnitOfWork(dbContext, serviceProviderMock.Object);

            dbContext
                .ProductSKUInventorySummaries
                .Add(new ProductSKUInventorySummaryModel(1, 1, "storageCode", "storageName", 1));

            dbContext.SaveChanges();
            var azureServiceBusSenderMock = new Mock<IAzureServiceBusSender<ProductSKUInventoryMovementModel>>();
            var service = GetService(GetServiceProvider(unitOfWork, azureServiceBusSenderMock.Object).Object);

            //Act
            service.AddMovement(inventoryMovement);
        }

        [Fact]
        public void AddMovement_with_existing_data_and_with_type_Invalid_return_success()
        {
            //Arrange
            var dbContext = new PackingInventoryDbContext(CreateNewContextOptions(MethodBase.GetCurrentMethod().ReflectedType.FullName + MethodBase.GetCurrentMethod().Name));
            var serviceProviderMock = new Mock<IServiceProvider>();

            serviceProviderMock
                .Setup(serviceProvider => serviceProvider
                .GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider() { TimezoneOffset = 1, Token = "token", Username = "username" });

            var inventoryMovement = new ProductSKUInventoryMovementModel(1, 1, 1, 1, "storageCode", "storageName", 1, "INVALID_TYPE", "Remark");

            var unitOfWork = new UnitOfWork(dbContext, serviceProviderMock.Object);

            dbContext
                .ProductSKUInventorySummaries
                .Add(new ProductSKUInventorySummaryModel(1, 1, "storageCode", "storageName", 1));

            dbContext.SaveChanges();
            var azureServiceBusSenderMock = new Mock<IAzureServiceBusSender<ProductSKUInventoryMovementModel>>();
            var service = GetService(GetServiceProvider(unitOfWork, azureServiceBusSenderMock.Object).Object);

            //Act
            Assert.Throws<Exception>(() => service.AddMovement(inventoryMovement));
        }


        [Fact]
        public void Should_Success_AddMovement_when_Type_Out()
        {
            //Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var azureServiceBusSenderMock = new Mock<IAzureServiceBusSender<ProductSKUInventoryMovementModel>>();

            unitOfWorkMock
                .Setup(s => s.ProductSKUInventorySummaries.Get(It.IsAny<Expression<Func<ProductSKUInventorySummaryModel, bool>>>(), It.IsAny<Func<IQueryable<ProductSKUInventorySummaryModel>, IOrderedQueryable<ProductSKUInventorySummaryModel>>>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<ProductSKUInventorySummaryModel>()
                {
                    new ProductSKUInventorySummaryModel(0,0,"storageCode","storageName",1)
                });

            unitOfWorkMock
                 .Setup(s => s.ProductSKUInventoryMovements
                 .Insert(It.IsAny<ProductSKUInventoryMovementModel>())).Verifiable();

            unitOfWorkMock
                .Setup(s => s.ProductSKUInventorySummaries
                .Insert(It.IsAny<ProductSKUInventorySummaryModel>())).Verifiable();

            var service = GetService(GetServiceProvider(unitOfWorkMock.Object, azureServiceBusSenderMock.Object).Object);

            //Act
            var inventoryMovement = new ProductSKUInventoryMovementModel(0, 0, 0, 0, "storageCode", "storageName", 0, "OUT", "Remark");
            inventoryMovement.SetPreviousBalance(0);
            inventoryMovement.SetCurrentBalance(1);
            inventoryMovement.AdjustCurrentBalance(1);

            service.AddMovement(inventoryMovement);

        }



        [Fact]
        public void Should_Success_AddMovement_when_Type_ADJ()
        {
            var dbContext = new PackingInventoryDbContext(CreateNewContextOptions(MethodBase.GetCurrentMethod().ReflectedType.FullName + MethodBase.GetCurrentMethod().Name));

            //Arrange
            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock
                .Setup(serviceProvider => serviceProvider.GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider() { TimezoneOffset = 1, Token = "token", Username = "username" });

            var unitOfWork = new UnitOfWork(dbContext, serviceProviderMock.Object);


            var azureServiceBusSenderMock = new Mock<IAzureServiceBusSender<ProductSKUInventoryMovementModel>>();

            var service = GetService(GetServiceProvider(unitOfWork, azureServiceBusSenderMock.Object).Object);

            //Act
            var inventoryMovement = new ProductSKUInventoryMovementModel(0, 0, 0, 0, "storageCode", "storageName", 0, "ADJ", "Remark");
            inventoryMovement.SetPreviousBalance(0);
            inventoryMovement.SetCurrentBalance(1);

            service.AddMovement(inventoryMovement);

        }

        [Fact]
        public void Should_Success_AddMovement_when_Throws_Exception()
        {
            //Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var azureServiceBusSenderMock = new Mock<IAzureServiceBusSender<ProductSKUInventoryMovementModel>>();

            unitOfWorkMock
                .Setup(s => s.ProductSKUInventorySummaries.Get(It.IsAny<Expression<Func<ProductSKUInventorySummaryModel, bool>>>(), It.IsAny<Func<IQueryable<ProductSKUInventorySummaryModel>, IOrderedQueryable<ProductSKUInventorySummaryModel>>>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<ProductSKUInventorySummaryModel>()
                {
                    new ProductSKUInventorySummaryModel(0,0,"storageCode","storageName",1)
                });

            var service = GetService(GetServiceProvider(unitOfWorkMock.Object, azureServiceBusSenderMock.Object).Object);

            //Act
            var inventoryMovement = new ProductSKUInventoryMovementModel(0, 0, 0, 0, "storageCode", "storageName", 0, "INVALID", "Remark");
            inventoryMovement.SetPreviousBalance(0);
            inventoryMovement.SetCurrentBalance(1);

            Assert.Throws<Exception>(() => service.AddMovement(inventoryMovement));
        }

        [Fact]
        public void GetMovementIndex_Throws_Exception()
        {
            //Arrange
            var dbContext = new PackingInventoryDbContext(CreateNewContextOptions(MethodBase.GetCurrentMethod().ReflectedType.FullName + MethodBase.GetCurrentMethod().Name));
            var serviceProviderMock = new Mock<IServiceProvider>();

            serviceProviderMock
                .Setup(serviceProvider => serviceProvider
                .GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider() { TimezoneOffset = 1, Token = "token", Username = "username" });

            var unitOfWork = new UnitOfWork(dbContext, serviceProviderMock.Object);

            var azureServiceBusSenderMock = new Mock<IAzureServiceBusSender<ProductSKUInventoryMovementModel>>();
            var service = GetService(GetServiceProvider(unitOfWork, azureServiceBusSenderMock.Object).Object);

            //Act
            Assert.Throws<NotImplementedException>(() => service.GetMovementIndex(1, 1, "IN", DateTime.Now, DateTime.Now));
        }

        [Fact]
        public void GetSummaryIndex_Throws_Exception()
        {
            //Arrange
            var dbContext = new PackingInventoryDbContext(CreateNewContextOptions(MethodBase.GetCurrentMethod().ReflectedType.FullName + MethodBase.GetCurrentMethod().Name));
            var serviceProviderMock = new Mock<IServiceProvider>();

            serviceProviderMock
                .Setup(serviceProvider => serviceProvider
                .GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider() { TimezoneOffset = 1, Token = "token", Username = "username" });

            var unitOfWork = new UnitOfWork(dbContext, serviceProviderMock.Object);

            var azureServiceBusSenderMock = new Mock<IAzureServiceBusSender<ProductSKUInventoryMovementModel>>();
            var service = GetService(GetServiceProvider(unitOfWork, azureServiceBusSenderMock.Object).Object);

            //Act
            Assert.Throws<NotImplementedException>(() => service.GetSummaryIndex(1, 1));
        }


        [Fact]
        public void GetDocumentById_Return_Success()
        {
            //Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var azureServiceBusSenderMock = new Mock<IAzureServiceBusSender<ProductSKUInventoryMovementModel>>();

            unitOfWorkMock
                .Setup(s => s.ProductSKUInventoryDocuments.GetByID(It.IsAny<int>()))
                .Returns(new ProductSKUInventoryDocumentModel("documentNo", DateTimeOffset.Now, "referenceNo", "referenceType", 1, "storageName", "storageCode", "type", "remark"));

            unitOfWorkMock
               .Setup(s => s.ProductSKUInventoryMovements.Get(It.IsAny<Expression<Func<ProductSKUInventoryMovementModel, bool>>>(), It.IsAny<Func<IQueryable<ProductSKUInventoryMovementModel>, IOrderedQueryable<ProductSKUInventoryMovementModel>>>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
               .Returns(new List<ProductSKUInventoryMovementModel>()
               {
                   new ProductSKUInventoryMovementModel(0, 0, 0, 0, "storageCode", "storageName", 0, "IN", "remark")
               });

            unitOfWorkMock
               .Setup(s => s.ProductSKUs.Get(It.IsAny<Expression<Func<ProductSKUModel, bool>>>(), It.IsAny<Func<IQueryable<ProductSKUModel>, IOrderedQueryable<ProductSKUModel>>>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
               .Returns(new List<ProductSKUModel>()
               {
                   new ProductSKUModel("code","name",0,0,"description")
               });

            unitOfWorkMock
              .Setup(s => s.UOMs.Get(It.IsAny<Expression<Func<UnitOfMeasurementModel, bool>>>(), It.IsAny<Func<IQueryable<UnitOfMeasurementModel>, IOrderedQueryable<UnitOfMeasurementModel>>>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
              .Returns(new List<UnitOfMeasurementModel>()
              {
                   new UnitOfMeasurementModel("unit")
              });

            unitOfWorkMock
            .Setup(s => s.Categories.Get(It.IsAny<Expression<Func<CategoryModel, bool>>>(), It.IsAny<Func<IQueryable<CategoryModel>, IOrderedQueryable<CategoryModel>>>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
            .Returns(new List<CategoryModel>()
            {
                   new CategoryModel("name","code")
            });

            var service = GetService(GetServiceProvider(unitOfWorkMock.Object, azureServiceBusSenderMock.Object).Object);
            //Act
            var result = service.GetDocumentById(1);

            //Assertion
            Assert.NotNull(result);
        }

        [Fact]
        public void GetDocumentIndex_NoKeyword_Return_Success()
        {
            //Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var azureServiceBusSenderMock = new Mock<IAzureServiceBusSender<ProductSKUInventoryMovementModel>>();

            unitOfWorkMock
                .Setup(s => s.ProductSKUInventoryDocuments.GetByID(It.IsAny<int>()))
                .Returns(new ProductSKUInventoryDocumentModel("documentNo", DateTimeOffset.Now, "referenceNo", "referenceType", 0, "storageName", "storageCode", "type", "remark"));


            var service = GetService(GetServiceProvider(unitOfWorkMock.Object, azureServiceBusSenderMock.Object).Object);
            //Act
            IndexQueryParam query = new IndexQueryParam()
            {
                page = 1,
                size = 25,
                keyword = "",
                order = "{}",
            };
            var result = service.GetDocumentIndex(query);

            //Assertion
            Assert.NotNull(result);

        }

        [Fact]
        public void GetDocumentIndex_WithKeyword_Return_Success()
        {
            //Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var azureServiceBusSenderMock = new Mock<IAzureServiceBusSender<ProductSKUInventoryMovementModel>>();

            unitOfWorkMock
                 .Setup(s => s.ProductSKUInventoryDocuments.GetByID(It.IsAny<int>()))
                 .Returns(new ProductSKUInventoryDocumentModel("documentNo", DateTimeOffset.Now, "referenceNo", "referenceType", 0, "storageName", "storageCode", "type", "remark"));


            var service = GetService(GetServiceProvider(unitOfWorkMock.Object, azureServiceBusSenderMock.Object).Object);
            //Act
            IndexQueryParam query = new IndexQueryParam()
            {
                page = 1,
                size = 25,
                keyword = "documentNo",
                order = "{}",
                
            };
            var result = service.GetDocumentIndex(query);

            //Assertion
            Assert.NotNull(result);

        }

    }
}
