using StudentApp.Model;
using StudentApp.Utility;
using System.Collections.Generic;

namespace StudentApp.DataAccess.Interface
{
    public interface IRolesDataAccess
    {
        List<RolesModel> GetAllRoles(int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<RolesModel> GetAllRolesByCreatedBy(string ownername,int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<RolesModel> SearchRoles(string searchKey,int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null);
        List<RolesModel> SearchRolesByCreatedBy(string ownername,string searchKey,int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null);
		List<RolesModel> FilterRoles(string ownername,List<FilterModel> filterModels, string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        RolesModel GetRolesByID(int role_id);
        RolesModel GetRolesByIDByCreatedBy(string ownername,int role_id);
        
        
        
		int GetAllTotalRecordRoles();
        int GetAllTotalRecordRolesByCreatedBy(string ownername);
        int GetSearchTotalRecordRoles(string searchKey);
        int GetSearchTotalRecordRolesByCreatedBy(string ownername,string searchKey);
        bool UpdateRoles(RolesModel model);
		int GetFilterTotalRecordRoles(string ownername,List<FilterModel> filterBy, string andOr);
        long AddRoles(RolesModel model);
        bool DeleteRoles(int role_id);
		bool DeleteMultipleRoles(List<DeleteMultipleModel> deleteParam, string andOr);
		
    }
}
