using StudentApp.Model;
using StudentApp.Utility;
using System.Collections.Generic;

namespace StudentApp.DataAccess.Interface
{
    public interface IS3bucketDataAccess
    {
        List<S3bucketModel> GetAllS3bucket(int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<S3bucketModel> GetAllS3bucketByCreatedBy(string ownername,int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<S3bucketModel> SearchS3bucket(string searchKey,int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null);
        List<S3bucketModel> SearchS3bucketByCreatedBy(string ownername,string searchKey,int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null);
		List<S3bucketModel> FilterS3bucket(string ownername,List<FilterModel> filterModels, string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        S3bucketModel GetS3bucketByID(int bucket_id);
        S3bucketModel GetS3bucketByIDByCreatedBy(string ownername,int bucket_id);
        
        
        
		int GetAllTotalRecordS3bucket();
        int GetAllTotalRecordS3bucketByCreatedBy(string ownername);
        int GetSearchTotalRecordS3bucket(string searchKey);
        int GetSearchTotalRecordS3bucketByCreatedBy(string ownername,string searchKey);
        bool UpdateS3bucket(S3bucketModel model);
		int GetFilterTotalRecordS3bucket(string ownername,List<FilterModel> filterBy, string andOr);
        long AddS3bucket(S3bucketModel model);
        bool DeleteS3bucket(int bucket_id);
		bool DeleteMultipleS3bucket(List<DeleteMultipleModel> deleteParam, string andOr);
		
    }
}
