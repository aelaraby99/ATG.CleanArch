using CleanArch.ATG.Application.DTO;
using CleanArch.ATG.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.ATG.Application.Interfaces.JWT
{
    public interface IJwtTokenService
    {
        string GenerateToken( UserApplication user );
    }
}
