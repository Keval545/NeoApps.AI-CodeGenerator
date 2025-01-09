using StudentApp.Model;
using StudentApp.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentApp.Manager.Interface
{
    public interface IWorkflow_deploymentsManager
    {
        APIResponse GetWorkflow_deployments(int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse GetWorkflow_deploymentsByCreatedBy(string ownername,int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse SearchWorkflow_deployments(string searchKey, int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse SearchWorkflow_deploymentsByCreatedBy(string ownername,string searchKey, int page, int itemsPerPage,List<OrderByModel> orderBy);
		APIResponse FilterWorkflow_deployments(string ownername,List<FilterModel> filterModels, string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        APIResponse GetWorkflow_deploymentsByID(int workflow_deployment_id);
        APIResponse GetWorkflow_deploymentsByIDByCreatedBy(string ownername,int workflow_deployment_id);
        
        
        
        APIResponse UpdateWorkflow_deploymentsWithToken(int workflow_deployment_id,Workflow_deploymentsModel model,string token);
        APIResponse AddWorkflow_deploymentsWithToken(Workflow_deploymentsModel model,string token);
		APIResponse DeleteWorkflow_deploymentsWithToken(int workflow_deployment_id,string token);
		APIResponse DeleteMultipleWorkflow_deploymentsWithToken(List<DeleteMultipleModel> deleteParam, string andOr,string token);
    }
}
