using System;

namespace CleanArchitectureProject.Domain.Entities;

public class UsersModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public Guid CarId { get; set; }
    public string Password { get; set; }
    public string UserName { get; set; }

    // Navigation property
    public CarsModel Car { get; set; } 


    public static bool IsValidUserName(string userName)
    {
        // En az 4, en fazla 20 karakter ve harf/rakam
        return !string.IsNullOrWhiteSpace(userName)
            && userName.Length >= 4
            && userName.Length <= 20
            && userName.All(char.IsLetterOrDigit);
    }

    public static bool IsValidPassword(string password)
    {
        // En az 8 karakter, bir büyük harf, bir rakam içeriyor mu?
        if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
            return false;
        return password.Any(char.IsUpper) && password.Any(char.IsDigit);
    }
}
