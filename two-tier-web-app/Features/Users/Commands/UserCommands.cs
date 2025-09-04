using MediatR;
using two_tier_web_app.Models;

namespace two_tier_web_app.Features.Users.Commands;

public record CreateUserCommand(CreateUserRequest User) : IRequest<int>;

public record UpdateUserCommand(UpdateUserRequest User) : IRequest<bool>;

public record DeleteUserCommand(int Id) : IRequest<bool>;
