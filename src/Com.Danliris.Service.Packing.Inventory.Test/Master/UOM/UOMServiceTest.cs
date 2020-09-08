using Com.Danliris.Service.Packing.Inventory.Application;
using Com.Danliris.Service.Packing.Inventory.Application.Master.UOM;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Master.UOM
{
    public class UOMServiceTest
    {
        public UOMService GetService(IServiceProvider serviceProvider)
        {
            return new UOMService(serviceProvider);
        }

        public Mock<IServiceProvider> GetServiceProvider(
           IRepository<UnitOfMeasurementModel> uomRepository
          )
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            serviceProviderMock.Setup(s => s.GetService(typeof(IRepository<UnitOfMeasurementModel>)))
                .Returns(uomRepository);

            return serviceProviderMock;
        }

        private FormDto formDto
        {
            get
            {
                return new FormDto()
                {
                  Unit = "Unit"
                };

            }
        }

        private UnitOfMeasurementModel unitOfMeasurementModel
        {
            get
            {
                return new UnitOfMeasurementModel("Unit");

            }
        }

        [Fact]
        public async Task Create_Return_Success()
        {
            var uomRepository = new Mock<IRepository<UnitOfMeasurementModel>>();

            var newUnitOfMeasurementModel = unitOfMeasurementModel;
            newUnitOfMeasurementModel.SetUnit("newUnit");
            uomRepository.Setup(s => s.ReadAll())
               .Returns(new List<UnitOfMeasurementModel>() { newUnitOfMeasurementModel }.AsQueryable());

            uomRepository.Setup(s => s.InsertAsync(It.IsAny<UnitOfMeasurementModel>()))
               .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(
               uomRepository.Object
               ).Object);

            var result = await service.Create(formDto);
            Assert.True(0 < result);
        }

        [Fact]
        public async Task Create_with_Duplicate_Unit_Throws_ServiceValidationException()
        {

            var uomRepository = new Mock<IRepository<UnitOfMeasurementModel>>();

            unitOfMeasurementModel.SetUnit("Unit");
            uomRepository.Setup(s => s.ReadAll())
               .Returns(new List<UnitOfMeasurementModel>() { unitOfMeasurementModel }.AsQueryable());

            uomRepository.Setup(s => s.InsertAsync(It.IsAny<UnitOfMeasurementModel>()))
               .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(
               uomRepository.Object
               ).Object);

            await Assert.ThrowsAsync<ServiceValidationException>(() => service.Create(formDto));
          
        }


        [Fact]
        public async Task Delete_Success()
        {
            var uomRepository = new Mock<IRepository<UnitOfMeasurementModel>>();

            var newUnitOfMeasurementModel = unitOfMeasurementModel;
            newUnitOfMeasurementModel.SetUnit("newUnit");
            uomRepository.Setup(s => s.DeleteAsync(It.IsAny<int>())).ReturnsAsync(1);

            var service = GetService(GetServiceProvider(
               uomRepository.Object
               ).Object);

            var result = await service.Delete(1);
            Assert.True(0 < result);
        }

        [Fact]
        public async Task GetById_Return_Success()
        {
            var uomRepository = new Mock<IRepository<UnitOfMeasurementModel>>();

            uomRepository.Setup(s => s.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(unitOfMeasurementModel);

            var service = GetService(GetServiceProvider(
               uomRepository.Object
               ).Object);

            var result = await service.GetById(1);
           
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetById_Return_Null()
        {
            var uomRepository = new Mock<IRepository<UnitOfMeasurementModel>>();

            uomRepository.Setup(s => s.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(()=>null);

            var service = GetService(GetServiceProvider(
               uomRepository.Object
               ).Object);

            var result = await service.GetById(1);
            Assert.Null( result);
        }

        [Fact]
        public async Task Update_Return_Success()
        {
            var uomRepository = new Mock<IRepository<UnitOfMeasurementModel>>();

            uomRepository.Setup(s => s.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(unitOfMeasurementModel);
            uomRepository.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<UnitOfMeasurementModel>())).ReturnsAsync(1);

            var service = GetService(GetServiceProvider(
               uomRepository.Object
               ).Object);

            var result = await service.Update(1,formDto);
            Assert.True(0 < result);
          
        }

        [Fact]
        public async Task Upsert_Success_Created()
        {
            var uomRepository = new Mock<IRepository<UnitOfMeasurementModel>>();
            var newUnitOfMeasurementModel = unitOfMeasurementModel;
            newUnitOfMeasurementModel.Id = 1;
            newUnitOfMeasurementModel.SetUnit("new unit");


            uomRepository.Setup(s => s.ReadAll())
                         .Returns(new List<UnitOfMeasurementModel>() { newUnitOfMeasurementModel }.AsQueryable());

            var service = GetService(GetServiceProvider(
               uomRepository.Object
               ).Object);

            var result = await service.Upsert(formDto);
           

        }

        [Fact]
        public async Task GetIndex_Success()
        {
            var uomRepository = new Mock<IRepository<UnitOfMeasurementModel>>();
           
            uomRepository.Setup(s => s.ReadAll())
                         .Returns(new List<UnitOfMeasurementModel>() { 
                             new UnitOfMeasurementModel("Unit") { 
                                Id=1,
                                Active=true,
                                
                             } 
                         }
                         .AsQueryable()
                         .BuildMock()
                         .Object);

            var service = GetService(GetServiceProvider(
               uomRepository.Object
               ).Object);

            IndexQueryParam queryParam = new IndexQueryParam()
            {
                page =1,
                size =1,
                order ="",
                keyword =""
            };

            var result = await service.GetIndex(queryParam);
            Assert.True(0 < result.data.Count);
        }

        

        [Fact]
        public async Task Upsert_return_success_when_data_hasExist()
        {
            var uomRepository = new Mock<IRepository<UnitOfMeasurementModel>>();
            var newUnitOfMeasurementModel = unitOfMeasurementModel;
           
            uomRepository.Setup(s => s.ReadAll())
                         .Returns(new List<UnitOfMeasurementModel>() { unitOfMeasurementModel }.AsQueryable());

            var service = GetService(GetServiceProvider(
               uomRepository.Object
               ).Object);

            var result = await service.Upsert(formDto);
          
        }

    }
}
