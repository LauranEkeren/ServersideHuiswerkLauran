using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfOpdracht.Domain
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Subject { get; set; }

        public ICollection<Group> Groups { get; set; } = new List<Group>();

        public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
    }
}
