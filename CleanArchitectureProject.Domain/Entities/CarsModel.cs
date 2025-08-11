using System;

namespace CleanArchitectureProject.Domain.Entities;

public class CarsModel
{
        public Guid Id { get; set; }
        public string CarName { get; set; }
        // navigation property: 
        public ICollection<UsersModel> Users { get; set; }
}
