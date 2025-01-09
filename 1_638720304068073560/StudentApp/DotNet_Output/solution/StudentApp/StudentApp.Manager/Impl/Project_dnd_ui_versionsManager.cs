//using StudentApp.Manager.RabitMQAPI.API;
using StudentApp.DataAccess.Interface;
using StudentApp.Manager.Interface;
using StudentApp.Model;
using StudentApp.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentApp.Manager.Impl
{
    public class Project_dnd_ui_versionsManager : IProject_dnd_ui_versionsManager
    {
        private readonly IProject_dnd_ui_versionsDataAccess DataAccess = null;
	//private readonly IRabitMQAsyncProducer _rabitMQAsyncProducer;
        public Project_dnd_ui_versionsManager(IProject_dnd_ui_versionsDataAccess dataAccess)
        {
            DataAccess = dataAccess;
	    
        }

        public APIResponse GetProject_dnd_ui_versions(int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null)
        {
            var result = DataAccess.GetAllProject_dnd_ui_versions(page,itemsPerPage,orderBy);
            if (result != null && result.Count > 0)
            { 
                var totalRecords = DataAccess.GetAllTotalRecordProject_dnd_ui_versions();
                var response = new { records = result, pageNumber = page, pageSize = itemsPerPage, totalRecords = totalRecords };
                return new APIResponse(ResponseCode.SUCCESS, "Record Found", response);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "No Record Found");
            }
        }
        public APIResponse GetProject_dnd_ui_versionsByCreatedBy(string ownername,int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null)
        {
            
            var result = DataAccess.GetAllProject_dnd_ui_versionsByCreatedBy(ownername,page,itemsPerPage,orderBy);
            if (result != null && result.Count > 0)
            { 
                var totalRecords = DataAccess.GetAllTotalRecordProject_dnd_ui_versionsByCreatedBy(ownername);
                var response = new { records = result, pageNumber = page, pageSize = itemsPerPage, totalRecords = totalRecords };
                return new APIResponse(ResponseCode.SUCCESS, "Record Found", response);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "No Record Found");
            }
        }
        public APIResponse GetProject_dnd_ui_versionsByID(int project_dnd_ui_version_id)
        {
            var result = DataAccess.GetProject_dnd_ui_versionsByID(project_dnd_ui_version_id);
            if (result != null)
            {
                return new APIResponse(ResponseCode.SUCCESS, "Record Found", result);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "No Record Found");
            }
        }
        public APIResponse GetProject_dnd_ui_versionsByIDByCreatedBy(string ownername,int project_dnd_ui_version_id)
        {
            
            var result = DataAccess.GetProject_dnd_ui_versionsByIDByCreatedBy(ownername,project_dnd_ui_version_id);
            if (result != null)
            {
                return new APIResponse(ResponseCode.SUCCESS, "Record Found", result);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "No Record Found");
            }
        }
        
        
        

        public APIResponse UpdateProject_dnd_ui_versionsWithToken(int project_dnd_ui_version_id, Project_dnd_ui_versionsModel model,string token)
        {
			model.project_dnd_ui_version_id=project_dnd_ui_version_id;
           
            var result = DataAccess.UpdateProject_dnd_ui_versions(model);
            if (result)
            {
                Dictionary<string, int> primary_key = new Dictionary<string, int>();
                primary_key.Add("project_dnd_ui_version_id", project_dnd_ui_version_id);

 		//_rabitMQAsyncProducer.SendAsyncMessage(model, primary_key, model.GetType().Name,token);
                return new APIResponse(ResponseCode.SUCCESS, "Record Updated");
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "Record Not Updated");
            }
        }

        public APIResponse AddProject_dnd_ui_versionsWithToken(Project_dnd_ui_versionsModel model,string token)
        {
            var result = DataAccess.AddProject_dnd_ui_versions(model);
            if (result > 0)
            {
		model.project_dnd_ui_version_id=Convert.ToInt32(result);

		//_rabitMQAsyncProducer.SendAsyncMessage(model, model.GetType().Name,token);
                return new APIResponse(ResponseCode.SUCCESS, "Record Created", result);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "Record Not Created");
            }
        }

        public APIResponse SearchProject_dnd_ui_versions(string searchKey,int page=1,int itemsPerPage=100,List<OrderByModel> orderBy = null)
        {
            var result = DataAccess.SearchProject_dnd_ui_versions(searchKey,page,itemsPerPage,orderBy);
            if (result != null && result.Count > 0)
            {	
				var totalRecords = DataAccess.GetSearchTotalRecordProject_dnd_ui_versions(searchKey);
                var response = new { records = result, pageNumber = page, pageSize = itemsPerPage, totalRecords = totalRecords };
                return new APIResponse(ResponseCode.SUCCESS, "Record Found", response);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "No Record Found");
            }
        }

        public APIResponse SearchProject_dnd_ui_versionsByCreatedBy(string ownername,string searchKey,int page=1,int itemsPerPage=100,List<OrderByModel> orderBy = null)
        {
            var result = DataAccess.SearchProject_dnd_ui_versionsByCreatedBy(ownername,searchKey,page,itemsPerPage,orderBy);
            if (result != null && result.Count > 0)
            {	
				var totalRecords = DataAccess.GetSearchTotalRecordProject_dnd_ui_versionsByCreatedBy(ownername,searchKey);
                var response = new { records = result, pageNumber = page, pageSize = itemsPerPage, totalRecords = totalRecords };
                return new APIResponse(ResponseCode.SUCCESS, "Record Found", response);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "No Record Found");
            }
        }
		public APIResponse DeleteProject_dnd_ui_versionsWithToken(int project_dnd_ui_version_id,string token)
        {
            var result = DataAccess.DeleteProject_dnd_ui_versions(project_dnd_ui_version_id);
            if (result)
            {
                Dictionary<string, int> primary_key = new Dictionary<string, int>();
                primary_key.Add("project_dnd_ui_version_id", project_dnd_ui_version_id);

		var className = GetType().Name.Replace("Manager", "Model");
 		//_rabitMQAsyncProducer.SendAsyncMessage(primary_key, className,token);
                return new APIResponse(ResponseCode.SUCCESS, "Record Deleted");
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "Record Not Deleted");
            }
        }
		
		public APIResponse DeleteMultipleProject_dnd_ui_versionsWithToken(List<DeleteMultipleModel> deleteParam, string andOr,string token)
        {
            var result = DataAccess.DeleteMultipleProject_dnd_ui_versions(deleteParam, andOr);
            if (result)
            {
               // _rabitMQAsyncProducer.SendAsyncMessage(deleteParam, GetType().Name,token);
                return new APIResponse(ResponseCode.SUCCESS, "Record Deleted");
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "Record Not Deleted");
            }
        }

        public APIResponse FilterProject_dnd_ui_versions(string ownername,List<FilterModel> filterModels, string andOr, int page = 1, int itemsPerPage = 100, List<OrderByModel> orderBy = null)
        {
            var result = DataAccess.FilterProject_dnd_ui_versions(ownername,filterModels, andOr, page, itemsPerPage, orderBy);
            if (result != null && result.Count > 0)
            {
                var totalRecords = DataAccess.GetFilterTotalRecordProject_dnd_ui_versions(ownername,filterModels, andOr);
                var response = new { records = result, pageNumber = page, pageSize = itemsPerPage, totalRecords = totalRecords };
                return new APIResponse(ResponseCode.SUCCESS, "Record Found", response);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "No Record Found");
            }
        }
    }
}
