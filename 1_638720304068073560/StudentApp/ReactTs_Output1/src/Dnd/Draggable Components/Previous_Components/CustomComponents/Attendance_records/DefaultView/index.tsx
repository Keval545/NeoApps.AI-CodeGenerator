import React, { useState,useEffect } from "react";
import { useSelector } from "react-redux";
import { setAttendance_recordsList, setAttendance_recordsMessage,resetAttendance_recordsToInit } from "redux/actions";
import { RootState } from "redux/reducers";
import { useAppDispatch } from "redux/store";
import { getAttendance_records,filterAttendance_recordsWithColumns } from "services/attendance_recordsService";
import { resetEmployeesToInit, setEmployeesList, setEmployeesMessage } from "redux/actions";

import { getEmployees } from "services/employeesService";

import ApiIcon from "@mui/icons-material/Api";
import Layout from "template";
import { Constant } from "template/Constant";
import { Attendance_recordsForm } from "./form";
import { Attendance_recordsTable } from "./table";
import { Container, Box, Alert, Button } from '@mui/material';
import {
  useRowSelector,
  getRowElement,
} from "Dnd/Dnd Designer/Utility/constants";


export const Attendance_recordsDefaultView = (props) => {
  let config = props.config;
    const [loading,setLoading] = useState(false);
  const dispatch = useAppDispatch();
    if (config === undefined) config = {};
  const attendance_recordsData = useSelector((state: RootState) => state.attendance_records);
  useEffect(() => {
    if (attendance_recordsData && attendance_recordsData.list && attendance_recordsData.list.length === 0 && (attendance_recordsData.message !== "No Record Found After Filter")) {
      dispatch(resetAttendance_recordsToInit());
      getAttendance_records(Constant.defaultPageNumber, Constant.defaultDropdownPageSize, "").then((response) => {
        if (response && response.records) {
          dispatch(
            setAttendance_recordsList({
              pageNo: Constant.defaultPageNumber,
              pageSize: Constant.defaultDropdownPageSize,
              list: response.records,
              totalCount: response.totalRecords,
              searchKey: "",
            })
          );
        } else {
          dispatch(setAttendance_recordsMessage(`No Record Found For Attendance_records`));
        }
      });
    }
  }, [attendance_recordsData.list.length]);
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


    useEffect(() => {
    dispatch(resetAttendance_recordsToInit());
    getData(Constant.defaultPageNumber, Constant.defaultPageSize, "");
  }, [config.filtercondition]);



 //console.log("props from Header:- ", props);
  let innerContent, backgroundColor, color, fontFamily;
  config.innerContent
    ? (innerContent = config.innerContent)
    : (innerContent = "Heading");
  config.backgroundColor
    ? (backgroundColor = config.backgroundColor)
    : (backgroundColor = "white");
  config.color ? (color = config.color) : (color = "black");
  config.fontFamily ? (fontFamily = config.fontFamily) : (fontFamily = "Arial");
  const renderHeader = (header, content) => {
    switch (header) {
      case "h1":
        return <h1>{content}</h1>;
      case "h2":
        return <h2>{content}</h2>;
      case "h3":
        return <h3>{content}</h3>;
      case "h4":
        return <h4>{content}</h4>;
      case "h5":
        return <h5>{content}</h5>;
      case "h6":
        return <h6>{content}</h6>;
      default:
        return <h2>{content}</h2>;
    }
  };

  const rData = useSelector((state: RootState) => state.attendance_records);
  const [row, setRow] = useState(undefined);
    const [action, setAction] = useState(
    config["ui_mode"] !== "Edit mode" ? "" : "add"
  );

   useEffect(() => {
    if (rData.message) {
      const timeoutId = setTimeout(() => {
        dispatch(setAttendance_recordsMessage(""));
      }, 5000);
      return () => clearTimeout(timeoutId);
    }
  }, [rData.message, dispatch]);

    useEffect(() => {
      if (config["ui_mode"] !== "Edit mode") setAction("");
      else setAction("add");
    }, [config["ui_mode"]]);
    let payload = [];
  let filterApplied = false;
  const getData = (page, pageSize, searchKey) => {
    if (config.filtercondition ? true : false) {
       const temp = eval(config.filtercondition);
      if (config.condition) {
        temp.push(config.condition);
      }
      const tp1 = temp.map((tp) => {
        return {
          ...tp,
          columnCondition: parseInt(tp.columnCondition),
          columnValue: tp.columnValue.toString(),
        };
      });
      if(config.filtercondition && config.filtercondition.length !== 0){
      filterAttendance_recordsWithColumns(eval(config.filtercondition)).then(
        (response: any) => {
          console.log("Filter condition returns ------------------->");
          console.log(response);
          if(response && response.totalCount === 0){
            dispatch(setAttendance_recordsMessage("No Record Found After Filter"));
          }
          if (response && response.records) {
            dispatch(
              setAttendance_recordsList({
                pageNo: page,
                pageSize: pageSize,
                list: response.records,
                totalCount: response.total_count,
                searchKey: searchKey,
              })
            );
          } else {
            dispatch(setAttendance_recordsMessage("No Record Found"));
          }
        }
      );
      } else{
        console.log("No filter condition ----------------->");
      getAttendance_records(page, pageSize, searchKey).then((response) => {
        if (response && response.records) {
          dispatch(
            setAttendance_recordsList({
              pageNo: page,
              pageSize: pageSize,
              list: response.records,
              totalCount: response.total_count,
              searchKey: searchKey,
            })
          );
        } else {
          dispatch(setAttendance_recordsMessage("No Record Found"));
        }
      });
      }
    } else {
      getAttendance_records(page, pageSize, searchKey).then((response) => {
        if (response && response.records) {
          dispatch(
            setAttendance_recordsList({
              pageNo: page,
              pageSize: pageSize,
              list: response.records,
              totalCount: response.totalRecords,
              searchKey: searchKey,
            })
          );
        } else {
          dispatch(setAttendance_recordsMessage("No Record Found"));
        }
      });
    }
  }
  interface Condition {
    columnName: string;
    columnCondition: string;
    columnValue: string;
  }
  const handleSubmit = (output: Condition[]) => {
    let result: any[] = [];

    output.forEach((condition) => {
      // Assign values to the new object and push it to the result array
      result.push({
        columnName: condition.columnName,
        columnCondition: Number(condition.columnCondition),
        columnValue: eval(condition.columnValue).toString(),
      });
    });

    // Log the result array
   //console.log("Result:", result);

    // Stringify the result array
    payload = result;
   //console.log("Result String:", JSON.stringify(payload));
    filterApplied = true;
  };
  const columnCondition = ["1", "2", "3"];
  const handleRowEdit = (rowData) => {
    setRow(rowData);
    dispatch(setAttendance_recordsMessage(''));
    setAction('edit');
  }
  function RowSelectorComponent({
    inputType,
    field,
    display_control,
    bucket_id,
    bucket_folder
  }) {
    const srcData = useRowSelector(
      inputType,
      field,
      display_control,
      bucket_id,
      bucket_folder
    );

    return getRowElement(inputType, field, display_control, srcData);
  }
const columnDefinitions=[{field:"attendance_id",headerName: config.attendance_id_new_name ? config.attendance_id_new_name:'attendance_id',sortable: true,hide: config.hasOwnProperty("attendance_id_visible") ? !config.attendance_id_visible: false,flex:1,renderCell: (params) => (<RowSelectorComponent inputType={config["attendance_id_control"]} field={params.row.attendance_id} display_control={config["attendance_id_display_control"]} bucket_id={config["attendance_id_bucket_name"]} bucket_folder={config["attendance_id_bucket_folder"]} />),},
{field:"employee_id",headerName: config.employee_id_new_name ? config.employee_id_new_name:'employee_id',sortable: true,renderCell:(params) => { const element = employeesData.list.find((element) => element.employee_id=== params.row.employee_id);return (<span> {element ? config["employee_id_ref"] === undefined? params.row.employee_id: element[config["employee_id_ref"]] :""} </span>); } ,hide: config.hasOwnProperty("employee_id_visible") ? !config.employee_id_visible: false,flex:1},
{field:"attendance_date",headerName: config.attendance_date_new_name ? config.attendance_date_new_name:'attendance_date',sortable: true,hide: config.hasOwnProperty("attendance_date_visible") ? !config.attendance_date_visible: false,flex:1,renderCell: (params) => (<RowSelectorComponent inputType={config["attendance_date_control"]} field={params.row.attendance_date} display_control={config["attendance_date_display_control"]} bucket_id={config["attendance_date_bucket_name"]} bucket_folder={config["attendance_date_bucket_folder"]} />),},
{field:"status",headerName: config.status_new_name ? config.status_new_name:'status',sortable: true,hide: config.hasOwnProperty("status_visible") ? !config.status_visible: false,flex:1,renderCell: (params) => (<RowSelectorComponent inputType={config["status_control"]} field={params.row.status} display_control={config["status_display_control"]} bucket_id={config["status_bucket_name"]} bucket_folder={config["status_bucket_folder"]} />),},
{field:"remarks",headerName: config.remarks_new_name ? config.remarks_new_name:'remarks',sortable: true,hide: config.hasOwnProperty("remarks_visible") ? !config.remarks_visible: false,flex:1,renderCell: (params) => (<RowSelectorComponent inputType={config["remarks_control"]} field={params.row.remarks} display_control={config["remarks_display_control"]} bucket_id={config["remarks_bucket_name"]} bucket_folder={config["remarks_bucket_folder"]} />),},
{field:"isActive",headerName: config.isActive_new_name ? config.isActive_new_name:'isActive',sortable: true,hide: config.hasOwnProperty("isActive_visible") ? !config.isActive_visible: false,flex:1,renderCell: (params) => (<RowSelectorComponent inputType={config["isActive_control"]} field={params.row.isActive} display_control={config["isActive_display_control"]} bucket_id={config["isActive_bucket_name"]} bucket_folder={config["isActive_bucket_folder"]} />),},
{field:"createdBy",headerName: config.createdBy_new_name ? config.createdBy_new_name:'createdBy',sortable: true,hide: config.hasOwnProperty("createdBy_visible") ? !config.createdBy_visible: false,flex:1,renderCell: (params) => (<RowSelectorComponent inputType={config["createdBy_control"]} field={params.row.createdBy} display_control={config["createdBy_display_control"]} bucket_id={config["createdBy_bucket_name"]} bucket_folder={config["createdBy_bucket_folder"]} />),},
{field:"modifiedBy",headerName: config.modifiedBy_new_name ? config.modifiedBy_new_name:'modifiedBy',sortable: true,hide: config.hasOwnProperty("modifiedBy_visible") ? !config.modifiedBy_visible: false,flex:1,renderCell: (params) => (<RowSelectorComponent inputType={config["modifiedBy_control"]} field={params.row.modifiedBy} display_control={config["modifiedBy_display_control"]} bucket_id={config["modifiedBy_bucket_name"]} bucket_folder={config["modifiedBy_bucket_folder"]} />),},
{field:"createdAt",headerName: config.createdAt_new_name ? config.createdAt_new_name:'createdAt',sortable: true,hide: config.hasOwnProperty("createdAt_visible") ? !config.createdAt_visible: false,flex:1,renderCell: (params) => (<RowSelectorComponent inputType={config["createdAt_control"]} field={params.row.createdAt} display_control={config["createdAt_display_control"]} bucket_id={config["createdAt_bucket_name"]} bucket_folder={config["createdAt_bucket_folder"]} />),},
{field:"modifiedAt",headerName: config.modifiedAt_new_name ? config.modifiedAt_new_name:'modifiedAt',sortable: true,hide: config.hasOwnProperty("modifiedAt_visible") ? !config.modifiedAt_visible: false,flex:1,renderCell: (params) => (<RowSelectorComponent inputType={config["modifiedAt_control"]} field={params.row.modifiedAt} display_control={config["modifiedAt_display_control"]} bucket_id={config["modifiedAt_bucket_name"]} bucket_folder={config["modifiedAt_bucket_folder"]} />),},
];

return (


    <div>
      <Container maxWidth={false}>
        {config["heading1_visible1"] && (
          <Box
            display="flex"
            alignItems="center"
            justifyContent="space-between"
            mb={2}
          >
            <div
              style={{
                backgroundColor,
                color,
                fontFamily,
                padding: "6px",
                borderRadius: "6px",
                textAlign: "center",
              }}
            >
              {renderHeader(config.headerSize, innerContent)}
            </div>
          </Box>
        )}
        <Box display="flex" flexDirection="column" minHeight="100vh">
          {rData.message ? (
            <Alert
              variant={Constant.defaultAlertVarient}
              className="alert-dismissible fade"
              onClose={() => dispatch(setAttendance_recordsMessage(''))}
              sx={{ mb: 2 }}
            >
              {rData.message}
              <Button
                color="inherit"
                size="small"
                onClick={() => dispatch(setAttendance_recordsMessage(''))}
              >
                &times;
              </Button>
            </Alert>
          ) : null}
          {action ? (
            <Attendance_recordsForm
              hideShowForm={setAction}
              action={action}
              row={row}
              config={config}
              getData={getData}
            />
          ) : (
            <>
              {!loading ? (
                <Attendance_recordsTable
                  columnDefinitions={columnDefinitions}
                  handleRowEdit={handleRowEdit}
                  hideShowForm={setAction}
                  getData={getData}
                  openLink={props.openLink}
                  config={config}
                />
              ) : (
                   <></>
                // <TableSkeleton />
              )}
            </>
          )}
        </Box>
      </Container>
    </div>
  );
};
