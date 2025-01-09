import { APIService } from "services";

export const getS3bucket_folders = async (pageNo,pageSize,search) => {
    let res;
    if(search.length===0) {
        res = await getAllS3bucket_folders(pageNo,pageSize);
    }
    else{
        try {
            res = await searchS3bucket_folders(search,pageNo,pageSize);
        } catch(err) {
             return { records:[], totalCount:0 }
        }
    }
    if(res && res.data && res.data.document){
    return res.data.document;
    }else{
    return { records:[], totalCount:0 }
    }
    
}

export const filterS3bucket_foldersWithColumns = async (filtercondition) => {
  let res;

  try {
    res = await filterS3bucket_folders(filtercondition);
   //console.log(res);
  } catch (err) {
    return { records: [], totalCount: 0 };
  }

  if (res && res.data && res.data.document) {
   //console.log("inside if");
    return res.data.document;
  } else {
    return { records: [], totalCount: 0 };
  }
};


export const getAllS3bucket_folders = (pageno,pagesize) => {
return APIService.api().get(`/s3bucket_folders?page=${pageno}&itemsPerPage=${pagesize}`)
}
export const getOneS3bucket_folders = (id) => {
return APIService.api().get(`/s3bucket_folders/${id}`)
}
export const searchS3bucket_folders = (key,pageno,pagesize) => {
return APIService.api().get(`/s3bucket_folders/search?searchKey=${key}&page=${pageno}&itemsPerPage=${pagesize}`)
}
export const addS3bucket_folders = (data) => {
return APIService.api().post(`/s3bucket_folders/`,data)
}
export const updateS3bucket_folders = (id1,data) => {
return APIService.api().put(`/s3bucket_folders/${id1}/`,data)
}
export const deleteS3bucket_folders = (folder_id) => {
return APIService.api().delete(`/s3bucket_folders/${folder_id}/`)
}
export const filterS3bucket_folders = (data) => {
return APIService.api().post(`/s3bucket_folders/filter`,data)
}
