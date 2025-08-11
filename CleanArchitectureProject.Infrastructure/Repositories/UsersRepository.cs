using System;
using CleanArchitectureProject.Domain.Entities;
using CleanArchitectureProject.Domain.Interfaces;
using CleanArchitectureProject.Infrastructure.Presistence;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureProject.Infrastructure.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly AppDbContext _context;
    public UsersRepository(AppDbContext context) => _context = context;
    
    public async Task AddAsync(UsersModel users)
    {
        _context.Users.Add(users);
        await _context.SaveChangesAsync();
    }

    public Task<List<UsersModel>> GetAllAsync()
        => _context.Users.AsNoTracking().ToListAsync();

    public Task<UsersModel?> GetByIdAsync(Guid id)
        => _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

    public Task<UsersModel?> GetByUserNameAsync(string userName)
        => _context.Users.FirstOrDefaultAsync(x => x.UserName == userName);

    public Task<bool> IsUserNameExistsAsync(string userName)
        => _context.Users.AsNoTracking().AnyAsync(x => x.UserName == userName);
}
