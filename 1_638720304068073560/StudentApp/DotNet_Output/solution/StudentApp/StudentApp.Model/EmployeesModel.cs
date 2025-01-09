using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace StudentApp.Model
{
    public class EmployeesModel
    {
        public int employee_id{get; set;}
[Required]
public string first_name{get; set;}
[Required]
public string last_name{get; set;}
[Required]
public string email{get; set;}
public string phone_number{get; set;}
public string department{get; set;}
[Range(int.MinValue,int.MaxValue)]
public int isActive{get; set;}
public string createdBy{get; set;}
public string modifiedBy{get; set;}
public string createdAt{get; set;}
public string modifiedAt{get; set;}

    }
}
