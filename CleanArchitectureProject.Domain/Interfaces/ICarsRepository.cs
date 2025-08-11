using System;
using CleanArchitectureProject.Domain.Entities;

namespace CleanArchitectureProject.Domain.Interfaces;

public interface ICarsRepository
{
    Task<bool> IsCarNameExistsAsync(string userName);
    Task AddAsync(CarsModel carsModel);
    Task<CarsModel> GetByIdAsync(Guid id);
    Task<List<CarsModel>> GetAllAsync();
    //ek methodlar gelecek
}
