using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace StudentApp.Model
{
    public class Workflow_trigger_conditionsReportingModel
    {
        public int workflow_trigger_id{get; set;}
[Required]
public string condition_type{get; set;}
[Required]
public string condition_value{get; set;}
public string modifiedBy{get; set;}
public string createdBy{get; set;}
public string modifiedAt{get; set;}
public string createdAt{get; set;}
[Range(int.MinValue,int.MaxValue)]
public int isActive{get; set;}


        
    }
}
