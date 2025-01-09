using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace StudentApp.Model
{
    public class Workflows_projectsRelationalModel
    {
        [Range(int.MinValue,int.MaxValue)]
public int workflow_id{get; set;}
public int project_id{get; set;}
public string modifiedBy{get; set;}
public string createdBy{get; set;}
public string modifiedAt{get; set;}
public string createdAt{get; set;}
[Range(int.MinValue,int.MaxValue)]
public int isActive{get; set;}


        

        
    }
}
