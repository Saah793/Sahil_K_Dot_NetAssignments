using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Employee_Management_System.Interfaces;
using Employee_Management_System.DTO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using Microsoft.AspNetCore.Http;    
using System.IO;

namespace Employee_Management_System.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class Import_Export : Controller
    {

        private readonly IEmpBasicDetail _empBasicDetails;
        private readonly IEmpAdditionalDetail _empAdditionalDetails;

        public Import_Export(IEmpBasicDetail empBasicDetails, IEmpAdditionalDetail empAdditionalDetail)
        {
            _empBasicDetails = empBasicDetails;
            _empAdditionalDetails = empAdditionalDetail;
        }

        [HttpPost]

        public async Task<EmpBasicDTO> AddEmployee(EmpBasicDTO empBasicDTO)
        {
            var response = await _empBasicDetails.AddEmployee(empBasicDTO);
            return response;
        }

        private string GetStringFormCell(ExcelWorksheet worksheet, int row, int column)
        {
            var cellValue = worksheet.Cells[row, column].Value;
            return cellValue?.ToString()?.Trim();
        }



        [HttpPost]
        public async Task<IActionResult> Export()
        {
            var basicDetails = await _empBasicDetails.GetAllEmployee();
            var additionalDetails = await _empAdditionalDetails.GetAllEmployeeAdditionalDetails(); 

            var employees = from basic in basicDetails
                            join additional in additionalDetails on basic.UId equals additional.UId
                            select new EmployeeDTO
                            {
                                FirstName = basic.FirstName,
                                LastName = basic.LastName,
                                Email = basic.Email,
                                Mobile = basic.Mobile,
                                ReportingManagerName = basic.ReportingManagerName,
                                DateOfBirth = basic.DateOfBirth,
                                DateOfJoining = basic.DateOfJoining,
                                AlternateEmail = additional.AlternateEmail,
                                AlternateMobile = additional.AlternateMobile,
                                WorkInformation = additional.WorkInformation,
                                PersonalDetails = additional.PersonalDetails,
                                IdentityInformation = additional.IdentityInformation
                            };

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Employees");

                // Adding headers
                worksheet.Cells[1, 1].Value = "FirstName";
                worksheet.Cells[1, 2].Value = "LastName";
                worksheet.Cells[1, 3].Value = "Email";
                worksheet.Cells[1, 4].Value = "Mobile";
                worksheet.Cells[1, 5].Value = "ReportingManagerName";
                worksheet.Cells[1, 6].Value = "DateOfBirth";
                worksheet.Cells[1, 7].Value = "DateOfJoining";
                worksheet.Cells[1, 8].Value = "AlternateEmail";
                worksheet.Cells[1, 9].Value = "AlternateMobile";
                worksheet.Cells[1, 10].Value = "WorkInformation";
                worksheet.Cells[1, 11].Value = "PersonalDetails";
                worksheet.Cells[1, 12].Value = "IdentityInformation";

                // Styling headers
                using (var range = worksheet.Cells[1, 1, 1, 12])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                }

                // Adding data rows
                int row = 2;
                foreach (var employee in employees)
                {
                    worksheet.Cells[row, 1].Value = employee.FirstName;
                    worksheet.Cells[row, 2].Value = employee.LastName;
                    worksheet.Cells[row, 3].Value = employee.Email;
                    worksheet.Cells[row, 4].Value = employee.Mobile;
                    worksheet.Cells[row, 5].Value = employee.ReportingManagerName;
                    worksheet.Cells[row, 6].Value = employee.DateOfBirth;
                    worksheet.Cells[row, 7].Value = employee.DateOfJoining;
                    worksheet.Cells[row, 8].Value = employee.AlternateEmail;
                    worksheet.Cells[row, 9].Value = employee.AlternateMobile;
                    worksheet.Cells[row, 10].Value = employee.WorkInformation; 
                    worksheet.Cells[row, 11].Value = employee.PersonalDetails; 
                    worksheet.Cells[row, 12].Value = employee.IdentityInformation; 
                    row++;
                }

                // Returning the file
                var stream = new System.IO.MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;
                var fileName = "EmployeeData.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }







    }
}
