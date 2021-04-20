using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TicketTypePromotion.Application.PromotionServices;
using TicketTypePromotion.Application.Common.Interfaces;
using TicketTypePromotion.Application.ReservationServices;
using TicketTypePromotion.Application.TicketTypeServices;
using TicketTypePromotion.Domain.Promotions;
using TicketTypePromotion.Domain.Reservations;
using TicketTypePromotion.Domain.TicketTypes;
using TicketTypePromotion.Infrastructure.DatabaseService;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;

namespace TicketTypePromotion.UI
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
             services.AddScoped<ITicketTypeService, TicketTypeService>();
             services.AddScoped<IReservationService, ReservationService>();
             services.AddScoped<IPromotionService, PromotionService>();
             services.AddSingleton<IPromotionRepository, PromotionRepository>();
             services.AddSingleton<ITicketTypeRepository, TicketTypeRepository>();
             services.AddSingleton<IReservationRepository, ReservationRepository>();



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

                CreateMap<TicketType, TicketTypeDto>();
                CreateMap<Reservation, ReservationDto>();
                CreateMap<Promotion, PromotionDto>();
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

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "My API V2");
            });


        }
    }
}
