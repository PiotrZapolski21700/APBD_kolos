using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APDB_Kolokwium_template.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Credits = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Teacher = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Enrollment",
                columns: table => new
                {
                    Student_ID = table.Column<int>(type: "int", nullable: false),
                    Course_ID = table.Column<int>(type: "int", nullable: false),
                    EnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollment", x => new { x.Student_ID, x.Course_ID });
                    table.ForeignKey(
                        name: "FK_Enrollment_Course_Course_ID",
                        column: x => x.Course_ID,
                        principalTable: "Course",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrollment_Student_Student_ID",
                        column: x => x.Student_ID,
                        principalTable: "Student",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Course",
                columns: new[] { "ID", "Credits", "Teacher", "Title" },
                values: new object[,]
                {
                    { 101, "3 ECTS", "dr Kowalski", "Wprowadzenie do Algorytmów" },
                    { 102, "4 ECTS", "mgr Nowicka", "Bazy Danych" },
                    { 103, "5 ECTS", "dr inż. Wiśniewski", "Programowanie Obiektowe" }
                });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "ID", "Email", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "anna.nowak@example.edu", "Anna", "Nowak" },
                    { 2, "tomasz.w@example.edu", "Tomasz", "Wiśniewski" },
                    { 3, "katarzyna.kowalska@example.edu", "Katarzyna", "Kowalska" }
                });

            migrationBuilder.InsertData(
                table: "Enrollment",
                columns: new[] { "Course_ID", "Student_ID", "EnrollmentDate" },
                values: new object[,]
                {
                    { 101, 1, new DateTime(2024, 10, 1, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 102, 1, new DateTime(2024, 10, 3, 14, 15, 0, 0, DateTimeKind.Unspecified) },
                    { 102, 2, new DateTime(2024, 10, 2, 9, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 103, 3, new DateTime(2024, 10, 5, 11, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_Course_ID",
                table: "Enrollment",
                column: "Course_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enrollment");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Student");
        }
    }
}
