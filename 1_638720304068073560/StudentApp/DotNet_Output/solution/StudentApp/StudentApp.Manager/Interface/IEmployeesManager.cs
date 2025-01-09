using StudentApp.Model;
using StudentApp.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentApp.Manager.Interface
{
    public interface IEmployeesManager
    {
        APIResponse GetEmployees(int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse GetEmployeesByCreatedBy(string ownername,int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse SearchEmployees(string searchKey, int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse SearchEmployeesByCreatedBy(string ownername,string searchKey, int page, int itemsPerPage,List<OrderByModel> orderBy);
		APIResponse FilterEmployees(string ownername,List<FilterModel> filterModels, string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        APIResponse GetEmployeesByID(int employee_id);
        APIResponse GetEmployeesByIDByCreatedBy(string ownername,int employee_id);
        
        
        
        APIResponse UpdateEmployeesWithToken(int employee_id,EmployeesModel model,string token);
        APIResponse AddEmployeesWithToken(EmployeesModel model,string token);
		APIResponse DeleteEmployeesWithToken(int employee_id,string token);
		APIResponse DeleteMultipleEmployeesWithToken(List<DeleteMultipleModel> deleteParam, string andOr,string token);
    }
}
