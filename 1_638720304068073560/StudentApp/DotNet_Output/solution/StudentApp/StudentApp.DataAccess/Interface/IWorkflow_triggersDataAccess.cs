using StudentApp.Model;
using StudentApp.Utility;
using System.Collections.Generic;

namespace StudentApp.DataAccess.Interface
{
    public interface IWorkflow_triggersDataAccess
    {
        List<Workflow_triggersModel> GetAllWorkflow_triggers(int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<Workflow_triggersModel> GetAllWorkflow_triggersByCreatedBy(string ownername,int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<Workflow_triggersModel> SearchWorkflow_triggers(string searchKey,int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null);
        List<Workflow_triggersModel> SearchWorkflow_triggersByCreatedBy(string ownername,string searchKey,int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null);
		List<Workflow_triggersModel> FilterWorkflow_triggers(string ownername,List<FilterModel> filterModels, string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        Workflow_triggersModel GetWorkflow_triggersByID(int workflow_trigger_id);
        Workflow_triggersModel GetWorkflow_triggersByIDByCreatedBy(string ownername,int workflow_trigger_id);
        
        
        
		int GetAllTotalRecordWorkflow_triggers();
        int GetAllTotalRecordWorkflow_triggersByCreatedBy(string ownername);
        int GetSearchTotalRecordWorkflow_triggers(string searchKey);
        int GetSearchTotalRecordWorkflow_triggersByCreatedBy(string ownername,string searchKey);
        bool UpdateWorkflow_triggers(Workflow_triggersModel model);
		int GetFilterTotalRecordWorkflow_triggers(string ownername,List<FilterModel> filterBy, string andOr);
        long AddWorkflow_triggers(Workflow_triggersModel model);
        bool DeleteWorkflow_triggers(int workflow_trigger_id);
		bool DeleteMultipleWorkflow_triggers(List<DeleteMultipleModel> deleteParam, string andOr);
		
    }
}
