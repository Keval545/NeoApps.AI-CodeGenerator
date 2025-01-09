import { APIService } from "services";

export const getAttendance_records = async (pageNo,pageSize,search) => {
    let res;
    if(search.length===0) {
        res = await getAllAttendance_records(pageNo,pageSize);
    }
    else{
        try {
            res = await searchAttendance_records(search,pageNo,pageSize);
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

export const filterAttendance_recordsWithColumns = async (filtercondition) => {
  let res;

  try {
    res = await filterAttendance_records(filtercondition);
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


export const getOneRelationalAttendance_records = (attendance_id) => {
return APIService.api().get(`/attendance_records/Relational/${attendance_id}`)
}
export const getAllRelationalAttendance_records = (pageno,pagesize) => {
return APIService.api().get(`/attendance_records/Relational?page=${pageno}&itemsPerPage=${pagesize}`)
}
export const getOneReportingAttendance_records = (attendance_id) => {
return APIService.api().get(`/attendance_records/Reporting/${attendance_id}`)
}
export const getAllReportingAttendance_records = (pageno,pagesize) => {
return APIService.api().get(`/attendance_records/Reporting?page=${pageno}&itemsPerPage=${pagesize}`)
}
export const getAllAttendance_records = (pageno,pagesize) => {
return APIService.api().get(`/attendance_records?page=${pageno}&itemsPerPage=${pagesize}`)
}
export const getOneAttendance_records = (id) => {
return APIService.api().get(`/attendance_records/${id}`)
}
export const searchAttendance_records = (key,pageno,pagesize) => {
return APIService.api().get(`/attendance_records/search?searchKey=${key}&page=${pageno}&itemsPerPage=${pagesize}`)
}
export const addAttendance_records = (data) => {
return APIService.api().post(`/attendance_records/`,data)
}
export const updateAttendance_records = (id1,data) => {
return APIService.api().put(`/attendance_records/${id1}/`,data)
}
export const deleteAttendance_records = (attendance_id) => {
return APIService.api().delete(`/attendance_records/${attendance_id}/`)
}
export const filterAttendance_records = (data) => {
return APIService.api().post(`/attendance_records/filter`,data)
}
