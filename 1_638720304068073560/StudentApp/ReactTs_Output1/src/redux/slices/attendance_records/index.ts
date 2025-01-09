import { createSlice, PayloadAction } from "@reduxjs/toolkit";


export interface IAttendance_records {
attendance_id:number,
employee_id:number,
attendance_date:Date,
status:string,
remarks?:string,
isActive:number,
createdBy:string,
modifiedBy:string,
createdAt:Date,
modifiedAt:Date,
}







interface IAttendance_recordsData {
    list?: Array<IAttendance_records>;
    pageNo: number;
    pageSize: number;
    searchKey?: string;
    totalCount?: number;
    message?: string;
}

export const IAttendance_recordsiData : IAttendance_records = {
    attendance_id:null,
employee_id:null,
attendance_date:null,
status:null,
remarks:null,
isActive:null,
createdBy:null,
modifiedBy:null,
createdAt:null,
modifiedAt:null,

}

const initialState: IAttendance_recordsData = {
    pageNo: 1,
    pageSize: 20,
    searchKey: '',
    list: [],
    totalCount: 0,
    message: '',
};

const attendance_recordsSlice = createSlice({
    name: "attendance_records",
    initialState,
    reducers: {
        setAttendance_recordsList: (state, _action: PayloadAction<IAttendance_recordsData>) => {
            state.list = _action.payload.list;
            state.pageNo = _action.payload.pageNo;
            state.pageSize = _action.payload.pageSize;
            state.totalCount = _action.payload.totalCount;
        },
        resetAttendance_recordsToInit: (state) => {
            state = initialState;
        },
        setAttendance_recordsMessage: (state, _action: PayloadAction<string>) => {
            state.message = _action.payload;
        },
    },
});

export const { setAttendance_recordsList, resetAttendance_recordsToInit, setAttendance_recordsMessage } = attendance_recordsSlice.actions;

export default attendance_recordsSlice.reducer;
