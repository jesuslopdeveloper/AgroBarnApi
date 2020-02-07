using System;
using System.Collections.Generic;
using System.Text;

namespace AgroBarn.Domain.ApiModels.V1.Response
{
    public class UserResponse
    {
        public int UserId { get; set; }

        public int PeopleId { get; set; }

        public string Name { get; set; }

        public string FirstSurname { get; set; }

        public string SecondSurname { get; set; }

        public string PhoneNumber { get; set; }

        public string Status { get; set; }

        public IEnumerable<RoleResponse> UserRoles { get; set; }
    }
}
