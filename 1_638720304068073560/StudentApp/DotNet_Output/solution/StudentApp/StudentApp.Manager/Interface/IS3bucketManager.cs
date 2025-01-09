using StudentApp.Model;
using StudentApp.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentApp.Manager.Interface
{
    public interface IS3bucketManager
    {
        APIResponse GetS3bucket(int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse GetS3bucketByCreatedBy(string ownername,int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse SearchS3bucket(string searchKey, int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse SearchS3bucketByCreatedBy(string ownername,string searchKey, int page, int itemsPerPage,List<OrderByModel> orderBy);
		APIResponse FilterS3bucket(string ownername,List<FilterModel> filterModels, string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        APIResponse GetS3bucketByID(int bucket_id);
        APIResponse GetS3bucketByIDByCreatedBy(string ownername,int bucket_id);
        
        
        
        APIResponse UpdateS3bucketWithToken(int bucket_id,S3bucketModel model,string token);
        APIResponse AddS3bucketWithToken(S3bucketModel model,string token);
		APIResponse DeleteS3bucketWithToken(int bucket_id,string token);
		APIResponse DeleteMultipleS3bucketWithToken(List<DeleteMultipleModel> deleteParam, string andOr,string token);
    }
}
