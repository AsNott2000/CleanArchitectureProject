using System;

namespace CleanArchitectureProject.Domain.Services;

public interface IUserValidation
{
    bool IsValidUserName(string userName);
    bool IsValidPassword(string password);
}
