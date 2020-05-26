using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.SystemCourse.Pagination
{
    public class PaginationModel
    {
        public List<IDictionary<string, object>> ListRows { get; set; }

        public int TotalRows { get; set; }
        public int NumberPages { get; set; }
    }
}
