using Dapper;
using Persistence.SystemCourse.DapperConection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.SystemCourse.Pagination
{
    public class PaginationRepository : IPagination
    {
        private readonly IFactoryConnection _factoryConnection;

        public PaginationRepository(IFactoryConnection factoryConnection)
        {
            _factoryConnection = factoryConnection;
        }
        public async Task<PaginationModel> ReturnPagination(string storedProcedure, int numberPage, int quantityElements, IDictionary<string, object> parameterFilter, string orderingColumn)      
        {
            PaginationModel paginationModel = new PaginationModel();
            List<IDictionary<string, object>> listReport = null;
            int totalRecords = 0;
            int totalPages = 0;
            try
            {
                var connection = _factoryConnection.GetConnection();
                DynamicParameters parameters = new DynamicParameters();

                foreach(var param in parameterFilter)
                {
                    parameters.Add("@" + param.Key, param.Value);
                }
                parameters.Add("@Ordering", orderingColumn);
                parameters.Add("@NumberPage", numberPage);
                parameters.Add("@QuantityElements", quantityElements);
                

                parameters.Add("@TotalRows", totalRecords, DbType.Int32, ParameterDirection.Output);
                parameters.Add("@TotalPages", totalPages, DbType.Int32, ParameterDirection.Output);


                var result = await connection.QueryAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                listReport = result.Select(x => (IDictionary<string, object>)x).ToList();
                paginationModel.ListRows = listReport;
                paginationModel.NumberPages = parameters.Get<int>("@TotalPages");
                paginationModel.TotalRows = parameters.Get<int>("@TotalRows");
            }
            catch (Exception ex)
            {

                throw new Exception ("Not eject procedure",ex);
            }
            finally
            {
                _factoryConnection.CloseConnection();
            }


            return paginationModel;
        }
    }
}
