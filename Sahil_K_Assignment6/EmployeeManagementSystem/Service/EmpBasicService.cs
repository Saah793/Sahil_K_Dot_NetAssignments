using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Employee_Management_System.Interfaces;
using Employee_Management_System.CosmosDB;
using Employee_Management_System.Entities;
using Employee_Management_System.DTO;
using Employee_Management_System.Common;
using AutoMapper;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Management_System.Service
{
    public class EmpBasicService : IEmpBasicDetail
    {
        public readonly ICosmosDBService _cosmosDBService;
        public readonly IMapper _mapper;

        public EmpBasicService(ICosmosDBService cosmosDBService,IMapper mapper)
        {
            _cosmosDBService = cosmosDBService;
            _mapper = mapper;
        }

        //Add Employee

        public async Task<EmpBasicDTO> AddEmployee(EmpBasicDTO empBasicDTO)
        {

            var employee = _mapper.Map<EmployeeBasicDetailsEntity>(empBasicDTO);

            employee.Intialize(true, Credentials.EmpDocumentType, "Saahil","Saahil");


            var response = await _cosmosDBService.AddEmployee(employee);

            var responseModel = _mapper.Map<EmpBasicDTO>(response);


            return responseModel;

        }

        //Get All Employee

        public async Task<List<EmpBasicDTO>> GetAllEmployee()
        {
            var employees = await _cosmosDBService.GetAllEmployee();

            var empBasicDTOs = new List<EmpBasicDTO>();
            foreach (var employee in employees)
            {
                var empBasicDTO = _mapper.Map<EmpBasicDTO>(employee);
                empBasicDTOs.Add(empBasicDTO);
            }
            return empBasicDTOs;
        }

        //Get Employee By uid
        public async Task<EmpBasicDTO> GetEmployeeByUId(string UId)
        {
            var response = await _cosmosDBService.GetEmployeeByUId(UId);

            var responseModel = _mapper.Map<EmpBasicDTO>(response);


            return responseModel;
        }

        //Update Employee

        public async Task<EmpBasicDTO> UpdateEmployee(EmpBasicDTO empBasicDTO)
        {
            var existingEmployee = await _cosmosDBService.GetEmployeeByUId(empBasicDTO.UId);
            existingEmployee.Active = false;
            existingEmployee.Archived = true;
            await _cosmosDBService.ReplaceAsync(existingEmployee);

            existingEmployee.Intialize(false, Credentials.EmpDocumentType, "Saahil","Saahil");

            _mapper.Map(empBasicDTO, existingEmployee);

            var response = await _cosmosDBService.AddEmployee(existingEmployee);

            var responseModel = _mapper.Map<EmpBasicDTO>(response);
            return responseModel;


        }
        //Delete Employee
        public async Task<string> DeleteEmployee(string uId)
        {

            var employee = await _cosmosDBService.GetEmployeeByUId(uId);
            employee.Active = false;
            employee.Archived = true;
            await _cosmosDBService.ReplaceAsync(employee);

            employee.Intialize(false, Credentials.EmpDocumentType, "Saahil","Saahil");
            employee.Archived = true;



            var response = await _cosmosDBService.AddEmployee(employee);

            return "Record has been deleted.";

        }
        //Get Employee By Role 

        public async Task<List<EmpBasicDTO>> GetEmployeeByRole(string role)
        {
            var allEmployee = await GetAllEmployee();

            var filteredList = allEmployee.FindAll(a => a.Role == role);

            return filteredList;
        }

         public async Task<EmployeeFilter> GetEmployeebyServiceFilter(EmployeeFilter employeeFilter)
        {
            EmployeeFilter response =new EmployeeFilter();

            var check = employeeFilter.filters.Any(a => a.FieldName == "status");
            var status = "";
            if(check)
            {
                status = employeeFilter.filters.Find(a => a.FieldName == "status").FieldValue;
            }
             var employees= await GetAllEmployee();
            var filterRecord=employees.FindAll(a=>a.Status==status);
            response.totalCount = employees.Count;
            response.page=employeeFilter.page;
            response.pageSize=employeeFilter.pageSize;




            var s = employeeFilter.pageSize * (employeeFilter.page - 1);

            employees = employees.Skip(s).Take(employeeFilter.pageSize).ToList();

            foreach(var employee in employees)
            {
                response.employeeBasicDetails.Add(employee);
            }
            return response;
        }

        public async Task<EmpBasicDTO> GetEmployeeByRolenew(string role)
        {
            var employees = await GetEmployeeByRole(role);

            return employees.FirstOrDefault();
        }

        public async Task<List<EmpBasicDTO>> GetEmpBasicDetailByMakeRequest()
        {
            var request = await HttpClientHelper.MakeGetRequest(Credentials.EmpUrl, Credentials.GetEmpEndPoint);
            return JsonConvert.DeserializeObject<List<EmpBasicDTO>>(request);
        }
        public async Task<IActionResult> AddEmpByMakePostRequest(EmpBasicDTO empBasicDTO)
        {
            var serializedObj = JsonConvert.SerializeObject(empBasicDTO);
            var requestObj = await HttpClientHelper.MakePostRequest(Credentials.EmpUrl, Credentials.GetEmpEndPoint, serializedObj);
            var studentdto = JsonConvert.DeserializeObject<EmpBasicDTO>(requestObj);
            return new JsonResult(studentdto);
        }
        public async Task<List<EmpBasicDTO>> GetEmpByMakeGetRequest()
        {
            var request = await HttpClientHelper.MakeGetRequest(Credentials.EmpUrl, Credentials.GetEmpEndPoint);
            return JsonConvert.DeserializeObject<List<EmpBasicDTO>>(request);
        }

       
    }
}
