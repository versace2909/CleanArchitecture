using CA.Application.Interfaces;
using CA.Application.Models;
using CA.Domain.Entities;
using CA.Domain.Events;
using CA.Infrastructure;

namespace CA.Application.Implementations;

public class UserService(AppDbContext context) : IUserService
{
    public async Task CreateUserAsync(UserCreatedModel userCreatedModel)
    {
        var user = new User
        {
            UserName = userCreatedModel.UserName,
            UserEmail = userCreatedModel.UserEmail,
            Password = userCreatedModel.Password,
            FirstName = userCreatedModel.FirstName,
            LastName = userCreatedModel.LastName,
            PhoneNumber = userCreatedModel.PhoneNumber,
            Address = userCreatedModel.Address
        };

        user.DomainEvents.Add(new UserCreatedEvent());
        user.DomainEvents.Add(new ChangePasswordEvent());
        
        context.Users.Add(user);
        await context.SaveChangesAsync();
    }
}