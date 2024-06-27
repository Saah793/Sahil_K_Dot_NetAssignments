using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Employee_Management_System.Entities;

namespace Employee_Management_System.CosmosDB
{
    public interface ICosmosDBService
    {
        Task<EmployeeBasicDetailsEntity> AddEmployee(EmployeeBasicDetailsEntity employeeBasicDetailsEntity);
        Task<List<EmployeeBasicDetailsEntity>> GetAllEmployee();

        Task<EmployeeBasicDetailsEntity> GetEmployeeByUId(string UId);

        Task ReplaceAsync(dynamic employee);


        Task<EmployeeAdditionalDetailsEntity> AddAdditionalDetails(EmployeeAdditionalDetailsEntity employee);

        Task<EmployeeAdditionalDetailsEntity> GetEmployeeAdditionalDetailsByUId(string uId);

        Task<List<EmployeeAdditionalDetailsEntity>> GetAllEmployeeAdditionalDetails();
     
    }
}
