using MinimalAPIWithAuthentication.Enums;

namespace MinimalAPIWithAuthentication.Entities
{
    public class User
    {
        public string? Name { get; set; }
        public string? Password { get; set; }
        public Role Role { get; set; }
    }
}
