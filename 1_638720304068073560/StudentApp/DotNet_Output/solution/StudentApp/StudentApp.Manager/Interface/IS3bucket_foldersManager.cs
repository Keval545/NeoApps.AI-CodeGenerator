using StudentApp.Model;
using StudentApp.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentApp.Manager.Interface
{
    public interface IS3bucket_foldersManager
    {
        APIResponse GetS3bucket_folders(int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse GetS3bucket_foldersByCreatedBy(string ownername,int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse SearchS3bucket_folders(string searchKey, int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse SearchS3bucket_foldersByCreatedBy(string ownername,string searchKey, int page, int itemsPerPage,List<OrderByModel> orderBy);
		APIResponse FilterS3bucket_folders(string ownername,List<FilterModel> filterModels, string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        APIResponse GetS3bucket_foldersByID(int folder_id);
        APIResponse GetS3bucket_foldersByIDByCreatedBy(string ownername,int folder_id);
        
        
        
        APIResponse UpdateS3bucket_foldersWithToken(int folder_id,S3bucket_foldersModel model,string token);
        APIResponse AddS3bucket_foldersWithToken(S3bucket_foldersModel model,string token);
		APIResponse DeleteS3bucket_foldersWithToken(int folder_id,string token);
		APIResponse DeleteMultipleS3bucket_foldersWithToken(List<DeleteMultipleModel> deleteParam, string andOr,string token);
    }
}
