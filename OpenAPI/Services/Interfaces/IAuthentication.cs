using OpenAPI.Modesl.Entities;
using OpenAPI.Modesl.Responses;

namespace OpenAPI.Services.Interfaces
{
    public interface IAuthentication
    {
        LoginResponse login(LoginModel login);
    }
}
