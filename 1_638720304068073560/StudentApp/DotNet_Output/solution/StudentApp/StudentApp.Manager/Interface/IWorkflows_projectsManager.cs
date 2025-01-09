using StudentApp.Model;
using StudentApp.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentApp.Manager.Interface
{
    public interface IWorkflows_projectsManager
    {
        APIResponse GetWorkflows_projects(int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse GetWorkflows_projectsByCreatedBy(string ownername,int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse SearchWorkflows_projects(string searchKey, int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse SearchWorkflows_projectsByCreatedBy(string ownername,string searchKey, int page, int itemsPerPage,List<OrderByModel> orderBy);
		APIResponse FilterWorkflows_projects(string ownername,List<FilterModel> filterModels, string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        APIResponse GetWorkflows_projectsByID(int project_id,int workflow_id);
        APIResponse GetWorkflows_projectsByIDByCreatedBy(string ownername,int project_id,int workflow_id);
        
        
        
        APIResponse UpdateWorkflows_projectsWithToken(int project_id,int workflow_id,Workflows_projectsModel model,string token);
        APIResponse AddWorkflows_projectsWithToken(Workflows_projectsModel model,string token);
		APIResponse DeleteWorkflows_projectsWithToken(int project_id,int workflow_id,string token);
		APIResponse DeleteMultipleWorkflows_projectsWithToken(List<DeleteMultipleModel> deleteParam, string andOr,string token);
    }
}
