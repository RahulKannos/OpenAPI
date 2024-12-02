using Dapper;
using OpenAPI.Common;
using OpenAPI.Modesl.Entities;
using OpenAPI.Modesl.Responses;
using OpenAPI.Services.Interfaces;
using System.Data.SQLite;

namespace OpenAPI.Services.Implementation
{
    public class UserService : IUser
    {
        private readonly SQLiteConnection _connection;

        public UserService()
        {
            _connection = new SQLiteConnection(Helper.connectionString);
        }

        public ApiResponse<UserModel> AddEditUser(UserModel user)
        {
            ApiResponse<UserModel> response = new ApiResponse<UserModel>();
            try
            {
                string query = string.Empty;
                if (string.IsNullOrEmpty(user.Id))
                {
                    user.Id = Guid.NewGuid().ToString();
                    query = "INSERT INTO " +
                        " Users (Id,Name,Email,Password,Age,CreatedOn,ModifiedOn,CreatedBy,ModifiedBy,Status) " +
                        " VALUES(@Id,@Name,@Email,@Password,@Age,@CreatedOn,@ModifiedOn,@CreatedBy,@ModifiedBy,@Status);";
                }
                else
                    query = "UPDATE Users " +
                        " SET Id = @Id,Name = @Name,Email = @Email,Password = @Password,Age = @Age,CreatedOn = @CreatedOn, " +
                        " ModifiedOn = @ModifiedOn,CreatedBy = @CreatedBy,ModifiedBy = @ModifiedBy,Status = @Status WHERE Id = @Id";

                using (SQLiteConnection con = new SQLiteConnection(Helper.connectionString))
                {

                    int status = con.Execute(query, user);
                    if (status > 0)
                    {
                        response = new ApiResponse<UserModel>(true, "User Saved successfuly");
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                response = new ApiResponse<UserModel>(false, ex.Message);
            }
            return response;
        }

        public ApiResponse<UserModel> DeleteUserById(string id)
        {
            ApiResponse<UserModel> response = new ApiResponse<UserModel>();
            try
            {
                ;
                if (!string.IsNullOrEmpty(id))
                {
                    int status = _connection.Execute($"update Users set Status=0 where Id='{id}'");
                    if (status > 0)
                    {
                        response = new ApiResponse<UserModel>(true, "User Deleted successfuly");
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                response = new ApiResponse<UserModel>(false, ex.Message);
            }
            return response;
        }

        public ApiResponse<UserModel> GetUserById(string id)
        {
            ApiResponse<UserModel> response = new ApiResponse<UserModel>();
            try
            {
                UserModel user = _connection.Query<UserModel>($"select * from Users where Id = '{id}'").FirstOrDefault() ?? new UserModel();
                if (user != null && !string.IsNullOrEmpty(user.Id))
                {
                    response = new ApiResponse<UserModel>(data: user, "User Fetch succesfully");
                    return response;
                }

            }
            catch (Exception ex)
            {
                response = new ApiResponse<UserModel>(false, ex.Message);
            }
            return response;
        }

        public ApiResponse<List<UserModel>> GetUsers()
        {
            ApiResponse<List<UserModel>> response = new ApiResponse<List<UserModel>>(false, "Users not exists");
            try
            {
                List<UserModel> users = _connection.Query<UserModel>("select * from Users where Status=1 order by name").ToList();
                if (users != null && users.Count > 0)
                {
                    response = new ApiResponse<List<UserModel>>(data: users, "User List Fetch succesfully");
                    return response;
                }

            }
            catch (Exception ex)
            {
                response = new ApiResponse<List<UserModel>>(false, ex.Message);
            }
            return response;
        }
    }
}
