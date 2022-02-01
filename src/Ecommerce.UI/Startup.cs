using AutoMapper;
using Ecommence.Application.CampaignServices;
using Ecommence.Application.Common.Interfaces;
using Ecommence.Application.OrderServices;
using Ecommence.Application.ProductServices;
using Ecommence.Domain.Campaigns;
using Ecommence.Domain.Orders;
using Ecommence.Domain.Products;
using Ecommence.Infrastructure.DatabaseService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();


            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICampaignRepository, CampaignRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddSingleton<IProductService, ProductService>();
            services.AddSingleton<IOrderService, OrderService>();
            services.AddSingleton<ICampaignService, CampaignService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "My API",
                    Description = "DDD TicketType Promotion API",

                    Contact = new OpenApiContact() { Name = "Talking Dotnet", Email = "cagataykiziltan@gmail.com" }
                });


            });

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MyMappingProfiles>();
            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }
        public class MyMappingProfiles : Profile
        {
            public MyMappingProfiles()
            {

                CreateMap<Campaign, CampaignDto>().ReverseMap();
                CreateMap<Order, OrderDto>().ReverseMap();
                CreateMap<Product, ProductDto>().ReverseMap();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
