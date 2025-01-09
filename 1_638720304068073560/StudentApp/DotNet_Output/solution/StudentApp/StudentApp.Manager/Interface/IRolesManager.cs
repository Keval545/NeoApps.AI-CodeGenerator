using StudentApp.Model;
using StudentApp.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentApp.Manager.Interface
{
    public interface IRolesManager
    {
        APIResponse GetRoles(int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse GetRolesByCreatedBy(string ownername,int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse SearchRoles(string searchKey, int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse SearchRolesByCreatedBy(string ownername,string searchKey, int page, int itemsPerPage,List<OrderByModel> orderBy);
		APIResponse FilterRoles(string ownername,List<FilterModel> filterModels, string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        APIResponse GetRolesByID(int role_id);
        APIResponse GetRolesByIDByCreatedBy(string ownername,int role_id);
        
        
        
        APIResponse UpdateRolesWithToken(int role_id,RolesModel model,string token);
        APIResponse AddRolesWithToken(RolesModel model,string token);
		APIResponse DeleteRolesWithToken(int role_id,string token);
		APIResponse DeleteMultipleRolesWithToken(List<DeleteMultipleModel> deleteParam, string andOr,string token);
    }
}
