using System;

namespace CleanArchitectureProject.Domain.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(Guid userId, string userName, IEnumerable<KeyValuePair<string,string>>? extraClaims = null);
}
