using GroupToSection.Logic.Http;
using GroupToSection.Logic.Model;
using GroupToSection.Logic.Services;
using Microsoft.Extensions.Logging;

namespace Logic.Service
{
    public interface IGroupService : IHttpService<Group>
    {
    }

    public class GroupService : HttpService<Group>, IGroupService
    {
        public GroupService(ILogger<GroupService> logger,
            IGroupHttpClient httpClient)
           : base(httpClient, logger)
        { }
    }
}
