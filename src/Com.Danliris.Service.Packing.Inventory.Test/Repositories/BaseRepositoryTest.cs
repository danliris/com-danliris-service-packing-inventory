using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
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
    public abstract class BaseRepositoryTest<TDbContext, TRepository, TModel, TDataUtil>
        where TDbContext : PackingInventoryDbContext
        where TRepository : class, IRepository<TModel>
        where TDataUtil : BaseDataUtil<TRepository, TModel>
        where TModel : StandardEntity
    {
        private string _entity;

        public BaseRepositoryTest(string entity)
        {
            _entity = entity;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected string GetCurrentMethod()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);

            return string.Concat(sf.GetMethod().Name, "_", _entity);
        }

        protected string GetCurrentAsyncMethod([CallerMemberName] string methodName = "")
        {
            var method = new StackTrace()
                .GetFrames()
                .Select(frame => frame.GetMethod())
                .FirstOrDefault(item => item.Name == methodName);

            return method.Name;

        }

        protected TDbContext DbContext(string testName)
        {
            DbContextOptionsBuilder<TDbContext> optionsBuilder = new DbContextOptionsBuilder<TDbContext>();
            var serviceProvider = new ServiceCollection()
               .AddEntityFrameworkInMemoryDatabase()
               .BuildServiceProvider();

            optionsBuilder
                .UseInMemoryDatabase(testName)
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .UseInternalServiceProvider(serviceProvider);

            TDbContext dbContext = Activator.CreateInstance(typeof(TDbContext), optionsBuilder.Options) as TDbContext;

            return dbContext;
        }

        protected virtual Mock<IServiceProvider> GetServiceProviderMock(TDbContext dbContext)
        {
            Mock<IServiceProvider> sp = new Mock<IServiceProvider>();
            sp.Setup(s => s.GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider() { TimezoneOffset = 7, Token = "token", Username = "username" });

            return sp;
        }

        protected virtual TDataUtil DataUtil(TRepository repository, TDbContext dbContext = null)
        {
            TDataUtil dataUtil = Activator.CreateInstance(typeof(TDataUtil), repository) as TDataUtil;
            return dataUtil;
        }

        [Fact]
        public virtual async Task Should_Success_Insert()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            TRepository repo = Activator.CreateInstance(typeof(TRepository), dbContext, serviceProvider) as TRepository;
            TModel data = DataUtil(repo, dbContext).GetModel();
            var result = await repo.InsertAsync(data);
            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_Delete()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            TRepository repo = Activator.CreateInstance(typeof(TRepository), dbContext, serviceProvider) as TRepository;
            TModel data = await DataUtil(repo, dbContext).GetTestData();
            var result = await repo.DeleteAsync(data.Id);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public virtual async Task Should_Success_ReadAll()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            TRepository repo = Activator.CreateInstance(typeof(TRepository), dbContext, serviceProvider) as TRepository;
            await DataUtil(repo, dbContext).GetTestData();
            var result = repo.ReadAll();

            Assert.NotEmpty(result);
        }

        [Fact]
        public virtual async Task Should_Success_ReadById()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            TRepository repo = Activator.CreateInstance(typeof(TRepository), dbContext, serviceProvider) as TRepository;
            TModel data = await DataUtil(repo, dbContext).GetTestData();
            var result = await repo.ReadByIdAsync(data.Id);

            Assert.NotNull(result);
        }

        [Fact]
        public virtual async Task Should_Success_Update()
        {
            string testName = GetCurrentMethod() + "Update";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            TRepository repo = Activator.CreateInstance(typeof(TRepository), dbContext, serviceProvider) as TRepository;
            TRepository repo2 = Activator.CreateInstance(typeof(TRepository), dbContext, serviceProvider) as TRepository;
            TModel emptyData = DataUtil(repo, dbContext).GetEmptyModel();
            await repo.InsertAsync(emptyData);
            var data = repo.ReadAll().FirstOrDefault();
            TModel model = DataUtil(repo, dbContext).GetModel();
            var result = await repo2.UpdateAsync(data.Id, model);

            Assert.NotEqual(0, result);
        }
    }
}
