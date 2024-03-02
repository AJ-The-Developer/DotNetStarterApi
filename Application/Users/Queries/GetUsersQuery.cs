using Application.Users.Common.Models;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Queries;

public record GetUsersQuery : IRequest<List<UserVM>>;

public record GetUsersQueryHandler(UserManager<User> userManager) : IRequestHandler<GetUsersQuery, List<UserVM>>
{
	public async Task<List<UserVM>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
	{
		var users = await userManager.Users.ToListAsync(cancellationToken);

		return users.Select(x => new UserVM {
			Id = x.Id,
			FirstName = x.FirstName,
			LastName = x.LastName,
			Email = x.Email,
			PhoneNumber = x.PhoneNumber
		}).ToList();
	}
}