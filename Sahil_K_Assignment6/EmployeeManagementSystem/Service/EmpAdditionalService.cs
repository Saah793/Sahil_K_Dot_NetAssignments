using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Employee_Management_System.Interfaces;
using Employee_Management_System.DTO;
using Employee_Management_System.Common;
using Employee_Management_System.CosmosDB;
using Employee_Management_System.Entities;
using AutoMapper;

namespace Employee_Management_System.Service
{
    public class EmpAdditionalService : IEmpAdditionalDetail
    {
        public readonly ICosmosDBService _cosmosDBService;
        public readonly IMapper _mapper;

        public EmpAdditionalService(ICosmosDBService cosmosDBService,IMapper mapper)
        {
            _cosmosDBService = cosmosDBService;
            _mapper = mapper;
        }

        //Add Additional Details
        public async Task<EmpAdditionalDTO> AddAdditionalDetails(EmpAdditionalDTO empAdditionalDTO)
        {
            var employee = _mapper.Map<EmployeeAdditionalDetailsEntity>(empAdditionalDTO);

            employee.Intialize(true, Credentials.EmpDocumentType, "Saahil","Saahil");

            var response = await _cosmosDBService.AddAdditionalDetails(employee);

            var responseModel = _mapper.Map<EmpAdditionalDTO>(response);

            return responseModel;
        }

        //Get All Employee Additional Details
        public async Task<List<EmpAdditionalDTO>> GetAllEmployeeAdditionalDetails()
        {
            var employees = await _cosmosDBService.GetAllEmployeeAdditionalDetails();

            var empAdditionalDTOs = new List<EmpAdditionalDTO>();
            foreach (var employee in employees)
            {
                var empAdditionalDTO = _mapper.Map<EmpAdditionalDTO>(employee);
                empAdditionalDTOs.Add(empAdditionalDTO);
            }
            return empAdditionalDTOs;
        }


        //Get Employee Additional Details By UId
        public async Task<EmpAdditionalDTO> GetEmployeeAdditionalDetailsByUId(string UId)
        {
            var response = await _cosmosDBService.GetEmployeeAdditionalDetailsByUId(UId);

            if (response != null)
            {
                var empAdditionalDTO = _mapper.Map<EmpAdditionalDTO>(response);

                return empAdditionalDTO;
            }
            else
            {
                throw new Exception("Response object is null.");

            }
        }

        // Update Employee Additional Details
        public async Task<EmpAdditionalDTO> UpdateEmployeeAdditionalDetails(EmpAdditionalDTO empAdditionalDTO)
        {
            var existingEmployee = await _cosmosDBService.GetEmployeeAdditionalDetailsByUId(empAdditionalDTO.UId);
            existingEmployee.Active = false;
            existingEmployee.Archived = true;
            await _cosmosDBService.ReplaceAsync(existingEmployee);

            existingEmployee.Intialize(false, Credentials.EmpDocumentType, "Saahil","Saahil");

            _mapper.Map(empAdditionalDTO, existingEmployee);


            var response = await _cosmosDBService.AddAdditionalDetails(existingEmployee);

            var responseModel = _mapper.Map<EmpAdditionalDTO>(response);
            return responseModel;


        }

        //Delete Employee Additional Details
        public async Task<string> DeleteEmployeeAdditionalDetails(string employeeBasicDetailsUId)
        {

            var employee = await _cosmosDBService.GetEmployeeAdditionalDetailsByUId(employeeBasicDetailsUId);
            employee.Active = false;
            employee.Archived = true;
            await _cosmosDBService.ReplaceAsync(employee);

            employee.Intialize(false, Credentials.EmpDocumentType, "Saahil","Saahil");
            employee.Archived = true;



            var response = await _cosmosDBService.AddAdditionalDetails(employee);

            return "Record has been deleted.";

        }

        public async Task<EmployeeAdditionalFilter> GetAllEmployeebyServiceFilter(EmployeeAdditionalFilter employeeAdditionalFilter)
        {
            EmployeeAdditionalFilter response = new EmployeeAdditionalFilter();

            var check = employeeAdditionalFilter.filters.Any(a => a.FieldName == "status");
            var status = "";
            if (check)
            {
                status = employeeAdditionalFilter.filters.Find(a => a.FieldName == "status").FieldValue;
            }
            var employees = await GetAllEmployeeAdditionalDetails();
            var filterRecord = employees.FindAll(a => a.Status == status);
            response.totalCount = employees.Count;
            response.page = employeeAdditionalFilter.page;
            response.pageSize = employeeAdditionalFilter.pageSize;




            var s = employeeAdditionalFilter.pageSize * (employeeAdditionalFilter.page - 1);

            employees = employees.Skip(s).Take(employeeAdditionalFilter.pageSize).ToList();

            foreach (var employee in employees)
            {
                response.employeeAdditionalDetails.Add(employee);
            }
            return response;
        }
    }
}
