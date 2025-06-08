using APDB_Kolokwium_template.Data;
using APDB_Kolokwium_template.DTOs;
using APDB_Kolokwium_template.Models;
using Microsoft.EntityFrameworkCore;

namespace APDB_Kolokwium_template.Services;

public class DbService : IDbService
{
    private readonly AppDbContext _context;

    public DbService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<EnrollmentGetDto>> GetEnrollmentsAsync()
    {
        var enrollments = await _context.Enrollments
            .Include(e => e.Student)
            .Include(e => e.Course)
            .OrderBy(e => e.EnrollmentDate)
            .Select(e => new EnrollmentGetDto
            {
                Student = new StudentDto
                {
                    Id = e.Student.ID,
                    FirstName = e.Student.FirstName,
                    LastName = e.Student.LastName,
                    Email = e.Student.Email
                },
                Course = new CourseDto
                {
                    Id = e.Course.ID,
                    Title = e.Course.Title,
                    Teacher = e.Course.Teacher
                },
                EnrollmentDate = e.EnrollmentDate
            })
            .ToListAsync();

        return enrollments;
    }

    public async Task<CourseCreatedResponseDto> CreateCourseWithEnrollmentsAsync(CourseCreateDto courseData)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        
        try
        {
            // Tworzenie nowego kursu
            var newCourse = new Course
            {
                Title = courseData.Title,
                Credits = courseData.Credits,
                Teacher = courseData.Teacher
            };

            await _context.Courses.AddAsync(newCourse);
            await _context.SaveChangesAsync();

            var enrollmentsList = new List<EnrollmentResponseDto>();
            var enrollmentDate = DateTime.Now;

            // Przetwarzanie studentów
            foreach (var studentData in courseData.Students)
            {
                // Sprawdzenie czy student już istnieje
                var existingStudent = await _context.Students
                    .FirstOrDefaultAsync(s => 
                        s.FirstName == studentData.FirstName && 
                        s.LastName == studentData.LastName && 
                        s.Email == studentData.Email);

                Student student;
                
                if (existingStudent != null)
                {
                    // Student istnieje - używamy istniejącego
                    student = existingStudent;
                }
                else
                {
                    // Student nie istnieje - tworzymy nowego
                    student = new Student
                    {
                        FirstName = studentData.FirstName,
                        LastName = studentData.LastName,
                        Email = studentData.Email
                    };
                    
                    await _context.Students.AddAsync(student);
                    await _context.SaveChangesAsync();
                }

                // Sprawdzenie czy student nie jest już zapisany na ten kurs
                var existingEnrollment = await _context.Enrollments
                    .AnyAsync(e => e.Student_ID == student.ID && e.Course_ID == newCourse.ID);

                if (!existingEnrollment)
                {
                    // Tworzenie wpisu w tabeli Enrollment
                    var enrollment = new Enrollment
                    {
                        Student_ID = student.ID,
                        Course_ID = newCourse.ID,
                        EnrollmentDate = enrollmentDate
                    };

                    await _context.Enrollments.AddAsync(enrollment);
                }

                // Dodanie do listy odpowiedzi
                enrollmentsList.Add(new EnrollmentResponseDto
                {
                    StudentId = student.ID,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Email = student.Email,
                    EnrollmentDate = enrollmentDate
                });
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            // Przygotowanie odpowiedzi
            return new CourseCreatedResponseDto
            {
                Message = "Kurs został utworzony i studenci zostali zapisani.",
                Course = new CourseResponseDto
                {
                    Id = newCourse.ID,
                    Title = newCourse.Title,
                    Credits = newCourse.Credits,
                    Teacher = newCourse.Teacher
                },
                Enrollments = enrollmentsList
            };
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
