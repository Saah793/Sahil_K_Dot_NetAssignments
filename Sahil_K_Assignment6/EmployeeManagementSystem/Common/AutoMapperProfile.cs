using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Employee_Management_System.DTO;
using Employee_Management_System.Entities;

namespace Employee_Management_System.Common
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<EmpAdditionalDTO, EmployeeAdditionalDetailsEntity>();

            CreateMap<EmployeeAdditionalDetailsEntity, EmpAdditionalDTO>();

            CreateMap<EmpBasicDTO, EmployeeBasicDetailsEntity>();

            CreateMap<EmployeeBasicDetailsEntity, EmpBasicDTO>();

        }
    }
}
