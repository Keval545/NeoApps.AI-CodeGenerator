import { combineReducers } from "redux";

import template from "redux/slices/template";
import authToken from "redux/slices/auth";
import dnd_ui_versions from 'redux/slices/dnd_ui_versions';
import workflows from 'redux/slices/workflows';
import users from 'redux/slices/users';
import s3bucket_folders from 'redux/slices/s3bucket_folders';
import s3bucket from 'redux/slices/s3bucket';
import leave_requests from 'redux/slices/leave_requests';
import employees from 'redux/slices/employees';
import attendance_records from 'redux/slices/attendance_records';


const rootReducer = combineReducers({ template,authToken,users,s3bucket_folders,s3bucket,leave_requests,employees,attendance_records,dnd_ui_versions,workflows });

export type RootState = ReturnType<typeof rootReducer>;

export default rootReducer;
