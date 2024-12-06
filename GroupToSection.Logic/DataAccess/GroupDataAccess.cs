
//--------------------------------------------------------------------------------------------------------------------
// Warning! This is an auto generated file. Changes may be overwritten. 
// Generator version: 0.0.1.0
//-------------------------------------------------------------------------------------------------------------------- 

using GroupToSection.Logic.Model;
using GroupToSection.Logic.DataAccess;

namespace GroupToSection.Logic.DataAccess
{
    public interface IGroupDataAccess : IDataAccess<Group>
    {    }

    public class GroupDataAccess : BaseDataAccess<Group>, IGroupDataAccess
    {
        public GroupDataAccess(ISqlDataAccess db, SqlStringBuilder<Group> sqlStringBuilder)
            : base(db, sqlStringBuilder)
        { }
     }
} 