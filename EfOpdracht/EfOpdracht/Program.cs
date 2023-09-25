// See https://aka.ms/new-console-template for more information
using EfOpdracht.Domain;
using EfOpdracht.Infrastructure;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

using (var context = new SchoolContext())
{

    var std = context.Students;
    Console.WriteLine(std.First().FirstName);

    Group group = context.Groups.First();
    Student student = group.Students.First();
    Console.WriteLine(student.FirstName);

    Lesson lesson = context.Lessons.Include(l => l.Groups).First();
    Student studentTwo = lesson.Groups.First().Students.First();
    Console.WriteLine(studentTwo.FirstName);

    Teacher teacher = context.Teachers.First();
    Lesson lessonTwo = new Lesson { Subject = "Flying"};
    lessonTwo.Teachers.Add(teacher);

    context.Lessons.Add(lessonTwo);
    await context.SaveChangesAsync();

}