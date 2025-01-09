import { APIService } from "services";

export const getLeave_requests = async (pageNo,pageSize,search) => {
    let res;
    if(search.length===0) {
        res = await getAllLeave_requests(pageNo,pageSize);
    }
    else{
        try {
            res = await searchLeave_requests(search,pageNo,pageSize);
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

export const filterLeave_requestsWithColumns = async (filtercondition) => {
  let res;

  try {
    res = await filterLeave_requests(filtercondition);
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


export const getOneRelationalLeave_requests = (leave_id) => {
return APIService.api().get(`/leave_requests/Relational/${leave_id}`)
}
export const getAllRelationalLeave_requests = (pageno,pagesize) => {
return APIService.api().get(`/leave_requests/Relational?page=${pageno}&itemsPerPage=${pagesize}`)
}
export const getOneReportingLeave_requests = (leave_id) => {
return APIService.api().get(`/leave_requests/Reporting/${leave_id}`)
}
export const getAllReportingLeave_requests = (pageno,pagesize) => {
return APIService.api().get(`/leave_requests/Reporting?page=${pageno}&itemsPerPage=${pagesize}`)
}
export const getAllLeave_requests = (pageno,pagesize) => {
return APIService.api().get(`/leave_requests?page=${pageno}&itemsPerPage=${pagesize}`)
}
export const getOneLeave_requests = (id) => {
return APIService.api().get(`/leave_requests/${id}`)
}
export const searchLeave_requests = (key,pageno,pagesize) => {
return APIService.api().get(`/leave_requests/search?searchKey=${key}&page=${pageno}&itemsPerPage=${pagesize}`)
}
export const addLeave_requests = (data) => {
return APIService.api().post(`/leave_requests/`,data)
}
export const updateLeave_requests = (id1,data) => {
return APIService.api().put(`/leave_requests/${id1}/`,data)
}
export const deleteLeave_requests = (leave_id) => {
return APIService.api().delete(`/leave_requests/${leave_id}/`)
}
export const filterLeave_requests = (data) => {
return APIService.api().post(`/leave_requests/filter`,data)
}
