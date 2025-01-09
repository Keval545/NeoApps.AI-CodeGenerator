using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace StudentApp.Model
{
    public class Attendance_recordsRelationalModel
    {
        public int attendance_id{get; set;}
[Range(int.MinValue,int.MaxValue)]
public int employee_id{get; set;}
[Required]
public string attendance_date{get; set;}
[Required]
public string status{get; set;}
public string remarks{get; set;}
[Range(int.MinValue,int.MaxValue)]
public int isActive{get; set;}
public string createdBy{get; set;}
public string modifiedBy{get; set;}
public string createdAt{get; set;}
public string modifiedAt{get; set;}


            public EmployeesRelationalModel employee_id_Employees { get; set; }


        
    }
}
