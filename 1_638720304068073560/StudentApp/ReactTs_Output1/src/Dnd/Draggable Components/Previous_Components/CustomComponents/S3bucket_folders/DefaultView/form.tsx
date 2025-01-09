import { useFormik, Formik,Field } from "formik";
import React, { useEffect, useState } from "react";
import {  Form } from "react-bootstrap";
import { useSelector } from "react-redux";
import { format } from "date-fns";
import { RootState } from "redux/reducers";
import { uploadFileService } from "services/fileUploadService";
import { resetS3bucket_foldersToInit, setS3bucket_foldersList, setS3bucket_foldersMessage } from "redux/actions";




import { useAppDispatch } from "redux/store";
import ReactQuill from "react-quill";
import "react-quill/dist/quill.snow.css";
import { addS3bucket_folders, updateS3bucket_folders, getS3bucket_folders } from "services/s3bucket_foldersService";
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
export const S3bucket_foldersForm: React.FC<Props> = ({ row, hideShowForm, getData, action,  config,}) => {
    const dispatch = useAppDispatch();
    const iValue={folder_id:'',folder_name:'',modifiedBy:'',createdBy:'',modifiedAt:format(new Date(), "yyyy-MM-dd"),createdAt:format(new Date(), "yyyy-MM-dd"),isActive:''};
    const initialValue = action === 'edit' ? row : iValue;
  const s3bucket_foldersData = useSelector((state: RootState) => state.s3bucket_folders);
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
    if (s3bucket_foldersData && s3bucket_foldersData.list && s3bucket_foldersData.list.length === 0) {
      dispatch(resetS3bucket_foldersToInit());
      getS3bucket_folders(Constant.defaultPageNumber, Constant.defaultDropdownPageSize, "").then((response) => {
        if (response && response.records) {
          dispatch(
            setS3bucket_foldersList({
              pageNo: Constant.defaultPageNumber,
              pageSize: Constant.defaultDropdownPageSize,
              list: response.records,
              totalCount: response.total_count,
              searchKey: "",
            })
          );
        } else {
          dispatch(setS3bucket_foldersMessage(`No Record Found For S3bucket_folders`));
        }
      });
    }
  }, [s3bucket_foldersData.list.length]);
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
              : `Add S3bucket_folders`
            : config["editFormHeading"] !== undefined
            ? config["editFormHeading"]
            : `Edit S3bucket_folders`
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
      values.folder_id = Number(values.folder_id)
