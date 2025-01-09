using StudentApp.Model;
using StudentApp.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentApp.Manager.Interface
{
    public interface IWorkflow_buildsManager
    {
        APIResponse GetWorkflow_builds(int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse GetWorkflow_buildsByCreatedBy(string ownername,int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse SearchWorkflow_builds(string searchKey, int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse SearchWorkflow_buildsByCreatedBy(string ownername,string searchKey, int page, int itemsPerPage,List<OrderByModel> orderBy);
		APIResponse FilterWorkflow_builds(string ownername,List<FilterModel> filterModels, string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        APIResponse GetWorkflow_buildsByID(int workflow_build_id);
        APIResponse GetWorkflow_buildsByIDByCreatedBy(string ownername,int workflow_build_id);
        
        
        
        APIResponse UpdateWorkflow_buildsWithToken(int workflow_build_id,Workflow_buildsModel model,string token);
        APIResponse AddWorkflow_buildsWithToken(Workflow_buildsModel model,string token);
		APIResponse DeleteWorkflow_buildsWithToken(int workflow_build_id,string token);
		APIResponse DeleteMultipleWorkflow_buildsWithToken(List<DeleteMultipleModel> deleteParam, string andOr,string token);
    }
}
