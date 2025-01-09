using StudentApp.Model;
using StudentApp.Utility;
using System.Collections.Generic;

namespace StudentApp.DataAccess.Interface
{
    public interface IDnd_ui_versionsDataAccess
    {
        List<Dnd_ui_versionsModel> GetAllDnd_ui_versions(int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<Dnd_ui_versionsModel> GetAllDnd_ui_versionsByCreatedBy(string ownername,int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<Dnd_ui_versionsModel> SearchDnd_ui_versions(string searchKey,int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null);
        List<Dnd_ui_versionsModel> SearchDnd_ui_versionsByCreatedBy(string ownername,string searchKey,int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null);
		List<Dnd_ui_versionsModel> FilterDnd_ui_versions(string ownername,List<FilterModel> filterModels, string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        Dnd_ui_versionsModel GetDnd_ui_versionsByID(int dnd_ui_version_id);
        Dnd_ui_versionsModel GetDnd_ui_versionsByIDByCreatedBy(string ownername,int dnd_ui_version_id);
        
        
        
		int GetAllTotalRecordDnd_ui_versions();
        int GetAllTotalRecordDnd_ui_versionsByCreatedBy(string ownername);
        int GetSearchTotalRecordDnd_ui_versions(string searchKey);
        int GetSearchTotalRecordDnd_ui_versionsByCreatedBy(string ownername,string searchKey);
        bool UpdateDnd_ui_versions(Dnd_ui_versionsModel model);
		int GetFilterTotalRecordDnd_ui_versions(string ownername,List<FilterModel> filterBy, string andOr);
        long AddDnd_ui_versions(Dnd_ui_versionsModel model);
        bool DeleteDnd_ui_versions(int dnd_ui_version_id);
		bool DeleteMultipleDnd_ui_versions(List<DeleteMultipleModel> deleteParam, string andOr);
		
    }
}
