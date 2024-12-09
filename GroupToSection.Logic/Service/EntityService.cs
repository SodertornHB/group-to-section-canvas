using GroupToSection.Logic.Http;
using GroupToSection.Logic.Model;
using GroupToSection.Logic.Services;
using GroupToSection.Logic.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Logic.Service
{
    public interface IEntityService : IHttpService<Entity>
    {
    }

    public class EntityService : HttpService<Entity>, IEntityService
    {
        private readonly CanvasApiSettings canvasApiSettings;

        public EntityService(ILogger<EntityService> logger,
            IEntityHttpClient entityHttpClient,
            IOptions<CanvasApiSettings> canvasApiSettingsOptions)
           : base(entityHttpClient, logger)
        {
            this.canvasApiSettings = canvasApiSettingsOptions.Value;
        }
    }
}
