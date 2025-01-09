using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace StudentApp.Model
{
    public class Workflow_triggersModel
    {
        public int workflow_trigger_id{get; set;}
[Range(int.MinValue,int.MaxValue)]
public int workflow_id{get; set;}
[Required]
public string trigger_name{get; set;}
[Required]
public string trigger_type{get; set;}
public string modifiedBy{get; set;}
public string createdBy{get; set;}
public string modifiedAt{get; set;}
public string createdAt{get; set;}
[Range(int.MinValue,int.MaxValue)]
public int isActive{get; set;}

    }
}
