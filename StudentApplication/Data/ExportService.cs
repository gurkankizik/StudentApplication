using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using StudentApplication.Data;

public class ExportService
{
    private readonly StudentDbContext _context;

    public ExportService(StudentDbContext context)
    {
        _context = context;
    }

    public byte[] ExportStudentsToExcel()
    {
        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Students");
            var currentRow = 1;

            // Header
            worksheet.Cell(currentRow, 1).Value = "Id";
            worksheet.Cell(currentRow, 2).Value = "Name";
            worksheet.Cell(currentRow, 3).Value = "Surname";
            worksheet.Cell(currentRow, 4).Value = "Faculty";
            worksheet.Cell(currentRow, 5).Value = "Department";
            worksheet.Cell(currentRow, 6).Value = "StudentNumber";
            worksheet.Cell(currentRow, 7).Value = "GPA";
            worksheet.Cell(currentRow, 8).Value = "IsDeleted";

            // Data
            foreach (var student in _context.Students.IgnoreQueryFilters().ToList())
            {
                currentRow++;
                worksheet.Cell(currentRow, 1).Value = student.Id;
                worksheet.Cell(currentRow, 2).Value = student.Name;
                worksheet.Cell(currentRow, 3).Value = student.Surname;
                worksheet.Cell(currentRow, 4).Value = student.Faculty;
                worksheet.Cell(currentRow, 5).Value = student.Department;
                worksheet.Cell(currentRow, 6).Value = student.StudentNumber;
                worksheet.Cell(currentRow, 7).Value = student.GPA;
                worksheet.Cell(currentRow, 8).Value = student.IsDeleted;
            }

            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                return stream.ToArray();
            }
        }
    }
}
