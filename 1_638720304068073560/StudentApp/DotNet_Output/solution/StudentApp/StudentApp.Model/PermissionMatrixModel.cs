using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace StudentApp.Model
{
    public class PermissionMatrixModel
    {
        public int permission_id{get; set;}
[Range(int.MinValue,int.MaxValue)]
public int role_id{get; set;}
[Range(int.MinValue,int.MaxValue)]
public int entity_id{get; set;}
public int? can_read{get; set;}
public int? can_write{get; set;}
public int? can_update{get; set;}
public int? can_delete{get; set;}
public int? user_id{get; set;}
public string owner_name{get; set;}
public string createdBy{get; set;}
public string modifiedBy{get; set;}
public string createdAt{get; set;}
public string modifiedAt{get; set;}
[Range(int.MinValue,int.MaxValue)]
public int isActive{get; set;}

    }
}
