using MinimalAPIWithAuthentication.Entities;

namespace MinimalAPIWithAuthentication.DTOs
{
    public class UserResponseDTO
    {
        public string Token { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }
}
