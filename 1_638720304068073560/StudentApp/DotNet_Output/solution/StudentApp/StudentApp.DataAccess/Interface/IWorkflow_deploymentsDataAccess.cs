using StudentApp.Model;
using StudentApp.Utility;
using System.Collections.Generic;

namespace StudentApp.DataAccess.Interface
{
    public interface IWorkflow_deploymentsDataAccess
    {
        List<Workflow_deploymentsModel> GetAllWorkflow_deployments(int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<Workflow_deploymentsModel> GetAllWorkflow_deploymentsByCreatedBy(string ownername,int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<Workflow_deploymentsModel> SearchWorkflow_deployments(string searchKey,int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null);
        List<Workflow_deploymentsModel> SearchWorkflow_deploymentsByCreatedBy(string ownername,string searchKey,int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null);
		List<Workflow_deploymentsModel> FilterWorkflow_deployments(string ownername,List<FilterModel> filterModels, string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        Workflow_deploymentsModel GetWorkflow_deploymentsByID(int workflow_deployment_id);
        Workflow_deploymentsModel GetWorkflow_deploymentsByIDByCreatedBy(string ownername,int workflow_deployment_id);
        
        
        
		int GetAllTotalRecordWorkflow_deployments();
        int GetAllTotalRecordWorkflow_deploymentsByCreatedBy(string ownername);
        int GetSearchTotalRecordWorkflow_deployments(string searchKey);
        int GetSearchTotalRecordWorkflow_deploymentsByCreatedBy(string ownername,string searchKey);
        bool UpdateWorkflow_deployments(Workflow_deploymentsModel model);
		int GetFilterTotalRecordWorkflow_deployments(string ownername,List<FilterModel> filterBy, string andOr);
        long AddWorkflow_deployments(Workflow_deploymentsModel model);
        bool DeleteWorkflow_deployments(int workflow_deployment_id);
		bool DeleteMultipleWorkflow_deployments(List<DeleteMultipleModel> deleteParam, string andOr);
		
    }
}
