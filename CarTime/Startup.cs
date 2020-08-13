using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CarTime.Domain;
using CarTime.Domain.Entities;
using CarTime.Domain.Repositories.Abstract;
using CarTime.Domain.Repositories.EntityFramework;
using CarTime.Models;
using CarTime.Service;

namespace CarTime
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            Configuration.Bind("Project", new Config());

            //����������� ������������ ����������� � �������� �������
            services.AddTransient<ICarItemsRepository, EFCarItemsRepository>();
            services.AddTransient<IBrandItemsRepository, EFBrandItemsRepository>();
            services.AddTransient<IOrderRepository, EFOrderRepository>();

            services.AddTransient<DataManager>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(sp => ShopCart.GetCart(sp));

            services.AddMemoryCache();
            services.AddSession();

            //���������� �������� ��
            services.AddDbContext<AppDbContext>(x => x.UseSqlServer(Config.ConnectionString));

            //��������� ������� ����������� ������������
            services.AddIdentity<UserData, IdentityRole>(opts =>
            {
                opts.User.RequireUniqueEmail = true;
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            // ����������� �������������� 
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "myCompanyAuth";
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/account/login";
                options.AccessDeniedPath = "/account/accessdenied";
                options.SlidingExpiration = true;
            });

            //�������� ����������� ��� ������
            services.AddAuthorization(x =>
            {
                x.AddPolicy("AdminArea", policy => policy.RequireRole("admin"));
                x.AddPolicy("ManagerArea", policy => policy.RequireRole("manager"));
                x.AddPolicy("UserArea", policy => policy.RequireRole("user"));
            });

            //��������� ������ ��� ������������ � �������������
            services.AddControllersWithViews(x =>
                {
                    x.Conventions.Add(new AdminAreaAuthorization("Admin", "AdminArea"));
                    x.Conventions.Add(new ManagerAreaAuthorization("Manager", "ManagerArea"));
                    x.Conventions.Add(new UserAreaAuthorization("User", "UserArea"));
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddSessionStateTempDataProvider();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //������ ���������� �� ������
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //��������� ����������� ������
            app.UseStaticFiles();

            //���������� ������� �������������
            app.UseRouting();

            //���������� ��������������� � �����������
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            //������������ ������ ��� ��������
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("admin", "{area:exists}/{controller=Manage}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("manager", "{area:exists}/{controller=EditMain}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("user", "{area:exists}/{controller=UserMain}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("default", "{controller=Main}/{action=Index}/{id?}");
            });
        }
    }
}
