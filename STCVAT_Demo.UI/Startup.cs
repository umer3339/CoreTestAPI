using MailHelper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using STAAPI.Infrastructure.Repository.GenericRepository;
using STAAPI.Infrastructure.Repository.STCVATRepository;
using STCAPI.Infrastructure.Implementation.GenericImplementation;
using STCAPI.Infrastructure.Implementation.STCVATFormImplemetation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STCVAT_Demo.UI
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
            services.AddTransient(typeof(IGenericRepository<,>), typeof(DetailImplementation<,>));
            services.AddTransient<ISTACVATFormRepository, STCVATFormDetail>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<ISTCPOstValidationRepository, STCPostValidationDetail>();
            services.AddTransient<IReconcilationSummaryRepository, ReconcilationSummaryImplementation>();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
