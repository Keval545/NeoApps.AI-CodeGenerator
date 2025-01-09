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
    public class EmployeesController : ControllerBase
    {   ILog log4Net;
        IEmployeesManager Manager;
        ValidationResult ValidationResult;
        public EmployeesController(IEmployeesManager manager)
        {  
			log4Net = this.Log<EmployeesController>();
            Manager = manager;
            ValidationResult = new ValidationResult();
        }
        [CheckPermission("Employees", "Get")]
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
            return Ok(Manager.GetEmployeesByCreatedBy(ownername,page, itemsPerPage,orderModelList));
			}catch(Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }
        [CheckPermission("Employees", "Get")]
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
            return Ok(Manager.SearchEmployeesByCreatedBy(ownername,searchKey, page, itemsPerPage,orderModelList));
			}catch(Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }
        [CheckPermission("Employees", "Get")]
        [HttpGet]
        [Route(APIEndpoint.DefaultRoute + "/{employee_id}")]
        public ActionResult GetById(int employee_id)
        {try
            {
			if (employee_id<= 0) { ValidationResult.AddEmptyFieldError("employee_id"); }
            
            if (ValidationResult.IsError)
            {
                return BadRequest(new APIResponse(ResponseCode.ERROR, "Validation failed", ValidationResult));
            }
            string ownername = HttpContext.Items["OwnerName"] as string;
            return Ok(Manager.GetEmployeesByIDByCreatedBy(ownername,employee_id));
			}catch(Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }

        
        
        

        [CheckPermission("Employees", "Post")]
        [HttpPost]
        [Route(APIEndpoint.DefaultRoute)]
        public ActionResult Post(EmployeesModel model)
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
                return Ok(Manager.AddEmployeesWithToken(model,token));
			}catch(Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }
        [CheckPermission("Employees", "Put")]
        [HttpPut]
        [Route(APIEndpoint.DefaultRoute + "/{employee_id}")]
        public ActionResult Put(int employee_id, EmployeesModel model)
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
			if (employee_id<= 0) { ValidationResult.AddEmptyFieldError("employee_id"); }
            
            if (ValidationResult.IsError)
            {
                return BadRequest(new APIResponse(ResponseCode.ERROR, "Validation failed", ValidationResult));
            }
            return Ok(Manager.UpdateEmployeesWithToken(employee_id, model,token));
			}catch(Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }
        [CheckPermission("Employees", "Delete")]
        [HttpDelete]
        [Route(APIEndpoint.DefaultRoute + "/{employee_id}")]
        public ActionResult Delete(int employee_id)
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
            if (employee_id<= 0) { ValidationResult.AddEmptyFieldError("employee_id"); }
            if (ValidationResult.IsError)
            {
                return BadRequest(new APIResponse(ResponseCode.ERROR, "Validation failed", ValidationResult));
            }
           return Ok(Manager.DeleteEmployeesWithToken(employee_id,token));
		   }catch(Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }
        [CheckPermission("Employees", "Delete")]
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
                return Ok(Manager.DeleteMultipleEmployeesWithToken(deleteParam, andOr.ToUpper(),token));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }
        [CheckPermission("Employees", "Post")]
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
                return Ok(Manager.FilterEmployees(ownername,filterColumnList, andOr, page, itemsPerPage, orderModelList));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, "Exception", ex.Message));
            }
        }
    }
}
