using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfOpdracht.Domain
{
    public class Student
    {
        public int Id { get; set; }
        public int StudentNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime BirthDate { get; set; }

        public int GroupId { get; set; }
        public Group? Group { get; set; }

        public int TeacherId { get; set; }
        public Teacher? Teacher { get; set; }

    }
}
