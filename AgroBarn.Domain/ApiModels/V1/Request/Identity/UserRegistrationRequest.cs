namespace AgroBarn.Domain.ApiModels.V1.Request
{
    public class UserRegistrationRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Name { get; set; }

        public string FirstSurname { get; set; }

        public string SecondSurname { get; set; }

        public string PhoneNumber { get; set; }
    }
}
