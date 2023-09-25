using EfOpdracht.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfOpdracht.Infrastructure
{
    public class SchoolContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Group> Groups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=SchoolDb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>().HasIndex(o => o.StudentNumber).IsUnique();
            modelBuilder.Entity<Teacher>().HasIndex(o => o.EmployeeNumber).IsUnique();

            Group groupOne = new Group { Id = 1, GroupCode = 404 };
            modelBuilder.Entity<Group>().HasData(groupOne);

            Teacher teacherOne = new Teacher { Id = 1, EmployeeNumber = 400, Firstname = "Fatalis", Lastname = "Elder" };
            modelBuilder.Entity<Teacher>().HasData(teacherOne);

            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, StudentNumber = 10, FirstName = "Rathalos", LastName = "Rath", BirthDate = new DateTime(1999, 5, 20), GroupId = 1, TeacherId = 1 }
                );


        }
    }
}
