using System.ComponentModel.DataAnnotations;

namespace mamisum_api.Models.Users
{
    public class UpdatePasswordRequest
    {
        public string Email { get; set; } = string.Empty;
        public string CurrentPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}

