import { APIService } from "services";

export const getUsers = async (pageNo,pageSize,search) => {
    let res;
    if(search.length===0) {
        res = await getAllUsers(pageNo,pageSize);
    }
    else{
        try {
            res = await searchUsers(search,pageNo,pageSize);
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

export const filterUsersWithColumns = async (filtercondition) => {
  let res;

  try {
    res = await filterUsers(filtercondition);
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


export const getAllUsers = (pageno,pagesize) => {
return APIService.api().get(`/users?page=${pageno}&itemsPerPage=${pagesize}`)
}
export const getOneUsers = (id) => {
return APIService.api().get(`/users/${id}`)
}
export const searchUsers = (key,pageno,pagesize) => {
return APIService.api().get(`/users/search?searchKey=${key}&page=${pageno}&itemsPerPage=${pagesize}`)
}
export const addUsers = (data) => {
return APIService.api().post(`/users/`,data)
}
export const updateUsers = (id1,data) => {
return APIService.api().put(`/users/${id1}/`,data)
}
export const deleteUsers = (user_id) => {
return APIService.api().delete(`/users/${user_id}/`)
}
export const filterUsers = (data) => {
return APIService.api().post(`/users/filter`,data)
}
