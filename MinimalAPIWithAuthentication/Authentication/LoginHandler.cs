using Microsoft.AspNetCore.Mvc;
using MinimalAPIWithAuthentication.DTOs;
using MinimalAPIWithAuthentication.Enums;
using MinimalAPIWithAuthentication.Repository;

namespace MinimalAPIWithAuthentication.Authentication
{
    public static class LoginHandler
    {
        public static IResult HandleLogin(
            [FromBody] AuthenticationRequestBody authenticationRequestBody,
            ITokenGenerator tokenGenerator,
            IRepository repository)
        {
            var user = repository.Find(authenticationRequestBody.UserName, authenticationRequestBody.Password);
            if (user is null)
                return Results.NotFound(new {message = "Invalid username or password" });

            var token = tokenGenerator.GenerateToken(user);
            var userResponse = new UserResponseDTO
            {
                Token = token,
                Name = user.Name,
                Role = Enum.GetName(typeof(Role), user.Role)
            };
            return Results.Ok(userResponse);
        }
    }
}
