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
    public class Workflow_runsManager : IWorkflow_runsManager
    {
        private readonly IWorkflow_runsDataAccess DataAccess = null;
	//private readonly IRabitMQAsyncProducer _rabitMQAsyncProducer;
        public Workflow_runsManager(IWorkflow_runsDataAccess dataAccess)
        {
            DataAccess = dataAccess;
	    
        }

        public APIResponse GetWorkflow_runs(int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null)
        {
            var result = DataAccess.GetAllWorkflow_runs(page,itemsPerPage,orderBy);
            if (result != null && result.Count > 0)
            { 
                var totalRecords = DataAccess.GetAllTotalRecordWorkflow_runs();
                var response = new { records = result, pageNumber = page, pageSize = itemsPerPage, totalRecords = totalRecords };
                return new APIResponse(ResponseCode.SUCCESS, "Record Found", response);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "No Record Found");
            }
        }
        public APIResponse GetWorkflow_runsByCreatedBy(string ownername,int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null)
        {
            
            var result = DataAccess.GetAllWorkflow_runsByCreatedBy(ownername,page,itemsPerPage,orderBy);
            if (result != null && result.Count > 0)
            { 
                var totalRecords = DataAccess.GetAllTotalRecordWorkflow_runsByCreatedBy(ownername);
                var response = new { records = result, pageNumber = page, pageSize = itemsPerPage, totalRecords = totalRecords };
                return new APIResponse(ResponseCode.SUCCESS, "Record Found", response);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "No Record Found");
            }
        }
        public APIResponse GetWorkflow_runsByID(int workflow_run_id)
        {
            var result = DataAccess.GetWorkflow_runsByID(workflow_run_id);
            if (result != null)
            {
                return new APIResponse(ResponseCode.SUCCESS, "Record Found", result);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "No Record Found");
            }
        }
        public APIResponse GetWorkflow_runsByIDByCreatedBy(string ownername,int workflow_run_id)
        {
            
            var result = DataAccess.GetWorkflow_runsByIDByCreatedBy(ownername,workflow_run_id);
            if (result != null)
            {
                return new APIResponse(ResponseCode.SUCCESS, "Record Found", result);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "No Record Found");
            }
        }
        
        
        

        public APIResponse UpdateWorkflow_runsWithToken(int workflow_run_id, Workflow_runsModel model,string token)
        {
			model.workflow_run_id=workflow_run_id;
           
            var result = DataAccess.UpdateWorkflow_runs(model);
            if (result)
            {
                Dictionary<string, int> primary_key = new Dictionary<string, int>();
                primary_key.Add("workflow_run_id", workflow_run_id);

 		//_rabitMQAsyncProducer.SendAsyncMessage(model, primary_key, model.GetType().Name,token);
                return new APIResponse(ResponseCode.SUCCESS, "Record Updated");
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "Record Not Updated");
            }
        }

        public APIResponse AddWorkflow_runsWithToken(Workflow_runsModel model,string token)
        {
            var result = DataAccess.AddWorkflow_runs(model);
            if (result > 0)
            {
		model.workflow_run_id=Convert.ToInt32(result);

		//_rabitMQAsyncProducer.SendAsyncMessage(model, model.GetType().Name,token);
                return new APIResponse(ResponseCode.SUCCESS, "Record Created", result);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "Record Not Created");
            }
        }

        public APIResponse SearchWorkflow_runs(string searchKey,int page=1,int itemsPerPage=100,List<OrderByModel> orderBy = null)
        {
            var result = DataAccess.SearchWorkflow_runs(searchKey,page,itemsPerPage,orderBy);
            if (result != null && result.Count > 0)
            {	
				var totalRecords = DataAccess.GetSearchTotalRecordWorkflow_runs(searchKey);
                var response = new { records = result, pageNumber = page, pageSize = itemsPerPage, totalRecords = totalRecords };
                return new APIResponse(ResponseCode.SUCCESS, "Record Found", response);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "No Record Found");
            }
        }

        public APIResponse SearchWorkflow_runsByCreatedBy(string ownername,string searchKey,int page=1,int itemsPerPage=100,List<OrderByModel> orderBy = null)
        {
            var result = DataAccess.SearchWorkflow_runsByCreatedBy(ownername,searchKey,page,itemsPerPage,orderBy);
            if (result != null && result.Count > 0)
            {	
				var totalRecords = DataAccess.GetSearchTotalRecordWorkflow_runsByCreatedBy(ownername,searchKey);
                var response = new { records = result, pageNumber = page, pageSize = itemsPerPage, totalRecords = totalRecords };
                return new APIResponse(ResponseCode.SUCCESS, "Record Found", response);
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "No Record Found");
            }
        }
		public APIResponse DeleteWorkflow_runsWithToken(int workflow_run_id,string token)
        {
            var result = DataAccess.DeleteWorkflow_runs(workflow_run_id);
            if (result)
            {
                Dictionary<string, int> primary_key = new Dictionary<string, int>();
                primary_key.Add("workflow_run_id", workflow_run_id);

		var className = GetType().Name.Replace("Manager", "Model");
 		//_rabitMQAsyncProducer.SendAsyncMessage(primary_key, className,token);
                return new APIResponse(ResponseCode.SUCCESS, "Record Deleted");
            }
            else
            {
                return new APIResponse(ResponseCode.ERROR, "Record Not Deleted");
            }
        }
		
		public APIResponse DeleteMultipleWorkflow_runsWithToken(List<DeleteMultipleModel> deleteParam, string andOr,string token)
        {
            var result = DataAccess.DeleteMultipleWorkflow_runs(deleteParam, andOr);
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

        public APIResponse FilterWorkflow_runs(string ownername,List<FilterModel> filterModels, string andOr, int page = 1, int itemsPerPage = 100, List<OrderByModel> orderBy = null)
        {
            var result = DataAccess.FilterWorkflow_runs(ownername,filterModels, andOr, page, itemsPerPage, orderBy);
            if (result != null && result.Count > 0)
            {
                var totalRecords = DataAccess.GetFilterTotalRecordWorkflow_runs(ownername,filterModels, andOr);
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
