using Dapper;
using MediatR;
using two_tier_web_app.Features.Users.Queries;
using two_tier_web_app.Infrastructure;
using two_tier_web_app.Models;

namespace two_tier_web_app.Features.Users.Queries;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<User>>
{
    private readonly IDbConnectionFactory _connectionFactory;

    public GetAllUsersQueryHandler(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        const string sql = @"
            SELECT Id, FirstName, LastName, Email, Phone, CreatedDate, UpdatedDate, IsActive
            FROM Users 
            WHERE IsActive = 1
            ORDER BY CreatedDate DESC";

        using var connection = _connectionFactory.CreateConnection();
        
        var users = await connection.QueryAsync<User>(sql);
        return users;
    }
}

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User?>
{
    private readonly IDbConnectionFactory _connectionFactory;

    public GetUserByIdQueryHandler(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<User?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        const string sql = @"
            SELECT Id, FirstName, LastName, Email, Phone, CreatedDate, UpdatedDate, IsActive
            FROM Users 
            WHERE Id = @Id AND IsActive = 1";

        using var connection = _connectionFactory.CreateConnection();
        
        var user = await connection.QueryFirstOrDefaultAsync<User>(sql, new { Id = request.Id });
        return user;
    }
}

public class GetUsersByEmailQueryHandler : IRequestHandler<GetUsersByEmailQuery, IEnumerable<User>>
{
    private readonly IDbConnectionFactory _connectionFactory;

    public GetUsersByEmailQueryHandler(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<User>> Handle(GetUsersByEmailQuery request, CancellationToken cancellationToken)
    {
        const string sql = @"
            SELECT Id, FirstName, LastName, Email, Phone, CreatedDate, UpdatedDate, IsActive
            FROM Users 
            WHERE Email LIKE @Email AND IsActive = 1
            ORDER BY CreatedDate DESC";

        using var connection = _connectionFactory.CreateConnection();
        
        var users = await connection.QueryAsync<User>(sql, new { Email = $"%{request.Email}%" });
        return users;
    }
}
