
//--------------------------------------------------------------------------------------------------------------------
// Warning! This is an auto generated file. Changes may be overwritten. 
// Generator version: 0.0.1.0
//-------------------------------------------------------------------------------------------------------------------- 

using GroupToSection.Logic.DataAccess;
using GroupToSection.Logic.Model;
using Microsoft.Extensions.Logging;

namespace GroupToSection.Logic.Services
{
    public partial interface IGroupCategoryService : IService<GroupCategory>
    {
    }

    public partial class GroupCategoryService : Service<GroupCategory>, IGroupCategoryService
    {
        public GroupCategoryService(ILogger<GroupCategoryService> logger,
           IGroupCategoryDataAccess dataAccess)
           : base(logger, dataAccess)
        { }
    }
}
