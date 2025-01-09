using StudentApp.Model;
using StudentApp.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentApp.Manager.Interface
{
    public interface IWorkflow_runsManager
    {
        APIResponse GetWorkflow_runs(int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse GetWorkflow_runsByCreatedBy(string ownername,int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse SearchWorkflow_runs(string searchKey, int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse SearchWorkflow_runsByCreatedBy(string ownername,string searchKey, int page, int itemsPerPage,List<OrderByModel> orderBy);
		APIResponse FilterWorkflow_runs(string ownername,List<FilterModel> filterModels, string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        APIResponse GetWorkflow_runsByID(int workflow_run_id);
        APIResponse GetWorkflow_runsByIDByCreatedBy(string ownername,int workflow_run_id);
        
        
        
        APIResponse UpdateWorkflow_runsWithToken(int workflow_run_id,Workflow_runsModel model,string token);
        APIResponse AddWorkflow_runsWithToken(Workflow_runsModel model,string token);
		APIResponse DeleteWorkflow_runsWithToken(int workflow_run_id,string token);
		APIResponse DeleteMultipleWorkflow_runsWithToken(List<DeleteMultipleModel> deleteParam, string andOr,string token);
    }
}
