using StudentApp.Model;
using StudentApp.Utility;
using System.Collections.Generic;

namespace StudentApp.DataAccess.Interface
{
    public interface IProject_dnd_ui_versionsDataAccess
    {
        List<Project_dnd_ui_versionsModel> GetAllProject_dnd_ui_versions(int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<Project_dnd_ui_versionsModel> GetAllProject_dnd_ui_versionsByCreatedBy(string ownername,int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<Project_dnd_ui_versionsModel> SearchProject_dnd_ui_versions(string searchKey,int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null);
        List<Project_dnd_ui_versionsModel> SearchProject_dnd_ui_versionsByCreatedBy(string ownername,string searchKey,int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null);
		List<Project_dnd_ui_versionsModel> FilterProject_dnd_ui_versions(string ownername,List<FilterModel> filterModels, string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        Project_dnd_ui_versionsModel GetProject_dnd_ui_versionsByID(int project_dnd_ui_version_id);
        Project_dnd_ui_versionsModel GetProject_dnd_ui_versionsByIDByCreatedBy(string ownername,int project_dnd_ui_version_id);
        
        
        
		int GetAllTotalRecordProject_dnd_ui_versions();
        int GetAllTotalRecordProject_dnd_ui_versionsByCreatedBy(string ownername);
        int GetSearchTotalRecordProject_dnd_ui_versions(string searchKey);
        int GetSearchTotalRecordProject_dnd_ui_versionsByCreatedBy(string ownername,string searchKey);
        bool UpdateProject_dnd_ui_versions(Project_dnd_ui_versionsModel model);
		int GetFilterTotalRecordProject_dnd_ui_versions(string ownername,List<FilterModel> filterBy, string andOr);
        long AddProject_dnd_ui_versions(Project_dnd_ui_versionsModel model);
        bool DeleteProject_dnd_ui_versions(int project_dnd_ui_version_id);
		bool DeleteMultipleProject_dnd_ui_versions(List<DeleteMultipleModel> deleteParam, string andOr);
		
    }
}
