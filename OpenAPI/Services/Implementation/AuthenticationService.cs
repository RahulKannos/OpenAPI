using Dapper;
using Microsoft.IdentityModel.Tokens;
using OpenAPI.Common;
using OpenAPI.Modesl.Entities;
using OpenAPI.Modesl.Responses;
using OpenAPI.Services.Interfaces;
using System.Data.SQLite;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OpenAPI.Services.Implementation
{
    public class AuthenticationService : IAuthentication
    {
        private readonly IConfiguration _configuration;
        private readonly SQLiteConnection _connection;


        public AuthenticationService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new SQLiteConnection(Helper.connectionString);
        }
        public LoginResponse login(LoginModel login)
        {
            LoginResponse response = new LoginResponse() { status = false, message = "Login failed" };
            try
            {
                UserModel user = _connection.Query<UserModel>($"select * from Users where Name='{login.userName}' and Password='{login.password}'").FirstOrDefault();
                if (user != null)
                {
                    if (user.Status == 0)
                    {
                        response.status = false;
                        response.message = "User De-activated contact with Admin";
                    }
                    else
                    {
                        response.status = true;
                        response.userName = user.Name;
                        response.message = "Login Successfully";
                        response.token = GenerateJwtToken(login.userName);
                    }
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }
        private string GenerateJwtToken(string userName)
        {
            // Define security key
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            // Define signing credentials
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Add claims
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, userName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            // Create the token
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);

            // Return the token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
