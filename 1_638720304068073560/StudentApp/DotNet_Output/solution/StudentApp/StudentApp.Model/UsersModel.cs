using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace StudentApp.Model
{
    public class UsersModel
    {
        public int user_id{get; set;}
[Required]
public string username{get; set;}
[Required]
public string password_hash{get; set;}
[Range(int.MinValue,int.MaxValue)]
public int role_id{get; set;}
public string createdBy{get; set;}
public string modifiedBy{get; set;}
public string createdAt{get; set;}
public string modifiedAt{get; set;}
[Range(int.MinValue,int.MaxValue)]
public int isActive{get; set;}

    }
}
