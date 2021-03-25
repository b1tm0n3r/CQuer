using Common.DTOs;

namespace CommonServices.TokenService
{
    public interface ITokenService
    {
        string CreateToken(LoginDto loginDto);
    }
}