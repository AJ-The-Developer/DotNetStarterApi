using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Commands;

public record CreateUserCommand : IRequest
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string PhoneNumber { get; set; }
	public string Email { get; set; }
	public string Role { get; set; }
}

public class CreateUserCommandHandler(UserManager<User> userManager, IAuthService authService) : IRequestHandler<CreateUserCommand>
{
    public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User {
	        Id = Guid.NewGuid(),
			FirstName = request.FirstName,
			LastName = request.LastName,
			PhoneNumber = request.PhoneNumber,
			Email = request.Email,
			UserName = request.Email,
		};

		var password = authService.GeneratePassword();
		
		Console.WriteLine("Password: " + password);

		await userManager.CreateAsync(user, password);

		await userManager.AddToRoleAsync(user, request.Role);
    }
}
