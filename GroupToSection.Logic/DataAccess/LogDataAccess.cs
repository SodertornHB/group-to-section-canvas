
//--------------------------------------------------------------------------------------------------------------------
// Warning! This is an auto generated file. Changes may be overwritten. 
// Generator version: 0.0.1.0
//-------------------------------------------------------------------------------------------------------------------- 

using GroupToSection.Logic.Model;
using GroupToSection.Logic.DataAccess;

namespace GroupToSection.Logic.DataAccess
{
    public interface ILogDataAccess : IDataAccess<Log>
    {    }

    public class LogDataAccess : BaseDataAccess<Log>, ILogDataAccess
    {
        public LogDataAccess(ISqlDataAccess db, SqlStringBuilder<Log> sqlStringBuilder)
            : base(db, sqlStringBuilder)
        { }
     }
} 