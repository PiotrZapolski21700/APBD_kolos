using APDB_Kolokwium_template.Models;
using Microsoft.EntityFrameworkCore;

namespace APDB_Kolokwium_template.Data;

public class AppDbContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Student)
            .WithMany(s => s.Enrollments)
            .HasForeignKey(e => e.Student_ID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Course)
            .WithMany(c => c.Enrollments)
            .HasForeignKey(e => e.Course_ID)
            .OnDelete(DeleteBehavior.Cascade);
        
        var students = new List<Student>
        {
            new()
            {
                ID = 1,
                FirstName = "Anna",
                LastName = "Nowak",
                Email = "anna.nowak@example.edu"
            },
            new()
            {
                ID = 2,
                FirstName = "Tomasz",
                LastName = "Wiśniewski",
                Email = "tomasz.w@example.edu"
            },
            new()
            {
                ID = 3,
                FirstName = "Katarzyna",
                LastName = "Kowalska",
                Email = "katarzyna.kowalska@example.edu"
            }
        };

        var courses = new List<Course>
        {
            new()
            {
                ID = 101,
                Title = "Wprowadzenie do Algorytmów",
                Credits = "3 ECTS",
                Teacher = "dr Kowalski"
            },
            new()
            {
                ID = 102,
                Title = "Bazy Danych",
                Credits = "4 ECTS",
                Teacher = "mgr Nowicka"
            },
            new()
            {
                ID = 103,
                Title = "Programowanie Obiektowe",
                Credits = "5 ECTS",
                Teacher = "dr inż. Wiśniewski"
            }
        };

        var enrollments = new List<Enrollment>
        {
            new()
            {
                Student_ID = 1,
                Course_ID = 101,
                EnrollmentDate = new DateTime(2024, 10, 1, 10, 0, 0)
            },
            new()
            {
                Student_ID = 2,
                Course_ID = 102,
                EnrollmentDate = new DateTime(2024, 10, 2, 9, 30, 0)
            },
            new()
            {
                Student_ID = 1,
                Course_ID = 102,
                EnrollmentDate = new DateTime(2024, 10, 3, 14, 15, 0)
            },
            new()
            {
                Student_ID = 3,
                Course_ID = 103,
                EnrollmentDate = new DateTime(2024, 10, 5, 11, 0, 0)
            }
        };
        
        modelBuilder.Entity<Student>().HasData(students);
        modelBuilder.Entity<Course>().HasData(courses);
        modelBuilder.Entity<Enrollment>().HasData(enrollments);
    }
} 