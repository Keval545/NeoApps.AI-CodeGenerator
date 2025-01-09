using StudentApp.Model;
using StudentApp.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentApp.Manager.Interface
{
    public interface IWorkflow_triggersManager
    {
        APIResponse GetWorkflow_triggers(int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse GetWorkflow_triggersByCreatedBy(string ownername,int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse SearchWorkflow_triggers(string searchKey, int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse SearchWorkflow_triggersByCreatedBy(string ownername,string searchKey, int page, int itemsPerPage,List<OrderByModel> orderBy);
		APIResponse FilterWorkflow_triggers(string ownername,List<FilterModel> filterModels, string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        APIResponse GetWorkflow_triggersByID(int workflow_trigger_id);
        APIResponse GetWorkflow_triggersByIDByCreatedBy(string ownername,int workflow_trigger_id);
        
        
        
        APIResponse UpdateWorkflow_triggersWithToken(int workflow_trigger_id,Workflow_triggersModel model,string token);
        APIResponse AddWorkflow_triggersWithToken(Workflow_triggersModel model,string token);
		APIResponse DeleteWorkflow_triggersWithToken(int workflow_trigger_id,string token);
		APIResponse DeleteMultipleWorkflow_triggersWithToken(List<DeleteMultipleModel> deleteParam, string andOr,string token);
    }
}
