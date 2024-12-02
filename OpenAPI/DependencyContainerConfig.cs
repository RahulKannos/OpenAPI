using OpenAPI.Services.Implementation;
using OpenAPI.Services.Interfaces;

namespace OpenAPI
{
    public static class DependencyContainerConfig
    {
        public static IServiceCollection RegisterDependencyService(this IServiceCollection service)
        {
            service.AddSingleton<IUser, UserService>();
            service.AddSingleton<IAuthentication, AuthenticationService>();
            return service;
        }
    }
}
