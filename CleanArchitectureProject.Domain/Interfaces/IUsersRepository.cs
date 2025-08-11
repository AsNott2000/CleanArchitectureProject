using System;
using CleanArchitectureProject.Domain.Entities;

namespace CleanArchitectureProject.Domain.Interfaces;

public interface IUsersRepository
{
    Task<bool> IsUserNameExistsAsync(string userName);
    Task AddAsync(UsersModel usersModel);
    Task<UsersModel> GetByIdAsync(Guid id);
    Task<List<UsersModel>> GetAllAsync();
    // Additional methods can be added here
}
