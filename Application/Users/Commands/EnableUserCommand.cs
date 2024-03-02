using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application;

public record EnableUserCommand : IRequest
{
	public Guid Id { get; set; }
}

public class EnableUserCommandHandler(UserManager<User> userManager) : IRequestHandler<EnableUserCommand>
{
	public async Task Handle(EnableUserCommand request, CancellationToken cancellationToken)
	{
		var user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);

		user.LockoutEnd = null;

		await userManager.UpdateAsync(user);
	}
}

