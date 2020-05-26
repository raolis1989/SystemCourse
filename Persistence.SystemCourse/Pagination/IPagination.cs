using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.SystemCourse.Pagination
{
    public interface IPagination
    {
        Task<PaginationModel> ReturnPagination(string storedProcedure, int numberPage, int quantityElements, IDictionary<string, object> parameterFilter, string orderingColumn);
    }
}
