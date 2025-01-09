using StudentApp.Model;
using StudentApp.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentApp.Manager.Interface
{
    public interface IMessagequeueManager
    {
        APIResponse GetMessagequeue(int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse GetMessagequeueByCreatedBy(string ownername,int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse SearchMessagequeue(string searchKey, int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse SearchMessagequeueByCreatedBy(string ownername,string searchKey, int page, int itemsPerPage,List<OrderByModel> orderBy);
		APIResponse FilterMessagequeue(string ownername,List<FilterModel> filterModels, string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        APIResponse GetMessagequeueByID(int id);
        APIResponse GetMessagequeueByIDByCreatedBy(string ownername,int id);
        
        
        
        APIResponse UpdateMessagequeueWithToken(int id,MessagequeueModel model,string token);
        APIResponse AddMessagequeueWithToken(MessagequeueModel model,string token);
		APIResponse DeleteMessagequeueWithToken(int id,string token);
		APIResponse DeleteMultipleMessagequeueWithToken(List<DeleteMultipleModel> deleteParam, string andOr,string token);
    }
}
