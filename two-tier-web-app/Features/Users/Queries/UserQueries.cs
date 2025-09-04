using MediatR;
using two_tier_web_app.Models;

namespace two_tier_web_app.Features.Users.Queries;

public record GetAllUsersQuery() : IRequest<IEnumerable<User>>;

public record GetUserByIdQuery(int Id) : IRequest<User?>;

public record GetUsersByEmailQuery(string Email) : IRequest<IEnumerable<User>>;
