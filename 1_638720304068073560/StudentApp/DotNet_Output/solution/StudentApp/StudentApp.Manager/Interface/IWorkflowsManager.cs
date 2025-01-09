using StudentApp.Model;
using StudentApp.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentApp.Manager.Interface
{
    public interface IWorkflowsManager
    {
        APIResponse GetWorkflows(int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse GetWorkflowsByCreatedBy(string ownername,int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse SearchWorkflows(string searchKey, int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse SearchWorkflowsByCreatedBy(string ownername,string searchKey, int page, int itemsPerPage,List<OrderByModel> orderBy);
		APIResponse FilterWorkflows(string ownername,List<FilterModel> filterModels, string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        APIResponse GetWorkflowsByID(int workflow_id);
        APIResponse GetWorkflowsByIDByCreatedBy(string ownername,int workflow_id);
        
        
        
        APIResponse UpdateWorkflowsWithToken(int workflow_id,WorkflowsModel model,string token);
        APIResponse AddWorkflowsWithToken(WorkflowsModel model,string token);
		APIResponse DeleteWorkflowsWithToken(int workflow_id,string token);
		APIResponse DeleteMultipleWorkflowsWithToken(List<DeleteMultipleModel> deleteParam, string andOr,string token);
    }
}
