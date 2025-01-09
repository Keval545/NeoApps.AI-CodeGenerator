import { useFormik, Formik,Field } from "formik";
import React, { useEffect, useState } from "react";
import {  Form } from "react-bootstrap";
import { useSelector } from "react-redux";
import { format } from "date-fns";
import { RootState } from "redux/reducers";
import { uploadFileService } from "services/fileUploadService";
import { resetAttendance_recordsToInit, setAttendance_recordsList, setAttendance_recordsMessage } from "redux/actions";
import { resetEmployeesToInit, setEmployeesList, setEmployeesMessage } from "redux/actions";

import { getEmployees } from "services/employeesService";

import { useAppDispatch } from "redux/store";
import ReactQuill from "react-quill";
import "react-quill/dist/quill.snow.css";
import { addAttendance_records, updateAttendance_records, getAttendance_records } from "services/attendance_recordsService";
import { Constant } from "template/Constant";
import { ValidationControl } from "Dnd/Dnd Designer/Utility/constants";
import CloudUploadIcon from "@mui/icons-material/CloudUpload";
import SignatureDialog from "components/icons/signatureDialog";
import moment from 'moment';
import {
  Card,
  CardHeader,
  CardContent,
  Button,
  Typography,
  Box,
  ListItem,
  ListItemText,
  InputLabel,
  Grid,
  TextField,
  TextareaAutosize,
  Radio,
  FormControl,
  FormControlLabel,
  Select,
  MenuItem,
  RadioGroup,
} from "@mui/material";
import CircularProgress from "@mui/material/CircularProgress";
import CloseIcon from "@mui/icons-material/Close";
import IconButton from "@mui/material/IconButton";
import { makeStyles } from "@mui/styles";
import { styled } from '@mui/material/styles';
import CustomizedSnackbars from "template/CustomizedSnackbar";
import * as yup from 'yup';
type Props = {
    row?: any,
    hideShowForm: (actionName) => void;
    getData: (page, pageSize, searchKey) => void;
    action?: string;
  config;
};
export const Attendance_recordsForm: React.FC<Props> = ({ row, hideShowForm, getData, action,  config,}) => {
    const dispatch = useAppDispatch();
    const iValue={attendance_id:'',employee_id:'',attendance_date:format(new Date(), "yyyy-MM-dd"),status:'',remarks:'',isActive:'',createdBy:'',modifiedBy:'',createdAt:format(new Date(), "yyyy-MM-dd"),modifiedAt:format(new Date(), "yyyy-MM-dd")};
    const initialValue = action === 'edit' ? row : iValue;
  const attendance_recordsData = useSelector((state: RootState) => state.attendance_records);
  const [isLoading, setIsLoading] = useState("");
  const [isSaving, setisSaving] = useState(false);
    const [uniquekey, setuniquekey] = useState(Date.now());
    const [openSignatureDialog, setOpenSignatureDialog] = useState(false);
     const [snackbarOpen, setSnackbarOpen] = useState(false);
  const [alertMessage, setAlertMessage] = useState("");
  const [severity, setSeverity] = useState("success");
  const handleSnackbarClose = (
    event: React.SyntheticEvent<Element, Event>,
    reason?: string
  ) => {
    if (reason === "clickaway") {
      return;
    }
    setSnackbarOpen(false);
    setSeverity("");
    setAlertMessage("");
  };

 

  const handleOpenSignatureDialog = () => {

    setOpenSignatureDialog(true);

  };

 

  const handleCloseSignatureDialog = () => {

    setOpenSignatureDialog(false);};
  
const employeesData = useSelector((state: RootState) => state.employees);

  
useEffect(() => {
    if (employeesData && employeesData.list && employeesData.list.length === 0) {
        dispatch(resetEmployeesToInit());
        getEmployees(Constant.defaultPageNumber, Constant.defaultDropdownPageSize, '').then((response) => {
            if (response && response.records) {
                dispatch(setEmployeesList({ pageNo: Constant.defaultPageNumber, pageSize: Constant.defaultDropdownPageSize, list: response.records, totalCount: response.total_count, searchKey: '' }));
            } else {
                dispatch(setAttendance_recordsMessage("No Record Found For Employees"));
            }
        })
    }
},[employeesData.list.length])

  
    const useStyles = makeStyles({
      richTextEditor: {
        '& .ql-container': {
          height: '300px'
        }
      }
    });
    const classes = useStyles();

  useEffect(() => {
    if (attendance_recordsData && attendance_recordsData.list && attendance_recordsData.list.length === 0) {
      dispatch(resetAttendance_recordsToInit());
      getAttendance_records(Constant.defaultPageNumber, Constant.defaultDropdownPageSize, "").then((response) => {
        if (response && response.records) {
          dispatch(
            setAttendance_recordsList({
              pageNo: Constant.defaultPageNumber,
              pageSize: Constant.defaultDropdownPageSize,
              list: response.records,
              totalCount: response.total_count,
              searchKey: "",
            })
          );
        } else {
          dispatch(setAttendance_recordsMessage(`No Record Found For Attendance_records`));
        }
      });
    }
  }, [attendance_recordsData.list.length]);
 const closeButtonClick = (
    setFieldValue: (
      field: string,
      value: any,
      shouldValidate?: boolean
    ) => void,
    field: string
  ) => {
    setFieldValue(field, "");
  };
      const handleFileupload = async (
    event: any,
    setFieldValue: (
      field: string,
      value: any,
      shouldValidate?: boolean
    ) => void,
    field: string
  ) => {
    setIsLoading(field);
    try {
      // Perform your file upload logic here
      // For example, make an API call to upload the file
      if (event && event.files && event.files.length > 0) {
        const formData = new FormData();
        formData.append("File", event.files[0]);
        formData.append("BucketId", config[field + "_bucket_name"]);
        formData.append("folderselected", config[field + "_bucket_folder"]);
        const response = await uploadFileService(formData);
        if (response) {
        setSeverity("success");
        setAlertMessage("File uploaded successfully");
          setIsLoading("");
         //console.log("File uploaded successfully");
         //console.log(response);

          // Instead of directly setting the 'value' on the input element, update the state using setFieldValue
          setFieldValue(field, response.data.document);
          setuniquekey(Date.now()); // <-- Update the key here to force a re-render of the input

          return response.data.document;
        } else {
        setSeverity("error");
            setAlertMessage("File upload Failed");
          setIsLoading("");
         //console.log("File upload failed");
          return "File upload failed";
        }
      } else {
      setSeverity("error");
      setAlertMessage("File upload Failed");
        setIsLoading("");
       //console.log(event);
        return false;
      }
    } catch (error) {
    setSeverity("error");
        setAlertMessage("File upload error");
      setIsLoading("");
      //console.error("File upload error:", error);
      return error;
    }
  };

    return (

         <Card className="shadow mb-4">
        <CardHeader
        title={`${
          action === "add"
            ? config["addFormHeading"] !== undefined
              ? config["addFormHeading"]
              : `Add Attendance_records`
            : config["editFormHeading"] !== undefined
            ? config["editFormHeading"]
            : `Edit Attendance_records`
        }`}
          action={
            <IconButton onClick={() => hideShowForm(false)}>
              <CloseIcon />
            </IconButton>
          }
        />
        <CardContent>
          <Formik
          initialValues= {initialValue}
      onSubmit= {async (values) => {
      values.attendance_id = Number(values.attendance_id)
values.attendance_id = Number(values.attendance_id)
values.employee_id = Number(values.employee_id)
values.employee_id = Number(values.employee_id)
values.isActive = Number(values.isActive)
values.isActive = Number(values.isActive)

            setisSaving(true);
          if (action === 'edit') {
              const response = await updateAttendance_records(values.attendance_id,values);
              if (response) {
                setisSaving(false);

                  dispatch(setAttendance_recordsMessage("Updated Successfully"));
                  getData(Constant.defaultPageNumber, Constant.defaultPageSize, '');
                  hideShowForm('');
              } else {
                setisSaving(false);
                  dispatch(setAttendance_recordsMessage("Some error occured!"));
              }
          } else if (action === 'add') {
              const response = await addAttendance_records(values);
              if (response) {
                setisSaving(false);
                  dispatch(setAttendance_recordsMessage("Added Successfully"));
                  getData(Constant.defaultPageNumber, Constant.defaultPageSize, '');
                  hideShowForm('');
              } else {
                setisSaving(false);
                  dispatch(setAttendance_recordsMessage("Some error occured!"));
              }
          }
      }}
      validationSchema= {yup.object({
         attendance_date: yup.string().test("validator-custom-name", function (value) { const validation = ValidationControl(value,config.attendance_date_error_control,config.attendance_date_error_message);if (!validation.isValid) {return this.createError({path: this.path,message: validation.errorMessage});} else {return true;}}),
status: yup.string().test("validator-custom-name", function (value) { const validation = ValidationControl(value,config.status_error_control,config.status_error_message);if (!validation.isValid) {return this.createError({path: this.path,message: validation.errorMessage});} else {return true;}}),
remarks: yup.string().test("validator-custom-name", function (value) { const validation = ValidationControl(value,config.remarks_error_control,config.remarks_error_message);if (!validation.isValid) {return this.createError({path: this.path,message: validation.errorMessage});} else {return true;}}),
isActive: yup.string().test("validator-custom-name", function (value) { const validation = ValidationControl(value,config.isActive_error_control,config.isActive_error_message);if (!validation.isValid) {return this.createError({path: this.path,message: validation.errorMessage});} else {return true;}}),
createdBy: yup.string().test("validator-custom-name", function (value) { const validation = ValidationControl(value,config.createdBy_error_control,config.createdBy_error_message);if (!validation.isValid) {return this.createError({path: this.path,message: validation.errorMessage});} else {return true;}}),
modifiedBy: yup.string().test("validator-custom-name", function (value) { const validation = ValidationControl(value,config.modifiedBy_error_control,config.modifiedBy_error_message);if (!validation.isValid) {return this.createError({path: this.path,message: validation.errorMessage});} else {return true;}}),
createdAt: yup.string().test("validator-custom-name", function (value) { const validation = ValidationControl(value,config.createdAt_error_control,config.createdAt_error_message);if (!validation.isValid) {return this.createError({path: this.path,message: validation.errorMessage});} else {return true;}}),
modifiedAt: yup.string().test("validator-custom-name", function (value) { const validation = ValidationControl(value,config.modifiedAt_error_control,config.modifiedAt_error_message);if (!validation.isValid) {return this.createError({path: this.path,message: validation.errorMessage});} else {return true;}}),
employee_id: yup.string().required('employee_id is required'),

      })}>
      {({
          errors,
          handleBlur,
          handleChange,
          handleSubmit,
          isSubmitting,
          touched,
          values,
setFieldValue,
        }) => (
              <Form  onSubmit={handleSubmit}>
              <Grid container spacing={2}>
                
{!config["attendance_date_isHidden"] && (
                    <>
                      {config["attendance_date_isNewline"] && < Grid xs ={ 12}
                md ={ 12}></ Grid >}
                      < Grid
                        item
                        xs = { config["attendance_date_grid_control"] }
                        md ={ config["attendance_date_grid_control"]}
                      >
                        < Form.Group >
                          {
                    (() =>
                    {
                    switch (config["attendance_date_control"])
                    {
                        case "rich text editor":
                            return (

                              <>

                                < InputLabel >
                                      {
                                config["attendance_date_form_new_name"] !==
                                      undefined
                                        ? config["attendance_date_form_new_name"]
                                        : "attendance_date"}
                                    </ InputLabel >
                                    < Field name = "attendance_date" >
                                      { ({ field }) => (
                                        < ReactQuill
                                          value ={ field.value}
                    onChange ={
                        (newValue) =>
                        {
                            setFieldValue(
                              "attendance_date",
                              newValue
                            );
                        }}
                                        />
                                      )}
                                    </Field>
                                  </>
                                );
                              case "file":
                                return (
                                  <>
                                    <input
                                      type = { config["attendance_date_control"] }
                                      name="attendance_date"
                                      key={uniquekey
    }
    id="attendance_date"
                                      className="form-control"
                                      onChange={handleChange
}
onBlur ={ handleBlur}
placeholder ={
    config["attendance_date_form_new_name"] !==
    undefined
      ? config["attendance_date_form_new_name"]
      : "attendance_date"
                                      }
                                    />

                                    < Button
                                      type = "button"
                                       sx={{my:1}}
                                      className = "p-1 mb-1 mt-1 d-flex justify-content-center"
                                      variant = "contained"
                                      onClick ={
    async(event) => {
        var inf =
          document.getElementById("attendance_date");
        await handleFileupload(
          inf,
          setFieldValue,
          "attendance_date"
        );
    }
}
disabled ={ isLoading == "attendance_date"}
                                    >
                                      {isLoading === "attendance_date" ? (
                                        <CircularProgress
                                          size={24}
                                          color="inherit"
                                        />
                                      ) : (
                                        "Upload"
                                      )}
                                    </ Button >
                                  </>
                                );
                              case "textarea":
    return (

      < TextareaAutosize
                                    minRows ={ 3}
    name = "attendance_date"
                                    key ={ uniquekey}
    id = "attendance_date"
                                    className = "form-control"
                                    value ={
        values.attendance_date
                                    }
    onChange ={ handleChange}
    onBlur ={ handleBlur}
                                  />
                                );
case "datetime":
    return (

      <>

        < InputLabel >
                                      {
        config["attendance_date_form_new_name"] !==
                                      undefined
                                        ? config["attendance_date_form_new_name"]
                                        : "attendance_date"}
                                    </ InputLabel >
                                    < TextField
                                      label ={
        config["attendance_date_form_new_name"] !==
        undefined
          ? config["attendance_date_form_new_name"]
          : "attendance_date"
                                      }
    type = "datetime-local"
                                      name = "attendance_date"
                                      id = "attendance_date"
                                      className = "form-control"
                                      value ={
        moment(values["attendance_date"]).format(
                                        "YYYY-MM-DD hh:mm:ss"
                                      )}
    onChange ={ handleChange}
    onBlur ={ handleBlur}
                                    />
                                  </>
                                );
                                                               case "signature":
                              return (
                                < ListItem
                                  sx ={ { justifyContent: "space-between" } }
                                >
                                  < InputLabel >
                                    {
                    config["attendance_date_form_new_name"] !==
                                    undefined
                                      ? config["attendance_date_form_new_name"]
                                      : "attendance_date"}
                                  </ InputLabel >
                                  < Button
                                    variant = "contained"
                                    color = "primary"
                                    onClick ={ handleOpenSignatureDialog}
                startIcon ={< CloudUploadIcon />}
                                  >
                                    Open Signature Dialog
                                  </ Button >

                                  < SignatureDialog
                                    open ={ openSignatureDialog}
                setFieldValue ={ setFieldValue}
                value ={ "attendance_date"}
                config ={ config}
                handleFileupload ={ handleFileupload}
                onClose ={ handleCloseSignatureDialog}
                                  />
                                </ ListItem >
                              );


case "date":
    return (

      <>

        < InputLabel >
                                      {
        config["attendance_date_form_new_name"] !==
                                      undefined
                                        ? config["attendance_date_form_new_name"]
                                        : "attendance_date"}
                                    </ InputLabel >
                                    < TextField
                                      type ={
        config["attendance_date_control"]
          ? config["attendance_date_control"]
          : "date"
                                      }
    name = "attendance_date"
                                      key ={ uniquekey}
    id = "attendance_date"
                                      className = "form-control"
                                      value ={
        moment(values["attendance_date"]).format(
                                        "YYYY-MM-DD"
                                      )}
    onChange ={ handleChange}
    onBlur ={ handleBlur}
                                    />
                                  </>
                                );
default:
    return (

      <>

        < InputLabel >
                                      {
        config["attendance_date_form_new_name"] !==
                                      undefined
                                        ? config["attendance_date_form_new_name"]
                                        : "attendance_date"}
                                    </ InputLabel >
                                    < TextField
                                      type ={
        config["attendance_date_control"]
          ? config["attendance_date_control"]
          : "text"
                                      }
    name = "attendance_date"
                                      key ={ uniquekey}
    id = "attendance_date"
                                      className = "form-control"
                                      value ={values["attendance_date"]}
    onChange ={ handleChange}
    onBlur ={ handleBlur}
                                    />
                                  </>
                                );
}
                          })()}

                          {
    errors.attendance_date && (
                            < Form.Control.Feedback type = "invalid" >
                              { errors.attendance_date}
                            </ Form.Control.Feedback >
                          )}
                        </ Form.Group >
                      </ Grid >
                    </>
)}
                  

{!config["status_isHidden"] && (
                    <>
                      {config["status_isNewline"] && < Grid xs ={ 12}
                md ={ 12}></ Grid >}
                      < Grid
                        item
                        xs = { config["status_grid_control"] }
                        md ={ config["status_grid_control"]}
                      >
                        < Form.Group >
                          {
                    (() =>
                    {
                    switch (config["status_control"])
                    {
                        case "rich text editor":
                            return (

                              <>

                                < InputLabel >
                                      {
                                config["status_form_new_name"] !==
                                      undefined
                                        ? config["status_form_new_name"]
                                        : "status"}
                                    </ InputLabel >
                                    < Field name = "status" >
                                      { ({ field }) => (
                                        < ReactQuill
                                          value ={ field.value}
                    onChange ={
                        (newValue) =>
                        {
                            setFieldValue(
                              "status",
                              newValue
                            );
                        }}
                                        />
                                      )}
                                    </Field>
                                  </>
                                );
                              case "file":
                                return (
                                  <>
                                    <input
                                      type = { config["status_control"] }
                                      name="status"
                                      key={uniquekey
    }
    id="status"
                                      className="form-control"
                                      onChange={handleChange
}
onBlur ={ handleBlur}
placeholder ={
    config["status_form_new_name"] !==
    undefined
      ? config["status_form_new_name"]
      : "status"
                                      }
                                    />

                                    < Button
                                      type = "button"
                                       sx={{my:1}}
                                      className = "p-1 mb-1 mt-1 d-flex justify-content-center"
                                      variant = "contained"
                                      onClick ={
    async(event) => {
        var inf =
          document.getElementById("status");
        await handleFileupload(
          inf,
          setFieldValue,
          "status"
        );
    }
}
disabled ={ isLoading == "status"}
                                    >
                                      {isLoading === "status" ? (
                                        <CircularProgress
                                          size={24}
                                          color="inherit"
                                        />
                                      ) : (
                                        "Upload"
                                      )}
                                    </ Button >
                                  </>
                                );
                              case "textarea":
    return (

      < TextareaAutosize
                                    minRows ={ 3}
    name = "status"
                                    key ={ uniquekey}
    id = "status"
                                    className = "form-control"
                                    value ={
        values.status
                                    }
    onChange ={ handleChange}
    onBlur ={ handleBlur}
                                  />
                                );
case "datetime":
    return (

      <>

        < InputLabel >
                                      {
        config["status_form_new_name"] !==
                                      undefined
                                        ? config["status_form_new_name"]
                                        : "status"}
                                    </ InputLabel >
                                    < TextField
                                      label ={
        config["status_form_new_name"] !==
        undefined
          ? config["status_form_new_name"]
          : "status"
                                      }
    type = "datetime-local"
                                      name = "status"
                                      id = "status"
                                      className = "form-control"
                                      value ={
        moment(values["status"]).format(
                                        "YYYY-MM-DD hh:mm:ss"
                                      )}
    onChange ={ handleChange}
    onBlur ={ handleBlur}
                                    />
                                  </>
                                );
                                                               case "signature":
                              return (
                                < ListItem
                                  sx ={ { justifyContent: "space-between" } }
                                >
                                  < InputLabel >
                                    {
                    config["status_form_new_name"] !==
                                    undefined
                                      ? config["status_form_new_name"]
                                      : "status"}
                                  </ InputLabel >
                                  < Button
                                    variant = "contained"
                                    color = "primary"
                                    onClick ={ handleOpenSignatureDialog}
                startIcon ={< CloudUploadIcon />}
                                  >
                                    Open Signature Dialog
                                  </ Button >

                                  < SignatureDialog
                                    open ={ openSignatureDialog}
                setFieldValue ={ setFieldValue}
                value ={ "status"}
                config ={ config}
                handleFileupload ={ handleFileupload}
                onClose ={ handleCloseSignatureDialog}
                                  />
                                </ ListItem >
                              );


case "date":
    return (

      <>

        < InputLabel >
                                      {
        config["status_form_new_name"] !==
                                      undefined
                                        ? config["status_form_new_name"]
                                        : "status"}
                                    </ InputLabel >
                                    < TextField
                                      type ={
        config["status_control"]
          ? config["status_control"]
          : "date"
                                      }
    name = "status"
                                      key ={ uniquekey}
    id = "status"
                                      className = "form-control"
                                      value ={
        moment(values["status"]).format(
                                        "YYYY-MM-DD"
                                      )}
    onChange ={ handleChange}
    onBlur ={ handleBlur}
                                    />
                                  </>
                                );
default:
    return (

      <>

        < InputLabel >
                                      {
        config["status_form_new_name"] !==
                                      undefined
                                        ? config["status_form_new_name"]
                                        : "status"}
                                    </ InputLabel >
                                    < TextField
                                      type ={
        config["status_control"]
          ? config["status_control"]
          : "text"
                                      }
    name = "status"
                                      key ={ uniquekey}
    id = "status"
                                      className = "form-control"
                                      value ={values["status"]}
    onChange ={ handleChange}
    onBlur ={ handleBlur}
                                    />
                                  </>
                                );
}
                          })()}

                          {
    errors.status && (
                            < Form.Control.Feedback type = "invalid" >
                              { errors.status}
                            </ Form.Control.Feedback >
                          )}
                        </ Form.Group >
                      </ Grid >
                    </>
)}
                  

{!config["remarks_isHidden"] && (
                    <>
                      {config["remarks_isNewline"] && < Grid xs ={ 12}
                md ={ 12}></ Grid >}
                      < Grid
                        item
                        xs = { config["remarks_grid_control"] }
                        md ={ config["remarks_grid_control"]}
                      >
                        < Form.Group >
                          {
                    (() =>
                    {
                    switch (config["remarks_control"])
                    {
                        case "rich text editor":
                            return (

                              <>

                                < InputLabel >
                                      {
                                config["remarks_form_new_name"] !==
                                      undefined
                                        ? config["remarks_form_new_name"]
                                        : "remarks"}
                                    </ InputLabel >
                                    < Field name = "remarks" >
                                      { ({ field }) => (
                                        < ReactQuill
                                          value ={ field.value}
                    onChange ={
                        (newValue) =>
                        {
                            setFieldValue(
                              "remarks",
                              newValue
                            );
                        }}
                                        />
                                      )}
                                    </Field>
                                  </>
                                );
                              case "file":
                                return (
                                  <>
                                    <input
                                      type = { config["remarks_control"] }
                                      name="remarks"
                                      key={uniquekey
    }
    id="remarks"
                                      className="form-control"
                                      onChange={handleChange
}
onBlur ={ handleBlur}
placeholder ={
    config["remarks_form_new_name"] !==
    undefined
      ? config["remarks_form_new_name"]
      : "remarks"
                                      }
                                    />

                                    < Button
                                      type = "button"
                                       sx={{my:1}}
                                      className = "p-1 mb-1 mt-1 d-flex justify-content-center"
                                      variant = "contained"
                                      onClick ={
    async(event) => {
        var inf =
          document.getElementById("remarks");
        await handleFileupload(
          inf,
          setFieldValue,
          "remarks"
        );
    }
}
disabled ={ isLoading == "remarks"}
                                    >
                                      {isLoading === "remarks" ? (
                                        <CircularProgress
                                          size={24}
                                          color="inherit"
                                        />
                                      ) : (
                                        "Upload"
                                      )}
                                    </ Button >
                                  </>
                                );
                              case "textarea":
    return (

      < TextareaAutosize
                                    minRows ={ 3}
    name = "remarks"
                                    key ={ uniquekey}
    id = "remarks"
                                    className = "form-control"
                                    value ={
        values.remarks
                                    }
    onChange ={ handleChange}
    onBlur ={ handleBlur}
                                  />
                                );
case "datetime":
    return (

      <>

        < InputLabel >
                                      {
        config["remarks_form_new_name"] !==
                                      undefined
                                        ? config["remarks_form_new_name"]
                                        : "remarks"}
                                    </ InputLabel >
                                    < TextField
                                      label ={
        config["remarks_form_new_name"] !==
        undefined
          ? config["remarks_form_new_name"]
          : "remarks"
                                      }
    type = "datetime-local"
                                      name = "remarks"
                                      id = "remarks"
                                      className = "form-control"
                                      value ={
        moment(values["remarks"]).format(
                                        "YYYY-MM-DD hh:mm:ss"
                                      )}
    onChange ={ handleChange}
    onBlur ={ handleBlur}
                                    />
                                  </>
                                );
                                                               case "signature":
                              return (
                                < ListItem
                                  sx ={ { justifyContent: "space-between" } }
                                >
                                  < InputLabel >
                                    {
                    config["remarks_form_new_name"] !==
                                    undefined
                                      ? config["remarks_form_new_name"]
                                      : "remarks"}
                                  </ InputLabel >
                                  < Button
                                    variant = "contained"
                                    color = "primary"
                                    onClick ={ handleOpenSignatureDialog}
                startIcon ={< CloudUploadIcon />}
                                  >
                                    Open Signature Dialog
                                  </ Button >

                                  < SignatureDialog
                                    open ={ openSignatureDialog}
                setFieldValue ={ setFieldValue}
                value ={ "remarks"}
                config ={ config}
                handleFileupload ={ handleFileupload}
                onClose ={ handleCloseSignatureDialog}
                                  />
                                </ ListItem >
                              );


case "date":
    return (

      <>

        < InputLabel >
                                      {
        config["remarks_form_new_name"] !==
                                      undefined
                                        ? config["remarks_form_new_name"]
                                        : "remarks"}
                                    </ InputLabel >
                                    < TextField
                                      type ={
        config["remarks_control"]
          ? config["remarks_control"]
          : "date"
                                      }
    name = "remarks"
                                      key ={ uniquekey}
    id = "remarks"
                                      className = "form-control"
                                      value ={
        moment(values["remarks"]).format(
                                        "YYYY-MM-DD"
                                      )}
    onChange ={ handleChange}
    onBlur ={ handleBlur}
                                    />
                                  </>
                                );
default:
    return (

      <>

        < InputLabel >
                                      {
        config["remarks_form_new_name"] !==
                                      undefined
                                        ? config["remarks_form_new_name"]
                                        : "remarks"}
                                    </ InputLabel >
                                    < TextField
                                      type ={
        config["remarks_control"]
          ? config["remarks_control"]
          : "text"
                                      }
    name = "remarks"
                                      key ={ uniquekey}
    id = "remarks"
                                      className = "form-control"
                                      value ={values["remarks"]}
    onChange ={ handleChange}
    onBlur ={ handleBlur}
                                    />
                                  </>
                                );
}
                          })()}

                          {
    errors.remarks && (
                            < Form.Control.Feedback type = "invalid" >
                              { errors.remarks}
                            </ Form.Control.Feedback >
                          )}
                        </ Form.Group >
                      </ Grid >
                    </>
)}
                  

{!config["isActive_isHidden"] && (
                    <>
                      {config["isActive_isNewline"] && < Grid xs ={ 12}
                md ={ 12}></ Grid >}
                      < Grid
                        item
                        xs = { config["isActive_grid_control"] }
                        md ={ config["isActive_grid_control"]}
                      >
                        < Form.Group >
                          {
                    (() =>
                    {
                    switch (config["isActive_control"])
                    {
                        case "rich text editor":
                            return (

                              <>

                                < InputLabel >
                                      {
                                config["isActive_form_new_name"] !==
                                      undefined
                                        ? config["isActive_form_new_name"]
                                        : "isActive"}
                                    </ InputLabel >
                                    < Field name = "isActive" >
                                      { ({ field }) => (
                                        < ReactQuill
                                          value ={ field.value}
                    onChange ={
                        (newValue) =>
                        {
                            setFieldValue(
                              "isActive",
                              newValue
                            );
                        }}
                                        />
                                      )}
                                    </Field>
                                  </>
                                );
                              case "file":
                                return (
                                  <>
                                    <input
                                      type = { config["isActive_control"] }
                                      name="isActive"
                                      key={uniquekey
    }
    id="isActive"
                                      className="form-control"
                                      onChange={handleChange
}
onBlur ={ handleBlur}
placeholder ={
    config["isActive_form_new_name"] !==
    undefined
      ? config["isActive_form_new_name"]
      : "isActive"
                                      }
                                    />

                                    < Button
                                      type = "button"
                                       sx={{my:1}}
                                      className = "p-1 mb-1 mt-1 d-flex justify-content-center"
                                      variant = "contained"
                                      onClick ={
    async(event) => {
        var inf =
          document.getElementById("isActive");
        await handleFileupload(
          inf,
          setFieldValue,
          "isActive"
        );
    }
}
disabled ={ isLoading == "isActive"}
                                    >
                                      {isLoading === "isActive" ? (
                                        <CircularProgress
                                          size={24}
                                          color="inherit"
                                        />
                                      ) : (
                                        "Upload"
                                      )}
                                    </ Button >
                                  </>
                                );
                              case "textarea":
    return (

      < TextareaAutosize
                                    minRows ={ 3}
    name = "isActive"
                                    key ={ uniquekey}
    id = "isActive"
                                    className = "form-control"
                                    value ={
        values.isActive
                                    }
    onChange ={ handleChange}
    onBlur ={ handleBlur}
                                  />
                                );
case "datetime":
    return (

      <>

        < InputLabel >
                                      {
        config["isActive_form_new_name"] !==
                                      undefined
                                        ? config["isActive_form_new_name"]
                                        : "isActive"}
                                    </ InputLabel >
                                    < TextField
                                      label ={
        config["isActive_form_new_name"] !==
        undefined
          ? config["isActive_form_new_name"]
          : "isActive"
                                      }
    type = "datetime-local"
                                      name = "isActive"
                                      id = "isActive"
                                      className = "form-control"
                                      value ={
        moment(values["isActive"]).format(
                                        "YYYY-MM-DD hh:mm:ss"
                                      )}
    onChange ={ handleChange}
    onBlur ={ handleBlur}
                                    />
                                  </>
                                );
                                                               case "signature":
                              return (
                                < ListItem
                                  sx ={ { justifyContent: "space-between" } }
                                >
                                  < InputLabel >
                                    {
                    config["isActive_form_new_name"] !==
                                    undefined
                                      ? config["isActive_form_new_name"]
                                      : "isActive"}
                                  </ InputLabel >
                                  < Button
                                    variant = "contained"
                                    color = "primary"
                                    onClick ={ handleOpenSignatureDialog}
                startIcon ={< CloudUploadIcon />}
                                  >
                                    Open Signature Dialog
                                  </ Button >

                                  < SignatureDialog
                                    open ={ openSignatureDialog}
                setFieldValue ={ setFieldValue}
                value ={ "isActive"}
                config ={ config}
                handleFileupload ={ handleFileupload}
                onClose ={ handleCloseSignatureDialog}
                                  />
                                </ ListItem >
                              );


case "date":
    return (

      <>

        < InputLabel >
                                      {
        config["isActive_form_new_name"] !==
                                      undefined
                                        ? config["isActive_form_new_name"]
                                        : "isActive"}
                                    </ InputLabel >
                                    < TextField
                                      type ={
        config["isActive_control"]
          ? config["isActive_control"]
          : "date"
                                      }
    name = "isActive"
                                      key ={ uniquekey}
    id = "isActive"
                                      className = "form-control"
                                      value ={
        moment(values["isActive"]).format(
                                        "YYYY-MM-DD"
                                      )}
    onChange ={ handleChange}
    onBlur ={ handleBlur}
                                    />
                                  </>
                                );
default:
    return (

      <>

        < InputLabel >
                                      {
        config["isActive_form_new_name"] !==
                                      undefined
                                        ? config["isActive_form_new_name"]
                                        : "isActive"}
                                    </ InputLabel >
                                    < TextField
                                      type ={
        config["isActive_control"]
          ? config["isActive_control"]
          : "text"
                                      }
    name = "isActive"
                                      key ={ uniquekey}
    id = "isActive"
                                      className = "form-control"
                                      value ={values["isActive"]}
    onChange ={ handleChange}
    onBlur ={ handleBlur}
                                    />
                                  </>
                                );
}
                          })()}

                          {
    errors.isActive && (
                            < Form.Control.Feedback type = "invalid" >
                              { errors.isActive}
                            </ Form.Control.Feedback >
                          )}
                        </ Form.Group >
                      </ Grid >
                    </>
)}
                  

{!config["createdBy_isHidden"] && (
                    <>
                      {config["createdBy_isNewline"] && < Grid xs ={ 12}
                md ={ 12}></ Grid >}
                      < Grid
                        item
                        xs = { config["createdBy_grid_control"] }
                        md ={ config["createdBy_grid_control"]}
                      >
                        < Form.Group >
                          {
                    (() =>
                    {
                    switch (config["createdBy_control"])
                    {
                        case "rich text editor":
                            return (

                              <>

                                < InputLabel >
                                      {
                                config["createdBy_form_new_name"] !==
                                      undefined
                                        ? config["createdBy_form_new_name"]
                                        : "createdBy"}
                                    </ InputLabel >
                                    < Field name = "createdBy" >
                                      { ({ field }) => (
                                        < ReactQuill
                                          value ={ field.value}
                    onChange ={
                        (newValue) =>
                        {
                            setFieldValue(
                              "createdBy",
                              newValue
                            );
                        }}
                                        />
                                      )}
                                    </Field>
                                  </>
                                );
                              case "file":
                                return (
                                  <>
                                    <input
                                      type = { config["createdBy_control"] }
                                      name="createdBy"
                                      key={uniquekey
    }
    id="createdBy"
                                      className="form-control"
                                      onChange={handleChange
}
onBlur ={ handleBlur}
placeholder ={
    config["createdBy_form_new_name"] !==
    undefined
      ? config["createdBy_form_new_name"]
      : "createdBy"
                                      }
                                    />

                                    < Button
                                      type = "button"
                                       sx={{my:1}}
                                      className = "p-1 mb-1 mt-1 d-flex justify-content-center"
                                      variant = "contained"
                                      onClick ={
    async(event) => {
        var inf =
          document.getElementById("createdBy");
        await handleFileupload(
          inf,
          setFieldValue,
          "createdBy"
        );
    }
}
disabled ={ isLoading == "createdBy"}
                                    >
                                      {isLoading === "createdBy" ? (
                                        <CircularProgress
                                          size={24}
                                          color="inherit"
                                        />
                                      ) : (
                                        "Upload"
                                      )}
                                    </ Button >
                                  </>
                                );
                              case "textarea":
    return (

      < TextareaAutosize
                                    minRows ={ 3}
    name = "createdBy"
                                    key ={ uniquekey}
    id = "createdBy"
                                    className = "form-control"
                                    value ={
        values.createdBy
                                    }
    onChange ={ handleChange}
    onBlur ={ handleBlur}
                                  />
                                );
case "datetime":
    return (

      <>

        < InputLabel >
                                      {
        config["createdBy_form_new_name"] !==
                                      undefined
                                        ? config["createdBy_form_new_name"]
                                        : "createdBy"}
                                    </ InputLabel >
                                    < TextField
                                      label ={
        config["createdBy_form_new_name"] !==
        undefined
          ? config["createdBy_form_new_name"]
          : "createdBy"
                                      }
    type = "datetime-local"
                                      name = "createdBy"
                                      id = "createdBy"
                                      className = "form-control"
                                      value ={
        moment(values["createdBy"]).format(
                                        "YYYY-MM-DD hh:mm:ss"
                                      )}
    onChange ={ handleChange}
    onBlur ={ handleBlur}
                                    />
                                  </>
                                );
                                                               case "signature":
                              return (
                                < ListItem
                                  sx ={ { justifyContent: "space-between" } }
                                >
                                  < InputLabel >
                                    {
                    config["createdBy_form_new_name"] !==
                                    undefined
                                      ? config["createdBy_form_new_name"]
                                      : "createdBy"}
                                  </ InputLabel >
                                  < Button
                                    variant = "contained"
                                    color = "primary"
                                    onClick ={ handleOpenSignatureDialog}
                startIcon ={< CloudUploadIcon />}
                                  >
                                    Open Signature Dialog
                                  </ Button >

                                  < SignatureDialog
                                    open ={ openSignatureDialog}
                setFieldValue ={ setFieldValue}
                value ={ "createdBy"}
                config ={ config}
                handleFileupload ={ handleFileupload}
                onClose ={ handleCloseSignatureDialog}
                                  />
                                </ ListItem >
                              );


case "date":
    return (

      <>

        < InputLabel >
                                      {
        config["createdBy_form_new_name"] !==
                                      undefined
                                        ? config["createdBy_form_new_name"]
                                        : "createdBy"}
                                    </ InputLabel >
                                    < TextField
                                      type ={
        config["createdBy_control"]
          ? config["createdBy_control"]
          : "date"
                                      }
    name = "createdBy"
                                      key ={ uniquekey}
    id = "createdBy"
                                      className = "form-control"
                                      value ={
        moment(values["createdBy"]).format(
                                        "YYYY-MM-DD"
                                      )}
    onChange ={ handleChange}
    onBlur ={ handleBlur}
                                    />
                                  </>
                                );
default:
    return (

      <>

        < InputLabel >
                                      {
        config["createdBy_form_new_name"] !==
                                      undefined
                                        ? config["createdBy_form_new_name"]
                                        : "createdBy"}
                                    </ InputLabel >
                                    < TextField
                                      type ={
        config["createdBy_control"]
          ? config["createdBy_control"]
          : "text"
                                      }
    name = "createdBy"
                                      key ={ uniquekey}
    id = "createdBy"
                                      className = "form-control"
                                      value ={values["createdBy"]}
    onChange ={ handleChange}
    onBlur ={ handleBlur}
                                    />
                                  </>
                                );
}
                          })()}

                          {
    errors.createdBy && (
                            < Form.Control.Feedback type = "invalid" >
                              { errors.createdBy}
                            </ Form.Control.Feedback >
                          )}
                        </ Form.Group >
                      </ Grid >
                    </>
)}
                  

{!config["modifiedBy_isHidden"] && (
                    <>
                      {config["modifiedBy_isNewline"] && < Grid xs ={ 12}
                md ={ 12}></ Grid >}
                      < Grid
                        item
                        xs = { config["modifiedBy_grid_control"] }
                        md ={ config["modifiedBy_grid_control"]}
                      >
                        < Form.Group >
                          {
                    (() =>
                    {
                    switch (config["modifiedBy_control"])
                    {
                        case "rich text editor":
                            return (

                              <>

                                < InputLabel >
                                      {
                                config["modifiedBy_form_new_name"] !==
                                      undefined
                                        ? config["modifiedBy_form_new_name"]
                                        : "modifiedBy"}
                                    </ InputLabel >
                                    < Field name = "modifiedBy" >
                                      { ({ field }) => (
                                        < ReactQuill
                                          value ={ field.value}
                    onChange ={
                        (newValue) =>
                        {
                            setFieldValue(
                              "modifiedBy",
                              newValue
                            );
                        }}
                                        />
                                      )}
                                    </Field>
                                  </>
                                );
                              case "file":
                                return (
                                  <>
                                    <input
                                      type = { config["modifiedBy_control"] }
                                      name="modifiedBy"
                                      key={uniquekey
    }
    id="modifiedBy"
                                      className="form-control"
                                      onChange={handleChange
}
onBlur ={ handleBlur}
placeholder ={
    config["modifiedBy_form_new_name"] !==
    undefined
      ? config["modifiedBy_form_new_name"]
      : "modifiedBy"
                                      }
                                    />

                                    < Button
                                      type = "button"
                                       sx={{my:1}}
                                      className = "p-1 mb-1 mt-1 d-flex justify-content-center"
                                      variant = "contained"
                                      onClick ={
    async(event) => {
        var inf =
          document.getElementById("modifiedBy");
        await handleFileupload(
          inf,
          setFieldValue,
          "modifiedBy"
        );
    }
}
disabled ={ isLoading == "modifiedBy"}
                                    >
                                      {isLoading === "modifiedBy" ? (
                                        <CircularProgress
                                          size={24}
                                          color="inherit"
                                        />
                                      ) : (
                                        "Upload"
                                      )}
                                    </ Button >
                                  </>
                                );
                              case "textarea":
    return (

      < TextareaAutosize
                                    minRows ={ 3}
    name = "modifiedBy"
                                    key ={ uniquekey}
    id = "modifiedBy"
                                    className = "form-control"
                                    value ={
        values.modifiedBy
                                    }
    onChange ={ handleChange}
    onBlur ={ handleBlur}
                                  />
                                );
case "datetime":
    return (

      <>

        < InputLabel >
                                      {
        config["modifiedBy_form_new_name"] !==
                                      undefined
                                        ? config["modifiedBy_form_new_name"]
                                        : "modifiedBy"}
                                    </ InputLabel >
                                    < TextField
                                      label ={
        config["modifiedBy_form_new_name"] !==
        undefined
          ? config["modifiedBy_form_new_name"]
          : "modifiedBy"
                                      }
    type = "datetime-local"
                                      name = "modifiedBy"
                                      id = "modifiedBy"
                                      className = "form-control"
                                      value ={
        moment(values["modifiedBy"]).format(
                                        "YYYY-MM-DD hh:mm:ss"
                                      )}
    onChange ={ handleChange}
    onBlur ={ handleBlur}
                                    />
                                  </>
                                );
                                                               case "signature":
                              return (
                                < ListItem
                                  sx ={ { justifyContent: "space-between" } }
                                >
                                  < InputLabel >
                                    {
                    config["modifiedBy_form_new_name"] !==
                                    undefined
                                      ? config["modifiedBy_form_new_name"]
                                      : "modifiedBy"}
                                  </ InputLabel >
                                  < Button
                                    variant = "contained"
                                    color = "primary"
                                    onClick ={ handleOpenSignatureDialog}
                startIcon ={< CloudUploadIcon />}
                                  >
                                    Open Signature Dialog
                                  </ Button >

                                  < SignatureDialog
                                    open ={ openSignatureDialog}
                setFieldValue ={ setFieldValue}
                value ={ "modifiedBy"}
                config ={ config}
                handleFileupload ={ handleFileupload}
                onClose ={ handleCloseSignatureDialog}
                                  />
                                </ ListItem >
                              );


case "date":
    return (

      <>

        < InputLabel >
                                      {
        config["modifiedBy_form_new_name"] !==
                                      undefined
                                        ? config["modifiedBy_form_new_name"]
                                        : "modifiedBy"}
                                    </ InputLabel >
                                    < TextField
                                      type ={
        config["modifiedBy_control"]
          ? config["modifiedBy_control"]
          : "date"
                                      }
    name = "modifiedBy"
                                      key ={ uniquekey}
    id = "modifiedBy"
                                      className = "form-control"
                                      value ={
        moment(values["modifiedBy"]).format(
                                        "YYYY-MM-DD"
                                      )}
    onChange ={ handleChange}
    onBlur ={ handleBlur}
                                    />
                                  </>
                                );
default:
    return (

      <>

        < InputLabel >
                                      {
        config["modifiedBy_form_new_name"] !==
                                      undefined
                                        ? config["modifiedBy_form_new_name"]
                                        : "modifiedBy"}
                                    </ InputLabel >
                                    < TextField
                                      type ={
        config["modifiedBy_control"]
          ? config["modifiedBy_control"]
          : "text"
                                      }
    name = "modifiedBy"
                                      key ={ uniquekey}
    id = "modifiedBy"
                                      className = "form-control"
                                      value ={values["modifiedBy"]}
    onChange ={ handleChange}
    onBlur ={ handleBlur}
                                    />
                                  </>
                                );
}
                          })()}

                          {
    errors.modifiedBy && (
                            < Form.Control.Feedback type = "invalid" >
                              { errors.modifiedBy}
                            </ Form.Control.Feedback >
                          )}
                        </ Form.Group >
                      </ Grid >
                    </>
)}
                  

{!config["createdAt_isHidden"] && (
                    <>
                      {config["createdAt_isNewline"] && < Grid xs ={ 12}
                md ={ 12}></ Grid >}
                      < Grid
                        item
                        xs = { config["createdAt_grid_control"] }
                        md ={ config["createdAt_grid_control"]}
                      >
                        < Form.Group >
                          {
                    (() =>
                    {
                    switch (config["createdAt_control"])
                    {
                        case "rich text editor":
                            return (

                              <>

                                < InputLabel >
                                      {
                                config["createdAt_form_new_name"] !==
                                      undefined
                                        ? config["createdAt_form_new_name"]
                                        : "createdAt"}
                                    </ InputLabel >
                                    < Field name = "createdAt" >
                                      { ({ field }) => (
                                        < ReactQuill
                                          value ={ field.value}
                    onChange ={
                        (newValue) =>
                        {
                            setFieldValue(
                              "createdAt",
                              newValue
                            );
                        }}
                                        />
                                      )}
                                    </Field>
                                  </>
                                );
                              case "file":
                                return (
                                  <>
                                    <input
                                      type = { config["createdAt_control"] }
                                      name="createdAt"
                                      key={uniquekey
    }
    id="createdAt"
                                      className="form-control"
                                      onChange={handleChange
}
onBlur ={ handleBlur}
placeholder ={
    config["createdAt_form_new_name"] !==
    undefined
      ? config["createdAt_form_new_name"]
      : "createdAt"
                                      }
                                    />

                                    < Button
                                      type = "button"
                                       sx={{my:1}}
                                      className = "p-1 mb-1 mt-1 d-flex justify-content-center"
                                      variant = "contained"
                                      onClick ={
    async(event) => {
        var inf =
          document.getElementById("createdAt");
        await handleFileupload(
          inf,
          setFieldValue,
          "createdAt"
        );
    }
}
disabled ={ isLoading == "createdAt"}
                                    >
                                      {isLoading === "createdAt" ? (
                                        <CircularProgress
                                          size={24}
                                          color="inherit"
                                        />
                                      ) : (
                                        "Upload"
                                      )}
                                    </ Button >
                                  </>
                                );
                              case "textarea":
    return (

      < TextareaAutosize
                                    minRows ={ 3}
    name = "createdAt"
                                    key ={ uniquekey}
    id = "createdAt"
                                    className = "form-control"
                                    value ={
        values.createdAt
                                    }
    onChange ={ handleChange}
    onBlur ={ handleBlur}
                                  />
                                );
case "datetime":
    return (

      <>

        < InputLabel >
                                      {
        config["createdAt_form_new_name"] !==
                                      undefined
                                        ? config["createdAt_form_new_name"]
                                        : "createdAt"}
                                    </ InputLabel >
                                    < TextField
                                      label ={
        config["createdAt_form_new_name"] !==
        undefined
          ? config["createdAt_form_new_name"]
          : "createdAt"
                                      }
    type = "datetime-local"
                                      name = "createdAt"
                                      id = "createdAt"
                                      className = "form-control"
                                      value ={
        moment(values["createdAt"]).format(
                                        "YYYY-MM-DD hh:mm:ss"
                                      )}
    onChange ={ handleChange}
    onBlur ={ handleBlur}
                                    />
                                  </>
                                );
                                                               case "signature":
                              return (
                                < ListItem
                                  sx ={ { justifyContent: "space-between" } }
                                >
                                  < InputLabel >
                                    {
                    config["createdAt_form_new_name"] !==
                                    undefined
                                      ? config["createdAt_form_new_name"]
                                      : "createdAt"}
                                  </ InputLabel >
                                  < Button
                                    variant = "contained"
                                    color = "primary"
                                    onClick ={ handleOpenSignatureDialog}
                startIcon ={< CloudUploadIcon />}
                                  >
                                    Open Signature Dialog
                                  </ Button >

                                  < SignatureDialog
                                    open ={ openSignatureDialog}
                setFieldValue ={ setFieldValue}
                value ={ "createdAt"}
                config ={ config}
                handleFileupload ={ handleFileupload}
                onClose ={ handleCloseSignatureDialog}
                                  />
                                </ ListItem >
                              );


case "date":
    return (

      <>

        < InputLabel >
                                      {
        config["createdAt_form_new_name"] !==
                                      undefined
                                        ? config["createdAt_form_new_name"]
                                        : "createdAt"}
                                    </ InputLabel >
                                    < TextField
                                      type ={
        config["createdAt_control"]
          ? config["createdAt_control"]
          : "date"
                                      }
    name = "createdAt"
                                      key ={ uniquekey}
    id = "createdAt"
                                      className = "form-control"
                                      value ={
        moment(values["createdAt"]).format(
                                        "YYYY-MM-DD"
                                      )}
    onChange ={ handleChange}
    onBlur ={ handleBlur}
                                    />
                                  </>
                                );
default:
    return (

      <>

        < InputLabel >
                                      {
        config["createdAt_form_new_name"] !==
                                      undefined
                                        ? config["createdAt_form_new_name"]
                                        : "createdAt"}
                                    </ InputLabel >
                                    < TextField
                                      type ={
        config["createdAt_control"]
          ? config["createdAt_control"]
          : "text"
                                      }
    name = "createdAt"
                                      key ={ uniquekey}
    id = "createdAt"
                                      className = "form-control"
                                      value ={values["createdAt"]}
    onChange ={ handleChange}
    onBlur ={ handleBlur}
                                    />
                                  </>
                                );
}
                          })()}

                          {
    errors.createdAt && (
                            < Form.Control.Feedback type = "invalid" >
                              { errors.createdAt}
                            </ Form.Control.Feedback >
                          )}
                        </ Form.Group >
                      </ Grid >
                    </>
)}
                  

{!config["modifiedAt_isHidden"] && (
                    <>
                      {config["modifiedAt_isNewline"] && < Grid xs ={ 12}
                md ={ 12}></ Grid >}
                      < Grid
                        item
                        xs = { config["modifiedAt_grid_control"] }
                        md ={ config["modifiedAt_grid_control"]}
                      >
                        < Form.Group >
                          {
                    (() =>
                    {
                    switch (config["modifiedAt_control"])
                    {
                        case "rich text editor":
                            return (

                              <>

                                < InputLabel >
                                      {
                                config["modifiedAt_form_new_name"] !==
                                      undefined
                                        ? config["modifiedAt_form_new_name"]
                                        : "modifiedAt"}
                                    </ InputLabel >
                                    < Field name = "modifiedAt" >
                                      { ({ field }) => (
                                        < ReactQuill
                                          value ={ field.value}
                    onChange ={
                        (newValue) =>
                        {
                            setFieldValue(
                              "modifiedAt",
                              newValue
                            );
                        }}
                                        />
                                      )}
                                    </Field>
                                  </>
                                );
                              case "file":
                                return (
                                  <>
                                    <input
                                      type = { config["modifiedAt_control"] }
                                      name="modifiedAt"
                                      key={uniquekey
    }
    id="modifiedAt"
                                      className="form-control"
                                      onChange={handleChange
}
onBlur ={ handleBlur}
placeholder ={
    config["modifiedAt_form_new_name"] !==
    undefined
      ? config["modifiedAt_form_new_name"]
      : "modifiedAt"
                                      }
                                    />

                                    < Button
                                      type = "button"
                                       sx={{my:1}}
                                      className = "p-1 mb-1 mt-1 d-flex justify-content-center"
                                      variant = "contained"
                                      onClick ={
    async(event) => {
        var inf =
          document.getElementById("modifiedAt");
        await handleFileupload(
          inf,
          setFieldValue,
          "modifiedAt"
        );
    }
}
disabled ={ isLoading == "modifiedAt"}
                                    >
                                      {isLoading === "modifiedAt" ? (
                                        <CircularProgress
                                          size={24}
                                          color="inherit"
                                        />
                                      ) : (
                                        "Upload"
                                      )}
                                    </ Button >
                                  </>
                                );
                              case "textarea":
    return (

      < TextareaAutosize
                                    minRows ={ 3}
    name = "modifiedAt"
                                    key ={ uniquekey}
    id = "modifiedAt"
                                    className = "form-control"
                                    value ={
        values.modifiedAt
                                    }
    onChange ={ handleChange}
    onBlur ={ handleBlur}
                                  />
                                );
case "datetime":
    return (

      <>

        < InputLabel >
                                      {
        config["modifiedAt_form_new_name"] !==
                                      undefined
                                        ? config["modifiedAt_form_new_name"]
                                        : "modifiedAt"}
                                    </ InputLabel >
                                    < TextField
                                      label ={
        config["modifiedAt_form_new_name"] !==
        undefined
          ? config["modifiedAt_form_new_name"]
          : "modifiedAt"
                                      }
    type = "datetime-local"
                                      name = "modifiedAt"
                                      id = "modifiedAt"
                                      className = "form-control"
                                      value ={
        moment(values["modifiedAt"]).format(
                                        "YYYY-MM-DD hh:mm:ss"
                                      )}
    onChange ={ handleChange}
    onBlur ={ handleBlur}
                                    />
                                  </>
                                );
                                                               case "signature":
                              return (
                                < ListItem
                                  sx ={ { justifyContent: "space-between" } }
                                >
                                  < InputLabel >
                                    {
                    config["modifiedAt_form_new_name"] !==
                                    undefined
                                      ? config["modifiedAt_form_new_name"]
                                      : "modifiedAt"}
                                  </ InputLabel >
                                  < Button
                                    variant = "contained"
                                    color = "primary"
                                    onClick ={ handleOpenSignatureDialog}
                startIcon ={< CloudUploadIcon />}
                                  >
                                    Open Signature Dialog
                                  </ Button >

                                  < SignatureDialog
                                    open ={ openSignatureDialog}
                setFieldValue ={ setFieldValue}
                value ={ "modifiedAt"}
                config ={ config}
                handleFileupload ={ handleFileupload}
                onClose ={ handleCloseSignatureDialog}
                                  />
                                </ ListItem >
                              );


case "date":
    return (

      <>

        < InputLabel >
                                      {
        config["modifiedAt_form_new_name"] !==
                                      undefined
                                        ? config["modifiedAt_form_new_name"]
                                        : "modifiedAt"}
                                    </ InputLabel >
                                    < TextField
                                      type ={
        config["modifiedAt_control"]
          ? config["modifiedAt_control"]
          : "date"
                                      }
    name = "modifiedAt"
                                      key ={ uniquekey}
    id = "modifiedAt"
                                      className = "form-control"
                                      value ={
        moment(values["modifiedAt"]).format(
                                        "YYYY-MM-DD"
                                      )}
    onChange ={ handleChange}
    onBlur ={ handleBlur}
                                    />
                                  </>
                                );
default:
    return (

      <>

        < InputLabel >
                                      {
        config["modifiedAt_form_new_name"] !==
                                      undefined
                                        ? config["modifiedAt_form_new_name"]
                                        : "modifiedAt"}
                                    </ InputLabel >
                                    < TextField
                                      type ={
        config["modifiedAt_control"]
          ? config["modifiedAt_control"]
          : "text"
                                      }
    name = "modifiedAt"
                                      key ={ uniquekey}
    id = "modifiedAt"
                                      className = "form-control"
                                      value ={values["modifiedAt"]}
    onChange ={ handleChange}
    onBlur ={ handleBlur}
                                    />
                                  </>
                                );
}
                          })()}

                          {
    errors.modifiedAt && (
                            < Form.Control.Feedback type = "invalid" >
                              { errors.modifiedAt}
                            </ Form.Control.Feedback >
                          )}
                        </ Form.Group >
                      </ Grid >
                    </>
)}
                  

{!config["employee_id_isHidden"] && (
  <>
    {config["employee_id_isNewline"] && <Grid xs={12} md={12}></Grid>}
    <Grid item xs={config["employee_id_grid_control"]} md={config["employee_id_grid_control"]}>
      <Form.Group>
        {(() =>
          {
            switch (config["employee_id_control"])
            {
              case "dropdown":
                return (
                  <>
                    <InputLabel id="employee_id-label">
                      {config["employee_id_form_new_name"] !== undefined
                        ? config["employee_id_form_new_name"]
                        : "employee_id"}
                    </InputLabel>
                    <FormControl fullWidth>
                      <Select
                        labelId="employee_id-label"
                        id="employee_id-select"
                        value={values.employee_id}
                        name="employee_id"
                        onChange={handleChange}
                        onBlur={handleBlur}
                      >
                        <MenuItem value={0}>Select Employees</MenuItem>
                        {employeesData.list.map((item, i) => (
                          <MenuItem
                            value={item.employee_id}
                            key={`$employees-${i}`}
                          >
                            {config.employee_id_ref === undefined
                              ? item["employee_id"]
                              : item[config.employee_id_ref]}
                          </MenuItem>
                        ))}
                      </Select>
                    </FormControl>
                  </>
                );
              case "radio":
                return (
                  <>
                    <InputLabel id="employee_id-label">
                      {config["employee_id_form_new_name"] !== undefined
                        ? config["employee_id_form_new_name"]
                        : "employee_id"}
                    </InputLabel>
                    <RadioGroup
                      name="employee_id"
                      value={values.employee_id}
                      onChange={handleChange}
                      onBlur={handleBlur}
                    >
                      <Grid container>
                        {" "}
                        
                        {employeesData.list.map((item, i) => (
                          <Grid
                            item
                            xs={12}
                            sm={6}
                            md={4}
                            lg={3}
                            key={`$employees-${i}`}
                          >
                            {" "}
                           
                            <FormControlLabel
                              value={item.employee_id}
                              control={<Radio />}
                              label={
                            config.employee_id_ref === undefined
                              ? item["employee_id"]
                              : item[config.employee_id_ref]
                          }
                            />
                          </Grid>
                        ))}
                      </Grid>
                    </RadioGroup>
                  </>
                );
              default:
                return (
                  <>
                    <InputLabel id="employee_id-label">
                      {config["employee_id_form_new_name"] !== undefined
                        ? config["employee_id_form_new_name"]
                        : "employee_id"}
                    </InputLabel>
                    <FormControl fullWidth>
                      <Select
                        labelId="employee_id-label"
                        id="employee_id-select"
                        value={values.employee_id}
                        name="employee_id"
                        onChange={handleChange}
                        onBlur={handleBlur}
                      >
                        <MenuItem value={0}>Select Employees</MenuItem>
                        {employeesData.list.map((item, i) => (
                          <MenuItem
                            value={item.employee_id}
                            key={`$employees-${i}`}
                          >
                            {config.employee_id_ref === undefined
                              ? item["employee_id"]
                              : item[config.employee_id_ref]}
                          </MenuItem>
                        ))}
                      </Select>
                    </FormControl>
                  </>
                );
            }
          })()}
      </Form.Group>
    </Grid>
  </>
)}


                </Grid>
                              <CustomizedSnackbars open={snackbarOpen} message={alertMessage} type={severity} onClose={handleSnackbarClose} />

<div
                style={{
                  display: "flex",
                  alignItems: "center",
                  justifyContent: "flex-end",
                }}
              >
                {isSaving ? (
                  <CircularProgress color="inherit" />
                ) : (
                   <Button 
  type="submit" 
  variant="contained"
  sx={{
    backgroundColor: 'black',
    color: 'white',
    '&:hover': {
      backgroundColor: '#333' // slightly lighter black on hover
    }
  }}
>
  Save
</Button>
                )}
              </div>
              </Form>
                       )}
      </Formik>
        </CardContent>
      </Card>
    );
}
