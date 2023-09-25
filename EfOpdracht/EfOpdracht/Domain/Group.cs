using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfOpdracht.Domain
{
    public class Group
    {
        public int Id { get; set; }
        public int GroupCode { get; set; }

        public ICollection<Student> Students { get; set; } 

        public ICollection<Lesson> Lessons { get; set; }
    }
}
