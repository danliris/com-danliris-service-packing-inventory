using Com.Danliris.Service.Packing.Inventory.Application.ProductSKU;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.ProductSKU;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Com.Danliris.Service.Packing.Inventory.WebApi
{
    public class Startup
    {
        private const string DEFAULT_CONNECTION = "DefaultConnection";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Register Validator
            services.AddSingleton<IValidator<CreateProductSKUViewModel>, CreateProductSKUValidator>();

            // Register Middleware
            services.AddTransient<IProductSKURepository, ProductSKURepository>();

            string connectionString = Configuration.GetConnectionString(DEFAULT_CONNECTION) ?? Configuration[DEFAULT_CONNECTION];
            services.AddDbContext<PackingInventoryDbContext>(options => options.UseSqlServer(connectionString));

            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new Info() { Title = "Packing Inventory API", Version = "v1" });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddFluentValidation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(swagger =>
            {
                swagger.SwaggerEndpoint("/swagger/v1/swagger.json", "Packing Inventory API");
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
