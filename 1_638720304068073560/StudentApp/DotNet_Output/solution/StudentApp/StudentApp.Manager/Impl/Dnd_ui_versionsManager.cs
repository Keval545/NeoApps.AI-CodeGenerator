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
    public class Dnd_ui_versionsManager : IDnd_ui_versionsManager
    {
        private readonly IDnd_ui_versionsDataAccess DataAccess = null;
	//private readonly IRabitMQAsyncProducer _rabitMQAsyncProducer;
        public Dnd_ui_versionsManager(IDnd_ui_versionsDataAccess dataAccess)
        {
            DataAccess = dataAccess;
	    
        }

        public APIResponse GetDnd_ui_versions(int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null)
        {
            var result = DataAccess.GetAllDnd_ui_versions(page,itemsPerPage,orderBy);
            if (result != null && result.Count > 0)
            { 
                var totalRecords = DataAccess.GetAllTotalRecordDnd_ui_versions();
                var response = new { records = result, pageNumber = page, pageSize = itemsPerPage, totalRecords = totalRecords };
                return new APIResponse(ResponseCode.SUCCESS, "Record Found", response);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "No Record Found");
            }
        }
        public APIResponse GetDnd_ui_versionsByCreatedBy(string ownername,int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null)
        {
            
            var result = DataAccess.GetAllDnd_ui_versionsByCreatedBy(ownername,page,itemsPerPage,orderBy);
            if (result != null && result.Count > 0)
            { 
                var totalRecords = DataAccess.GetAllTotalRecordDnd_ui_versionsByCreatedBy(ownername);
                var response = new { records = result, pageNumber = page, pageSize = itemsPerPage, totalRecords = totalRecords };
                return new APIResponse(ResponseCode.SUCCESS, "Record Found", response);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "No Record Found");
            }
        }
        public APIResponse GetDnd_ui_versionsByID(int dnd_ui_version_id)
        {
            var result = DataAccess.GetDnd_ui_versionsByID(dnd_ui_version_id);
            if (result != null)
            {
                return new APIResponse(ResponseCode.SUCCESS, "Record Found", result);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "No Record Found");
            }
        }
        public APIResponse GetDnd_ui_versionsByIDByCreatedBy(string ownername,int dnd_ui_version_id)
        {
            
            var result = DataAccess.GetDnd_ui_versionsByIDByCreatedBy(ownername,dnd_ui_version_id);
            if (result != null)
            {
                return new APIResponse(ResponseCode.SUCCESS, "Record Found", result);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "No Record Found");
            }
        }
        
        
        

        public APIResponse UpdateDnd_ui_versionsWithToken(int dnd_ui_version_id, Dnd_ui_versionsModel model,string token)
        {
			model.dnd_ui_version_id=dnd_ui_version_id;
           
            var result = DataAccess.UpdateDnd_ui_versions(model);
            if (result)
            {
                Dictionary<string, int> primary_key = new Dictionary<string, int>();
                primary_key.Add("dnd_ui_version_id", dnd_ui_version_id);

 		//_rabitMQAsyncProducer.SendAsyncMessage(model, primary_key, model.GetType().Name,token);
                return new APIResponse(ResponseCode.SUCCESS, "Record Updated");
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "Record Not Updated");
            }
        }

        public APIResponse AddDnd_ui_versionsWithToken(Dnd_ui_versionsModel model,string token)
        {
            var result = DataAccess.AddDnd_ui_versions(model);
            if (result > 0)
            {
		model.dnd_ui_version_id=Convert.ToInt32(result);

		//_rabitMQAsyncProducer.SendAsyncMessage(model, model.GetType().Name,token);
                return new APIResponse(ResponseCode.SUCCESS, "Record Created", result);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "Record Not Created");
            }
        }

        public APIResponse SearchDnd_ui_versions(string searchKey,int page=1,int itemsPerPage=100,List<OrderByModel> orderBy = null)
        {
            var result = DataAccess.SearchDnd_ui_versions(searchKey,page,itemsPerPage,orderBy);
            if (result != null && result.Count > 0)
            {	
				var totalRecords = DataAccess.GetSearchTotalRecordDnd_ui_versions(searchKey);
                var response = new { records = result, pageNumber = page, pageSize = itemsPerPage, totalRecords = totalRecords };
                return new APIResponse(ResponseCode.SUCCESS, "Record Found", response);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "No Record Found");
            }
        }

        public APIResponse SearchDnd_ui_versionsByCreatedBy(string ownername,string searchKey,int page=1,int itemsPerPage=100,List<OrderByModel> orderBy = null)
        {
            var result = DataAccess.SearchDnd_ui_versionsByCreatedBy(ownername,searchKey,page,itemsPerPage,orderBy);
            if (result != null && result.Count > 0)
            {	
				var totalRecords = DataAccess.GetSearchTotalRecordDnd_ui_versionsByCreatedBy(ownername,searchKey);
                var response = new { records = result, pageNumber = page, pageSize = itemsPerPage, totalRecords = totalRecords };
                return new APIResponse(ResponseCode.SUCCESS, "Record Found", response);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "No Record Found");
            }
        }
		public APIResponse DeleteDnd_ui_versionsWithToken(int dnd_ui_version_id,string token)
        {
            var result = DataAccess.DeleteDnd_ui_versions(dnd_ui_version_id);
            if (result)
            {
                Dictionary<string, int> primary_key = new Dictionary<string, int>();
                primary_key.Add("dnd_ui_version_id", dnd_ui_version_id);

		var className = GetType().Name.Replace("Manager", "Model");
 		//_rabitMQAsyncProducer.SendAsyncMessage(primary_key, className,token);
                return new APIResponse(ResponseCode.SUCCESS, "Record Deleted");
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "Record Not Deleted");
            }
        }
		
		public APIResponse DeleteMultipleDnd_ui_versionsWithToken(List<DeleteMultipleModel> deleteParam, string andOr,string token)
        {
            var result = DataAccess.DeleteMultipleDnd_ui_versions(deleteParam, andOr);
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

        public APIResponse FilterDnd_ui_versions(string ownername,List<FilterModel> filterModels, string andOr, int page = 1, int itemsPerPage = 100, List<OrderByModel> orderBy = null)
        {
            var result = DataAccess.FilterDnd_ui_versions(ownername,filterModels, andOr, page, itemsPerPage, orderBy);
            if (result != null && result.Count > 0)
            {
                var totalRecords = DataAccess.GetFilterTotalRecordDnd_ui_versions(ownername,filterModels, andOr);
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
