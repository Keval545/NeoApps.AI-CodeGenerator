import { createSlice, PayloadAction } from "@reduxjs/toolkit";


export interface IUsers {
user_id:number,
username:string,
password_hash:string,
role_id:number,
createdBy?:string,
modifiedBy?:string,
createdAt:Date,
modifiedAt:Date,
isActive:number,
}







interface IUsersData {
    list?: Array<IUsers>;
    pageNo: number;
    pageSize: number;
    searchKey?: string;
    totalCount?: number;
    message?: string;
}

export const IUsersiData : IUsers = {
    user_id:null,
username:null,
password_hash:null,
role_id:null,
createdBy:null,
modifiedBy:null,
createdAt:null,
modifiedAt:null,
isActive:null,

}

const initialState: IUsersData = {
    pageNo: 1,
    pageSize: 20,
    searchKey: '',
    list: [],
    totalCount: 0,
    message: '',
};

const usersSlice = createSlice({
    name: "users",
    initialState,
    reducers: {
        setUsersList: (state, _action: PayloadAction<IUsersData>) => {
            state.list = _action.payload.list;
            state.pageNo = _action.payload.pageNo;
            state.pageSize = _action.payload.pageSize;
            state.totalCount = _action.payload.totalCount;
        },
        resetUsersToInit: (state) => {
            state = initialState;
        },
        setUsersMessage: (state, _action: PayloadAction<string>) => {
            state.message = _action.payload;
        },
    },
});

export const { setUsersList, resetUsersToInit, setUsersMessage } = usersSlice.actions;

export default usersSlice.reducer;
