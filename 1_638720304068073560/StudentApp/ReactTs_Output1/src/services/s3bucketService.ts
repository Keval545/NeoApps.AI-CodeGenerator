import { APIService } from "services";

export const getS3bucket = async (pageNo,pageSize,search) => {
    let res;
    if(search.length===0) {
        res = await getAllS3bucket(pageNo,pageSize);
    }
    else{
        try {
            res = await searchS3bucket(search,pageNo,pageSize);
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

export const filterS3bucketWithColumns = async (filtercondition) => {
  let res;

  try {
    res = await filterS3bucket(filtercondition);
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


export const getAllS3bucket = (pageno,pagesize) => {
return APIService.api().get(`/s3bucket?page=${pageno}&itemsPerPage=${pagesize}`)
}
export const getOneS3bucket = (id) => {
return APIService.api().get(`/s3bucket/${id}`)
}
export const searchS3bucket = (key,pageno,pagesize) => {
return APIService.api().get(`/s3bucket/search?searchKey=${key}&page=${pageno}&itemsPerPage=${pagesize}`)
}
export const addS3bucket = (data) => {
return APIService.api().post(`/s3bucket/`,data)
}
export const updateS3bucket = (id1,data) => {
return APIService.api().put(`/s3bucket/${id1}/`,data)
}
export const deleteS3bucket = (bucket_id) => {
return APIService.api().delete(`/s3bucket/${bucket_id}/`)
}
export const filterS3bucket = (data) => {
return APIService.api().post(`/s3bucket/filter`,data)
}
