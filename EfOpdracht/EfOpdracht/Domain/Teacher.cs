using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfOpdracht.Domain
{
    public class Teacher
    {
        public int Id { get; set; }
        public int EmployeeNumber { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; } 

        public ICollection<Student> Students { get; set; }

        public ICollection<Lesson> Lessons { get; set; }
    }
}
