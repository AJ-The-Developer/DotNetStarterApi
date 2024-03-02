using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application;

public record DisableUserCommand : IRequest
{
	public Guid Id { get; set; }
}

public class DisableUserCommandHandler(UserManager<User> userManager) : IRequestHandler<DisableUserCommand>
{
	public async Task Handle(DisableUserCommand request, CancellationToken cancellationToken)
	{
		var user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);

		user.LockoutEnd = DateTime.UtcNow.AddYears(100);

		await userManager.UpdateAsync(user);
	}
}
