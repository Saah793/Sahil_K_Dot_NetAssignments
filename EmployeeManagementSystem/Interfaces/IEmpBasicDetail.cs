using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Employee_Management_System.DTO;
using Employee_Management_System.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Management_System.Interfaces
{
    public interface IEmpBasicDetail
    {

        Task<EmpBasicDTO> AddEmployee(EmpBasicDTO empBasicDTO);

        Task<EmpBasicDTO> GetEmployeeByUId(string UId);
       
        Task<List<EmpBasicDTO>> GetAllEmployee();

        Task<EmpBasicDTO> UpdateEmployee(EmpBasicDTO empBasicDTO);

        Task<string> DeleteEmployee(string uId);

        Task<List<EmpBasicDTO>> GetEmployeeByRole(string role);
        Task<EmployeeFilter> GetEmployeebyServiceFilter(EmployeeFilter employeeFilter);
        Task <EmpBasicDTO> GetEmployeeByRolenew(string role);
        Task<List<EmpBasicDTO>> GetEmpBasicDetailByMakeRequest();
        Task<IActionResult> AddEmpByMakePostRequest(EmpBasicDTO empBasicDto);

        Task<List<EmpBasicDTO>> GetEmpByMakeGetRequest();
    }
}
