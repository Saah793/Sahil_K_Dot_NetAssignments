using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Employee_Management_System.Interfaces;
using Employee_Management_System.CosmosDB;
using Employee_Management_System.Entities;
using Employee_Management_System.DTO;
using Employee_Management_System.Common;

namespace Employee_Management_System.Service
{
    public class EmpBasicService : IEmpBasicDetail
    {
        public readonly ICosmosDBService _cosmosDBService;

        public EmpBasicService(ICosmosDBService cosmosDBService)
        {
            _cosmosDBService = cosmosDBService;
        }

        //Add Employee

        public async Task<EmpBasicDTO> AddEmployee(EmpBasicDTO empBasicDTO)
        {
            EmployeeBasicDetailsEntity employee = new EmployeeBasicDetailsEntity();
            employee.Salutory = empBasicDTO.Salutory;
            employee.FirstName = empBasicDTO.FirstName;
            employee.MiddleName = empBasicDTO.MiddleName;
            employee.LastName = empBasicDTO.LastName;
            employee.NickName = empBasicDTO.NickName;
            employee.Email = empBasicDTO.Email;
            employee.Mobile = empBasicDTO.Mobile;
            employee.EmployeeId = empBasicDTO.EmployeeId;
            employee.Role = empBasicDTO.Role;
            employee.ReportingManagerUId = Guid.NewGuid().ToString();
            employee.ReportingManagerName = empBasicDTO.ReportingManagerName;
            employee.Address = empBasicDTO.Address;

            employee.DateOfBirth = empBasicDTO.DateOfBirth;
            employee.DateOfJoining = empBasicDTO.DateOfJoining;


            employee.Intialize(true, Credentials.EmpDocumentType, "Sahil","Sahil");


            var response = await _cosmosDBService.AddEmployee(employee);

            var responseModel = new EmpBasicDTO();
            responseModel.UId = response.UId;
            responseModel.Salutory = response.Salutory;
            responseModel.FirstName = response.FirstName;
            responseModel.MiddleName = response.MiddleName;
            responseModel.LastName = response.LastName;
            responseModel.NickName = response.NickName;
            responseModel.Email = response.Email;
            responseModel.Mobile = response.Mobile;
            responseModel.EmployeeId = response.Id;
            responseModel.Role = response.Role;
            responseModel.ReportingManagerUId = response.ReportingManagerUId;
            responseModel.ReportingManagerName = response.ReportingManagerName;
            responseModel.Address = response.Address;
            responseModel.DateOfBirth = response.DateOfBirth;
            responseModel.DateOfJoining = response.DateOfJoining;


            return responseModel;

        }

        //Get All Employee

        public async Task<List<EmpBasicDTO>> GetAllEmployee()
        {
            var employees = await _cosmosDBService.GetAllEmployee();

            var empBasicDTOs = new List<EmpBasicDTO>();
            foreach (var employee in employees)
            {
                var empBasicDTO = new EmpBasicDTO();
                empBasicDTO.UId = employee.UId;
                empBasicDTO.Salutory = employee.Salutory;
                empBasicDTO.FirstName = employee.FirstName;
                empBasicDTO.MiddleName = employee.MiddleName;
                empBasicDTO.LastName = employee.LastName;
                empBasicDTO.NickName = employee.NickName;
                empBasicDTO.Email = employee.Email;
                empBasicDTO.Mobile = employee.Mobile;
                empBasicDTO.EmployeeId = employee.Id;
                empBasicDTO.Role = employee.Role;
                empBasicDTO.ReportingManagerUId = employee.ReportingManagerUId;
                empBasicDTO.ReportingManagerName = employee.ReportingManagerName;
                empBasicDTO.Address = employee.Address;
                empBasicDTOs.Add(empBasicDTO);


            }
            return empBasicDTOs;
        }

        //Get Employee By uid
        public async Task<EmpBasicDTO> GetEmployeeByUId(string UId)
        {
            var response = await _cosmosDBService.GetEmployeeByUId(UId);

            var empBasicDTO = new EmpBasicDTO();
            empBasicDTO.UId = response.UId;
            empBasicDTO.Salutory = response.Salutory;
            empBasicDTO.FirstName = response.FirstName;
            empBasicDTO.MiddleName = response.MiddleName;
            empBasicDTO.LastName = response.LastName;
            empBasicDTO.NickName = response.NickName;
            empBasicDTO.Email = response.Email;
            empBasicDTO.Mobile = response.Mobile;
            empBasicDTO.EmployeeId = response.Id;
            empBasicDTO.Role = response.Role;
            empBasicDTO.ReportingManagerUId = response.ReportingManagerUId;
            empBasicDTO.ReportingManagerName = response.ReportingManagerName;
            empBasicDTO.Address = response.Address;


            return empBasicDTO;
        }

        //Update Employee

        public async Task<EmpBasicDTO> UpdateEmployee(EmpBasicDTO empBasicDTO)
        {
            var existingEmployee = await _cosmosDBService.GetEmployeeByUId(empBasicDTO.UId);
            existingEmployee.Active = false;
            existingEmployee.Archived = true;
            await _cosmosDBService.ReplaceAsync(existingEmployee);

            existingEmployee.Intialize(false, Credentials.EmpDocumentType, "Sahil","Sahil");

            existingEmployee.Salutory = empBasicDTO.Salutory;
            existingEmployee.FirstName = empBasicDTO.FirstName;
            existingEmployee.MiddleName = empBasicDTO.MiddleName;
            existingEmployee.LastName = empBasicDTO.LastName;
            existingEmployee.NickName = empBasicDTO.NickName;
            existingEmployee.Email = empBasicDTO.Email;
            existingEmployee.Mobile = empBasicDTO.Mobile;
            existingEmployee.EmployeeId = empBasicDTO.EmployeeId;
            existingEmployee.Role = empBasicDTO.Role;
            existingEmployee.ReportingManagerUId = empBasicDTO.ReportingManagerUId;
            existingEmployee.ReportingManagerName = empBasicDTO.ReportingManagerName;
            existingEmployee.Address = empBasicDTO.Address;

            var response = await _cosmosDBService.AddEmployee(existingEmployee);

            var responseModel = new EmpBasicDTO
            {
                UId = response.UId,
                Salutory = response.Salutory,
                FirstName = response.FirstName,
                MiddleName = response.MiddleName,
                LastName = response.LastName,
                NickName = response.NickName,
                Email = response.Email,
                Mobile = response.Mobile,
                EmployeeId = response.Id,
                Role = response.Role,
                ReportingManagerUId = response.ReportingManagerUId,
                ReportingManagerName = response.ReportingManagerName,
                Address = response.Address,


            };
            return responseModel;


        }
        //Delete Employee
        public async Task<string> DeleteEmployee(string uId)
        {

            var employee = await _cosmosDBService.GetEmployeeByUId(uId);
            employee.Active = false;
            employee.Archived = true;
            await _cosmosDBService.ReplaceAsync(employee);

            employee.Intialize(false, Credentials.EmpDocumentType, "Sahil","Sahil");
            employee.Archived = true;



            var response = await _cosmosDBService.AddEmployee(employee);

            return "This Record has been deleted!";

        }
        //Get Employee By Role 

        public async Task<List<EmpBasicDTO>> GetEmployeeByRole(string role)
        {
            var allEmployee = await GetAllEmployee();

            var filteredList = allEmployee.FindAll(a => a.Role == role);

            return filteredList;
        }

    }
}
