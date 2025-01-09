using StudentApp.Model;
using StudentApp.Utility;
using System.Collections.Generic;

namespace StudentApp.DataAccess.Interface
{
    public interface IAttendance_recordsDataAccess
    {
        List<Attendance_recordsModel> GetAllAttendance_records(int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<Attendance_recordsModel> GetAllAttendance_recordsByCreatedBy(string ownername,int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<Attendance_recordsModel> SearchAttendance_records(string searchKey,int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null);
        List<Attendance_recordsModel> SearchAttendance_recordsByCreatedBy(string ownername,string searchKey,int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null);
		List<Attendance_recordsModel> FilterAttendance_records(string ownername,List<FilterModel> filterModels, string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        Attendance_recordsModel GetAttendance_recordsByID(int attendance_id);
        Attendance_recordsModel GetAttendance_recordsByIDByCreatedBy(string ownername,int attendance_id);
        Attendance_recordsRelationalModel GetAttendance_recordsRelational(string ownername,int attendance_id);
                List<Attendance_recordsRelationalModel> GetAllAttendance_recordsRelational(string ownername,int page=1,int itemsPerPage=100,List<OrderByModel> orderBy = null);
        Attendance_recordsReportingModel GetAttendance_recordsReporting(string ownername,int attendance_id);
List<Attendance_recordsReportingModel> GetAllAttendance_recordsReporting(string ownername,int page=1,int itemsPerPage=100,List<OrderByModel> orderBy = null);
        
		int GetAllTotalRecordAttendance_records();
        int GetAllTotalRecordAttendance_recordsByCreatedBy(string ownername);
        int GetSearchTotalRecordAttendance_records(string searchKey);
        int GetSearchTotalRecordAttendance_recordsByCreatedBy(string ownername,string searchKey);
        bool UpdateAttendance_records(Attendance_recordsModel model);
		int GetFilterTotalRecordAttendance_records(string ownername,List<FilterModel> filterBy, string andOr);
        long AddAttendance_records(Attendance_recordsModel model);
        bool DeleteAttendance_records(int attendance_id);
		bool DeleteMultipleAttendance_records(List<DeleteMultipleModel> deleteParam, string andOr);
		
    }
}
