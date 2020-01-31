using System;
using System.Collections.Generic;
using System.Text;

namespace AgroBarn.Domain.Entities
{
    public class ConceptionDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Status { get; set; }

        public int UserCreate { get; set; }

        public DateTime DateCreate { get; set; }

        public int? UserModify { get; set; }

        public DateTime? DateModify { get; set; }
    }
}
