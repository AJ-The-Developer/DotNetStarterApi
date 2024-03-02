using Application.Users.Common.Models;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application;
public record GetUserByIdQuery : IRequest<UserVM>
{
	public Guid Id { get; set; }
};

public record GetUsersQueryHandler(UserManager<User> userManager) : IRequestHandler<GetUserByIdQuery, UserVM>
{
	public async Task<UserVM> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
	{
		var user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);

		return new UserVM {
			Id = user.Id,
			FirstName = user.FirstName,
			LastName = user.LastName,
			Email = user.Email,
			PhoneNumber = user.PhoneNumber
		};
	}
}
