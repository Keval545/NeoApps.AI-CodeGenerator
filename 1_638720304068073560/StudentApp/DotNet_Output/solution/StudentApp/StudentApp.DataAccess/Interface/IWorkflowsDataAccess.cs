using StudentApp.Model;
using StudentApp.Utility;
using System.Collections.Generic;

namespace StudentApp.DataAccess.Interface
{
    public interface IWorkflowsDataAccess
    {
        List<WorkflowsModel> GetAllWorkflows(int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<WorkflowsModel> GetAllWorkflowsByCreatedBy(string ownername,int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<WorkflowsModel> SearchWorkflows(string searchKey,int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null);
        List<WorkflowsModel> SearchWorkflowsByCreatedBy(string ownername,string searchKey,int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null);
		List<WorkflowsModel> FilterWorkflows(string ownername,List<FilterModel> filterModels, string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        WorkflowsModel GetWorkflowsByID(int workflow_id);
        WorkflowsModel GetWorkflowsByIDByCreatedBy(string ownername,int workflow_id);
        
        
        
		int GetAllTotalRecordWorkflows();
        int GetAllTotalRecordWorkflowsByCreatedBy(string ownername);
        int GetSearchTotalRecordWorkflows(string searchKey);
        int GetSearchTotalRecordWorkflowsByCreatedBy(string ownername,string searchKey);
        bool UpdateWorkflows(WorkflowsModel model);
		int GetFilterTotalRecordWorkflows(string ownername,List<FilterModel> filterBy, string andOr);
        long AddWorkflows(WorkflowsModel model);
        bool DeleteWorkflows(int workflow_id);
		bool DeleteMultipleWorkflows(List<DeleteMultipleModel> deleteParam, string andOr);
		
    }
}
