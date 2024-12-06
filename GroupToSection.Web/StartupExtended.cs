using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using AutoMapper;
using System;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;
using GroupToSection.Logic.Services;
using GroupToSection.Logic.Settings;
using Logic.Service;

namespace GroupToSection.Web
{
    public class StartupExtended : Startup
    {

        public StartupExtended(IConfiguration configuration, IWebHostEnvironment env) : base(configuration, env)
        { }

        protected override IList<CultureInfo> GetSupportedLanguages()
        {
            return new List<CultureInfo> {
                new CultureInfo("sv-se"),
                new CultureInfo("en-gb"),
            };
        }

        protected override RequestCulture GetDefaultCulture()
        {
            return new RequestCulture("sv-se");
        }

        protected override void CustomServiceConfiguration(IServiceCollection services)
        {
            services.AddTransient<IGroupService, GroupService>();
            services.AddTransient<IGroupHttpClient, GroupHttpClient>();
            services.Configure<CanvasApiSettings>(Configuration.GetSection("CanvasApiSettings"));

            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
            });
        }

        protected override void CustomConfiguration(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }

        protected override void ConfigureExceptionHandler(IApplicationBuilder app)
        {
            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    var e = context.Features.Get<IExceptionHandlerFeature>();
                    if (e == null) return;
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "text/html";
                    var factory = builder.ApplicationServices.GetService<ILoggerFactory>();
                    var logger = factory.CreateLogger("ExceptionLogger");
                    logger.LogError(e.Error, e.Error.Message);
                    if (e.Error.Message.Contains("No connection could be made because the target machine actively refused it")) context.Response.Redirect("/no-auth");
                });
            });
        }

        public override IMapper GetMapper()
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                var profile = new MappingConfiguration();
                profile = AddAdditionalMappingConfig(profile);

                mc.AddProfile(profile);
            });

            return mapperConfig.CreateMapper();
        }

        public static MappingConfiguration AddAdditionalMappingConfig(MappingConfiguration profile)
        {
            return profile;
        }

    }

    public class CleanUpServiceExtended : CleanUpService
    {
        private const int WEEDING_TIME_IN_DAYS = -14;
        private readonly ILogService logService;

        public CleanUpServiceExtended(IOptions<ApplicationSettings> options,
            ILogService logService) : base(options)
        {
            this.logService = logService;
        }

        public override async Task ProcessCleanUp()
        {
            await base.ProcessCleanUp();
            await DeleteLogs();
        }

        private async Task DeleteLogs()
        {
            var all = await logService.GetAll();
            foreach (var item in all.Where(x => x.CreatedOn < DateTime.Now.AddDays(WEEDING_TIME_IN_DAYS)))
            {
                await logService.Delete(item.Id);
            }
        }
    }
}
