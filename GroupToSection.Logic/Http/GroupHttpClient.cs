using GroupToSection.Logic.Http;
using GroupToSection.Logic.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GroupToSection.Logic.Services
{
    public interface IGroupHttpClient : IHttpClient
    { }

    public class GroupHttpClient : HttpClient, IGroupHttpClient
    {
        public GroupHttpClient(System.Net.Http.IHttpClientFactory factory,
            ILogger<GroupHttpClient> logger,
            IOptions<CanvasApiSettings> settingsOptions)
           : base(factory, logger, settingsOptions)
        { }
    }
}
