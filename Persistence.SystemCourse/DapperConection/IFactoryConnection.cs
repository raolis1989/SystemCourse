using System.Data;

namespace Persistence.SystemCourse.DapperConection
{
    public interface IFactoryConnection
    {
         void CloseConnection(); 
         IDbConnection GetConnection();
    }
}