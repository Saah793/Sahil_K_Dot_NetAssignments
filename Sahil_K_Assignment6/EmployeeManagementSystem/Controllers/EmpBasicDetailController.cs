using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Employee_Management_System.Interfaces;
using Employee_Management_System.DTO;
using Employee_Management_System.Entities;
using Employee_Management_System.ServiceFolder;

namespace Employee_Management_System.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmpBasicDetailController : Controller
    {

        private readonly IEmpBasicDetail _empBasicDetails;

        public EmpBasicDetailController(IEmpBasicDetail empBasicDetails)
        {
            _empBasicDetails = empBasicDetails;
        }

        [HttpPost]

        public async Task<EmpBasicDTO> AddEmployee(EmpBasicDTO empBasicDetailsDTO)
        {
            var response = await _empBasicDetails.AddEmployee(empBasicDetailsDTO);
            return response;
        }


        [HttpGet]

        public async Task<List<EmpBasicDTO>> GetAllEmployee()
        {
            var response = await _empBasicDetails.GetAllEmployee();
            return response;
        }

        [HttpGet]

        public async Task<EmpBasicDTO> GetEmployeeByUId(string UId)
        {
            var response = await _empBasicDetails.GetEmployeeByUId(UId);
            return response;
        }

        [HttpPost]
        public async Task<EmpBasicDTO> UpdateEmployee(EmpBasicDTO empBasicDetailsDTO)
        {
            var response = await _empBasicDetails.UpdateEmployee(empBasicDetailsDTO);
            return response;
        }

        [HttpPost]
        public async Task<string> DeleteEmployee(string UId)
        {
            var response = await _empBasicDetails.DeleteEmployee(UId);
            return response;
        }
        [HttpGet]
        public async Task<List<EmpBasicDTO>> GetEmployeeByRole(string role)
        {
            var response = await _empBasicDetails.GetEmployeeByRole(role);
            return response;
        }
       
        [HttpGet("employee")]
        public async Task<IActionResult> GetEmployeeByRolenew(string role)
        {
            var employee = await _empBasicDetails.GetEmployeeByRolenew(role);
            return Ok(employee);
        }
        [HttpGet]
        public async Task<List<EmpBasicDTO>> GetEmpBasicDetailByMakeRequest()
        {

            return await _empBasicDetails.GetEmpBasicDetailByMakeRequest();
        }
        [HttpPost]
        public async Task<IActionResult> AddStudentByMakePostRequest(EmpBasicDTO studentDTO)
        {
            var response = await _empBasicDetails.AddEmpByMakePostRequest(studentDTO);
            return Ok(response);

        }
        [HttpGet]
        public async Task<List<EmpBasicDTO>> GetStudentByMakeGetRequest()
        {
            var response = await _empBasicDetails.GetEmpByMakeGetRequest();
            return response;
        }

        //Pagination
        [HttpPost]
        [ServiceFilter(typeof(BuildEmpFilter))]
        public async Task<EmployeeFilter> GetEmployeebyServiceFilter(EmployeeFilter employeeFilter)
        {
            var response = await _empBasicDetails.GetEmployeebyServiceFilter(employeeFilter);
            return response;
        }
    }
}
