using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace StudentApp.Model
{
    public class WorkflowsReportingModel
    {
        public int workflow_id{get; set;}
[Required]
public string workflow_name{get; set;}
[Required]
public string workflow_description{get; set;}
[Required]
public string steps{get; set;}
[Required]
public string triggerpoint{get; set;}
public string modifiedBy{get; set;}
public string createdBy{get; set;}
public string modifiedAt{get; set;}
public string createdAt{get; set;}
[Range(int.MinValue,int.MaxValue)]
public int isActive{get; set;}


        
    }
}
