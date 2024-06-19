using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Employee_Management_System.Interfaces;
using Employee_Management_System.DTO;
using Employee_Management_System.Common;
using Employee_Management_System.CosmosDB;
using Employee_Management_System.Entities;

namespace Employee_Management_System.Service
{
    public class EmpAdditionalService : IEmpAdditionalDetail
    {
        public readonly ICosmosDBService _cosmosDBService;

        public EmpAdditionalService(ICosmosDBService cosmosDBService)
        {
            _cosmosDBService = cosmosDBService;
        }

        //Add Additional Details
        public async Task<EmpAdditionalDTO> AddAdditionalDetails(EmpAdditionalDTO empAdditionalDTO)
        {
            EmployeeAdditionalDetailsEntity employee = new EmployeeAdditionalDetailsEntity();
            employee.UId = empAdditionalDTO.UId;
            employee.AlternateEmail = empAdditionalDTO.AlternateEmail;
            employee.AlternateMobile = empAdditionalDTO.AlternateMobile;
            employee.WorkInformation = empAdditionalDTO.WorkInformation;
            employee.PersonalDetails = empAdditionalDTO.PersonalDetails;
            employee.IdentityInformation = empAdditionalDTO.IdentityInformation;

            employee.Intialize(true, Credentials.EmpDocumentType, "Sahil","Sahil");

            var response = await _cosmosDBService.AddAdditionalDetails(employee);


            var responseModel = new EmpAdditionalDTO();
            responseModel.UId = response.UId;
            responseModel.AlternateEmail = response.AlternateEmail;
            responseModel.AlternateMobile = response.AlternateMobile;
            responseModel.WorkInformation = response.WorkInformation;
            responseModel.PersonalDetails = response.PersonalDetails;
            responseModel.IdentityInformation = response.IdentityInformation;
            return responseModel;
        }

        //Get All Employee Additional Details
        public async Task<List<EmpAdditionalDTO>> GetAllEmployeeAdditionalDetails()
        {
            var employees = await _cosmosDBService.GetAllEmployeeAdditionalDetails();

            var empAdditionalDTOs = new List<EmpAdditionalDTO>();
            foreach (var employee in employees)
            {
                var empAdditionalDTO = new EmpAdditionalDTO();
                empAdditionalDTO.UId = employee.UId;
                empAdditionalDTO.AlternateEmail = employee.AlternateEmail;
                empAdditionalDTO.AlternateMobile = employee.AlternateMobile;
                empAdditionalDTO.WorkInformation = employee.WorkInformation;
                empAdditionalDTO.PersonalDetails = employee.PersonalDetails;
                empAdditionalDTO.IdentityInformation = employee.IdentityInformation;


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
                var empAdditionalDTO = new EmpAdditionalDTO();

                empAdditionalDTO.UId = response.UId;
                empAdditionalDTO.AlternateEmail = response.AlternateEmail;
                empAdditionalDTO.AlternateMobile = response.AlternateMobile;
                empAdditionalDTO.WorkInformation = response.WorkInformation;
                empAdditionalDTO.PersonalDetails = response.PersonalDetails;
                empAdditionalDTO.IdentityInformation = response.IdentityInformation;

                return empAdditionalDTO;
            }
            else
            {
                throw new Exception("Response object is null..");

            }
        }

        // Update Employee Additional Details
        public async Task<EmpAdditionalDTO> UpdateEmployeeAdditionalDetails(EmpAdditionalDTO empAdditionalDTO)
        {
            var existingEmployee = await _cosmosDBService.GetEmployeeAdditionalDetailsByUId(empAdditionalDTO.UId);
            existingEmployee.Active = false;
            existingEmployee.Archived = true;
            await _cosmosDBService.ReplaceAsync(existingEmployee);

            existingEmployee.Intialize(false, Credentials.EmpDocumentType, "Sahil","Sahil");




            existingEmployee.UId = empAdditionalDTO.UId;
            existingEmployee.AlternateEmail = empAdditionalDTO.AlternateEmail;
            existingEmployee.AlternateMobile = empAdditionalDTO.AlternateMobile;
            existingEmployee.WorkInformation = empAdditionalDTO.WorkInformation;
            existingEmployee.PersonalDetails = empAdditionalDTO.PersonalDetails;
            existingEmployee.IdentityInformation = empAdditionalDTO.IdentityInformation;


            var response = await _cosmosDBService.AddAdditionalDetails(existingEmployee);

            var responseModel = new EmpAdditionalDTO
            {
                UId = response.UId,
                AlternateEmail = response.AlternateEmail,
                AlternateMobile = response.AlternateMobile,
                WorkInformation = response.WorkInformation,
                PersonalDetails = response.PersonalDetails,
                IdentityInformation = response.IdentityInformation,



            };
            return responseModel;


        }

        //Delete Employee Additional Details
        public async Task<string> DeleteEmployeeAdditionalDetails(string employeeBasicDetailsUId)
        {

            var employee = await _cosmosDBService.GetEmployeeAdditionalDetailsByUId(employeeBasicDetailsUId);
            employee.Active = false;
            employee.Archived = true;
            await _cosmosDBService.ReplaceAsync(employee);

            employee.Intialize(false, Credentials.EmpDocumentType, "Sahil","Sahil");
            employee.Archived = true;



            var response = await _cosmosDBService.AddAdditionalDetails(employee);

            return "Record has been deleted!";

        }




    }
}
