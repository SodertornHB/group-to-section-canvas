using GroupToSection.Logic.Http;
using GroupToSection.Logic.Model;
using Microsoft.Extensions.Logging;

namespace GroupToSection.Logic.Services
{
    public partial interface IGroupCategoryHttpService : IHttpService<GroupCategory>
    {
    }

    public partial class GroupCategoryHttpService : HttpService<GroupCategory>, IGroupCategoryHttpService
    {
        public GroupCategoryHttpService(ILogger<GroupCategoryHttpService> logger,
           IGroupHttpClient groupHttpClient)
           : base(groupHttpClient, logger)
        { }
    }
}
