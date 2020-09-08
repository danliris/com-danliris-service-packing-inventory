using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Infrastructure
{
    public class BaseRepositoryTest
    {
        private DbContextOptions<PackingInventoryDbContext> CreateNewContextOptions(string currentMethod)
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

        private PackingInventoryDbContext GetDbContext(string currentMethod)
        {
            return new PackingInventoryDbContext(CreateNewContextOptions(currentMethod));
        }

        private Mock<IServiceProvider> GetServiceProviderMock()
        {
            var result = new Mock<IServiceProvider>();

            result
                .Setup(serviceProvider => serviceProvider.GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider()
                {
                    TimezoneOffset = 1,
                    Token = "token",
                    Username = "username"
                });

            return result;
        }

        [Fact]
        public void Should_Success_Insert_Data()
        {
            var dbContext = GetDbContext(MethodBase.GetCurrentMethod().ReflectedType.FullName + MethodBase.GetCurrentMethod().Name);
            var serviceProvider = GetServiceProviderMock().Object;

            var unitOfWork = new UnitOfWork(dbContext, serviceProvider);

            var model = new UnitOfMeasurementModel("MTR");

            unitOfWork.UOMs.Insert(model);
            unitOfWork.Commit();

            Assert.True(unitOfWork.UOMs.Get().Count() > 0);
        }

        [Fact]
        public void Should_Success_Delete_Data()
        {
            var dbContext = GetDbContext(MethodBase.GetCurrentMethod().ReflectedType.FullName + MethodBase.GetCurrentMethod().Name);
            var serviceProvider = GetServiceProviderMock().Object;

            var unitOfWork = new UnitOfWork(dbContext, serviceProvider);

            var model = new UnitOfMeasurementModel("MTR");

            unitOfWork.UOMs.Insert(model);
            unitOfWork.Commit();
            Assert.True(unitOfWork.UOMs.Get().Count() > 0);

            unitOfWork.UOMs.Delete(model);
            unitOfWork.Commit();

            Assert.True(unitOfWork.UOMs.Get().Count() <= 0);
        }

        [Fact]
        public void Should_Success_Delete_Data_By_Identity()
        {
            var dbContext = GetDbContext(MethodBase.GetCurrentMethod().ReflectedType.FullName + MethodBase.GetCurrentMethod().Name);
            var serviceProvider = GetServiceProviderMock().Object;

            var unitOfWork = new UnitOfWork(dbContext, serviceProvider);

            var model = new UnitOfMeasurementModel("MTR");

            unitOfWork.UOMs.Insert(model);
            unitOfWork.Commit();
            Assert.True(unitOfWork.UOMs.Get().Count() > 0);

            unitOfWork.UOMs.Delete(model.Id);
            unitOfWork.Commit();

            Assert.True(unitOfWork.UOMs.Get().Count() <= 0);
        }

        [Fact]
        public void Should_Success_Get_By_Id()
        {
            var dbContext = GetDbContext(MethodBase.GetCurrentMethod().ReflectedType.FullName + MethodBase.GetCurrentMethod().Name);
            var serviceProvider = GetServiceProviderMock().Object;

            var unitOfWork = new UnitOfWork(dbContext, serviceProvider);

            var model = new UnitOfMeasurementModel("MTR");

            unitOfWork.UOMs.Insert(model);
            unitOfWork.Commit();

            Assert.NotNull(unitOfWork.UOMs.GetByID(model.Id));
        }

        [Fact]
        public void Should_Success_Update_Data()
        {
            var dbContext = GetDbContext(MethodBase.GetCurrentMethod().ReflectedType.FullName + MethodBase.GetCurrentMethod().Name);
            var serviceProvider = GetServiceProviderMock().Object;

            var unitOfWork = new UnitOfWork(dbContext, serviceProvider);

            var model = new UnitOfMeasurementModel("MTR");

            unitOfWork.UOMs.Insert(model);
            unitOfWork.Commit();
            Assert.NotNull(unitOfWork.UOMs.GetByID(model.Id));

            model.SetUnit("YDS");

            unitOfWork.UOMs.Update(model);
            Assert.NotNull(unitOfWork.UOMs.GetByID(model.Id));
        }

        [Fact]
        public void Should_Success_Generate_Code()
        {
            var result = CodeGenerator.GenerateCode();
            Assert.NotNull(result);
        }
    }
}
