using StudentApp.Model;
using StudentApp.Utility;
using System.Collections.Generic;

namespace StudentApp.DataAccess.Interface
{
    public interface ILeave_requestsDataAccess
    {
        List<Leave_requestsModel> GetAllLeave_requests(int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<Leave_requestsModel> GetAllLeave_requestsByCreatedBy(string ownername,int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<Leave_requestsModel> SearchLeave_requests(string searchKey,int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null);
        List<Leave_requestsModel> SearchLeave_requestsByCreatedBy(string ownername,string searchKey,int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null);
		List<Leave_requestsModel> FilterLeave_requests(string ownername,List<FilterModel> filterModels, string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        Leave_requestsModel GetLeave_requestsByID(int leave_id);
        Leave_requestsModel GetLeave_requestsByIDByCreatedBy(string ownername,int leave_id);
        Leave_requestsRelationalModel GetLeave_requestsRelational(string ownername,int leave_id);
                List<Leave_requestsRelationalModel> GetAllLeave_requestsRelational(string ownername,int page=1,int itemsPerPage=100,List<OrderByModel> orderBy = null);
        Leave_requestsReportingModel GetLeave_requestsReporting(string ownername,int leave_id);
List<Leave_requestsReportingModel> GetAllLeave_requestsReporting(string ownername,int page=1,int itemsPerPage=100,List<OrderByModel> orderBy = null);
        
		int GetAllTotalRecordLeave_requests();
        int GetAllTotalRecordLeave_requestsByCreatedBy(string ownername);
        int GetSearchTotalRecordLeave_requests(string searchKey);
        int GetSearchTotalRecordLeave_requestsByCreatedBy(string ownername,string searchKey);
        bool UpdateLeave_requests(Leave_requestsModel model);
		int GetFilterTotalRecordLeave_requests(string ownername,List<FilterModel> filterBy, string andOr);
        long AddLeave_requests(Leave_requestsModel model);
        bool DeleteLeave_requests(int leave_id);
		bool DeleteMultipleLeave_requests(List<DeleteMultipleModel> deleteParam, string andOr);
		
    }
}
