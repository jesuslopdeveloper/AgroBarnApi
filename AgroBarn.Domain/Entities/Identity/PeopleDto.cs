using System;

namespace AgroBarn.Domain.Entities
{
    public class PeopleDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; }

        public string FirstSurname { get; set; }

        public string SecondSurname { get; set; }

        public int Status { get; set; }

        public int UserCreate { get; set; }

        public DateTime DateCreate { get; set; }

        public int? UserModify { get; set; }

        public DateTime? DateModify { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
