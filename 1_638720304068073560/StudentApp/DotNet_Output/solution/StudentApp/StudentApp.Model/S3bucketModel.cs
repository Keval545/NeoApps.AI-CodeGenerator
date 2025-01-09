using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace StudentApp.Model
{
    public class S3bucketModel
    {
        public int bucket_id{get; set;}
[Required]
public string bucket_name{get; set;}
[Required]
public string bucket_url{get; set;}
public string modifiedBy{get; set;}
public string createdBy{get; set;}
public string modifiedAt{get; set;}
public string createdAt{get; set;}
[Range(int.MinValue,int.MaxValue)]
public int isActive{get; set;}

    }
}
