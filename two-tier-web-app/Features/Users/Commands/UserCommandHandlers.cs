using Dapper;
using MediatR;
using two_tier_web_app.Features.Users.Commands;
using two_tier_web_app.Infrastructure;

namespace two_tier_web_app.Features.Users.Commands;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
{
    private readonly IDbConnectionFactory _connectionFactory;

    public CreateUserCommandHandler(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        const string sql = @"
            INSERT INTO Users (FirstName, LastName, Email, Phone, CreatedDate, IsActive)
            VALUES (@FirstName, @LastName, @Email, @Phone, @CreatedDate, @IsActive);
            SELECT CAST(SCOPE_IDENTITY() as int);";

        using var connection = _connectionFactory.CreateConnection();
        
        var userId = await connection.QuerySingleAsync<int>(sql, new
        {
            request.User.FirstName,
            request.User.LastName,
            request.User.Email,
            request.User.Phone,
            CreatedDate = DateTime.UtcNow,
            IsActive = true
        });

        return userId;
    }
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
{
    private readonly IDbConnectionFactory _connectionFactory;

    public UpdateUserCommandHandler(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        const string sql = @"
            UPDATE Users 
            SET FirstName = @FirstName, 
                LastName = @LastName, 
                Email = @Email, 
                Phone = @Phone, 
                UpdatedDate = @UpdatedDate
            WHERE Id = @Id AND IsActive = 1";

        using var connection = _connectionFactory.CreateConnection();
        
        var rowsAffected = await connection.ExecuteAsync(sql, new
        {
            request.User.Id,
            request.User.FirstName,
            request.User.LastName,
            request.User.Email,
            request.User.Phone,
            UpdatedDate = DateTime.UtcNow
        });

        return rowsAffected > 0;
    }
}

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IDbConnectionFactory _connectionFactory;

    public DeleteUserCommandHandler(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        const string sql = "UPDATE Users SET IsActive = 0, UpdatedDate = @UpdatedDate WHERE Id = @Id";

        using var connection = _connectionFactory.CreateConnection();
        
        var rowsAffected = await connection.ExecuteAsync(sql, new
        {
            Id = request.Id,
            UpdatedDate = DateTime.UtcNow
        });

        return rowsAffected > 0;
    }
}
