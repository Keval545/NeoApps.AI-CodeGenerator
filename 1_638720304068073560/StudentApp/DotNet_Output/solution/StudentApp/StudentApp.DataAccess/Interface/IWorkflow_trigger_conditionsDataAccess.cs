using StudentApp.Model;
using StudentApp.Utility;
using System.Collections.Generic;

namespace StudentApp.DataAccess.Interface
{
    public interface IWorkflow_trigger_conditionsDataAccess
    {
        List<Workflow_trigger_conditionsModel> GetAllWorkflow_trigger_conditions(int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<Workflow_trigger_conditionsModel> GetAllWorkflow_trigger_conditionsByCreatedBy(string ownername,int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<Workflow_trigger_conditionsModel> SearchWorkflow_trigger_conditions(string searchKey,int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null);
        List<Workflow_trigger_conditionsModel> SearchWorkflow_trigger_conditionsByCreatedBy(string ownername,string searchKey,int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null);
		List<Workflow_trigger_conditionsModel> FilterWorkflow_trigger_conditions(string ownername,List<FilterModel> filterModels, string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        Workflow_trigger_conditionsModel GetWorkflow_trigger_conditionsByID(int workflow_trigger_id);
        Workflow_trigger_conditionsModel GetWorkflow_trigger_conditionsByIDByCreatedBy(string ownername,int workflow_trigger_id);
        
        
        
		int GetAllTotalRecordWorkflow_trigger_conditions();
        int GetAllTotalRecordWorkflow_trigger_conditionsByCreatedBy(string ownername);
        int GetSearchTotalRecordWorkflow_trigger_conditions(string searchKey);
        int GetSearchTotalRecordWorkflow_trigger_conditionsByCreatedBy(string ownername,string searchKey);
        bool UpdateWorkflow_trigger_conditions(Workflow_trigger_conditionsModel model);
		int GetFilterTotalRecordWorkflow_trigger_conditions(string ownername,List<FilterModel> filterBy, string andOr);
        long AddWorkflow_trigger_conditions(Workflow_trigger_conditionsModel model);
        bool DeleteWorkflow_trigger_conditions(int workflow_trigger_id);
		bool DeleteMultipleWorkflow_trigger_conditions(List<DeleteMultipleModel> deleteParam, string andOr);
		
    }
}
