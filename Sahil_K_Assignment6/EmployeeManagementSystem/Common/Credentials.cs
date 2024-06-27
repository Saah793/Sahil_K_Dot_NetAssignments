using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_Management_System.Common
{
    public class Credentials
    {
        public static readonly string databaseName = Environment.GetEnvironmentVariable("databaseName");
        public static readonly string containerName = Environment.GetEnvironmentVariable("containerName");
        public static readonly string CosmosEndPoint = Environment.GetEnvironmentVariable("cosmosUrl");
        public static readonly string PrimaryKey = Environment.GetEnvironmentVariable("primaryKey");
        public static readonly string EmpDocumentType = "employee";
        public static readonly string EmpUrl = Environment.GetEnvironmentVariable("empUrl");
        public static readonly string GetEmpEndPoint = "/api/EmpBasicDetail/GetAllEmployee";
        public static readonly string AddEmpEndPoint = "/api/EmpBasicDetail/AddEmployee";
    }
}
