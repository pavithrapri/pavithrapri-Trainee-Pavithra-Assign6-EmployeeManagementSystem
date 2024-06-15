// Services/ExcelService.cs
using EmployeeManagementSystems_A6.Models;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystems_A6.Services
{
    public class ExcelService
    {
        private readonly ICosmosDbService _cosmosDbService;

        public ExcelService(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        public async Task<byte[]> ExportEmployeesToExcel()
        {
            var employees = await _cosmosDbService.GetItemsAsync<EmployeeBasicDetails>("SELECT * FROM c");
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Employees");
                worksheet.Cells[1, 1].Value = "Sr.No";
                worksheet.Cells[1, 2].Value = "First Name";
                worksheet.Cells[1, 3].Value = "Last Name";
                worksheet.Cells[1, 4].Value = "Email";
                worksheet.Cells[1, 5].Value = "Phone No";
                worksheet.Cells[1, 6].Value = "Reporting Manager Name";
                worksheet.Cells[1, 7].Value = "Date Of Birth";
                worksheet.Cells[1, 8].Value = "Date of Joining";

                int row = 2;
                foreach (var emp in employees)
                {
                    worksheet.Cells[row, 1].Value = row - 1;
                    worksheet.Cells[row, 2].Value = emp.FirstName;
                    worksheet.Cells[row, 3].Value = emp.LastName;
                    worksheet.Cells[row, 4].Value = emp.Email;
                    worksheet.Cells[row, 5].Value = emp.Mobile;
                    worksheet.Cells[row, 6].Value = emp.ReportingManagerName;

                    var additionalDetails = await _cosmosDbService.GetItemsAsync<EmployeeAdditionalDetails>($"SELECT * FROM c WHERE c.EmployeeBasicDetailsUId = '{emp.Id}'");
                    if (additionalDetails != null)
                    {
                        var details = additionalDetails.FirstOrDefault();
                        if (details != null)
                        {
                            worksheet.Cells[row, 7].Value = details.PersonalDetails.DateOfBirth.ToShortDateString();
                            worksheet.Cells[row, 8].Value = details.WorkInformation.DateOfJoining.ToShortDateString();
                        }
                    }

                    row++;
                }

                return package.GetAsByteArray();
            }
        }

        public async Task ImportEmployeesFromExcel(Stream fileStream)
        {
            using (var package = new ExcelPackage(fileStream))
            {
                var worksheet = package.Workbook.Worksheets.First();
                var rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    var emp = new EmployeeBasicDetails
                    {
                        FirstName = worksheet.Cells[row, 2].Text,
                        LastName = worksheet.Cells[row, 3].Text,
                        Email = worksheet.Cells[row, 4].Text,
                        Mobile = worksheet.Cells[row, 5].Text,
                        ReportingManagerName = worksheet.Cells[row, 6].Text
                    };

                    await _cosmosDbService.AddItemAsync(emp);
                }
            }
        }
    }
}
