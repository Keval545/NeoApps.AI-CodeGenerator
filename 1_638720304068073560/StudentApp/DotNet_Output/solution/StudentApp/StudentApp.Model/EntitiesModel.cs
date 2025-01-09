using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace StudentApp.Model
{
    public class EntitiesModel
    {
        public int entity_id{get; set;}
[Required]
public string entity_name{get; set;}
public string createdBy{get; set;}
public string modifiedBy{get; set;}
public string createdAt{get; set;}
public string modifiedAt{get; set;}
[Range(int.MinValue,int.MaxValue)]
public int isActive{get; set;}

    }
}
