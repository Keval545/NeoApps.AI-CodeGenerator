using StudentApp.Model;
using StudentApp.Utility;
using System.Collections.Generic;

namespace StudentApp.DataAccess.Interface
{
    public interface IWorkflow_buildsDataAccess
    {
        List<Workflow_buildsModel> GetAllWorkflow_builds(int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<Workflow_buildsModel> GetAllWorkflow_buildsByCreatedBy(string ownername,int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<Workflow_buildsModel> SearchWorkflow_builds(string searchKey,int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null);
        List<Workflow_buildsModel> SearchWorkflow_buildsByCreatedBy(string ownername,string searchKey,int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null);
		List<Workflow_buildsModel> FilterWorkflow_builds(string ownername,List<FilterModel> filterModels, string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        Workflow_buildsModel GetWorkflow_buildsByID(int workflow_build_id);
        Workflow_buildsModel GetWorkflow_buildsByIDByCreatedBy(string ownername,int workflow_build_id);
        
        
        
		int GetAllTotalRecordWorkflow_builds();
        int GetAllTotalRecordWorkflow_buildsByCreatedBy(string ownername);
        int GetSearchTotalRecordWorkflow_builds(string searchKey);
        int GetSearchTotalRecordWorkflow_buildsByCreatedBy(string ownername,string searchKey);
        bool UpdateWorkflow_builds(Workflow_buildsModel model);
		int GetFilterTotalRecordWorkflow_builds(string ownername,List<FilterModel> filterBy, string andOr);
        long AddWorkflow_builds(Workflow_buildsModel model);
        bool DeleteWorkflow_builds(int workflow_build_id);
		bool DeleteMultipleWorkflow_builds(List<DeleteMultipleModel> deleteParam, string andOr);
		
    }
}
