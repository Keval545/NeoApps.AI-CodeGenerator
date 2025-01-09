using StudentApp.Model;
using StudentApp.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentApp.Manager.Interface
{
    public interface IAttendance_recordsManager
    {
        APIResponse GetAttendance_records(int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse GetAttendance_recordsByCreatedBy(string ownername,int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse SearchAttendance_records(string searchKey, int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse SearchAttendance_recordsByCreatedBy(string ownername,string searchKey, int page, int itemsPerPage,List<OrderByModel> orderBy);
		APIResponse FilterAttendance_records(string ownername,List<FilterModel> filterModels, string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        APIResponse GetAttendance_recordsByID(int attendance_id);
        APIResponse GetAttendance_recordsByIDByCreatedBy(string ownername,int attendance_id);
        APIResponse GetAttendance_recordsRelational(string ownername,int attendance_id);
APIResponse GetAllAttendance_recordsRelational(string ownername,int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse GetAttendance_recordsReporting(string ownername,int attendance_id);
APIResponse GetAllAttendance_recordsReporting(string ownername,int page, int itemsPerPage,List<OrderByModel> orderBy);
        
        APIResponse UpdateAttendance_recordsWithToken(int attendance_id,Attendance_recordsModel model,string token);
        APIResponse AddAttendance_recordsWithToken(Attendance_recordsModel model,string token);
		APIResponse DeleteAttendance_recordsWithToken(int attendance_id,string token);
		APIResponse DeleteMultipleAttendance_recordsWithToken(List<DeleteMultipleModel> deleteParam, string andOr,string token);
    }
}
