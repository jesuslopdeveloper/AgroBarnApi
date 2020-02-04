using System;

namespace AgroBarn.Domain.Entities
{
    public class MessageDto
    {
        public int Id { get; set; }

        public string Module { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public int Status { get; set; }

        public int UserCreate { get; set; }

        public DateTime DateCreate { get; set; }

        public int? UserModify { get; set; }

        public DateTime? DateModify { get; set; }
    }
}
