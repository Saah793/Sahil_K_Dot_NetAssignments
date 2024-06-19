using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Employee_Management_System.Interfaces;
using Employee_Management_System.DTO;

namespace Employee_Management_System.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmpAdditionalDetailController : Controller
    {
        private readonly IEmpAdditionalDetail _empAdditionalDetails;

        public EmpAdditionalDetailController(IEmpAdditionalDetail empAdditionalDetails)
        {
            _empAdditionalDetails = empAdditionalDetails;
        }

        [HttpPost]

        public async Task<EmpAdditionalDTO> AddAdditionalDetails(EmpAdditionalDTO empAdditionalDTO)
        {
            var response = await _empAdditionalDetails.AddAdditionalDetails(empAdditionalDTO);
            return response;
        }

        [HttpGet]

        public async Task<List<EmpAdditionalDTO>> GetAllEmployeeAdditionalDetails()
        {
            var response = await _empAdditionalDetails.GetAllEmployeeAdditionalDetails();
            return response;
        }

        [HttpGet]

        public async Task<EmpAdditionalDTO> GetEmployeeAdditionalDetailsByUId(string UId)
        {
            var response = await _empAdditionalDetails.GetEmployeeAdditionalDetailsByUId(UId);
            return response;
        }


        [HttpPost]
        public async Task<EmpAdditionalDTO> UpdateEmployeeAdditionalDetails(EmpAdditionalDTO empAdditionalDTO)
        {
            var response = await _empAdditionalDetails.UpdateEmployeeAdditionalDetails(empAdditionalDTO);
            return response;
        }

        [HttpPost]
        public async Task<string> DeleteEmployeeAdditionalDetails(string UId)
        {
            var response = await _empAdditionalDetails.DeleteEmployeeAdditionalDetails(UId);
            return response;
        }


    }
}
