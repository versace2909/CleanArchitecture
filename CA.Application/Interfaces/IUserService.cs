using CA.Application.Models;

namespace CA.Application.Interfaces;

public interface IUserService
{
    Task CreateUserAsync(UserCreatedModel userCreatedModel);
}