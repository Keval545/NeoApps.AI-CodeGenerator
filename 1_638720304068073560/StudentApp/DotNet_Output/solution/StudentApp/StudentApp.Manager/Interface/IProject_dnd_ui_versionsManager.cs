using StudentApp.Model;
using StudentApp.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentApp.Manager.Interface
{
    public interface IProject_dnd_ui_versionsManager
    {
        APIResponse GetProject_dnd_ui_versions(int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse GetProject_dnd_ui_versionsByCreatedBy(string ownername,int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse SearchProject_dnd_ui_versions(string searchKey, int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse SearchProject_dnd_ui_versionsByCreatedBy(string ownername,string searchKey, int page, int itemsPerPage,List<OrderByModel> orderBy);
		APIResponse FilterProject_dnd_ui_versions(string ownername,List<FilterModel> filterModels, string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        APIResponse GetProject_dnd_ui_versionsByID(int project_dnd_ui_version_id);
        APIResponse GetProject_dnd_ui_versionsByIDByCreatedBy(string ownername,int project_dnd_ui_version_id);
        
        
        
        APIResponse UpdateProject_dnd_ui_versionsWithToken(int project_dnd_ui_version_id,Project_dnd_ui_versionsModel model,string token);
        APIResponse AddProject_dnd_ui_versionsWithToken(Project_dnd_ui_versionsModel model,string token);
		APIResponse DeleteProject_dnd_ui_versionsWithToken(int project_dnd_ui_version_id,string token);
		APIResponse DeleteMultipleProject_dnd_ui_versionsWithToken(List<DeleteMultipleModel> deleteParam, string andOr,string token);
    }
}
