using System.API.Helpers;
using System.BLL.AgreementManagement;
using System.BLL.ClubManagement;
using System.BLL.CostManagement;
using System.BLL.CourseManagement;
using System.BLL.EmailManagement;
using System.BLL.EventManagement;
using System.BLL.GroupManagement;
using System.BLL.PaymentManagement;
using System.BLL.PhotoManagement;
using System.BLL.RoleManagement;
using System.BLL.RoomManagement;
using System.BLL.UserManagement;
using System.DAL;
using System.DAL.Entities;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace System.API
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
			services.AddCors();

			services.AddIdentity<User, Role>(options =>
				{
					options.Password.RequireDigit = true;
					options.Password.RequireLowercase = true;
					options.Password.RequireNonAlphanumeric = false;
					options.Password.RequireUppercase = true;
					options.Password.RequiredLength = 7;
					options.User.AllowedUserNameCharacters =
						"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ@.123457890";
				})
				.AddEntityFrameworkStores<DataContext>()
				.AddDefaultTokenProviders();

			services.AddDbContext<DataContext>(options =>
				options
					.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
					.UseLazyLoadingProxies());


			services.AddControllers()
					.AddNewtonsoftJson(opt =>
					{
						opt.SerializerSettings.Converters.Add(new StringEnumConverter());
						opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
					});

			// Auto Mapper Configurations
			var mappingConfig = new MapperConfiguration(mapperConfig =>
			{
				mapperConfig.AddProfile(new AgreementManagementMappingProfile());
				mapperConfig.AddProfile(new ClubManagementMappingProfile());
				mapperConfig.AddProfile(new CourseManagementMappingProfile());
				mapperConfig.AddProfile(new RoomManagementMappingProfile());
				mapperConfig.AddProfile(new UserManagementMappingProfile());
				mapperConfig.AddProfile(new CostsManagementMappingProfile());
				mapperConfig.AddProfile(new CourseManagementMappingProfile());
				mapperConfig.AddProfile(new EventManagementMappingProfile());
				mapperConfig.AddProfile(new GroupManagementMappingProfile());
				mapperConfig.AddProfile(new PaymentManagementMappingProfile());
				mapperConfig.AddProfile(new PhotoManagementMappingProfile());
			});

			var mapper = mappingConfig.CreateMapper();
			services.AddSingleton(mapper);


			// configure strongly typed settings objects
			var appSettingsSection = Configuration.GetSection("AppSettings");
			services.Configure<AppSettings>(appSettingsSection);

			// configure jwt authentication
			var appSettings = appSettingsSection.Get<AppSettings>();
			var key = Encoding.ASCII.GetBytes(appSettings.Secret);
			services.AddAuthentication(x =>
				{
					x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
					x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				})
				.AddJwtBearer(x =>
				{
					x.Events = new JwtBearerEvents
					{
						OnTokenValidated = async context =>
						{
							var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
							var userId = int.Parse(context.Principal.Identity.Name);
							var user = await userService.GetAsync(userId);
							if (user == null) context.Fail("Unauthorized");
						}
					};
					x.RequireHttpsMetadata = false;
					x.SaveToken = true;
					x.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(key),
						ValidateIssuer = false,
						ValidateAudience = false
					};
				});

			// configure DI for application services
			services.AddScoped<IAgreementService, AgreementService>();
			services.AddScoped<IClubService, ClubService>();
			services.AddScoped<ICostsService, CostsService>();
			services.AddScoped<ICourseService, CourseService>();
			services.AddScoped<IEventService, EventService>();
			services.AddScoped<IGroupService, GroupService>();
			services.AddScoped<IPaymentService, PaymentService>();
			services.AddScoped<IPhotoService, PhotoService>();
			services.AddScoped<IRoleService, RoleService>();
			services.AddScoped<IRoomService, RoomService>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IEmailService, EmailService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			// global cors policy
			app.UseCors(x => x
				.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader()
			);

			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
		}
	}
}