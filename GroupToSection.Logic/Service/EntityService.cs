using GroupToSection.Logic.Http;
using GroupToSection.Logic.Model;
using GroupToSection.Logic.Services;
using Microsoft.Extensions.Logging;

namespace Logic.Service
{
    public interface IEntityService : IHttpService<Entity>
    { }

    public class EntityService : HttpService<Entity>, IEntityService
    {
        public EntityService(ILogger<EntityService> logger,
            IEntityHttpClient entityHttpClient)
           : base(entityHttpClient, logger)
        {
        }
    }
}
