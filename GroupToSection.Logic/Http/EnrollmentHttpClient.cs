using GroupToSection.Logic.Http;
using GroupToSection.Logic.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EnrollmentToSection.Logic.Services
{
    public interface IEnrollmentHttpClient : IHttpClient
    {
    }

    public class EnrollmentHttpClient : HttpClient, IEnrollmentHttpClient
    {
        public EnrollmentHttpClient(System.Net.Http.IHttpClientFactory factory,
            ILogger<EnrollmentHttpClient> logger,
            IOptions<AuthenticationSettings> authenticationSettingsOptions)
           : base(factory, logger, authenticationSettingsOptions)
        {
        }
    }
}
