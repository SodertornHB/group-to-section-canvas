using GroupToSection.Logic.Http;
using GroupToSection.Logic.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GroupToSection.Logic.Services
{
    public interface ICourseHttpClient : IHttpClient
    { }

    public class CourseHttpClient : HttpClient, ICourseHttpClient
    {
        public CourseHttpClient(System.Net.Http.IHttpClientFactory factory,
            ILogger<CourseHttpClient> logger,
            IOptions<AuthenticationSettings> authenticationSettingsOptions)
           : base(factory, logger, authenticationSettingsOptions)
        { }
    }
}
