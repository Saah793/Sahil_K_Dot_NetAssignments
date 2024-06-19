using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Employee_Management_System.DTO;

namespace Employee_Management_System.Interfaces
{
    public interface IEmpAdditionalDetail
    {
        Task<EmpAdditionalDTO> AddAdditionalDetails(EmpAdditionalDTO empAdditinalDTO);
        Task<List<EmpAdditionalDTO>> GetAllEmployeeAdditionalDetails();

        Task<EmpAdditionalDTO> GetEmployeeAdditionalDetailsByUId(string uId);

        Task<EmpAdditionalDTO> UpdateEmployeeAdditionalDetails(EmpAdditionalDTO empAdditionalDTO);

        Task<string> DeleteEmployeeAdditionalDetails(string UId);


    }
}
