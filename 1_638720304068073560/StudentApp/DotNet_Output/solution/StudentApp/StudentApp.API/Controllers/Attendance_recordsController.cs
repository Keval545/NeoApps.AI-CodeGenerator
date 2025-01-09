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
    public class Attendance_recordsController : ControllerBase
    {   ILog log4Net;
        IAttendance_recordsManager Manager;
        ValidationResult ValidationResult;
        public Attendance_recordsController(IAttendance_recordsManager manager)
        {  
			log4Net = this.Log<Attendance_recordsController>();
            Manager = manager;
            ValidationResult = new ValidationResult();
        }
        [CheckPermission("Attendance_records", "Get")]
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
            return Ok(Manager.GetAttendance_recordsByCreatedBy(ownername,page, itemsPerPage,orderModelList));
			}catch(Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }
        [CheckPermission("Attendance_records", "Get")]
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
            return Ok(Manager.SearchAttendance_recordsByCreatedBy(ownername,searchKey, page, itemsPerPage,orderModelList));
			}catch(Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }
        [CheckPermission("Attendance_records", "Get")]
        [HttpGet]
        [Route(APIEndpoint.DefaultRoute + "/{attendance_id}")]
        public ActionResult GetById(int attendance_id)
        {try
            {
			if (attendance_id<= 0) { ValidationResult.AddEmptyFieldError("attendance_id"); }
            
            if (ValidationResult.IsError)
            {
                return BadRequest(new APIResponse(ResponseCode.ERROR, "Validation failed", ValidationResult));
            }
            string ownername = HttpContext.Items["OwnerName"] as string;
            return Ok(Manager.GetAttendance_recordsByIDByCreatedBy(ownername,attendance_id));
			}catch(Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }

        
        [CheckPermission("Attendance_records", "Get")]
        [HttpGet]
        [Route(APIEndpoint.DefaultRoute + "/Relational/{attendance_id}")]
        public ActionResult GetAttendance_recordsRelational(int attendance_id)
        {try
            {
			if (attendance_id<= 0) { ValidationResult.AddEmptyFieldError("attendance_id"); }
            
            if (ValidationResult.IsError)
            {
                return BadRequest(new APIResponse(ResponseCode.ERROR, "Validation failed", ValidationResult));
            }
            string ownername = HttpContext.Items["OwnerName"] as string;
            return Ok(Manager.GetAttendance_recordsRelational(ownername,attendance_id));
			}catch(Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }

        [CheckPermission("Attendance_records", "Get")]
        [HttpGet]
        [Route(APIEndpoint.DefaultRoute + "/Relational")]
        public ActionResult GetAllAttendance_recordsRelational(int page = 1, int itemsPerPage = 100,string orderBy = null)
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
return Ok(Manager.GetAllAttendance_recordsRelational(ownername,page, itemsPerPage,orderModelList));
			}catch(Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }

        
        [CheckPermission("Attendance_records", "Get")]
        [HttpGet]
        [Route(APIEndpoint.DefaultRoute + "/Reporting/{attendance_id}")]
        public ActionResult GetAttendance_recordsReporting(int attendance_id)
        {try
            {
			if (attendance_id<= 0) { ValidationResult.AddEmptyFieldError("attendance_id"); }
            
            if (ValidationResult.IsError)
            {
                return BadRequest(new APIResponse(ResponseCode.ERROR, "Validation failed", ValidationResult));
            }
string ownername = HttpContext.Items["OwnerName"] as string;
            return Ok(Manager.GetAttendance_recordsReporting(ownername,attendance_id));
			}catch(Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }

        [CheckPermission("Attendance_records", "Get")]
        [HttpGet]
        [Route(APIEndpoint.DefaultRoute + "/Reporting")]
        public ActionResult GetAllAttendance_recordsReporting(int page = 1, int itemsPerPage = 100,string orderBy = null)
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
return Ok(Manager.GetAllAttendance_recordsReporting(ownername,page, itemsPerPage,orderModelList));
			}catch(Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }

        

        [CheckPermission("Attendance_records", "Post")]
        [HttpPost]
        [Route(APIEndpoint.DefaultRoute)]
        public ActionResult Post(Attendance_recordsModel model)
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
                return Ok(Manager.AddAttendance_recordsWithToken(model,token));
			}catch(Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }
        [CheckPermission("Attendance_records", "Put")]
        [HttpPut]
        [Route(APIEndpoint.DefaultRoute + "/{attendance_id}")]
        public ActionResult Put(int attendance_id, Attendance_recordsModel model)
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
			if (attendance_id<= 0) { ValidationResult.AddEmptyFieldError("attendance_id"); }
            
            if (ValidationResult.IsError)
            {
                return BadRequest(new APIResponse(ResponseCode.ERROR, "Validation failed", ValidationResult));
            }
            return Ok(Manager.UpdateAttendance_recordsWithToken(attendance_id, model,token));
			}catch(Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }
        [CheckPermission("Attendance_records", "Delete")]
        [HttpDelete]
        [Route(APIEndpoint.DefaultRoute + "/{attendance_id}")]
        public ActionResult Delete(int attendance_id)
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
            if (attendance_id<= 0) { ValidationResult.AddEmptyFieldError("attendance_id"); }
            if (ValidationResult.IsError)
            {
                return BadRequest(new APIResponse(ResponseCode.ERROR, "Validation failed", ValidationResult));
            }
           return Ok(Manager.DeleteAttendance_recordsWithToken(attendance_id,token));
		   }catch(Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }
        [CheckPermission("Attendance_records", "Delete")]
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
                return Ok(Manager.DeleteMultipleAttendance_recordsWithToken(deleteParam, andOr.ToUpper(),token));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }
        [CheckPermission("Attendance_records", "Post")]
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
                return Ok(Manager.FilterAttendance_records(ownername,filterColumnList, andOr, page, itemsPerPage, orderModelList));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }
    }
}
