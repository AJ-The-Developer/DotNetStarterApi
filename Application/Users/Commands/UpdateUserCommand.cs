using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application;

public class UpdateUserCommand : IRequest
{
	public Guid Id { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string PhoneNumber { get; set; }
	public string Email { get; set; }
	public string Role { get; set; }
}


public class CreateUserCommandHandler(UserManager<User> userManager) : IRequestHandler<UpdateUserCommand>
{
	public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
	{
		var user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);

		user.FirstName = request.FirstName;
		user.LastName = request.LastName;
		user.Email = request.Email;
		user.PhoneNumber = request.PhoneNumber;
		user.UserName = request.Email;

		await userManager.UpdateAsync(user);

		var currentRole = await userManager.GetRolesAsync(user);

		await userManager.RemoveFromRoleAsync(user, currentRole[0]);

		await userManager.AddToRoleAsync(user, request.Role);
	}
}
