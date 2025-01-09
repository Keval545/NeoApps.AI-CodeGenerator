import { APIService } from "services";

export const getEmployees = async (pageNo,pageSize,search) => {
    let res;
    if(search.length===0) {
        res = await getAllEmployees(pageNo,pageSize);
    }
    else{
        try {
            res = await searchEmployees(search,pageNo,pageSize);
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

export const filterEmployeesWithColumns = async (filtercondition) => {
  let res;

  try {
    res = await filterEmployees(filtercondition);
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


export const getOneRelationalEmployees = (employee_id) => {
return APIService.api().get(`/employees/Relational/${employee_id}`)
}
export const getAllRelationalEmployees = (pageno,pagesize) => {
return APIService.api().get(`/employees/Relational?page=${pageno}&itemsPerPage=${pagesize}`)
}
export const getAllEmployees = (pageno,pagesize) => {
return APIService.api().get(`/employees?page=${pageno}&itemsPerPage=${pagesize}`)
}
export const getOneEmployees = (id) => {
return APIService.api().get(`/employees/${id}`)
}
export const searchEmployees = (key,pageno,pagesize) => {
return APIService.api().get(`/employees/search?searchKey=${key}&page=${pageno}&itemsPerPage=${pagesize}`)
}
export const addEmployees = (data) => {
return APIService.api().post(`/employees/`,data)
}
export const updateEmployees = (id1,data) => {
return APIService.api().put(`/employees/${id1}/`,data)
}
export const deleteEmployees = (employee_id) => {
return APIService.api().delete(`/employees/${employee_id}/`)
}
export const filterEmployees = (data) => {
return APIService.api().post(`/employees/filter`,data)
}
