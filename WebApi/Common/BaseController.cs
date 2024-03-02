using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi;

public class BaseController : ControllerBase
{
	private IMediator? _mediator;

	protected IMediator mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;
}