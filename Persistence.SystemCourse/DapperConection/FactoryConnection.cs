using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Persistence.SystemCourse.DapperConection
{
    public class FactoryConnection : IFactoryConnection
    {
        private  IDbConnection _connection;
        private readonly IOptions<ConnectionConfiguration> _configs;

        public FactoryConnection(IOptions<ConnectionConfiguration> configs)
        {
            _configs = configs;
        }
        public void CloseConnection()
        {
            if(_connection !=null && _connection.State== ConnectionState.Open)
            {
                _connection.Close();
            }
        }

        public IDbConnection GetConnection()
        {
            if(_connection==null)
            {
                _connection = new SqlConnection(_configs.Value.Conexion);

            }
            if(_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }

            return _connection;
        }
    }
}