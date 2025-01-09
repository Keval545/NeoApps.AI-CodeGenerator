using StudentApp.Model;
using StudentApp.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentApp.Manager.Interface
{
    public interface IDnd_ui_versionsManager
    {
        APIResponse GetDnd_ui_versions(int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse GetDnd_ui_versionsByCreatedBy(string ownername,int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse SearchDnd_ui_versions(string searchKey, int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse SearchDnd_ui_versionsByCreatedBy(string ownername,string searchKey, int page, int itemsPerPage,List<OrderByModel> orderBy);
		APIResponse FilterDnd_ui_versions(string ownername,List<FilterModel> filterModels, string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        APIResponse GetDnd_ui_versionsByID(int dnd_ui_version_id);
        APIResponse GetDnd_ui_versionsByIDByCreatedBy(string ownername,int dnd_ui_version_id);
        
        
        
        APIResponse UpdateDnd_ui_versionsWithToken(int dnd_ui_version_id,Dnd_ui_versionsModel model,string token);
        APIResponse AddDnd_ui_versionsWithToken(Dnd_ui_versionsModel model,string token);
		APIResponse DeleteDnd_ui_versionsWithToken(int dnd_ui_version_id,string token);
		APIResponse DeleteMultipleDnd_ui_versionsWithToken(List<DeleteMultipleModel> deleteParam, string andOr,string token);
    }
}
