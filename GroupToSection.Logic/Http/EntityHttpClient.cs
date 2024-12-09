using GroupToSection.Logic.Http;
using GroupToSection.Logic.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GroupToSection.Logic.Services
{
    public interface IEntityHttpClient : IHttpClient
    {
    }

    public class EntityHttpClient : HttpClient, IEntityHttpClient
    {
        public EntityHttpClient(System.Net.Http.IHttpClientFactory factory,
            ILogger<EntityHttpClient> logger,
            IOptions<AuthenticationSettings> authenticationSettingsOptions)
           : base(factory, logger, authenticationSettingsOptions)
        {
        }
    }
}
