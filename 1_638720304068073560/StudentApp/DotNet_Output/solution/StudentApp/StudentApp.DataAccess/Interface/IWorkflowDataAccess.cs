using StudentApp.Model;
using StudentApp.Utility;
using System.Collections.Generic;

namespace StudentApp.DataAccess.Interface
{
    public interface IWorkflowDataAccess
    {
        List<WorkflowModel> GetAllWorkflow(int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<WorkflowModel> GetAllWorkflowByCreatedBy(string ownername,int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<WorkflowModel> SearchWorkflow(string searchKey,int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null);
        List<WorkflowModel> SearchWorkflowByCreatedBy(string ownername,string searchKey,int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null);
		List<WorkflowModel> FilterWorkflow(string ownername,List<FilterModel> filterModels, string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        WorkflowModel GetWorkflowByID(int id);
        WorkflowModel GetWorkflowByIDByCreatedBy(string ownername,int id);
        
        
        
		int GetAllTotalRecordWorkflow();
        int GetAllTotalRecordWorkflowByCreatedBy(string ownername);
        int GetSearchTotalRecordWorkflow(string searchKey);
        int GetSearchTotalRecordWorkflowByCreatedBy(string ownername,string searchKey);
        bool UpdateWorkflow(WorkflowModel model);
		int GetFilterTotalRecordWorkflow(string ownername,List<FilterModel> filterBy, string andOr);
        long AddWorkflow(WorkflowModel model);
        bool DeleteWorkflow(int id);
		bool DeleteMultipleWorkflow(List<DeleteMultipleModel> deleteParam, string andOr);
		
    }
}
