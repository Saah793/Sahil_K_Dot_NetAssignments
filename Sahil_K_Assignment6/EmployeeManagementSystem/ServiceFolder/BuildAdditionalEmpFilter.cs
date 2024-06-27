using Employee_Management_System.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_Management_System.ServiceFolder
{
    public class BuildAdditionalEmpFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var param = context.ActionArguments.SingleOrDefault(p => p.Value is EmployeeAdditionalFilter);
            if (param.Value == null)
            {
                context.Result = new BadRequestObjectResult("Object is null");
                return;
            }
            EmployeeAdditionalFilter employeeAdditionalFilter = (EmployeeAdditionalFilter)param.Value;
            var statusFilter = employeeAdditionalFilter.filters.Find(a => a.FieldName == "status");
            if (statusFilter != null)
            {
                statusFilter = new Filter();
                statusFilter.FieldValue = "Active";
                statusFilter.FieldName = "status";
                employeeAdditionalFilter.filters.Add(statusFilter);
            }
            employeeAdditionalFilter.filters.RemoveAll(a => string.IsNullOrEmpty(a.FieldName));

            var result = await next();
        }
    }
}
