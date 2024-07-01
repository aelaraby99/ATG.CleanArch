using CleanArch.ATG.Domain.Entities.Identity;

namespace CleanArch.ATG.Application.Interfaces.JWT
{
    public interface IJwtTokenService
    {
        string GenerateToken( UserApplication user , IEnumerable<string> roles , IEnumerable<string> permissions );
    }
}
