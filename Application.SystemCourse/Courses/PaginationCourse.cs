using MediatR;
using Persistence.SystemCourse.Pagination;
using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.SystemCourse.Courses
{
    public class PaginationCourse
    {
        public class Eject : IRequest<PaginationModel>
        {
            public string Title { get; set; }
            public int NumberPage { get; set; }
            public int QuantityElements { get; set; }
        }

        public class Handler : IRequestHandler<Eject, PaginationModel>
        {
            private readonly IPagination _pagination;

            public Handler(IPagination pagination)
            {
                _pagination = pagination;
            }

            public async Task<PaginationModel> Handle (Eject request, CancellationToken cancellationToken)
            {
                var storedProcedure = "usp_Obtain_Course_Pagination";
                var ordering = "Title";
                var paramaters = new Dictionary<string, object>();
                paramaters.Add("NameCourse", request.Title);
                return await _pagination.ReturnPagination(storedProcedure, request.NumberPage, request.QuantityElements, paramaters, ordering);
            }
        }
    }
}
