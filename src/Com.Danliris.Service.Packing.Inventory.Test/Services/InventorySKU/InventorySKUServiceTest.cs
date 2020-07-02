using Com.Danliris.Service.Packing.Inventory.Application;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.InventorySKU;
using Com.Danliris.Service.Packing.Inventory.Application.QueueService;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Inventory;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        [Fact]
        public async Task Should_Success_AddDocument()
        {
            //Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var azureServiceBusSenderMock = new Mock<IAzureServiceBusSender<ProductSKUInventoryMovementModel>>();

            unitOfWorkMock
                .Setup(s => s.ProductSKUInventoryDocuments.Get(It.IsAny<Expression<Func<ProductSKUInventoryDocumentModel, bool>>>(), It.IsAny<Func<IQueryable<ProductSKUInventoryDocumentModel>, IOrderedQueryable<ProductSKUInventoryDocumentModel>>>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<ProductSKUInventoryDocumentModel>() { 
                  
                });

            unitOfWorkMock
                .Setup(s => s.ProductSKUInventoryDocuments
                .Insert(It.IsAny<ProductSKUInventoryDocumentModel>())).Verifiable();
                
            var service = GetService(GetServiceProvider(unitOfWorkMock.Object, azureServiceBusSenderMock.Object).Object);
            
            var data = new FormDto();
            data.GetType().GetProperty("Date").SetValue(data, DateTimeOffset.Now);
            data.GetType().GetProperty("ReferenceNo").SetValue(data, "ReferenceNo");
            data.GetType().GetProperty("ReferenceType").SetValue(data, "ReferenceType");
            data.GetType().GetProperty("Type").SetValue(data, "Type");
            data.GetType().GetProperty("Remark").SetValue(data, "Remark");
            data.Storage = new Storage()
            {
                Id = 1,
                Code = "Code",
                Name = "Name",
                Unit = new UnitStorage()
                {
                    Division = new DivisionStorage()
                    {
                        Name = "Name"
                    },
                    Name="Name",
                } 
            };
            data.Items = new List<FormItemDto>()
            {
                new FormItemDto()
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
            var inventoryMovement = new ProductSKUInventoryMovementModel(0,0,0,0,"storageCode","storageName",0,"IN","Remark");
            inventoryMovement.SetPreviousBalance(0);
            inventoryMovement.SetCurrentBalance(1);

            service.AddMovement(inventoryMovement);

        }

        [Fact]
        public void AddMovement_with_existing_data_return_success()
        {
            //Arrange
            //var unitOfWorkMock = new Mock<IUnitOfWork>();
            var unitOfWorkMock = new Mock<UnitOfWork>();
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
            inventoryMovement.SetCurrentBalance(0);

            service.AddMovement(inventoryMovement);

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

            service.AddMovement(inventoryMovement);

        }

        [Fact]
        public void Should_Success_AddMovement_when_Type_ADJ()
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
        public void GetDocumentById_Return_Success()
        {
            //Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var azureServiceBusSenderMock = new Mock<IAzureServiceBusSender<ProductSKUInventoryMovementModel>>();


            unitOfWorkMock
                .Setup(s => s.ProductSKUInventoryDocuments.GetByID(It.IsAny<int>()))
                .Returns(new ProductSKUInventoryDocumentModel("documentNo",DateTimeOffset.Now,"referenceNo", "referenceType",1,"storageName", "storageCode","type","remark"));

            unitOfWorkMock
               .Setup(s => s.ProductSKUInventoryMovements.Get(It.IsAny<Expression<Func<ProductSKUInventoryMovementModel, bool>>>(), It.IsAny<Func<IQueryable<ProductSKUInventoryMovementModel>, IOrderedQueryable<ProductSKUInventoryMovementModel>>>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
               .Returns(new List<ProductSKUInventoryMovementModel>()
               {
                   new ProductSKUInventoryMovementModel(0,0,0,0,"storageCode","storageName",0,"IN","remark")
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
            var result =  service.GetDocumentById(1);

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
                page =1,
                size =25,
                keyword ="",
                order ="{}",
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
