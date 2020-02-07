using System.ComponentModel.DataAnnotations;

namespace AgroBarn.Domain.ApiModels.V1.Request
{
    public class UserChangePasswordRequest
    {
        [Required]
        public string OldPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }
    }
}
