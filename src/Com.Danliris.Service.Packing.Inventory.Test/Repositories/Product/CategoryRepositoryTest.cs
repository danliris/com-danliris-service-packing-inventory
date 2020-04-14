using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.Product.Category;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.Product
{
    public class CategoryRepositoryTest
    {
        private const string ENTITY = "Category";
        [MethodImpl(MethodImplOptions.NoInlining)]
        private string GetCurrentMethod()
        {
            var st = new StackTrace();
            var sf = st.GetFrame(1);

            return string.Concat(sf.GetMethod().Name, "_", ENTITY);
        }

        private PackingInventoryDbContext GetDbContext(string testName)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PackingInventoryDbContext>();
            optionsBuilder
                .UseInMemoryDatabase(testName)
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));

            return new PackingInventoryDbContext(optionsBuilder.Options);
        }

        private Mock<IServiceProvider> GetServiceProviderMock()
        {
            var identityProviderMock = new Mock<IIdentityProvider>();
            identityProviderMock.Setup(identityProvider => identityProvider.TimezoneOffset).Returns(7);
            identityProviderMock.Setup(identityProvider => identityProvider.Token).Returns("token");
            identityProviderMock.Setup(identityProvider => identityProvider.Username).Returns("username");

            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(s => s.GetService(typeof(IIdentityProvider)))
                .Returns(identityProviderMock.Object);

            return serviceProviderMock;
        }

        [Fact]
        public async Task Should_Success_Create_New_Data()
        {
            var dbContext = GetDbContext(GetCurrentMethod());
            var serviceProviderMock = GetServiceProviderMock();

            var dataUtil = new CategoryDataUtil(dbContext);
            var modelToCreate = dataUtil.GetModel();

            var repository = new CategoryRepository(dbContext, serviceProviderMock.Object);

            var result = await repository.InsertAsync(modelToCreate);

            Assert.True(result > 0);
        }

        [Fact]
        public async Task Should_Success_Read_All_Existing_Data()
        {
            var dbContext = GetDbContext(GetCurrentMethod());
            var serviceProviderMock = GetServiceProviderMock();

            var dataUtil = new CategoryDataUtil(dbContext);
            await dataUtil.GetNewData();

            var repository = new CategoryRepository(dbContext, serviceProviderMock.Object);

            var result = await repository.ReadAll().ToListAsync();

            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task Should_Success_Update_Existing_Data()
        {
            var dbContext = GetDbContext(GetCurrentMethod());
            var serviceProviderMock = GetServiceProviderMock();

            var dataUtil = new CategoryDataUtil(dbContext);
            var modelToUpdate = await dataUtil.GetNewData();

            var repository = new CategoryRepository(dbContext, serviceProviderMock.Object);

            var result = await repository.UpdateAsync(modelToUpdate.Id, modelToUpdate);

            Assert.True(result > 0);
        }

        [Fact]
        public async Task Should_Success_Delete_Existing_Data()
        {
            var dbContext = GetDbContext(GetCurrentMethod());
            var serviceProviderMock = GetServiceProviderMock();

            var dataUtil = new CategoryDataUtil(dbContext);
            var modelToDelete = await dataUtil.GetNewData();

            var repository = new CategoryRepository(dbContext, serviceProviderMock.Object);

            var result = await repository.DeleteAsync(modelToDelete.Id);

            Assert.True(result > 0);
        }
    }
}
