using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace StudentApp.Model
{
    public class Dnd_ui_versionsModel
    {
        public int dnd_ui_version_id{get; set;}
[Required]
public string layout{get; set;}
[Required]
public string components{get; set;}
[Required]
public string ui_pages{get; set;}
[Required]
public string dnd_ui_type{get; set;}
public string createdBy{get; set;}
public string modifiedBy{get; set;}
public string modifiedAt{get; set;}
public string createdAt{get; set;}
[Range(int.MinValue,int.MaxValue)]
public int isActive{get; set;}

    }
}
