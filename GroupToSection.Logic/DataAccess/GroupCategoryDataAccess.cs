
//--------------------------------------------------------------------------------------------------------------------
// Warning! This is an auto generated file. Changes may be overwritten. 
// Generator version: 0.0.1.0
//-------------------------------------------------------------------------------------------------------------------- 

using GroupToSection.Logic.Model;
using GroupToSection.Logic.DataAccess;

namespace GroupToSection.Logic.DataAccess
{
    public interface IGroupCategoryDataAccess : IDataAccess<GroupCategory>
    {    }

    public class GroupCategoryDataAccess : BaseDataAccess<GroupCategory>, IGroupCategoryDataAccess
    {
        public GroupCategoryDataAccess(ISqlDataAccess db, SqlStringBuilder<GroupCategory> sqlStringBuilder)
            : base(db, sqlStringBuilder)
        { }
     }
} 