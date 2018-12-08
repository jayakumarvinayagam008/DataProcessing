using DataProcessing.Application.B2B.Command;
using DataProcessing.Application.B2B.Query;
using DataProcessing.Application.B2C.Command;
using DataProcessing.Application.B2C.Query;
using DataProcessing.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataProcessing.Core.Web
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
            var noSqlConnection = Configuration["CustomerData:NoSqlSource"];

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


           
            services.Configure<NoSqlSettings>(
            options =>
            {
                options.ConnectionString = Configuration.GetSection("NoSql:Server").Value;
                options.Database = Configuration.GetSection("NoSql:Database").Value;
            });

            services.Configure<DataProcessingSetting>(Configuration.GetSection("DataProcessingSetting"));

            services.AddScoped<ISaveB2B, SaveB2B>();
            services.AddScoped<IReadDataFromFile, ReadDataFromFile>();

            services.AddScoped<IBusinessToBusinessRepository, BusinessToBusinessRepository>();
            services.AddScoped<IDataProcessingContext, DataProcessingContext>();
            services.AddScoped<IB2BSearchBlock, B2BSearchBlock>();
            services.AddScoped<IBusinessCategoryRepository, BusinessCategoryRepository>();
            services.AddScoped<ISearchAction, SearchAction>();

            services.AddScoped<IB2CSearchBlock, B2CSearchBlock>();
            services.AddScoped<ISaveB2C, SaveB2C>();
            services.AddScoped<IB2CSearchAction, B2CSearchAction>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
