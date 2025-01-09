import { useFormik, Formik,Field } from "formik";
import React, { useEffect, useState } from "react";
import {  Form } from "react-bootstrap";
import { useSelector } from "react-redux";
import { format } from "date-fns";
import { RootState } from "redux/reducers";
import { uploadFileService } from "services/fileUploadService";
import { resetEmployeesToInit, setEmployeesList, setEmployeesMessage } from "redux/actions";




import { useAppDispatch } from "redux/store";
import ReactQuill from "react-quill";
import "react-quill/dist/quill.snow.css";
import { addEmployees, updateEmployees, getEmployees } from "services/employeesService";
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
export const EmployeesForm: React.FC<Props> = ({ row, hideShowForm, getData, action,  config,}) => {
    const dispatch = useAppDispatch();
    const iValue={employee_id:'',first_name:'',last_name:'',email:'',phone_number:'',department:'',isActive:'',createdBy:'',modifiedBy:'',createdAt:format(new Date(), "yyyy-MM-dd"),modifiedAt:format(new Date(), "yyyy-MM-dd")};
    const initialValue = action === 'edit' ? row : iValue;
  const employeesData = useSelector((state: RootState) => state.employees);
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
  


  

  
    const useStyles = makeStyles({
      richTextEditor: {
        '& .ql-container': {
          height: '300px'
        }
      }
    });
    const classes = useStyles();

  useEffect(() => {
    if (employeesData && employeesData.list && employeesData.list.length === 0) {
      dispatch(resetEmployeesToInit());
      getEmployees(Constant.defaultPageNumber, Constant.defaultDropdownPageSize, "").then((response) => {
        if (response && response.records) {
          dispatch(
            setEmployeesList({
              pageNo: Constant.defaultPageNumber,
              pageSize: Constant.defaultDropdownPageSize,
              list: response.records,
              totalCount: response.total_count,
              searchKey: "",
            })
          );
        } else {
          dispatch(setEmployeesMessage(`No Record Found For Employees`));
        }
      });
    }
  }, [employeesData.list.length]);
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
              : `Add Employees`
            : config["editFormHeading"] !== undefined
            ? config["editFormHeading"]
            : `Edit Employees`
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
      values.employee_id = Number(values.employee_id)
values.employee_id = Number(values.employee_id)
values.isActive = Number(values.isActive)
values.isActive = Number(values.isActive)

            setisSaving(true);
          if (action === 'edit') {
              const response = await updateEmployees(values.employee_id,values);
              if (response) {
                setisSaving(false);

                  dispatch(setEmployeesMessage("Updated Successfully"));
                  getData(Constant.defaultPageNumber, Constant.defaultPageSize, '');
                  hideShowForm('');
              } else {
                setisSaving(false);
                  dispatch(setEmployeesMessage("Some error occured!"));
              }
          } else if (action === 'add') {
              const response = await addEmployees(values);
              if (response) {
                setisSaving(false);
                  dispatch(setEmployeesMessage("Added Successfully"));
                  getData(Constant.defaultPageNumber, Constant.defaultPageSize, '');
                  hideShowForm('');
              } else {
                setisSaving(false);
                  dispatch(setEmployeesMessage("Some error occured!"));
              }
          }
      }}
      validationSchema= {yup.object({
         first_name: yup.string().test("validator-custom-name", function (value) { const validation = ValidationControl(value,config.first_name_error_control,config.first_name_error_message);if (!validation.isValid) {return this.createError({path: this.path,message: validation.errorMessage});} else {return true;}}),
last_name: yup.string().test("validator-custom-name", function (value) { const validation = ValidationControl(value,config.last_name_error_control,config.last_name_error_message);if (!validation.isValid) {return this.createError({path: this.path,message: validation.errorMessage});} else {return true;}}),
email: yup.string().test("validator-custom-name", function (value) { const validation = ValidationControl(value,config.email_error_control,config.email_error_message);if (!validation.isValid) {return this.createError({path: this.path,message: validation.errorMessage});} else {return true;}}),
phone_number: yup.string().test("validator-custom-name", function (value) { const validation = ValidationControl(value,config.phone_number_error_control,config.phone_number_error_message);if (!validation.isValid) {return this.createError({path: this.path,message: validation.errorMessage});} else {return true;}}),
department: yup.string().test("validator-custom-name", function (value) { const validation = ValidationControl(value,config.department_error_control,config.department_error_message);if (!validation.isValid) {return this.createError({path: this.path,message: validation.errorMessage});} else {return true;}}),
isActive: yup.string().test("validator-custom-name", function (value) { const validation = ValidationControl(value,config.isActive_error_control,config.isActive_error_message);if (!validation.isValid) {return this.createError({path: this.path,message: validation.errorMessage});} else {return true;}}),
createdBy: yup.string().test("validator-custom-name", function (value) { const validation = ValidationControl(value,config.createdBy_error_control,config.createdBy_error_message);if (!validation.isValid) {return this.createError({path: this.path,message: validation.errorMessage});} else {return true;}}),
modifiedBy: yup.string().test("validator-custom-name", function (value) { const validation = ValidationControl(value,config.modifiedBy_error_control,config.modifiedBy_error_message);if (!validation.isValid) {return this.createError({path: this.path,message: validation.errorMessage});} else {return true;}}),
createdAt: yup.string().test("validator-custom-name", function (value) { const validation = ValidationControl(value,config.createdAt_error_control,config.createdAt_error_message);if (!validation.isValid) {return this.createError({path: this.path,message: validation.errorMessage});} else {return true;}}),
modifiedAt: yup.string().test("validator-custom-name", function (value) { const validation = ValidationControl(value,config.modifiedAt_error_control,config.modifiedAt_error_message);if (!validation.isValid) {return this.createError({path: this.path,message: validation.errorMessage});} else {return true;}}),

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
                
{!config["first_name_isHidden"] && (
                    <>
                      {config["first_name_isNewline"] && < Grid xs ={ 12}
                md ={ 12}></ Grid >}
                      < Grid
                        item
                        xs = { config["first_name_grid_control"] }
                        md ={ config["first_name_grid_control"]}
                      >
                        < Form.Group >
                          {
                    (() =>
                    {
                    switch (config["first_name_control"])
                    {
                        case "rich text editor":
                            return (

                              <>

                                < InputLabel >
                                      {
                                config["first_name_form_new_name"] !==
                                      undefined
                                        ? config["first_name_form_new_name"]
                                        : "first_name"}
                                    </ InputLabel >
                                    < Field name = "first_name" >
                                      { ({ field }) => (
                                        < ReactQuill
                                          value ={ field.value}
                    onChange ={
                        (newValue) =>
                        {
                            setFieldValue(
                              "first_name",
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
                                      type = { config["first_name_control"] }
                                      name="first_name"
                                      key={uniquekey
    }
    id="first_name"
                                      className="form-control"
                                      onChange={handleChange
}
onBlur ={ handleBlur}
placeholder ={
    config["first_name_form_new_name"] !==
    undefined
      ? config["first_name_form_new_name"]
      : "first_name"
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
          document.getElementById("first_name");
        await handleFileupload(
          inf,
          setFieldValue,
          "first_name"
        );
    }
}
disabled ={ isLoading == "first_name"}
                                    >
                                      {isLoading === "first_name" ? (
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
    name = "first_name"
                                    key ={ uniquekey}
    id = "first_name"
                                    className = "form-control"
                                    value ={
        values.first_name
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
        config["first_name_form_new_name"] !==
                                      undefined
                                        ? config["first_name_form_new_name"]
                                        : "first_name"}
                                    </ InputLabel >
                                    < TextField
                                      label ={
        config["first_name_form_new_name"] !==
        undefined
          ? config["first_name_form_new_name"]
          : "first_name"
                                      }
    type = "datetime-local"
                                      name = "first_name"
                                      id = "first_name"
                                      className = "form-control"
                                      value ={
        moment(values["first_name"]).format(
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
                    config["first_name_form_new_name"] !==
                                    undefined
                                      ? config["first_name_form_new_name"]
                                      : "first_name"}
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
                value ={ "first_name"}
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
        config["first_name_form_new_name"] !==
                                      undefined
                                        ? config["first_name_form_new_name"]
                                        : "first_name"}
                                    </ InputLabel >
                                    < TextField
                                      type ={
        config["first_name_control"]
          ? config["first_name_control"]
          : "date"
                                      }
    name = "first_name"
                                      key ={ uniquekey}
    id = "first_name"
                                      className = "form-control"
                                      value ={
        moment(values["first_name"]).format(
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
        config["first_name_form_new_name"] !==
                                      undefined
                                        ? config["first_name_form_new_name"]
                                        : "first_name"}
                                    </ InputLabel >
                                    < TextField
                                      type ={
        config["first_name_control"]
          ? config["first_name_control"]
          : "text"
                                      }
    name = "first_name"
                                      key ={ uniquekey}
    id = "first_name"
                                      className = "form-control"
                                      value ={values["first_name"]}
    onChange ={ handleChange}
    onBlur ={ handleBlur}
                                    />
                                  </>
                                );
}
                          })()}

                          {
    errors.first_name && (
                            < Form.Control.Feedback type = "invalid" >
                              { errors.first_name}
                            </ Form.Control.Feedback >
                          )}
                        </ Form.Group >
                      </ Grid >
                    </>
)}
                  

{!config["last_name_isHidden"] && (
                    <>
                      {config["last_name_isNewline"] && < Grid xs ={ 12}
                md ={ 12}></ Grid >}
                      < Grid
                        item
                        xs = { config["last_name_grid_control"] }
                        md ={ config["last_name_grid_control"]}
                      >
                        < Form.Group >
                          {
                    (() =>
                    {
                    switch (config["last_name_control"])
                    {
                        case "rich text editor":
                            return (

                              <>

                                < InputLabel >
                                      {
                                config["last_name_form_new_name"] !==
                                      undefined
                                        ? config["last_name_form_new_name"]
                                        : "last_name"}
                                    </ InputLabel >
                                    < Field name = "last_name" >
                                      { ({ field }) => (
                                        < ReactQuill
                                          value ={ field.value}
                    onChange ={
                        (newValue) =>
                        {
                            setFieldValue(
                              "last_name",
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
                                      type = { config["last_name_control"] }
                                      name="last_name"
                                      key={uniquekey
    }
    id="last_name"
                                      className="form-control"
                                      onChange={handleChange
}
onBlur ={ handleBlur}
placeholder ={
    config["last_name_form_new_name"] !==
    undefined
      ? config["last_name_form_new_name"]
      : "last_name"
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
          document.getElementById("last_name");
        await handleFileupload(
          inf,
          setFieldValue,
          "last_name"
        );
    }
}
disabled ={ isLoading == "last_name"}
                                    >
                                      {isLoading === "last_name" ? (
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
    name = "last_name"
                                    key ={ uniquekey}
    id = "last_name"
                                    className = "form-control"
                                    value ={
        values.last_name
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
        config["last_name_form_new_name"] !==
                                      undefined
                                        ? config["last_name_form_new_name"]
                                        : "last_name"}
                                    </ InputLabel >
                                    < TextField
                                      label ={
        config["last_name_form_new_name"] !==
        undefined
          ? config["last_name_form_new_name"]
          : "last_name"
                                      }
    type = "datetime-local"
                                      name = "last_name"
                                      id = "last_name"
                                      className = "form-control"
                                      value ={
        moment(values["last_name"]).format(
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
                    config["last_name_form_new_name"] !==
                                    undefined
                                      ? config["last_name_form_new_name"]
                                      : "last_name"}
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
                value ={ "last_name"}
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
        config["last_name_form_new_name"] !==
                                      undefined
                                        ? config["last_name_form_new_name"]
                                        : "last_name"}
                                    </ InputLabel >
                                    < TextField
                                      type ={
        config["last_name_control"]
          ? config["last_name_control"]
          : "date"
                                      }
    name = "last_name"
                                      key ={ uniquekey}
    id = "last_name"
                                      className = "form-control"
                                      value ={
        moment(values["last_name"]).format(
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
        config["last_name_form_new_name"] !==
                                      undefined
                                        ? config["last_name_form_new_name"]
                                        : "last_name"}
                                    </ InputLabel >
                                    < TextField
                                      type ={
        config["last_name_control"]
          ? config["last_name_control"]
          : "text"
                                      }
    name = "last_name"
                                      key ={ uniquekey}
    id = "last_name"
                                      className = "form-control"
                                      value ={values["last_name"]}
    onChange ={ handleChange}
    onBlur ={ handleBlur}
                                    />
                                  </>
                                );
}
                          })()}

                          {
    errors.last_name && (
                            < Form.Control.Feedback type = "invalid" >
                              { errors.last_name}
                            </ Form.Control.Feedback >
                          )}
                        </ Form.Group >
                      </ Grid >
                    </>
)}
                  

{!config["email_isHidden"] && (
                    <>
                      {config["email_isNewline"] && < Grid xs ={ 12}
                md ={ 12}></ Grid >}
                      < Grid
                        item
                        xs = { config["email_grid_control"] }
                        md ={ config["email_grid_control"]}
                      >
                        < Form.Group >
                          {
                    (() =>
                    {
                    switch (config["email_control"])
                    {
                        case "rich text editor":
                            return (

                              <>

                                < InputLabel >
                                      {
                                config["email_form_new_name"] !==
                                      undefined
                                        ? config["email_form_new_name"]
                                        : "email"}
                                    </ InputLabel >
                                    < Field name = "email" >
                                      { ({ field }) => (
                                        < ReactQuill
                                          value ={ field.value}
                    onChange ={
                        (newValue) =>
                        {
                            setFieldValue(
                              "email",
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
                                      type = { config["email_control"] }
                                      name="email"
                                      key={uniquekey
    }
    id="email"
                                      className="form-control"
                                      onChange={handleChange
}
onBlur ={ handleBlur}
placeholder ={
    config["email_form_new_name"] !==
    undefined
      ? config["email_form_new_name"]
      : "email"
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
          document.getElementById("email");
        await handleFileupload(
          inf,
          setFieldValue,
          "email"
        );
    }
}
disabled ={ isLoading == "email"}
                                    >
                                      {isLoading === "email" ? (
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
    name = "email"
                                    key ={ uniquekey}
    id = "email"
                                    className = "form-control"
                                    value ={
        values.email
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
        config["email_form_new_name"] !==
                                      undefined
                                        ? config["email_form_new_name"]
                                        : "email"}
                                    </ InputLabel >
                                    < TextField
                                      label ={
        config["email_form_new_name"] !==
        undefined
          ? config["email_form_new_name"]
          : "email"
                                      }
    type = "datetime-local"
                                      name = "email"
                                      id = "email"
                                      className = "form-control"
                                      value ={
        moment(values["email"]).format(
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
                    config["email_form_new_name"] !==
                                    undefined
                                      ? config["email_form_new_name"]
                                      : "email"}
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
                value ={ "email"}
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
        config["email_form_new_name"] !==
                                      undefined
                                        ? config["email_form_new_name"]
                                        : "email"}
                                    </ InputLabel >
                                    < TextField
                                      type ={
        config["email_control"]
          ? config["email_control"]
          : "date"
                                      }
    name = "email"
                                      key ={ uniquekey}
    id = "email"
                                      className = "form-control"
                                      value ={
        moment(values["email"]).format(
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
        config["email_form_new_name"] !==
                                      undefined
                                        ? config["email_form_new_name"]
                                        : "email"}
                                    </ InputLabel >
                                    < TextField
                                      type ={
        config["email_control"]
          ? config["email_control"]
          : "text"
                                      }
    name = "email"
                                      key ={ uniquekey}
    id = "email"
                                      className = "form-control"
                                      value ={values["email"]}
    onChange ={ handleChange}
    onBlur ={ handleBlur}
                                    />
                                  </>
                                );
}
                          })()}

                          {
    errors.email && (
                            < Form.Control.Feedback type = "invalid" >
                              { errors.email}
                            </ Form.Control.Feedback >
                          )}
                        </ Form.Group >
                      </ Grid >
                    </>
)}
                  

{!config["phone_number_isHidden"] && (
                    <>
                      {config["phone_number_isNewline"] && < Grid xs ={ 12}
                md ={ 12}></ Grid >}
                      < Grid
                        item
                        xs = { config["phone_number_grid_control"] }
                        md ={ config["phone_number_grid_control"]}
                      >
                        < Form.Group >
                          {
                    (() =>
                    {
                    switch (config["phone_number_control"])
                    {
                        case "rich text editor":
                            return (

                              <>

                                < InputLabel >
                                      {
                                config["phone_number_form_new_name"] !==
                                      undefined
                                        ? config["phone_number_form_new_name"]
                                        : "phone_number"}
                                    </ InputLabel >
                                    < Field name = "phone_number" >
                                      { ({ field }) => (
                                        < ReactQuill
                                          value ={ field.value}
                    onChange ={
                        (newValue) =>
                        {
                            setFieldValue(
                              "phone_number",
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
                                      type = { config["phone_number_control"] }
                                      name="phone_number"
                                      key={uniquekey
    }
    id="phone_number"
                                      className="form-control"
                                      onChange={handleChange
}
onBlur ={ handleBlur}
placeholder ={
    config["phone_number_form_new_name"] !==
    undefined
      ? config["phone_number_form_new_name"]
      : "phone_number"
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
          document.getElementById("phone_number");
        await handleFileupload(
          inf,
          setFieldValue,
          "phone_number"
        );
    }
}
disabled ={ isLoading == "phone_number"}
                                    >
                                      {isLoading === "phone_number" ? (
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
    name = "phone_number"
                                    key ={ uniquekey}
    id = "phone_number"
                                    className = "form-control"
                                    value ={
        values.phone_number
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
        config["phone_number_form_new_name"] !==
                                      undefined
                                        ? config["phone_number_form_new_name"]
                                        : "phone_number"}
                                    </ InputLabel >
                                    < TextField
                                      label ={
        config["phone_number_form_new_name"] !==
        undefined
          ? config["phone_number_form_new_name"]
          : "phone_number"
                                      }
    type = "datetime-local"
                                      name = "phone_number"
                                      id = "phone_number"
                                      className = "form-control"
                                      value ={
        moment(values["phone_number"]).format(
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
                    config["phone_number_form_new_name"] !==
                                    undefined
                                      ? config["phone_number_form_new_name"]
                                      : "phone_number"}
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
                value ={ "phone_number"}
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
        config["phone_number_form_new_name"] !==
                                      undefined
                                        ? config["phone_number_form_new_name"]
                                        : "phone_number"}
                                    </ InputLabel >
                                    < TextField
                                      type ={
        config["phone_number_control"]
          ? config["phone_number_control"]
          : "date"
                                      }
    name = "phone_number"
                                      key ={ uniquekey}
    id = "phone_number"
                                      className = "form-control"
                                      value ={
        moment(values["phone_number"]).format(
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
        config["phone_number_form_new_name"] !==
                                      undefined
                                        ? config["phone_number_form_new_name"]
                                        : "phone_number"}
                                    </ InputLabel >
                                    < TextField
                                      type ={
        config["phone_number_control"]
          ? config["phone_number_control"]
          : "text"
                                      }
    name = "phone_number"
                                      key ={ uniquekey}
    id = "phone_number"
                                      className = "form-control"
                                      value ={values["phone_number"]}
    onChange ={ handleChange}
    onBlur ={ handleBlur}
                                    />
                                  </>
                                );
}
                          })()}

                          {
    errors.phone_number && (
                            < Form.Control.Feedback type = "invalid" >
                              { errors.phone_number}
                            </ Form.Control.Feedback >
                          )}
                        </ Form.Group >
                      </ Grid >
                    </>
)}
                  

{!config["department_isHidden"] && (
                    <>
                      {config["department_isNewline"] && < Grid xs ={ 12}
                md ={ 12}></ Grid >}
                      < Grid
                        item
                        xs = { config["department_grid_control"] }
                        md ={ config["department_grid_control"]}
                      >
                        < Form.Group >
                          {
                    (() =>
                    {
                    switch (config["department_control"])
                    {
                        case "rich text editor":
                            return (

                              <>

                                < InputLabel >
                                      {
                                config["department_form_new_name"] !==
                                      undefined
                                        ? config["department_form_new_name"]
                                        : "department"}
                                    </ InputLabel >
                                    < Field name = "department" >
                                      { ({ field }) => (
                                        < ReactQuill
                                          value ={ field.value}
                    onChange ={
                        (newValue) =>
                        {
                            setFieldValue(
                              "department",
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
                                      type = { config["department_control"] }
                                      name="department"
                                      key={uniquekey
    }
    id="department"
                                      className="form-control"
                                      onChange={handleChange
}
onBlur ={ handleBlur}
placeholder ={
    config["department_form_new_name"] !==
    undefined
      ? config["department_form_new_name"]
      : "department"
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
          document.getElementById("department");
        await handleFileupload(
          inf,
          setFieldValue,
          "department"
        );
    }
}
disabled ={ isLoading == "department"}
                                    >
                                      {isLoading === "department" ? (
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
    name = "department"
                                    key ={ uniquekey}
    id = "department"
                                    className = "form-control"
                                    value ={
        values.department
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
        config["department_form_new_name"] !==
                                      undefined
                                        ? config["department_form_new_name"]
                                        : "department"}
                                    </ InputLabel >
                                    < TextField
                                      label ={
        config["department_form_new_name"] !==
        undefined
          ? config["department_form_new_name"]
          : "department"
                                      }
    type = "datetime-local"
                                      name = "department"
                                      id = "department"
                                      className = "form-control"
                                      value ={
        moment(values["department"]).format(
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
                    config["department_form_new_name"] !==
                                    undefined
                                      ? config["department_form_new_name"]
                                      : "department"}
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
                value ={ "department"}
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
        config["department_form_new_name"] !==
                                      undefined
                                        ? config["department_form_new_name"]
                                        : "department"}
                                    </ InputLabel >
                                    < TextField
                                      type ={
        config["department_control"]
          ? config["department_control"]
          : "date"
                                      }
    name = "department"
                                      key ={ uniquekey}
    id = "department"
                                      className = "form-control"
                                      value ={
        moment(values["department"]).format(
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
        config["department_form_new_name"] !==
                                      undefined
                                        ? config["department_form_new_name"]
                                        : "department"}
                                    </ InputLabel >
                                    < TextField
                                      type ={
        config["department_control"]
          ? config["department_control"]
          : "text"
                                      }
    name = "department"
                                      key ={ uniquekey}
    id = "department"
                                      className = "form-control"
                                      value ={values["department"]}
    onChange ={ handleChange}
    onBlur ={ handleBlur}
                                    />
                                  </>
                                );
}
                          })()}

                          {
    errors.department && (
                            < Form.Control.Feedback type = "invalid" >
                              { errors.department}
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
