using Com.Danliris.Service.Packing.Inventory.Data.Models.FabricQualityControl;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.FabricQualityControl;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories
{
    public class CriteriaRepositoryTest
    {
        private const string ENTITY = "Criteria";
        [MethodImpl(MethodImplOptions.NoInlining)]
        protected string GetCurrentMethod()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);

            return string.Concat(sf.GetMethod().Name, "_", ENTITY);
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

        protected virtual Mock<IServiceProvider> GetServiceProviderMock(PackingInventoryDbContext dbContext)
        {
            Mock<IServiceProvider> sp = new Mock<IServiceProvider>();
            sp.Setup(s => s.GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider() { TimezoneOffset = 7, Token = "token", Username = "username" });

            return sp;
        }

        public CriteriaModel GetModel()
        {
            return new CriteriaModel("code", "group", 1, "name", 1, 1, 1, 1);
        }

        public async Task<CriteriaModel> CreateHelper(CriteriaRepository repo)
        {
            var result = await repo.InsertAsync(GetModel());
            var data = repo.ReadAll().FirstOrDefault();
            return data;
        }

        [Fact]
        public async Task Should_Success_Insert()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            CriteriaRepository repo = new CriteriaRepository(dbContext, serviceProvider);
            var result = await repo.InsertAsync(GetModel());
            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Delete()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            CriteriaRepository repo = new CriteriaRepository(dbContext, serviceProvider);
            var data = await CreateHelper(repo);
            var result = await repo.DeleteAsync(data.Id);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_ReadAll()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            CriteriaRepository repo = new CriteriaRepository(dbContext, serviceProvider);
            await CreateHelper(repo);
            var result = repo.ReadAll();

            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task Should_Success_ReadById()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            CriteriaRepository repo = new CriteriaRepository(dbContext, serviceProvider);
            var data = await CreateHelper(repo);
            var result = await repo.ReadByIdAsync(data.Id);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_Success_Update()
        {
            string testName = GetCurrentMethod() + "Update";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            CriteriaRepository repo = new CriteriaRepository(dbContext, serviceProvider);

            CriteriaRepository repo2 = new CriteriaRepository(dbContext, serviceProvider);
            
            await repo.InsertAsync(new CriteriaModel());
            var data = repo.ReadAll().FirstOrDefault();
            var model = GetModel();
            var result = await repo2.UpdateAsync(data.Id, model);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_GetDbSet()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var repo = new CriteriaRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var data = await CreateHelper(repo);
            var result = repo.GetDbSet();

            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task Should_Success_ReadAllIgnoreQueryFilter()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var repo = new CriteriaRepository(dbContext, GetServiceProviderMock(dbContext).Object);
            var data = await CreateHelper(repo);
            var result = repo.ReadAllIgnoreQueryFilter();

            Assert.NotEmpty(result);
        }
    }
}
