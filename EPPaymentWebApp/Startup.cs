using System;
using EPPaymentWebApp.Interfaces;
using EPPaymentWebApp.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EPPaymentWebApp
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
            services.AddTransient<IBeginPaymentRepository, BeginPaymentRepository>();
            services.AddTransient<IResponsePaymentRepository, ResponsePaymentRepository>();
            services.AddTransient<ILogPaymentRepository, LogPaymentRepository>();
            services.AddTransient<IEndPaymentRepository, EndPaymentRepository>();
            services.AddTransient<IResponseBankRequestTypeTibcoRepository, ResponseBankRequestTypeTibcoRepository>();
            services.AddTransient<IEnterprisePaymentViewModelRepository, EnterprisePaymentViewModelRepository>();
            services.AddTransient<ISentToTibcoRepository, SentToTibcoRepository>();

            services.AddDistributedMemoryCache();

            services.AddSession(options => {
                options.Cookie.HttpOnly = true;
                options.Cookie.Name = "EpSession";
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.IdleTimeout = TimeSpan.FromMinutes(20);

            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


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
                app.UseDeveloperExceptionPage();
            }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
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
