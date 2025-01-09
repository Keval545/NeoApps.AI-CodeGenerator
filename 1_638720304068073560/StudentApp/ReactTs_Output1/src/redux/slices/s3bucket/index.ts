import { createSlice, PayloadAction } from "@reduxjs/toolkit";


export interface IS3bucket {
bucket_id:number,
bucket_name:string,
bucket_url:string,
modifiedBy:string,
createdBy:string,
modifiedAt:Date,
createdAt:Date,
isActive:number,
}







interface IS3bucketData {
    list?: Array<IS3bucket>;
    pageNo: number;
    pageSize: number;
    searchKey?: string;
    totalCount?: number;
    message?: string;
}

export const IS3bucketiData : IS3bucket = {
    bucket_id:null,
bucket_name:null,
bucket_url:null,
modifiedBy:null,
createdBy:null,
modifiedAt:null,
createdAt:null,
isActive:null,

}

const initialState: IS3bucketData = {
    pageNo: 1,
    pageSize: 20,
    searchKey: '',
    list: [],
    totalCount: 0,
    message: '',
};

const s3bucketSlice = createSlice({
    name: "s3bucket",
    initialState,
    reducers: {
        setS3bucketList: (state, _action: PayloadAction<IS3bucketData>) => {
            state.list = _action.payload.list;
            state.pageNo = _action.payload.pageNo;
            state.pageSize = _action.payload.pageSize;
            state.totalCount = _action.payload.totalCount;
        },
        resetS3bucketToInit: (state) => {
            state = initialState;
        },
        setS3bucketMessage: (state, _action: PayloadAction<string>) => {
            state.message = _action.payload;
        },
    },
});

export const { setS3bucketList, resetS3bucketToInit, setS3bucketMessage } = s3bucketSlice.actions;

export default s3bucketSlice.reducer;
