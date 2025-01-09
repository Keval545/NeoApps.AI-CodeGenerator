using System;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using StudentApp.Manager.Interface;
using StudentApp.Model;
using StudentApp.Utility;
using log4net;
using System.Collections.Generic;
using StudentApp.API.Attributes;

namespace StudentApp.API.Controllers
{
	[Authorize]
    [ApiController]
    public class Dnd_ui_versionsController : ControllerBase
    {   ILog log4Net;
        IDnd_ui_versionsManager Manager;
        ValidationResult ValidationResult;
        public Dnd_ui_versionsController(IDnd_ui_versionsManager manager)
        {  
			log4Net = this.Log<Dnd_ui_versionsController>();
            Manager = manager;
            ValidationResult = new ValidationResult();
        }
        [CheckPermission("Dnd_ui_versions", "Get")]
        [HttpGet]
        [Route(APIEndpoint.DefaultRoute)]
        public ActionResult Get(int page = 1, int itemsPerPage = 100,string orderBy = null)
        {try
            {
            if (page <= 0)
            {
                ValidationResult.AddFieldError("Id", "Invalid page number");
            }
            if (ValidationResult.IsError)
            {
                return BadRequest(new APIResponse(ResponseCode.ERROR, "Validation failed", ValidationResult));
            }
			List<OrderByModel> orderModelList = UtilityCommon.ConvertStringOrderToOrderModel(orderBy);
            string ownername = HttpContext.Items["OwnerName"] as string;
            return Ok(Manager.GetDnd_ui_versionsByCreatedBy(ownername,page, itemsPerPage,orderModelList));
			}catch(Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }
        [CheckPermission("Dnd_ui_versions", "Get")]
        [HttpGet]
        [Route(APIEndpoint.DefaultRoute + "/search")]
        public ActionResult Search(string searchKey, int page = 1, int itemsPerPage = 100,string orderBy = null)
        {try
            {
            if (string.IsNullOrEmpty(searchKey))
            {
                ValidationResult.AddEmptyFieldError("SearchKey");
            }
            else if (!string.IsNullOrEmpty(searchKey) && searchKey.Length < 3)
            {
                ValidationResult.AddFieldError("SearchKey", "Minimum 3 chracters required for search");
            }
            if (page <= 0)
            {
                ValidationResult.AddFieldError("Id", "Invalid page number");
            }
            if (ValidationResult.IsError)
            {
                return BadRequest(new APIResponse(ResponseCode.ERROR, "Validation failed", ValidationResult));
            }
			List<OrderByModel> orderModelList = UtilityCommon.ConvertStringOrderToOrderModel(orderBy);
            string ownername = HttpContext.Items["OwnerName"] as string;
            return Ok(Manager.SearchDnd_ui_versionsByCreatedBy(ownername,searchKey, page, itemsPerPage,orderModelList));
			}catch(Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }
        [CheckPermission("Dnd_ui_versions", "Get")]
        [HttpGet]
        [Route(APIEndpoint.DefaultRoute + "/{dnd_ui_version_id}")]
        public ActionResult GetById(int dnd_ui_version_id)
        {try
            {
			if (dnd_ui_version_id<= 0) { ValidationResult.AddEmptyFieldError("dnd_ui_version_id"); }
            
            if (ValidationResult.IsError)
            {
                return BadRequest(new APIResponse(ResponseCode.ERROR, "Validation failed", ValidationResult));
            }
            string ownername = HttpContext.Items["OwnerName"] as string;
            return Ok(Manager.GetDnd_ui_versionsByIDByCreatedBy(ownername,dnd_ui_version_id));
			}catch(Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }

        
        
        

        [CheckPermission("Dnd_ui_versions", "Post")]
        [HttpPost]
        [Route(APIEndpoint.DefaultRoute)]
        public ActionResult Post(Dnd_ui_versionsModel model)
        {try
            {
                string token = Request.Headers["Authorization"];

                // Check if the token is present
                if (string.IsNullOrEmpty(token))
                {
                    return StatusCode(401, new APIResponse(ResponseCode.UNAUTHORIZED, "Unauthorized", "Token not provided"));
                }

                // Extract the actual token from the Authorization header (e.g., "Bearer token")
                token = token.Replace("Bearer ", "");
				string ownername = HttpContext.Items["OwnerName"] as string;
                model.createdBy = ownername;
                model.modifiedBy = ownername;
                return Ok(Manager.AddDnd_ui_versionsWithToken(model,token));
			}catch(Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }
        [CheckPermission("Dnd_ui_versions", "Put")]
        [HttpPut]
        [Route(APIEndpoint.DefaultRoute + "/{dnd_ui_version_id}")]
        public ActionResult Put(int dnd_ui_version_id, Dnd_ui_versionsModel model)
        {try
            {
                string token = Request.Headers["Authorization"];

                // Check if the token is present
                if (string.IsNullOrEmpty(token))
                {
                    return StatusCode(401, new APIResponse(ResponseCode.UNAUTHORIZED, "Unauthorized", "Token not provided"));
                }

                // Extract the actual token from the Authorization header (e.g., "Bearer token")
                token = token.Replace("Bearer ", "");
				string ownername = HttpContext.Items["OwnerName"] as string;
                model.createdBy = ownername;
                model.modifiedBy = ownername;
			if (dnd_ui_version_id<= 0) { ValidationResult.AddEmptyFieldError("dnd_ui_version_id"); }
            
            if (ValidationResult.IsError)
            {
                return BadRequest(new APIResponse(ResponseCode.ERROR, "Validation failed", ValidationResult));
            }
            return Ok(Manager.UpdateDnd_ui_versionsWithToken(dnd_ui_version_id, model,token));
			}catch(Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }
        [CheckPermission("Dnd_ui_versions", "Delete")]
        [HttpDelete]
        [Route(APIEndpoint.DefaultRoute + "/{dnd_ui_version_id}")]
        public ActionResult Delete(int dnd_ui_version_id)
        {
			try
            {
                string token = Request.Headers["Authorization"];

                // Check if the token is present
                if (string.IsNullOrEmpty(token))
                {
                    return StatusCode(401, new APIResponse(ResponseCode.UNAUTHORIZED, "Unauthorized", "Token not provided"));
                }

                // Extract the actual token from the Authorization header (e.g., "Bearer token")
                token = token.Replace("Bearer ", "");
            if (dnd_ui_version_id<= 0) { ValidationResult.AddEmptyFieldError("dnd_ui_version_id"); }
            if (ValidationResult.IsError)
            {
                return BadRequest(new APIResponse(ResponseCode.ERROR, "Validation failed", ValidationResult));
            }
           return Ok(Manager.DeleteDnd_ui_versionsWithToken(dnd_ui_version_id,token));
		   }catch(Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }
        [CheckPermission("Dnd_ui_versions", "Delete")]
		[HttpDelete]
        [Route(APIEndpoint.DefaultRoute + "/Multiple")]
        public ActionResult DeleteMultiple(List<DeleteMultipleModel> deleteParam, string andOr = "AND")
        {
            try
            {
                string token = Request.Headers["Authorization"];

                // Check if the token is present
                if (string.IsNullOrEmpty(token))
                {
                    return StatusCode(401, new APIResponse(ResponseCode.UNAUTHORIZED, "Unauthorized", "Token not provided"));
                }

                // Extract the actual token from the Authorization header (e.g., "Bearer token")
                token = token.Replace("Bearer ", "");
                if (deleteParam == null) { ValidationResult.AddEmptyFieldError("DeleteParam"); }
                else if (deleteParam.Count <= 0) { ValidationResult.AddEmptyFieldError("DeleteParam"); }
                if (string.IsNullOrEmpty(andOr)) { ValidationResult.AddEmptyFieldError("andOr"); }
                else if (andOr.ToUpper() == "OR" || andOr.ToUpper() == "AND") { }
                else { ValidationResult.AddFieldError("andOr", "Invalid value(only OR / AND allowed)"); }
                if (ValidationResult.IsError)
                {
                    return BadRequest(new APIResponse(ResponseCode.ERROR, "Validation failed", ValidationResult));
                }
                return Ok(Manager.DeleteMultipleDnd_ui_versionsWithToken(deleteParam, andOr.ToUpper(),token));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }
        [CheckPermission("Dnd_ui_versions", "Post")]
        [HttpPost]
        [Route(APIEndpoint.DefaultRoute + "/filter")]
        public ActionResult Filter(List<FilterModel> filterColumnList, string andOr = "AND", int page = 1, int itemsPerPage = 100, string orderBy = null)
        {
            try
            {
                if (string.IsNullOrEmpty(andOr))
                {
                    ValidationResult.AddEmptyFieldError("andOr");
                }
                else if (andOr.ToUpper() == "OR" || andOr.ToUpper() == "AND") { }
                else { ValidationResult.AddFieldError("andOr", "Invalid value(only OR / AND allowed)"); }
                if (page <= 0)
                {
                    ValidationResult.AddFieldError("Id", "Invalid page number");
                }
                if (filterColumnList == null)
                {
                    ValidationResult.AddFieldError("FilterColumnList", "Filter Column Required");
                }
                else if (filterColumnList != null && filterColumnList.Count <= 0)
                {
                    ValidationResult.AddFieldError("FilterColumnList", "Filter Column Required");
                }

                if (ValidationResult.IsError)
                {
                    return BadRequest(new APIResponse(ResponseCode.ERROR, "Validation failed", ValidationResult));
                }
                List<OrderByModel> orderModelList = UtilityCommon.ConvertStringOrderToOrderModel(orderBy);
                string ownername = HttpContext.Items["OwnerName"] as string;
                return Ok(Manager.FilterDnd_ui_versions(ownername,filterColumnList, andOr, page, itemsPerPage, orderModelList));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }
    }
}
