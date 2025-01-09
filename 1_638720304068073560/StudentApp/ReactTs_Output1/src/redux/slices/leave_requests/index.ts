import { createSlice, PayloadAction } from "@reduxjs/toolkit";


export interface ILeave_requests {
leave_id:number,
employee_id:number,
leave_start_date:Date,
leave_end_date:Date,
leave_type:string,
status:string,
remarks?:string,
isActive:number,
createdBy:string,
modifiedBy:string,
createdAt:Date,
modifiedAt:Date,
}







interface ILeave_requestsData {
    list?: Array<ILeave_requests>;
    pageNo: number;
    pageSize: number;
    searchKey?: string;
    totalCount?: number;
    message?: string;
}

export const ILeave_requestsiData : ILeave_requests = {
    leave_id:null,
employee_id:null,
leave_start_date:null,
leave_end_date:null,
leave_type:null,
status:null,
remarks:null,
isActive:null,
createdBy:null,
modifiedBy:null,
createdAt:null,
modifiedAt:null,

}

const initialState: ILeave_requestsData = {
    pageNo: 1,
    pageSize: 20,
    searchKey: '',
    list: [],
    totalCount: 0,
    message: '',
};

const leave_requestsSlice = createSlice({
    name: "leave_requests",
    initialState,
    reducers: {
        setLeave_requestsList: (state, _action: PayloadAction<ILeave_requestsData>) => {
            state.list = _action.payload.list;
            state.pageNo = _action.payload.pageNo;
            state.pageSize = _action.payload.pageSize;
            state.totalCount = _action.payload.totalCount;
        },
        resetLeave_requestsToInit: (state) => {
            state = initialState;
        },
        setLeave_requestsMessage: (state, _action: PayloadAction<string>) => {
            state.message = _action.payload;
        },
    },
});

export const { setLeave_requestsList, resetLeave_requestsToInit, setLeave_requestsMessage } = leave_requestsSlice.actions;

export default leave_requestsSlice.reducer;
