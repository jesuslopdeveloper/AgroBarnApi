using AgroBarn.Domain.ApiModels.V1.Response;

using System.Collections.Generic;

namespace AgroBarn.Domain.ApiModels.V1.Result
{
    public class UserResult
    {
        public int UserId { get; set; }

        public int PeopleId { get; set; }

        public string Name { get; set; }

        public string FirstSurname { get; set; }

        public string SecondSurname { get; set; }

        public string PhoneNumber { get; set; }

        public string Status { get; set; }

        public bool Success { get; set; }

        public int CodeError { get; set; }

        public List<RoleResponse> UserRoles { get; set; }

        public List<ErrorModel> Errors { get; set; }
    }
}
