using StudentApp.Model;
using StudentApp.Utility;
using System.Collections.Generic;

namespace StudentApp.DataAccess.Interface
{
    public interface IS3bucket_foldersDataAccess
    {
        List<S3bucket_foldersModel> GetAllS3bucket_folders(int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<S3bucket_foldersModel> GetAllS3bucket_foldersByCreatedBy(string ownername,int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<S3bucket_foldersModel> SearchS3bucket_folders(string searchKey,int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null);
        List<S3bucket_foldersModel> SearchS3bucket_foldersByCreatedBy(string ownername,string searchKey,int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null);
		List<S3bucket_foldersModel> FilterS3bucket_folders(string ownername,List<FilterModel> filterModels, string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        S3bucket_foldersModel GetS3bucket_foldersByID(int folder_id);
        S3bucket_foldersModel GetS3bucket_foldersByIDByCreatedBy(string ownername,int folder_id);
        
        
        
		int GetAllTotalRecordS3bucket_folders();
        int GetAllTotalRecordS3bucket_foldersByCreatedBy(string ownername);
        int GetSearchTotalRecordS3bucket_folders(string searchKey);
        int GetSearchTotalRecordS3bucket_foldersByCreatedBy(string ownername,string searchKey);
        bool UpdateS3bucket_folders(S3bucket_foldersModel model);
		int GetFilterTotalRecordS3bucket_folders(string ownername,List<FilterModel> filterBy, string andOr);
        long AddS3bucket_folders(S3bucket_foldersModel model);
        bool DeleteS3bucket_folders(int folder_id);
		bool DeleteMultipleS3bucket_folders(List<DeleteMultipleModel> deleteParam, string andOr);
		
    }
}
