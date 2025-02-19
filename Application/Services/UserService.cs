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

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        return user;
    }

    public async Task<bool> CreateUserAsync(string email, string passwordHash)
    {
        var user = new User
        {
            Email = email,
            PasswordHash = passwordHash 
        };

         /*
         * TODO: по моему тут как-то неправильно сделано, нужно подумать как сделать лучше
         *       возвращаемое значение
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