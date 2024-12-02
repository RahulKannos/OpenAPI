using OpenAPI.Modesl.Entities;
using OpenAPI.Modesl.Responses;

namespace OpenAPI.Services.Interfaces
{
    public interface IUser
    {
        ApiResponse<List<UserModel>> GetUsers();
        ApiResponse<UserModel> GetUserById(string id);
        ApiResponse<UserModel> AddEditUser(UserModel user);
        ApiResponse<UserModel> DeleteUserById(string id);
    }
}
