using Com.Danliris.Service.Packing.Inventory.Application.DTOs;
using Com.Danliris.Service.Packing.Inventory.Application.Master.Category;
using Com.Danliris.Service.Packing.Inventory.Application.Master.Fabric;
using Com.Danliris.Service.Packing.Inventory.Application.Master.ProductPacking;
using Com.Danliris.Service.Packing.Inventory.Application.Master.ProductSKU;
using Com.Danliris.Service.Packing.Inventory.Application.Master.UOM;
using Com.Danliris.Service.Packing.Inventory.Application.Master.UpsertMaster;
using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;
using Com.Danliris.Service.Packing.Inventory.Data.Models.ProductByDivisionOrCategory;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Master.Fabric
{
   public class FabricProductServiceTest
    {
        public FabricProductService GetService(IServiceProvider serviceProvider)
        {
            return new FabricProductService(serviceProvider);
        }

        public Mock<IServiceProvider> GetServiceProvider(
            ICategoryService categoryService, 
            IUOMService UOMService,
            IProductSKUService productSKUService, 
            IProductPackingService productPackingService,
            IRepository<FabricProductSKUModel> fabricProductSKUModel,
            IRepository<FabricProductPackingModel> fabricProductPackingModel,
            IRepository<ProductPackingModel> productPackingModel,
            IUpsertMasterService upsertMasterService,
            IIdentityProvider identityProvider)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            serviceProviderMock.Setup(s => s.GetService(typeof(ICategoryService)))
                .Returns(categoryService);

            serviceProviderMock.Setup(s => s.GetService(typeof(IUOMService)))
             .Returns(UOMService);

            serviceProviderMock.Setup(s => s.GetService(typeof(IProductSKUService)))
             .Returns(productSKUService);

            serviceProviderMock.Setup(s => s.GetService(typeof(IProductPackingService)))
            .Returns(productPackingService);

            serviceProviderMock.Setup(s => s.GetService(typeof(IRepository<FabricProductSKUModel>)))
             .Returns(fabricProductSKUModel);

            serviceProviderMock.Setup(s => s.GetService(typeof(IRepository<FabricProductPackingModel>)))
            .Returns(fabricProductPackingModel);

            serviceProviderMock.Setup(s => s.GetService(typeof(IRepository<ProductPackingModel>)))
            .Returns(productPackingModel);

            serviceProviderMock.Setup(s => s.GetService(typeof(IUpsertMasterService)))
            .Returns(upsertMasterService);

            serviceProviderMock.Setup(s => s.GetService(typeof(IIdentityProvider)))
            .Returns(identityProvider);

            return serviceProviderMock;
        }

        private FabricProductPackingCompositeIdFormDto formDto
        {
            get
            {
                return new FabricProductPackingCompositeIdFormDto()
                {
                  

                };

            }
        }

        private FabricProductSKUModel fabricProductSKUModel
        {
            get
            {
                return new FabricProductSKUModel();
            }
        }

        private UnitOfMeasurementDto unitOfMeasurementDto
        {
            get
            {
                return new UnitOfMeasurementDto(new UnitOfMeasurementModel());
            }
        }

        [Fact]
        public async Task Should_Success_GenerateProductPackingCodeByCompositeId()
        {
            var categoryService = new Mock<ICategoryService>();
            var UOMService = new Mock<IUOMService>();
            var productSKUService = new Mock<IProductSKUService>();
            var productPackingService = new Mock<IProductPackingService>();
            var fabricProductSKURepository = new Mock<IRepository<FabricProductSKUModel>>();
            var fabricProductPackingRepository = new Mock<IRepository<FabricProductPackingModel>>();
            var productPackingRepository = new Mock<IRepository<ProductPackingModel>>();
            var upsertMasterService = new Mock<IUpsertMasterService>();
            var identityProvider = new Mock<IIdentityProvider>();

            fabricProductSKURepository.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
               .ReturnsAsync(fabricProductSKUModel);

            UOMService.Setup(s => s.GetById(It.IsAny<int>()))
              .ReturnsAsync(unitOfMeasurementDto);

            var service = GetService(GetServiceProvider(categoryService.Object,
                UOMService.Object,
                productSKUService.Object,
                productPackingService.Object,
                fabricProductSKURepository.Object,
                fabricProductPackingRepository.Object,
                productPackingRepository.Object,
                upsertMasterService.Object,
                identityProvider.Object
                ).Object);

            var result = await service.GenerateProductPackingCodeByCompositeId(formDto);
            Assert.NotEmpty(result);
        }


        [Fact]
        public async Task GenerateProductPackingCodeByCompositeId_Return_Empty()
        {
            var categoryService = new Mock<ICategoryService>();
            var UOMService = new Mock<IUOMService>();
            var productSKUService = new Mock<IProductSKUService>();
            var productPackingService = new Mock<IProductPackingService>();
            var fabricProductSKURepository = new Mock<IRepository<FabricProductSKUModel>>();
            var fabricProductPackingRepository = new Mock<IRepository<FabricProductPackingModel>>();
            var productPackingRepository = new Mock<IRepository<ProductPackingModel>>();
            var upsertMasterService = new Mock<IUpsertMasterService>();
            var identityProvider = new Mock<IIdentityProvider>();

            fabricProductSKURepository.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
               .ReturnsAsync(()=>null);

            UOMService.Setup(s => s.GetById(It.IsAny<int>()))
              .ReturnsAsync(()=>null);

            var service = GetService(GetServiceProvider(categoryService.Object,
                UOMService.Object,
                productSKUService.Object,
                productPackingService.Object,
                fabricProductSKURepository.Object,
                fabricProductPackingRepository.Object,
                productPackingRepository.Object,
                upsertMasterService.Object,
                identityProvider.Object
                ).Object);

            var result = await service.GenerateProductPackingCodeByCompositeId(formDto);
            Assert.Empty(result);
        }


    }
}
