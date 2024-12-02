using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenAPI.Modesl.Entities;
using OpenAPI.Modesl.Responses;
using OpenAPI.Services.Interfaces;

namespace OpenAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _user;
        public UserController(IUser user)
        {
            _user = user;
        }
        [HttpGet, Route("GetAllUser")]
        public IActionResult Get()
        {
            ApiResponse<List<UserModel>> apiResponse = _user.GetUsers();
            return Ok(apiResponse);
        }

        [HttpGet, Route("GetUserById/{id}")]
        public IActionResult GetById(string id)
        {
            ApiResponse<UserModel> apiResponse = _user.GetUserById(id);
            return Ok(apiResponse);
        }
        [HttpPost, Route("AddEditUser")]
        public IActionResult AddEditUser(UserModel model)
        {
            ApiResponse<UserModel> apiResponse = _user.AddEditUser(model);
            return Ok(apiResponse);
        }
        [HttpDelete, Route("DeleteUserById/{id}")]
        public IActionResult DeleteUser(string id)
        {
            ApiResponse<UserModel> apiResponse = _user.DeleteUserById(id);
            return Ok(apiResponse);
        }
    }
}
