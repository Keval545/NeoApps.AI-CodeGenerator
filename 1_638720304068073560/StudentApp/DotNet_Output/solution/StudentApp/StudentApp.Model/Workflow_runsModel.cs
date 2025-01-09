using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace StudentApp.Model
{
    public class Workflow_runsModel
    {
        public int workflow_run_id{get; set;}
[Range(int.MinValue,int.MaxValue)]
public int workflow_build_id{get; set;}
[Required]
public string workflow_run_status{get; set;}
[Required]
public string workflow_run_start_time{get; set;}
[Required]
public string workflow_run_end_time{get; set;}
public string modifiedBy{get; set;}
public string createdBy{get; set;}
public string modifiedAt{get; set;}
public string createdAt{get; set;}
[Range(int.MinValue,int.MaxValue)]
public int isActive{get; set;}

    }
}
