
//--------------------------------------------------------------------------------------------------------------------
// Warning! This is an auto generated file. Changes may be overwritten. 
// Generator version: 0.0.1.0
//-------------------------------------------------------------------------------------------------------------------- 

using GroupToSection.Logic.DataAccess;
using GroupToSection.Logic.Model;
using Microsoft.Extensions.Logging;

namespace GroupToSection.Logic.Services
{
    public partial interface IGroupService : IService<Group>
    {
    }

    public partial class GroupService : Service<Group>, IGroupService
    {
        public GroupService(ILogger<GroupService> logger,
           IGroupDataAccess dataAccess)
           : base(logger, dataAccess)
        { }
    }
}
