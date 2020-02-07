using System.ComponentModel.DataAnnotations;

namespace AgroBarn.Domain.ApiModels.V1.Request.Identity
{
    public class UserPatchRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string FistSurname { get; set; }

        public string SecondSurname { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
    }
}
