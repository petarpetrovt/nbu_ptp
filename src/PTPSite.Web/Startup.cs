using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using PTPSite.Services.Impl;
using PTPSite.Web.Services;
using DATABASE = PTPSite.Database;
using SERVICES = PTPSite.Services;

namespace PTPSite.Web
{
	public class Startup
	{
		private IConfigurationRoot Configuration { get; }

		private IHostingEnvironment HostingEnvironment { get; }

		public Startup(IHostingEnvironment env)
		{
			IConfigurationBuilder builder = new ConfigurationBuilder()
				.SetBasePath(basePath: env.ContentRootPath)
				.AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile(path: $"appsettings.{env.EnvironmentName}.json", optional: false, reloadOnChange: true)
				.AddEnvironmentVariables();

			Configuration = builder.Build();

			HostingEnvironment = env;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services
				.AddDbContext<DATABASE.ApplicationDbContext>(options =>
				{
					string connectionString = Configuration.GetConnectionString("DefaultConnection");
					options.UseSqlServer(connectionString);
				});

			services
				.AddIdentity<SERVICES.ApplicationUser, ApplicationRole>(options =>
				{
					options.Password.RequireDigit = false;
					options.Password.RequiredLength = 5;
					options.Password.RequireNonAlphanumeric = false;
					options.Password.RequireUppercase = false;
					options.Password.RequireLowercase = false;
					options.Password.RequiredUniqueChars = 2;
				})
				.AddUserStore<ApplicationUserStore>()
				.AddRoleStore<ApplicationRoleStore>()
				.AddDefaultTokenProviders();

			services
				.AddScoped<SERVICES.ICommentService, CommentService>()
				.AddScoped<SERVICES.IUserService, UserService>();

			services.AddAuthentication();

			services.AddAuthorization(options =>
			{
				options.AddPolicy(nameof(SERVICES.ApplicationRole.Administrator), config =>
				{
					string roleName = Enum.GetName(typeof(SERVICES.ApplicationRole), SERVICES.ApplicationRole.Administrator);

					config.RequireClaim(ApplicationRole.ClaimType, roleName);
				});
			});

			services
				.AddMemoryCache()
				.AddResponseCompression()
				.AddResponseCaching()
				.AddMvc()
				.AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
		}

		public void Configure(IApplicationBuilder application, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				application.UseDeveloperExceptionPage();
				application.UseBrowserLink();
				application.UseDatabaseErrorPage();
			}

			application
				.UseStaticFiles()
				.UseAuthentication()
				.UseMvc(routes =>
				{
					routes.MapRoute(
						name: "default",
						template: "{controller=Home}/{action=Index}/{id?}");
				});
		}
	}
}
