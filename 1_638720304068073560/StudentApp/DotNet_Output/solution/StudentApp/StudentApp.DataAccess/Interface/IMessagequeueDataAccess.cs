using StudentApp.Model;
using StudentApp.Utility;
using System.Collections.Generic;

namespace StudentApp.DataAccess.Interface
{
    public interface IMessagequeueDataAccess
    {
        List<MessagequeueModel> GetAllMessagequeue(int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<MessagequeueModel> GetAllMessagequeueByCreatedBy(string ownername,int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<MessagequeueModel> SearchMessagequeue(string searchKey,int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null);
        List<MessagequeueModel> SearchMessagequeueByCreatedBy(string ownername,string searchKey,int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null);
		List<MessagequeueModel> FilterMessagequeue(string ownername,List<FilterModel> filterModels, string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        MessagequeueModel GetMessagequeueByID(int id);
        MessagequeueModel GetMessagequeueByIDByCreatedBy(string ownername,int id);
        
        
        
		int GetAllTotalRecordMessagequeue();
        int GetAllTotalRecordMessagequeueByCreatedBy(string ownername);
        int GetSearchTotalRecordMessagequeue(string searchKey);
        int GetSearchTotalRecordMessagequeueByCreatedBy(string ownername,string searchKey);
        bool UpdateMessagequeue(MessagequeueModel model);
		int GetFilterTotalRecordMessagequeue(string ownername,List<FilterModel> filterBy, string andOr);
        long AddMessagequeue(MessagequeueModel model);
        bool DeleteMessagequeue(int id);
		bool DeleteMultipleMessagequeue(List<DeleteMultipleModel> deleteParam, string andOr);
		
    }
}
