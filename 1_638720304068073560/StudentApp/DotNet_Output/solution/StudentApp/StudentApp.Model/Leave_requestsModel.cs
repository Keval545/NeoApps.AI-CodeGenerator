using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace StudentApp.Model
{
    public class Leave_requestsModel
    {
        public int leave_id{get; set;}
[Range(int.MinValue,int.MaxValue)]
public int employee_id{get; set;}
[Required]
public string leave_start_date{get; set;}
[Required]
public string leave_end_date{get; set;}
[Required]
public string leave_type{get; set;}
[Required]
public string status{get; set;}
public string remarks{get; set;}
[Range(int.MinValue,int.MaxValue)]
public int isActive{get; set;}
public string createdBy{get; set;}
public string modifiedBy{get; set;}
public string createdAt{get; set;}
public string modifiedAt{get; set;}

    }
}
