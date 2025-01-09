using StudentApp.Model;
using StudentApp.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentApp.Manager.Interface
{
    public interface ILeave_requestsManager
    {
        APIResponse GetLeave_requests(int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse GetLeave_requestsByCreatedBy(string ownername,int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse SearchLeave_requests(string searchKey, int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse SearchLeave_requestsByCreatedBy(string ownername,string searchKey, int page, int itemsPerPage,List<OrderByModel> orderBy);
		APIResponse FilterLeave_requests(string ownername,List<FilterModel> filterModels, string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        APIResponse GetLeave_requestsByID(int leave_id);
        APIResponse GetLeave_requestsByIDByCreatedBy(string ownername,int leave_id);
        APIResponse GetLeave_requestsRelational(string ownername,int leave_id);
APIResponse GetAllLeave_requestsRelational(string ownername,int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse GetLeave_requestsReporting(string ownername,int leave_id);
APIResponse GetAllLeave_requestsReporting(string ownername,int page, int itemsPerPage,List<OrderByModel> orderBy);
        
        APIResponse UpdateLeave_requestsWithToken(int leave_id,Leave_requestsModel model,string token);
        APIResponse AddLeave_requestsWithToken(Leave_requestsModel model,string token);
		APIResponse DeleteLeave_requestsWithToken(int leave_id,string token);
		APIResponse DeleteMultipleLeave_requestsWithToken(List<DeleteMultipleModel> deleteParam, string andOr,string token);
    }
}
