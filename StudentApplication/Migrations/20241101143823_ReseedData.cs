using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentApplication.Migrations
{
    /// <inheritdoc />
    public partial class ReseedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insert seed data for Student entity
            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Name", "Surname", "Faculty", "Department", "StudentNumber", "GPA" },
                values: new object[] { 1, "Gürkan", "Kızık", "Engineering", "Computer Engineering", "202103001099", 2.80 }
            );

            // Insert seed data for Grade entity
            migrationBuilder.InsertData(
                table: "Grades",
                columns: new[] { "Id", "StudentId", "CourseName", "Score" },
                values: new object[,]
                {
                    { 1, 1, "Math", 85 },
                    { 2, 1, "Science", 90 }
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove seed data for Grade entity
            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: 1
            );

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: 2
            );

            // Remove seed data for Student entity
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1
            );
        }
    }
}
