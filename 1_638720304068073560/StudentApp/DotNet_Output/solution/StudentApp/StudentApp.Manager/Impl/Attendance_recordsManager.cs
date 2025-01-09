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
    public class Attendance_recordsManager : IAttendance_recordsManager
    {
        private readonly IAttendance_recordsDataAccess DataAccess = null;
	//private readonly IRabitMQAsyncProducer _rabitMQAsyncProducer;
        public Attendance_recordsManager(IAttendance_recordsDataAccess dataAccess)
        {
            DataAccess = dataAccess;
	    
        }

        public APIResponse GetAttendance_records(int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null)
        {
            var result = DataAccess.GetAllAttendance_records(page,itemsPerPage,orderBy);
            if (result != null && result.Count > 0)
            { 
                var totalRecords = DataAccess.GetAllTotalRecordAttendance_records();
                var response = new { records = result, pageNumber = page, pageSize = itemsPerPage, totalRecords = totalRecords };
                return new APIResponse(ResponseCode.SUCCESS, "Record Found", response);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "No Record Found");
            }
        }
        public APIResponse GetAttendance_recordsByCreatedBy(string ownername,int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null)
        {
            
            var result = DataAccess.GetAllAttendance_recordsByCreatedBy(ownername,page,itemsPerPage,orderBy);
            if (result != null && result.Count > 0)
            { 
                var totalRecords = DataAccess.GetAllTotalRecordAttendance_recordsByCreatedBy(ownername);
                var response = new { records = result, pageNumber = page, pageSize = itemsPerPage, totalRecords = totalRecords };
                return new APIResponse(ResponseCode.SUCCESS, "Record Found", response);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "No Record Found");
            }
        }
        public APIResponse GetAttendance_recordsByID(int attendance_id)
        {
            var result = DataAccess.GetAttendance_recordsByID(attendance_id);
            if (result != null)
            {
                return new APIResponse(ResponseCode.SUCCESS, "Record Found", result);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "No Record Found");
            }
        }
        public APIResponse GetAttendance_recordsByIDByCreatedBy(string ownername,int attendance_id)
        {
            
            var result = DataAccess.GetAttendance_recordsByIDByCreatedBy(ownername,attendance_id);
            if (result != null)
            {
                return new APIResponse(ResponseCode.SUCCESS, "Record Found", result);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "No Record Found");
            }
        }
        
        public APIResponse GetAttendance_recordsRelational(string ownername,int attendance_id)
        {
            var result = DataAccess.GetAttendance_recordsRelational(ownername,attendance_id);
            if (result != null)
            {
                return new APIResponse(ResponseCode.SUCCESS, "Record Found", result);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "No Record Found");
            }
        }    

        public APIResponse GetAllAttendance_recordsRelational(string ownername,int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null)
        {
            var result = DataAccess.GetAllAttendance_recordsRelational(ownername,page,itemsPerPage,orderBy);
            if (result != null && result.Count > 0)
            { 
                var totalRecords = DataAccess.GetAllTotalRecordAttendance_recordsByCreatedBy(ownername);
                var response = new { records = result, pageNumber = page, pageSize = itemsPerPage, totalRecords = totalRecords };
                return new APIResponse(ResponseCode.SUCCESS, "Record Found", response);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "No Record Found");
            }
        }
                            
        
        public APIResponse GetAttendance_recordsReporting(string ownername,int attendance_id)
        {
            var result = DataAccess.GetAttendance_recordsReporting(ownername,attendance_id);
            if (result != null)
            {
                return new APIResponse(ResponseCode.SUCCESS, "Record Found", result);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "No Record Found");
            }
        }          

        public APIResponse GetAllAttendance_recordsReporting(string ownername,int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null)
        {
            var result = DataAccess.GetAllAttendance_recordsReporting(ownername,page,itemsPerPage,orderBy);
            if (result != null && result.Count > 0)
            { 
                var totalRecords = DataAccess.GetAllTotalRecordAttendance_recordsByCreatedBy(ownername);
                var response = new { records = result, pageNumber = page, pageSize = itemsPerPage, totalRecords = totalRecords };
                return new APIResponse(ResponseCode.SUCCESS, "Record Found", response);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "No Record Found");
            }
        }
                            
        

        public APIResponse UpdateAttendance_recordsWithToken(int attendance_id, Attendance_recordsModel model,string token)
        {
			model.attendance_id=attendance_id;
           
            var result = DataAccess.UpdateAttendance_records(model);
            if (result)
            {
                Dictionary<string, int> primary_key = new Dictionary<string, int>();
                primary_key.Add("attendance_id", attendance_id);

 		//_rabitMQAsyncProducer.SendAsyncMessage(model, primary_key, model.GetType().Name,token);
                return new APIResponse(ResponseCode.SUCCESS, "Record Updated");
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "Record Not Updated");
            }
        }

        public APIResponse AddAttendance_recordsWithToken(Attendance_recordsModel model,string token)
        {
            var result = DataAccess.AddAttendance_records(model);
            if (result > 0)
            {
		model.attendance_id=Convert.ToInt32(result);

		//_rabitMQAsyncProducer.SendAsyncMessage(model, model.GetType().Name,token);
                return new APIResponse(ResponseCode.SUCCESS, "Record Created", result);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "Record Not Created");
            }
        }

        public APIResponse SearchAttendance_records(string searchKey,int page=1,int itemsPerPage=100,List<OrderByModel> orderBy = null)
        {
            var result = DataAccess.SearchAttendance_records(searchKey,page,itemsPerPage,orderBy);
            if (result != null && result.Count > 0)
            {	
				var totalRecords = DataAccess.GetSearchTotalRecordAttendance_records(searchKey);
                var response = new { records = result, pageNumber = page, pageSize = itemsPerPage, totalRecords = totalRecords };
                return new APIResponse(ResponseCode.SUCCESS, "Record Found", response);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "No Record Found");
            }
        }

        public APIResponse SearchAttendance_recordsByCreatedBy(string ownername,string searchKey,int page=1,int itemsPerPage=100,List<OrderByModel> orderBy = null)
        {
            var result = DataAccess.SearchAttendance_recordsByCreatedBy(ownername,searchKey,page,itemsPerPage,orderBy);
            if (result != null && result.Count > 0)
            {	
				var totalRecords = DataAccess.GetSearchTotalRecordAttendance_recordsByCreatedBy(ownername,searchKey);
                var response = new { records = result, pageNumber = page, pageSize = itemsPerPage, totalRecords = totalRecords };
                return new APIResponse(ResponseCode.SUCCESS, "Record Found", response);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "No Record Found");
            }
        }
		public APIResponse DeleteAttendance_recordsWithToken(int attendance_id,string token)
        {
            var result = DataAccess.DeleteAttendance_records(attendance_id);
            if (result)
            {
                Dictionary<string, int> primary_key = new Dictionary<string, int>();
                primary_key.Add("attendance_id", attendance_id);

		var className = GetType().Name.Replace("Manager", "Model");
 		//_rabitMQAsyncProducer.SendAsyncMessage(primary_key, className,token);
                return new APIResponse(ResponseCode.SUCCESS, "Record Deleted");
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "Record Not Deleted");
            }
        }
		
		public APIResponse DeleteMultipleAttendance_recordsWithToken(List<DeleteMultipleModel> deleteParam, string andOr,string token)
        {
            var result = DataAccess.DeleteMultipleAttendance_records(deleteParam, andOr);
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

        public APIResponse FilterAttendance_records(string ownername,List<FilterModel> filterModels, string andOr, int page = 1, int itemsPerPage = 100, List<OrderByModel> orderBy = null)
        {
            var result = DataAccess.FilterAttendance_records(ownername,filterModels, andOr, page, itemsPerPage, orderBy);
            if (result != null && result.Count > 0)
            {
                var totalRecords = DataAccess.GetFilterTotalRecordAttendance_records(ownername,filterModels, andOr);
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
