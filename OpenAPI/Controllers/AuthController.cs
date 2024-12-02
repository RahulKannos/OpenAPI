using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenAPI.Modesl.Entities;
using OpenAPI.Modesl.Responses;
using OpenAPI.Services.Interfaces;

namespace OpenAPI.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthentication _authentication;
        public AuthController(IAuthentication authentication)
        {
            _authentication = authentication;
        }
        [HttpPost, Route("login")]
        public IActionResult login(LoginModel login)
        {
            LoginResponse loginResponse = null;
            if (string.IsNullOrEmpty(login.userName) || string.IsNullOrEmpty(login.password))
            {
                loginResponse = new LoginResponse() { status = false, message = "Please enter User Name or Password" };
            }
            else
            {
                loginResponse = _authentication.login(login);
            }
            return Ok(loginResponse);
        }
    }
}
