using InstructionRAG.Application.Interfaces;
using InstructionRAG.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace InstructionRAG.Application.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    public async Task<User?> GetUserByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return user;
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        var user = await _userRepository.GetByUsernameAsync(username);
        return user;
    }

    public async Task<bool> CreateUserAsync(string username, string password)
    {
        var user = new User
        {
            UserName = username,
        };

        var passwordHasher = new PasswordHasher<User>();
        passwordHasher.HashPassword(user, password);
        
         /*
         * по моему тут как-то неправильно сделано, нужно подумать как сделать лучше
         * возвращаемое значение
         */
         var count = await _userRepository.CreateAsync(user);
        return count;
    }

    // TODO: реализовать
    public Task UpdateUserAsync(User user)
    {
        throw new NotImplementedException();
    }
}