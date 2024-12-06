using GroupToSection.Logic.DataAccess;
using GroupToSection.Logic.Model;
using Microsoft.Extensions.Logging;

namespace GroupToSection.Logic.Services
{
    public interface IGroupHttpService : IService<Group>
    {
    }

    public class GroupHttpService : Service<Group>, IGroupHttpService
    {
        public GroupHttpService(ILogger<GroupHttpService> logger,
           IGroupDataAccess dataAccess)
           : base(logger, dataAccess)
        { }
    }
}
