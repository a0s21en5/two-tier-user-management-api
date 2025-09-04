using MediatR;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using two_tier_web_app.Features.Users.Commands;
using two_tier_web_app.Features.Users.Queries;
using two_tier_web_app.Models;

namespace two_tier_web_app.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<CreateUserRequest> _createUserValidator;
    private readonly IValidator<UpdateUserRequest> _updateUserValidator;

    public UsersController(
        IMediator mediator,
        IValidator<CreateUserRequest> createUserValidator,
        IValidator<UpdateUserRequest> updateUserValidator)
    {
        _mediator = mediator;
        _createUserValidator = createUserValidator;
        _updateUserValidator = updateUserValidator;
    }

    /// <summary>
    /// Get all active users
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
    {
        var users = await _mediator.Send(new GetAllUsersQuery());
        return Ok(users);
    }

    /// <summary>
    /// Get user by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUserById(int id)
    {
        if (id <= 0)
            return BadRequest("Invalid user ID");

        var user = await _mediator.Send(new GetUserByIdQuery(id));
        
        if (user == null)
            return NotFound($"User with ID {id} not found");

        return Ok(user);
    }

    /// <summary>
    /// Search users by email
    /// </summary>
    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<User>>> SearchUsersByEmail([FromQuery] string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return BadRequest("Email parameter is required");

        var users = await _mediator.Send(new GetUsersByEmailQuery(email));
        return Ok(users);
    }

    /// <summary>
    /// Create a new user
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<int>> CreateUser([FromBody] CreateUserRequest request)
    {
        var validationResult = await _createUserValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
        }

        try
        {
            var userId = await _mediator.Send(new CreateUserCommand(request));
            return CreatedAtAction(nameof(GetUserById), new { id = userId }, userId);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error creating user: {ex.Message}");
        }
    }

    /// <summary>
    /// Update an existing user
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateUser(int id, [FromBody] UpdateUserRequest request)
    {
        if (id != request.Id)
            return BadRequest("ID in URL does not match ID in request body");

        var validationResult = await _updateUserValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
        }

        try
        {
            var success = await _mediator.Send(new UpdateUserCommand(request));
            
            if (!success)
                return NotFound($"User with ID {id} not found");

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error updating user: {ex.Message}");
        }
    }

    /// <summary>
    /// Delete a user (soft delete)
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        if (id <= 0)
            return BadRequest("Invalid user ID");

        try
        {
            var success = await _mediator.Send(new DeleteUserCommand(id));
            
            if (!success)
                return NotFound($"User with ID {id} not found");

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error deleting user: {ex.Message}");
        }
    }
}
