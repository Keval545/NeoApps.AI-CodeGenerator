import { createSlice, PayloadAction } from "@reduxjs/toolkit";


export interface IEmployees {
employee_id:number,
first_name:string,
last_name:string,
email:string,
phone_number?:string,
department?:string,
isActive:number,
createdBy:string,
modifiedBy:string,
createdAt:Date,
modifiedAt:Date,
}







interface IEmployeesData {
    list?: Array<IEmployees>;
    pageNo: number;
    pageSize: number;
    searchKey?: string;
    totalCount?: number;
    message?: string;
}

export const IEmployeesiData : IEmployees = {
    employee_id:null,
first_name:null,
last_name:null,
email:null,
phone_number:null,
department:null,
isActive:null,
createdBy:null,
modifiedBy:null,
createdAt:null,
modifiedAt:null,

}

const initialState: IEmployeesData = {
    pageNo: 1,
    pageSize: 20,
    searchKey: '',
    list: [],
    totalCount: 0,
    message: '',
};

const employeesSlice = createSlice({
    name: "employees",
    initialState,
    reducers: {
        setEmployeesList: (state, _action: PayloadAction<IEmployeesData>) => {
            state.list = _action.payload.list;
            state.pageNo = _action.payload.pageNo;
            state.pageSize = _action.payload.pageSize;
            state.totalCount = _action.payload.totalCount;
        },
        resetEmployeesToInit: (state) => {
            state = initialState;
        },
        setEmployeesMessage: (state, _action: PayloadAction<string>) => {
            state.message = _action.payload;
        },
    },
});

export const { setEmployeesList, resetEmployeesToInit, setEmployeesMessage } = employeesSlice.actions;

export default employeesSlice.reducer;
