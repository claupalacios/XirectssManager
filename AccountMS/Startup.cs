using AccountMS.Helpers;
using AccountMS.Models;
using AccountMS.Repositories;
using AccountMS.Repositories.Interfaces;
using AccountMS.Services;
using AccountMS.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;

namespace AccountMS
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(Configuration)
            .CreateLogger();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AccountDBContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("AccountMSDatabase")));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "AccountMS V1",
                });
            });

            //Repository Contracts
            services.AddScoped<IAccountRepository, AccountRepository>();

            //Services Contracts
            services.AddScoped<IAccountService, AccountService>();

            //Mapper profile
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfiles(new AutoMapper.Profile[] {
                        new MappingProfile()
                 });
            });
            services.AddControllers();
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
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1"));
        }
    }
}
