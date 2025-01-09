import { createSlice, PayloadAction } from "@reduxjs/toolkit";


export interface IS3bucket_folders {
folder_id:number,
folder_name:string,
modifiedBy:string,
createdBy:string,
modifiedAt:Date,
createdAt:Date,
isActive:number,
}







interface IS3bucket_foldersData {
    list?: Array<IS3bucket_folders>;
    pageNo: number;
    pageSize: number;
    searchKey?: string;
    totalCount?: number;
    message?: string;
}

export const IS3bucket_foldersiData : IS3bucket_folders = {
    folder_id:null,
folder_name:null,
modifiedBy:null,
createdBy:null,
modifiedAt:null,
createdAt:null,
isActive:null,

}

const initialState: IS3bucket_foldersData = {
    pageNo: 1,
    pageSize: 20,
    searchKey: '',
    list: [],
    totalCount: 0,
    message: '',
};

const s3bucket_foldersSlice = createSlice({
    name: "s3bucket_folders",
    initialState,
    reducers: {
        setS3bucket_foldersList: (state, _action: PayloadAction<IS3bucket_foldersData>) => {
            state.list = _action.payload.list;
            state.pageNo = _action.payload.pageNo;
            state.pageSize = _action.payload.pageSize;
            state.totalCount = _action.payload.totalCount;
        },
        resetS3bucket_foldersToInit: (state) => {
            state = initialState;
        },
        setS3bucket_foldersMessage: (state, _action: PayloadAction<string>) => {
            state.message = _action.payload;
        },
    },
});

export const { setS3bucket_foldersList, resetS3bucket_foldersToInit, setS3bucket_foldersMessage } = s3bucket_foldersSlice.actions;

export default s3bucket_foldersSlice.reducer;
