using StudentApp.Model;
using StudentApp.Utility;
using System.Collections.Generic;

namespace StudentApp.DataAccess.Interface
{
    public interface IEmployeesDataAccess
    {
        List<EmployeesModel> GetAllEmployees(int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<EmployeesModel> GetAllEmployeesByCreatedBy(string ownername,int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<EmployeesModel> SearchEmployees(string searchKey,int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null);
        List<EmployeesModel> SearchEmployeesByCreatedBy(string ownername,string searchKey,int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null);
		List<EmployeesModel> FilterEmployees(string ownername,List<FilterModel> filterModels, string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        EmployeesModel GetEmployeesByID(int employee_id);
        EmployeesModel GetEmployeesByIDByCreatedBy(string ownername,int employee_id);
        
        
        
		int GetAllTotalRecordEmployees();
        int GetAllTotalRecordEmployeesByCreatedBy(string ownername);
        int GetSearchTotalRecordEmployees(string searchKey);
        int GetSearchTotalRecordEmployeesByCreatedBy(string ownername,string searchKey);
        bool UpdateEmployees(EmployeesModel model);
		int GetFilterTotalRecordEmployees(string ownername,List<FilterModel> filterBy, string andOr);
        long AddEmployees(EmployeesModel model);
        bool DeleteEmployees(int employee_id);
		bool DeleteMultipleEmployees(List<DeleteMultipleModel> deleteParam, string andOr);
		
    }
}
