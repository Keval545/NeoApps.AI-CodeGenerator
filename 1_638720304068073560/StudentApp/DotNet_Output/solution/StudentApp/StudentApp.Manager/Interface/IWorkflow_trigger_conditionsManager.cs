using StudentApp.Model;
using StudentApp.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentApp.Manager.Interface
{
    public interface IWorkflow_trigger_conditionsManager
    {
        APIResponse GetWorkflow_trigger_conditions(int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse GetWorkflow_trigger_conditionsByCreatedBy(string ownername,int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse SearchWorkflow_trigger_conditions(string searchKey, int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse SearchWorkflow_trigger_conditionsByCreatedBy(string ownername,string searchKey, int page, int itemsPerPage,List<OrderByModel> orderBy);
		APIResponse FilterWorkflow_trigger_conditions(string ownername,List<FilterModel> filterModels, string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        APIResponse GetWorkflow_trigger_conditionsByID(int workflow_trigger_id);
        APIResponse GetWorkflow_trigger_conditionsByIDByCreatedBy(string ownername,int workflow_trigger_id);
        
        
        
        APIResponse UpdateWorkflow_trigger_conditionsWithToken(int workflow_trigger_id,Workflow_trigger_conditionsModel model,string token);
        APIResponse AddWorkflow_trigger_conditionsWithToken(Workflow_trigger_conditionsModel model,string token);
		APIResponse DeleteWorkflow_trigger_conditionsWithToken(int workflow_trigger_id,string token);
		APIResponse DeleteMultipleWorkflow_trigger_conditionsWithToken(List<DeleteMultipleModel> deleteParam, string andOr,string token);
    }
}
