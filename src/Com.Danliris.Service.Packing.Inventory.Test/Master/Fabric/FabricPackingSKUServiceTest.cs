using Com.Danliris.Service.Packing.Inventory.Application;
using Com.Danliris.Service.Packing.Inventory.Application.Master.Fabric;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Master;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;
using Com.Danliris.Service.Packing.Inventory.Data.Models.ProductByDivisionOrCategory;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Moonlay.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Master.Fabric
{
    public class FabricPackingSKUServiceTest
    {
        private const string Entity = "FabricPackingSKUService";
        public FabricPackingSKUService GetService(IServiceProvider serviceProvider, PackingInventoryDbContext dbContext)
        {
            return new FabricPackingSKUService(serviceProvider, dbContext);
        }

        public PackingInventoryDbContext GetDbContext(string testName)
        {
            string databaseName = testName;
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var optionsBuilder = new DbContextOptionsBuilder<PackingInventoryDbContext>();

            optionsBuilder
                .UseInMemoryDatabase(databaseName)
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                 .UseInternalServiceProvider(serviceProvider);

            PackingInventoryDbContext DbContex = new PackingInventoryDbContext(optionsBuilder.Options);
            return DbContex;

        }

        public Mock<IServiceProvider> GetServiceProvider(PackingInventoryDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            var unitOfWork = new UnitOfWork(dbContext, serviceProviderMock.Object);
            serviceProviderMock
                .Setup(s => s.GetService(typeof(IUnitOfWork)))
                .Returns(unitOfWork);

            serviceProviderMock
                .Setup(s => s.GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider() { TimezoneOffset = 1, Token = "token", Username = "username" });
            
            return serviceProviderMock;
        }

        FabricSKUFormDto fabricSKUFormDto
        {
            get
            {
                return new FabricSKUFormDto()
                {
                    ConstructionId = 1,
                    GradeId = 1,
                    ProcessTypeId = 1,
                    UOMId = 1,
                    WarpId = 1,
                    WeftId = 1,
                    WidthId = 1,
                    WovenTypeId = 1,
                    YarnTypeId = 1
                };
            }
        }

        FabricPackingAutoCreateFormDto fabricPackingAutoCreate
        {
            get
            {
                return new FabricPackingAutoCreateFormDto()
                {
                    FabricSKUId = 1,
                    ProductSKUId =1,
                    Length = 1,
                    PackingType = "METER",
                    Quantity = 1
                };
            }
        }
        ProductSKUModel productSKUModel
        {
            get
            {
                return new ProductSKUModel("CODE", "name", 1, 1, "description");
               
            }
        }

        ProductPackingModel productPackingModel
        {
            get 
            {
                return new ProductPackingModel(1, 1, 1, "CODE0001", "CODE0001", "");
            }
        }

        CategoryModel categoryModel
        {
            get
            {
                return new CategoryModel("FABRIC", "CODE");
            }
        }

        IPWovenTypeModel iPWovenTypeModel
        {
            get
            {
                return new IPWovenTypeModel("CODE", "wovenType");
            }
        }

        MaterialConstructionModel materialConstructionModel
        {
            get
            {
                return new MaterialConstructionModel("TypeMaterial", "CODE");
            }
        }

        IPWidthTypeModel iPWidthTypeModel
        {
            get
            {
                return new IPWidthTypeModel("CODE", "widthType");
            }
        }

        WarpTypeModel warpTypeModel
        {
            get
            {
                return new WarpTypeModel("TYPE", "CODE");
            }
        }

        WeftTypeModel weftTypeModel
        {
            get
            {
                return new WeftTypeModel("TYPE", "CODE");
            }
        }

        IPProcessTypeModel iPProcessTypeModel
        {
            get
            {
                return new IPProcessTypeModel("CODE", "ProcesType");
            }
        }

        IPYarnTypeModel iPYarnTypeModel
        {
            get
            {
                return new IPYarnTypeModel("CODE", "YARNTYPE");
            }
        }

        GradeModel gradeModel
        {
            get
            {
                return new GradeModel("Type", "CODE", true);
            }
        }

        UnitOfMeasurementModel unitOfMeasurementModel
        {
            get
            {
                return new UnitOfMeasurementModel("METER")
                {
                    Active = true
                };
            }
        }

        FabricProductSKUModel fabricProductSKUModel
        {
            get
            {
                return new FabricProductSKUModel("CODE", 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
            }
        }

        [Fact]
        public void CreateSKU_Should_Succes()
        {
            //Setup
            PackingInventoryDbContext dbContext = GetDbContext(Entity);
            dbContext.ProductSKUs.Add(productSKUModel);
            dbContext.IPCategories.Add(categoryModel);
            dbContext.IPWovenType.Add(iPWovenTypeModel);
            dbContext.IPMaterialConstructions.Add(materialConstructionModel);
            dbContext.IPWidthType.Add(iPWidthTypeModel);
            dbContext.IPWarpTypes.Add(warpTypeModel);
            dbContext.IPWeftTypes.Add(weftTypeModel);
            dbContext.IPProcessType.Add(iPProcessTypeModel);
            dbContext.IPYarnType.Add(iPYarnTypeModel);
            dbContext.IPGrades.Add(gradeModel);
            dbContext.IPUnitOfMeasurements.Add(unitOfMeasurementModel);
            dbContext.SaveChanges();

            //act
            FabricPackingSKUService service = GetService(GetServiceProvider(dbContext).Object, dbContext);
            var dataUtil = fabricSKUFormDto;
            int result = service.CreateSKU(dataUtil);

            //assert
            Assert.NotEqual(0, result);
            Assert.True(0 < result);
        }

        [Fact]
        public void CreateSKU_When_Unit_YARD_Return_Succes()
        {
            //Setup
            PackingInventoryDbContext dbContext = GetDbContext(Entity);
            dbContext.ProductSKUs.Add(productSKUModel);
            dbContext.IPCategories.Add(categoryModel);
            dbContext.IPWovenType.Add(iPWovenTypeModel);
            dbContext.IPMaterialConstructions.Add(materialConstructionModel);
            dbContext.IPWidthType.Add(iPWidthTypeModel);
            dbContext.IPWarpTypes.Add(warpTypeModel);
            dbContext.IPWeftTypes.Add(weftTypeModel);
            dbContext.IPProcessType.Add(iPProcessTypeModel);
            dbContext.IPYarnType.Add(iPYarnTypeModel);
            dbContext.IPGrades.Add(gradeModel);
            dbContext.IPUnitOfMeasurements.Add(new UnitOfMeasurementModel("YARD"));
            dbContext.SaveChanges();

            //act
            FabricPackingSKUService service = GetService(GetServiceProvider(dbContext).Object, dbContext);
            var dataUtil = fabricSKUFormDto;
            int result = service.CreateSKU(dataUtil);

            //assert
            Assert.NotEqual(0, result);
            Assert.True(0 < result);
        }

        [Fact]
        public void DeleteSKU_Return_Succes()
        {
            //Setup
            PackingInventoryDbContext dbContext = GetDbContext(Entity);
            var dataUtil = fabricProductSKUModel;
            dbContext.FabricProductSKUs.Add(dataUtil);
            dbContext.SaveChanges();

            //act
            FabricPackingSKUService service = GetService(GetServiceProvider(dbContext).Object, dbContext);
            int result = service.DeleteSKU(dataUtil.Id);

            //assert
            Assert.NotEqual(0, result);
            Assert.True(0 < result);
        }

        [Fact]
        public void  GetById_Return_Success()
        {
            //Setup
            PackingInventoryDbContext dbContext = GetDbContext(Entity);
            var fabricProductSKU = fabricProductSKUModel;
            dbContext.FabricProductSKUs.Add(fabricProductSKU);

            dbContext.IPWovenType.Add(iPWovenTypeModel);
            dbContext.IPMaterialConstructions.Add(materialConstructionModel);
            dbContext.IPWidthType.Add(iPWidthTypeModel);
            dbContext.IPWarpTypes.Add(warpTypeModel);
            dbContext.IPWeftTypes.Add(weftTypeModel);
            dbContext.IPProcessType.Add(iPProcessTypeModel);
            dbContext.IPYarnType.Add(iPYarnTypeModel);
            dbContext.IPGrades.Add(gradeModel);
            dbContext.IPUnitOfMeasurements.Add(unitOfMeasurementModel);
            dbContext.SaveChanges();

            //act
            FabricPackingSKUService service = GetService(GetServiceProvider(dbContext).Object, dbContext);
            FabricSKUDto result = service.GetById(fabricProductSKU.Id);

            //assert
            Assert.NotNull(result);
            Assert.NotEqual(0, result.Id);
            Assert.True(0 < result.Id);
        }

        [Fact]
        public async Task GetIndex_Return_Success()
        {
            //Setup
            PackingInventoryDbContext dbContext = GetDbContext(Entity);
            var fabricProductSKU = fabricProductSKUModel;
            dbContext.FabricProductSKUs.Add(fabricProductSKU);

            dbContext.IPWovenType.Add(iPWovenTypeModel);
            dbContext.IPMaterialConstructions.Add(materialConstructionModel);
            dbContext.IPWidthType.Add(iPWidthTypeModel);
            dbContext.IPWarpTypes.Add(warpTypeModel);
            dbContext.IPWeftTypes.Add(weftTypeModel);
            dbContext.IPProcessType.Add(iPProcessTypeModel);
            dbContext.IPYarnType.Add(iPYarnTypeModel);
            dbContext.IPGrades.Add(gradeModel);
            dbContext.IPUnitOfMeasurements.Add(unitOfMeasurementModel);
            dbContext.SaveChanges();

            //act
            FabricPackingSKUService service = GetService(GetServiceProvider(dbContext).Object, dbContext);
            IndexQueryParam queryParam = new IndexQueryParam()
            {
                keyword="",
                order="",
                page =0,
                size=1
            };
            FabricSKUIndex result =await service.GetIndex(queryParam);
            
            //assert
            Assert.NotNull(result);
            Assert.NotEmpty(result.data);
        }

        //[Fact]
        //public void AutoCreateSKU_Return_Success()
        //{
        //    //Setup
        //    PackingInventoryDbContext dbContext = GetDbContext(Entity);
            
        //    //act
        //    var service = GetService(GetServiceProvider(dbContext).Object, dbContext);
        //    var form = new FabricSKUAutoCreateFormDto() 
        //    {
        //        ProductionOrderNo = "TEST12345"
        //    };
        //    FabricSKUIdCodeDto result =  service.AutoCreateSKU(form);

        //    //assert
        //    Assert.NotNull(result);
        //}

        [Fact]
        public void AutoCreatePacking_Return_Success()
        {
            //Setup
            PackingInventoryDbContext dbContext = GetDbContext(Entity);
            var dataUtil = fabricProductSKUModel;
            dbContext.FabricProductSKUs.Add(dataUtil);
            dbContext.ProductPackings.Add(productPackingModel);
            dbContext.ProductSKUs.Add(productSKUModel);
            dbContext.IPUnitOfMeasurements.Add(unitOfMeasurementModel);

            dbContext.SaveChanges();

            //act
            var service = GetService(GetServiceProvider(dbContext).Object, dbContext);
            var  form = fabricPackingAutoCreate;
            FabricPackingIdCodeDto result = service.AutoCreatePacking(form);

            //assert
            Assert.NotNull(result);
        }

        //[Fact]
        //public void AutoCreatePacking_Return_Fail()
        //{
        //    //Setup
        //    PackingInventoryDbContext dbContext = GetDbContext(Entity);


        //    //act
        //    var service = GetService(GetServiceProvider(dbContext).Object, dbContext);
        //    var form = new FabricPackingAutoCreateFormDto();
        //    FabricPackingIdCodeDto result = service.AutoCreatePacking(form);
        //    //assert
        //    Assert.ThrowsAny<Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities.ServiceValidationException>(() => service.AutoCreatePacking(form));
        //}


        [Fact]
        public async Task UpdateSKU_Throws_NotImplementedException()
        {
            //Setup
            PackingInventoryDbContext dbContext = GetDbContext(Entity);

            //act
            var service = GetService(GetServiceProvider(dbContext).Object, dbContext);
            var form = new FabricSKUFormDto();
            await Assert.ThrowsAnyAsync<NotImplementedException>(() => service.UpdateSKU(1, form));

            //assert
           // Assert.NotEqual(0,result);
        }

        //[Fact]
        //public void AutoCreateSKU_with_NewFabricSKUAutoCreateFormDto_Return_Success()
        //{
        //    //Setup
        //    PackingInventoryDbContext dbContext = GetDbContext(Entity);

        //    //act
        //    var service = GetService(GetServiceProvider(dbContext).Object, dbContext);
        //    var form = new NewFabricSKUAutoCreateFormDto();
        //    FabricSKUIdCodeDto result = service.AutoCreateSKU(form);

        //    //assert
        //    Assert.NotNull(result);
        //}

    }
}
