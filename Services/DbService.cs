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
        if (string.IsNullOrWhiteSpace(courseData.Title) || 
            string.IsNullOrWhiteSpace(courseData.Credits) || 
            string.IsNullOrWhiteSpace(courseData.Teacher) ||
            courseData.Students == null || 
            !courseData.Students.Any())
        {
            throw new ArgumentException("Invalid course data");
        }

        using var transaction = await _context.Database.BeginTransactionAsync();
        
        try
        {
            var existingCourse = await _context.Courses
                .FirstOrDefaultAsync(c => 
                    c.Title.ToLower() == courseData.Title.ToLower() && 
                    c.Teacher.ToLower() == courseData.Teacher.ToLower());

            Course course;
            if (existingCourse != null)
            {
                course = existingCourse;
            }
            else
            {
                course = new Course
                {
                    Title = courseData.Title,
                    Credits = courseData.Credits,
                    Teacher = courseData.Teacher
                };
                await _context.Courses.AddAsync(course);
                await _context.SaveChangesAsync();
            }

            var enrollmentsList = new List<EnrollmentResponseDto>();
            var enrollmentDate = DateTime.Now;

            foreach (var studentData in courseData.Students)
            {
                if (string.IsNullOrWhiteSpace(studentData.FirstName) || 
                    string.IsNullOrWhiteSpace(studentData.LastName) || 
                    string.IsNullOrWhiteSpace(studentData.Email))
                {
                    throw new ArgumentException("Invalid student data");
                }

                var existingStudent = await _context.Students
                    .FirstOrDefaultAsync(s => 
                        s.FirstName.ToLower() == studentData.FirstName.ToLower() && 
                        s.LastName.ToLower() == studentData.LastName.ToLower() && 
                        s.Email.ToLower() == studentData.Email.ToLower());

                Student student;
                
                if (existingStudent != null)
                {
                    student = existingStudent;
                }
                else
                {
                    student = new Student
                    {
                        FirstName = studentData.FirstName,
                        LastName = studentData.LastName,
                        Email = studentData.Email
                    };
                    
                    await _context.Students.AddAsync(student);
                    await _context.SaveChangesAsync();
                }

                var existingEnrollment = await _context.Enrollments
                    .AnyAsync(e => e.Student_ID == student.ID && e.Course_ID == course.ID);

                if (!existingEnrollment)
                {
                    var enrollment = new Enrollment
                    {
                        Student_ID = student.ID,
                        Course_ID = course.ID,
                        EnrollmentDate = enrollmentDate
                    };

                    await _context.Enrollments.AddAsync(enrollment);
                }

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

            return new CourseCreatedResponseDto
            {
                Message = existingCourse != null 
                    ? "Studenci zostali zapisani na istniejący kurs." 
                    : "Kurs został utworzony i studenci zostali zapisani.",
                Course = new CourseResponseDto
                {
                    Id = course.ID,
                    Title = course.Title,
                    Credits = course.Credits,
                    Teacher = course.Teacher
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