using Application;
using Application.Users.Commands;
using Application.Users.Common.Models;
using Application.Users.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebApi;

[ApiController]
[Route("api/[controller]")]
public class UserController : BaseController
{
	[HttpGet]
	public async Task<ActionResult<List<UserVM>>> GetAll()
	{
		var users = await mediator.Send(new GetUsersQuery());

		return Ok(users);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<UserVM>> GetById(Guid id)
	{
		var user = await mediator.Send(new GetUserByIdQuery { Id = id });

		return Ok(user);
	}

	[HttpPost]
	public async Task<IActionResult> Create(CreateUserCommand command)
	{
		await mediator.Send(command);

		return NoContent();
	}

	[HttpPatch]
	public async Task<IActionResult> Update(UpdateUserCommand command)
	{
		await mediator.Send(command);

		return NoContent();
	}

	[HttpPatch("disable/{id}")]
	public async Task<IActionResult> Disable(Guid id)
	{
		await mediator.Send(new DisableUserCommand { Id = id });

		return NoContent();
	}

	[HttpPatch("enable/{id}")]
	public async Task<IActionResult> Enable(Guid id)
	{
		await mediator.Send(new EnableUserCommand { Id = id });

		return NoContent();
	}
}