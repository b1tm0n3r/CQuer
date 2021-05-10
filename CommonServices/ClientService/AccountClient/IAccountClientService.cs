using System.Net;
using System.Threading.Tasks;
using Common.DTOs;
using RestSharp;

namespace CommonServices.ClientService.AccountClient
{
    public interface IAccountClientService
    {
        Task<IRestResponse> LoginResponse(LoginDto loginDto);
        Task<IRestResponse> RegisterResponse(RegisterDto loginDto);
        Task<UserDto> GetUserDtoById(int id);
        string GetUserLocation(IRestResponse response);
    }
}