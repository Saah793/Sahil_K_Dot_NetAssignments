using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Employee_Management_System.Interfaces;
using Employee_Management_System.DTO;
using Employee_Management_System.Entities;

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

        public async Task<EmpBasicDTO> AddEmployee(EmpBasicDTO empBasicDTO)
        {
            var response = await _empBasicDetails.AddEmployee(empBasicDTO);
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
        public async Task<EmpBasicDTO> UpdateEmployee(EmpBasicDTO empBasicDTO)
        {
            var response = await _empBasicDetails.UpdateEmployee(empBasicDTO);
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

    }
}
