using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;


namespace WebAPI.SystemCourse.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenericControllerBase :ControllerBase
    {
        private IMediator _mediator;

        protected IMediator mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());
    }
}