using StudentApp.Model;
using StudentApp.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentApp.Manager.Interface
{
    public interface IWorkflowManager
    {
        APIResponse GetWorkflow(int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse GetWorkflowByCreatedBy(string ownername,int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse SearchWorkflow(string searchKey, int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse SearchWorkflowByCreatedBy(string ownername,string searchKey, int page, int itemsPerPage,List<OrderByModel> orderBy);
		APIResponse FilterWorkflow(string ownername,List<FilterModel> filterModels, string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        APIResponse GetWorkflowByID(int id);
        APIResponse GetWorkflowByIDByCreatedBy(string ownername,int id);
        
        
        
        APIResponse UpdateWorkflowWithToken(int id,WorkflowModel model,string token);
        APIResponse AddWorkflowWithToken(WorkflowModel model,string token);
		APIResponse DeleteWorkflowWithToken(int id,string token);
		APIResponse DeleteMultipleWorkflowWithToken(List<DeleteMultipleModel> deleteParam, string andOr,string token);
    }
}
