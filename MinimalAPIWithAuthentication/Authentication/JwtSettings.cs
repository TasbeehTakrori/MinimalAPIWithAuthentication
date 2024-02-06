using System.ComponentModel.DataAnnotations;

namespace MinimalAPIWithAuthentication.Authentication
{
    public class JwtSettings
    {
        [Required]
        public string Issuer { get; set; }

        [Required]
        public string Audience { get; set; }

        [Required]
        public int ExpiresMinutes { get; set; }

        [Required]
        public string Key { get; set; }
    }
}
