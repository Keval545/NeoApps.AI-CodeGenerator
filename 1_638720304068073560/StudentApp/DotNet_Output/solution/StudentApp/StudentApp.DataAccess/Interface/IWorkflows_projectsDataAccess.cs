using StudentApp.Model;
using StudentApp.Utility;
using System.Collections.Generic;

namespace StudentApp.DataAccess.Interface
{
    public interface IWorkflows_projectsDataAccess
    {
        List<Workflows_projectsModel> GetAllWorkflows_projects(int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<Workflows_projectsModel> GetAllWorkflows_projectsByCreatedBy(string ownername,int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<Workflows_projectsModel> SearchWorkflows_projects(string searchKey,int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null);
        List<Workflows_projectsModel> SearchWorkflows_projectsByCreatedBy(string ownername,string searchKey,int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null);
		List<Workflows_projectsModel> FilterWorkflows_projects(string ownername,List<FilterModel> filterModels, string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        Workflows_projectsModel GetWorkflows_projectsByID(int project_id,int workflow_id);
        Workflows_projectsModel GetWorkflows_projectsByIDByCreatedBy(string ownername,int project_id,int workflow_id);
        
        
        
		int GetAllTotalRecordWorkflows_projects();
        int GetAllTotalRecordWorkflows_projectsByCreatedBy(string ownername);
        int GetSearchTotalRecordWorkflows_projects(string searchKey);
        int GetSearchTotalRecordWorkflows_projectsByCreatedBy(string ownername,string searchKey);
        bool UpdateWorkflows_projects(Workflows_projectsModel model);
		int GetFilterTotalRecordWorkflows_projects(string ownername,List<FilterModel> filterBy, string andOr);
        long AddWorkflows_projects(Workflows_projectsModel model);
        bool DeleteWorkflows_projects(int project_id,int workflow_id);
		bool DeleteMultipleWorkflows_projects(List<DeleteMultipleModel> deleteParam, string andOr);
		
    }
}
