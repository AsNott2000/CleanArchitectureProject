using System;
using CleanArchitectureProject.Domain.Entities;
using CleanArchitectureProject.Domain.Interfaces;
using CleanArchitectureProject.Infrastructure.Presistence;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureProject.Infrastructure.Repositories;

public class CarsRepository : ICarsRepository
{
    private readonly AppDbContext _context;
    public CarsRepository(AppDbContext context) => _context = context;

    public async Task AddAsync(CarsModel car)
    {
        _context.Cars.Add(car);
        await _context.SaveChangesAsync();
    }

    public Task<List<CarsModel>> GetAllAsync()
        => _context.Cars.AsNoTracking().ToListAsync();

    public Task<CarsModel?> GetByIdAsync(Guid id)
        => _context.Cars.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

    public Task<bool> IsCarNameExistsAsync(string carName)
        => _context.Cars.AsNoTracking().AnyAsync(x => x.CarName == carName);
}
