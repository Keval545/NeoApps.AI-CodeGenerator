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
    public class S3bucket_foldersManager : IS3bucket_foldersManager
    {
        private readonly IS3bucket_foldersDataAccess DataAccess = null;
	//private readonly IRabitMQAsyncProducer _rabitMQAsyncProducer;
        public S3bucket_foldersManager(IS3bucket_foldersDataAccess dataAccess)
        {
            DataAccess = dataAccess;
	    
        }

        public APIResponse GetS3bucket_folders(int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null)
        {
            var result = DataAccess.GetAllS3bucket_folders(page,itemsPerPage,orderBy);
            if (result != null && result.Count > 0)
            { 
                var totalRecords = DataAccess.GetAllTotalRecordS3bucket_folders();
                var response = new { records = result, pageNumber = page, pageSize = itemsPerPage, totalRecords = totalRecords };
                return new APIResponse(ResponseCode.SUCCESS, "Record Found", response);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "No Record Found");
            }
        }
        public APIResponse GetS3bucket_foldersByCreatedBy(string ownername,int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null)
        {
            
            var result = DataAccess.GetAllS3bucket_foldersByCreatedBy(ownername,page,itemsPerPage,orderBy);
            if (result != null && result.Count > 0)
            { 
                var totalRecords = DataAccess.GetAllTotalRecordS3bucket_foldersByCreatedBy(ownername);
                var response = new { records = result, pageNumber = page, pageSize = itemsPerPage, totalRecords = totalRecords };
                return new APIResponse(ResponseCode.SUCCESS, "Record Found", response);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "No Record Found");
            }
        }
        public APIResponse GetS3bucket_foldersByID(int folder_id)
        {
            var result = DataAccess.GetS3bucket_foldersByID(folder_id);
            if (result != null)
            {
                return new APIResponse(ResponseCode.SUCCESS, "Record Found", result);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "No Record Found");
            }
        }
        public APIResponse GetS3bucket_foldersByIDByCreatedBy(string ownername,int folder_id)
        {
            
            var result = DataAccess.GetS3bucket_foldersByIDByCreatedBy(ownername,folder_id);
            if (result != null)
            {
                return new APIResponse(ResponseCode.SUCCESS, "Record Found", result);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "No Record Found");
            }
        }
        
        
        

        public APIResponse UpdateS3bucket_foldersWithToken(int folder_id, S3bucket_foldersModel model,string token)
        {
			model.folder_id=folder_id;
           
            var result = DataAccess.UpdateS3bucket_folders(model);
            if (result)
            {
                Dictionary<string, int> primary_key = new Dictionary<string, int>();
                primary_key.Add("folder_id", folder_id);

 		//_rabitMQAsyncProducer.SendAsyncMessage(model, primary_key, model.GetType().Name,token);
                return new APIResponse(ResponseCode.SUCCESS, "Record Updated");
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "Record Not Updated");
            }
        }

        public APIResponse AddS3bucket_foldersWithToken(S3bucket_foldersModel model,string token)
        {
            var result = DataAccess.AddS3bucket_folders(model);
            if (result > 0)
            {
		model.folder_id=Convert.ToInt32(result);

		//_rabitMQAsyncProducer.SendAsyncMessage(model, model.GetType().Name,token);
                return new APIResponse(ResponseCode.SUCCESS, "Record Created", result);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "Record Not Created");
            }
        }

        public APIResponse SearchS3bucket_folders(string searchKey,int page=1,int itemsPerPage=100,List<OrderByModel> orderBy = null)
        {
            var result = DataAccess.SearchS3bucket_folders(searchKey,page,itemsPerPage,orderBy);
            if (result != null && result.Count > 0)
            {	
				var totalRecords = DataAccess.GetSearchTotalRecordS3bucket_folders(searchKey);
                var response = new { records = result, pageNumber = page, pageSize = itemsPerPage, totalRecords = totalRecords };
                return new APIResponse(ResponseCode.SUCCESS, "Record Found", response);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "No Record Found");
            }
        }

        public APIResponse SearchS3bucket_foldersByCreatedBy(string ownername,string searchKey,int page=1,int itemsPerPage=100,List<OrderByModel> orderBy = null)
        {
            var result = DataAccess.SearchS3bucket_foldersByCreatedBy(ownername,searchKey,page,itemsPerPage,orderBy);
            if (result != null && result.Count > 0)
            {	
				var totalRecords = DataAccess.GetSearchTotalRecordS3bucket_foldersByCreatedBy(ownername,searchKey);
                var response = new { records = result, pageNumber = page, pageSize = itemsPerPage, totalRecords = totalRecords };
                return new APIResponse(ResponseCode.SUCCESS, "Record Found", response);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "No Record Found");
            }
        }
		public APIResponse DeleteS3bucket_foldersWithToken(int folder_id,string token)
        {
            var result = DataAccess.DeleteS3bucket_folders(folder_id);
            if (result)
            {
                Dictionary<string, int> primary_key = new Dictionary<string, int>();
                primary_key.Add("folder_id", folder_id);

		var className = GetType().Name.Replace("Manager", "Model");
 		//_rabitMQAsyncProducer.SendAsyncMessage(primary_key, className,token);
                return new APIResponse(ResponseCode.SUCCESS, "Record Deleted");
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "Record Not Deleted");
            }
        }
		
		public APIResponse DeleteMultipleS3bucket_foldersWithToken(List<DeleteMultipleModel> deleteParam, string andOr,string token)
        {
            var result = DataAccess.DeleteMultipleS3bucket_folders(deleteParam, andOr);
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

        public APIResponse FilterS3bucket_folders(string ownername,List<FilterModel> filterModels, string andOr, int page = 1, int itemsPerPage = 100, List<OrderByModel> orderBy = null)
        {
            var result = DataAccess.FilterS3bucket_folders(ownername,filterModels, andOr, page, itemsPerPage, orderBy);
            if (result != null && result.Count > 0)
            {
                var totalRecords = DataAccess.GetFilterTotalRecordS3bucket_folders(ownername,filterModels, andOr);
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
