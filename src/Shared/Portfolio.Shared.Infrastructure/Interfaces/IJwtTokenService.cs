
using Portfolio.Shared.Contracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Shared.Infrastructure.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateAccessToken(UserDto user);

        string GenerateRefreshToken();
    }
}
