using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Infrastructure.Configuration;
using System.Infrastructure.HealthCheck;
using System.Infrastructure.Helpers;
using System.Infrastructure.IRepository;
using System.Infrastructure.Mapping;
using System.Infrastructure.Repository;
using System.Infrastructure.Services;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using SystemTheLastBugSpa.Data;
using SystemTheLastBugSpa.Data.Entity;

namespace System.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfiles));

            #region Jwt Configuration
            services.AddIdentity<Users, Roles>(cfg =>
            {
                cfg.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultAuthenticatorProvider;
                cfg.SignIn.RequireConfirmedEmail = true;
                cfg.User.RequireUniqueEmail = true;
                cfg.Password.RequireDigit = false;
                cfg.Password.RequiredUniqueChars = 0;
                cfg.Password.RequireLowercase = false;
                cfg.Password.RequireNonAlphanumeric = false;
                cfg.Password.RequireUppercase = false;
            }).AddDefaultTokenProviders().AddEntityFrameworkStores<AplicationDataContext>();

            //Jwt configuration
            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));
            //Authentication
            var key = Encoding.ASCII.GetBytes(Configuration["JwtConfig:Secret"]);
            var tokenValidationParams = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                RequireExpirationTime = true
            };

            services.AddSingleton(tokenValidationParams);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwt =>
           {
               jwt.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidIssuer = Configuration["Tokens:Issuer"],
                   ValidAudience = Configuration["Tokens:Audience"],
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]))
               };
               jwt.SaveToken = true;
               jwt.TokenValidationParameters = tokenValidationParams;
           });

            //services.AddAuthentication()
            //    .AddCookie()
            //    .AddJwtBearer(cfg =>
            //    {
            //        cfg.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidIssuer = Configuration["Tokens:Issuer"],
            //            ValidAudience = Configuration["Tokens:Audience"],
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]))
            //        };
            //    });

            #endregion

            services.AddDbContext<AplicationDataContext>(cfg =>
            {
                cfg.UseSqlServer(Configuration.GetConnectionString("SQL_CONNECTION"));
            });

            #region List of Repository
            services.AddScoped<ISendMailServices, SendMailHelpers>();
            services.AddScoped<ISendMailServices, SendMailHelpers>();           
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IStorerFilesServices, StorerFilesServices>();
            services.AddScoped<IMailServices, MailServices>();
            services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
            services.AddScoped<IRolPermissionsRepository, RolPermissionsRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IComunaRepository, ComunaRepository>();
            services.AddScoped<IRegionesRepository, RegionRepository>();
            services.AddScoped<IPeopleRepository, PeopleRepository>();
            services.AddScoped<ISocialHelpRepository, SocialHelpRepository>();
            services.AddScoped<IServicesByPeopleRepository, ServicesByPeopleRepository>();
            #endregion

            #region Helper

            services.AddScoped<ISendMailServices, SendMailHelpers>();

            #endregion

            #region Services
            services.AddScoped<AccountServices>();           
            services.AddScoped<RolPermissionsServices>();
            services.AddScoped<CountryServices>();
            services.AddScoped<RegionServices>();
            services.AddScoped<ComunaServices>();
            services.AddScoped<PeopleServices>();
            services.AddScoped<SocialHelpServices>();
            services.AddScoped<ServicesByPeopleServices>();
            #endregion

            #region Configuration of Swuager
            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Systema de comunidades Api", Version = "v1", Description = "Esta documentación es utilizada para lisit" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                     {
                        new OpenApiSecurityScheme
                        {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                        },
                             new string[]{}
                     }
               });

            });

            services.AddSwaggerGen();

            #endregion

            #region cors
            services.AddCors(options =>
            {
                //"https://frontendlenguasextranjeras.azurewebsites.net"
                options.AddPolicy("Todos",
                builder => builder.WithOrigins("https://frontendlenguasextranjeras.azurewebsites.net").WithHeaders("*").WithMethods("*")
                .AllowAnyMethod()
                .AllowAnyOrigin()
                .AllowAnyHeader()
                );
            });
            #endregion

            #region HealthChecks
            services.AddHealthChecks()
                    .AddCheck<EnvironmentVariablesHealthCheck>("EnvironmentVariables", null, new[] { "EnvironmentVariables" })
                    .AddCheck<SqlConnectionHealthCheck>("SQLDBConnectionCheck", null, new[] { "PingDataBase" })
                    .AddDbContextCheck<AplicationDataContext>();
            services.AddApplicationInsightsTelemetry(Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]);
            #endregion

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            // loggerFactory.AddFile("LogStore/Log-{Date}.txt");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStaticFiles();

            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SystemApi v1");
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("Todos");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/quickhealth", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions()
                {
                    Predicate = _ => false

                });
                endpoints.MapHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions()
                {
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse

                });
            });
        }
    }
}
