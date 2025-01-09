using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace StudentApp.Model
{
    public class Project_dnd_ui_versionsReportingModel
    {
        public int project_dnd_ui_version_id{get; set;}
[Range(int.MinValue,int.MaxValue)]
public int project_id{get; set;}
[Range(int.MinValue,int.MaxValue)]
public int dnd_ui_version_id{get; set;}
public string createdBy{get; set;}
public string modifiedBy{get; set;}
public string modifiedAt{get; set;}
public string createdAt{get; set;}
[Range(int.MinValue,int.MaxValue)]
public int isActive{get; set;}


        
    }
}
