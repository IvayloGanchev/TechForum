﻿namespace TechForum.Web
{
    using System.Reflection;
    using System.Threading.Tasks;
    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using TechForum.Data;
    using TechForum.Data.Common;
    using TechForum.Data.Common.Repositories;
    using TechForum.Data.Models;
    using TechForum.Data.Repositories;
    using TechForum.Data.Seeding;
    using TechForum.Services.Data;
    using TechForum.Services.Mapping;
    using TechForum.Services.Messaging;
    using TechForum.Web.ViewModels;
   

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<CookiePolicyOptions>(
                options =>
                    {
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });
            Account cloudinaryCredentials = new Account("dpscqebc4", "719475685325931", "PxOMUut3GlWGPP4Jyfho6fKsv0Q");

            Cloudinary cloudinaryUtility = new Cloudinary(cloudinaryCredentials);

            services.AddSingleton(cloudinaryUtility);
            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });
            services.AddAntiforgery(options =>
            {
                options.HeaderName = "X-CSRF-TOKEN";
            });
            services.AddRazorPages();

            services.AddSingleton(this.configuration);

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<IEmailSender>(x => new SendGridEmailSender("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"));
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<ICategoriesService, CategoriesService>();
            services.AddTransient<IPostsService, PostsService>();
            services.AddTransient<IVoteService, VoteService>();
            services.AddTransient<ICommentsService, CommentsService>();
            services.AddTransient<ICloudinaryService, CloudinaryService>();
            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = this.configuration["Authentication:Facebook:AppId"];
                facebookOptions.AppSecret = this.configuration["Authentication:Facebook:AppSecret"];

                facebookOptions.Events = new Microsoft.AspNetCore.Authentication.OAuth.OAuthEvents()
                {
                    OnRemoteFailure = LoginFailureHandler =>
                    {
                        var authProperties = facebookOptions.StateDataFormat.Unprotect(LoginFailureHandler.Request.Query["state"]);
                        LoginFailureHandler.Response.Redirect("/Identity/Account/Login");
                        return Task.FromResult(0);
                    }
                };

            });
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                if (env.IsDevelopment())
                {
                    dbContext.Database.Migrate();
                }

                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapControllerRoute("forumCategory", "c/{name:minlength(3)}", new { controller = "Categories", action = "ByName" });
                        endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapRazorPages();
                    });
        }
    }
}