values.folder_id = Number(values.folder_id)
values.isActive = Number(values.isActive)
values.isActive = Number(values.isActive)

            setisSaving(true);
          if (action === 'edit') {
              const response = await updateS3bucket_folders(values.folder_id,values);
              if (response) {
                setisSaving(false);

                  dispatch(setS3bucket_foldersMessage("Updated Successfully"));
                  getData(Constant.defaultPageNumber, Constant.defaultPageSize, '');
                  hideShowForm('');
              } else {
                setisSaving(false);
                  dispatch(setS3bucket_foldersMessage("Some error occured!"));
              }
          } else if (action === 'add') {
              const response = await addS3bucket_folders(values);
              if (response) {
                setisSaving(false);
                  dispatch(setS3bucket_foldersMessage("Added Successfully"));
                  getData(Constant.defaultPageNumber, Constant.defaultPageSize, '');
                  hideShowForm('');
              } else {
                setisSaving(false);
                  dispatch(setS3bucket_foldersMessage("Some error occured!"));
              }
          }
      }}
      validationSchema= {yup.object({
         folder_name: yup.string().test("validator-custom-name", function (value) { const validation = ValidationControl(value,config.folder_name_error_control,config.folder_name_error_message);if (!validation.isValid) {return this.createError({path: this.path,message: validation.errorMessage});} else {return true;}}),
modifiedBy: yup.string().test("validator-custom-name", function (value) { const validation = ValidationControl(value,config.modifiedBy_error_control,config.modifiedBy_error_message);if (!validation.isValid) {return this.createError({path: this.path,message: validation.errorMessage});} else {return true;}}),
createdBy: yup.string().test("validator-custom-name", function (value) { const validation = ValidationControl(value,config.createdBy_error_control,config.createdBy_error_message);if (!validation.isValid) {return this.createError({path: this.path,message: validation.errorMessage});} else {return true;}}),
modifiedAt: yup.string().test("validator-custom-name", function (value) { const validation = ValidationControl(value,config.modifiedAt_error_control,config.modifiedAt_error_message);if (!validation.isValid) {return this.createError({path: this.path,message: validation.errorMessage});} else {return true;}}),
createdAt: yup.string().test("validator-custom-name", function (value) { const validation = ValidationControl(value,config.createdAt_error_control,config.createdAt_error_message);if (!validation.isValid) {return this.createError({path: this.path,message: validation.errorMessage});} else {return true;}}),
isActive: yup.string().test("validator-custom-name", function (value) { const validation = ValidationControl(value,config.isActive_error_control,config.isActive_error_message);if (!validation.isValid) {return this.createError({path: this.path,message: validation.errorMessage});} else {return true;}}),

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
                
{!config["folder_name_isHidden"] && (
                    <>
                      {config["folder_name_isNewline"] && < Grid xs ={ 12}
                md ={ 12}></ Grid >}
                      < Grid
                        item
                        xs = { config["folder_name_grid_control"] }
                        md ={ config["folder_name_grid_control"]}
                      >
                        < Form.Group >
                          {
                    (() =>
                    {
                    switch (config["folder_name_control"])
                    {
                        case "rich text editor":
                            return (

                              <>

                                < InputLabel >
                                      {
                                config["folder_name_form_new_name"] !==
                                      undefined
                                        ? config["folder_name_form_new_name"]
                                        : "folder_name"}
                                    </ InputLabel >
                                    < Field name = "folder_name" >
                                      { ({ field }) => (
                                        < ReactQuill
                                          value ={ field.value}
                    onChange ={
                        (newValue) =>
                        {
                            setFieldValue(
                              "folder_name",
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
                                      type = { config["folder_name_control"] }
                                      name="folder_name"
                                      key={uniquekey
    }
    id="folder_name"
                                      className="form-control"
                                      onChange={handleChange
}
onBlur ={ handleBlur}
placeholder ={
    config["folder_name_form_new_name"] !==
    undefined
      ? config["folder_name_form_new_name"]
      : "folder_name"
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
          document.getElementById("folder_name");
        await handleFileupload(
          inf,
          setFieldValue,
          "folder_name"
        );
    }
}
disabled ={ isLoading == "folder_name"}
                                    >
                                      {isLoading === "folder_name" ? (
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
    name = "folder_name"
                                    key ={ uniquekey}
    id = "folder_name"
                                    className = "form-control"
                                    value ={
        values.folder_name
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
        config["folder_name_form_new_name"] !==
                                      undefined
                                        ? config["folder_name_form_new_name"]
                                        : "folder_name"}
                                    </ InputLabel >
                                    < TextField
                                      label ={
        config["folder_name_form_new_name"] !==
        undefined
          ? config["folder_name_form_new_name"]
          : "folder_name"
                                      }
    type = "datetime-local"
                                      name = "folder_name"
                                      id = "folder_name"
                                      className = "form-control"
                                      value ={
        moment(values["folder_name"]).format(
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
                    config["folder_name_form_new_name"] !==
                                    undefined
                                      ? config["folder_name_form_new_name"]
                                      : "folder_name"}
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
                value ={ "folder_name"}
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
        config["folder_name_form_new_name"] !==
                                      undefined
                                        ? config["folder_name_form_new_name"]
                                        : "folder_name"}
                                    </ InputLabel >
                                    < TextField
                                      type ={
        config["folder_name_control"]
          ? config["folder_name_control"]
          : "date"
                                      }
    name = "folder_name"
                                      key ={ uniquekey}
    id = "folder_name"
                                      className = "form-control"
                                      value ={
        moment(values["folder_name"]).format(
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
        config["folder_name_form_new_name"] !==
                                      undefined
                                        ? config["folder_name_form_new_name"]
                                        : "folder_name"}
                                    </ InputLabel >
                                    < TextField
                                      type ={
        config["folder_name_control"]
          ? config["folder_name_control"]
          : "text"
                                      }
    name = "folder_name"
                                      key ={ uniquekey}
    id = "folder_name"
                                      className = "form-control"
                                      value ={values["folder_name"]}
    onChange ={ handleChange}
    onBlur ={ handleBlur}
                                    />
                                  </>
                                );
}
                          })()}

                          {
    errors.folder_name && (
                            < Form.Control.Feedback type = "invalid" >
                              { errors.folder_name}
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
