using StudentApp.Model;
using StudentApp.Utility;
using System.Collections.Generic;

namespace StudentApp.DataAccess.Interface
{
    public interface IWorkflow_runsDataAccess
    {
        List<Workflow_runsModel> GetAllWorkflow_runs(int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<Workflow_runsModel> GetAllWorkflow_runsByCreatedBy(string ownername,int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<Workflow_runsModel> SearchWorkflow_runs(string searchKey,int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null);
        List<Workflow_runsModel> SearchWorkflow_runsByCreatedBy(string ownername,string searchKey,int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null);
		List<Workflow_runsModel> FilterWorkflow_runs(string ownername,List<FilterModel> filterModels, string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        Workflow_runsModel GetWorkflow_runsByID(int workflow_run_id);
        Workflow_runsModel GetWorkflow_runsByIDByCreatedBy(string ownername,int workflow_run_id);
        
        
        
		int GetAllTotalRecordWorkflow_runs();
        int GetAllTotalRecordWorkflow_runsByCreatedBy(string ownername);
        int GetSearchTotalRecordWorkflow_runs(string searchKey);
        int GetSearchTotalRecordWorkflow_runsByCreatedBy(string ownername,string searchKey);
        bool UpdateWorkflow_runs(Workflow_runsModel model);
		int GetFilterTotalRecordWorkflow_runs(string ownername,List<FilterModel> filterBy, string andOr);
        long AddWorkflow_runs(Workflow_runsModel model);
        bool DeleteWorkflow_runs(int workflow_run_id);
		bool DeleteMultipleWorkflow_runs(List<DeleteMultipleModel> deleteParam, string andOr);
		
    }
}
