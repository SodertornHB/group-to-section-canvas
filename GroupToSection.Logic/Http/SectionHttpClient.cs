using GroupToSection.Logic.Http;
using GroupToSection.Logic.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GroupToSection.Logic.Services
{
    public interface ISectionHttpClient : IHttpClient
    {
    }

    public class SectionHttpClient : HttpClient, ISectionHttpClient
    {
        public SectionHttpClient(System.Net.Http.IHttpClientFactory factory,
            ILogger<SectionHttpClient> logger,
            IOptions<AuthenticationSettings> authenticationSettingsOptions)
           : base(factory, logger, authenticationSettingsOptions)
        {
        }
    }
}
