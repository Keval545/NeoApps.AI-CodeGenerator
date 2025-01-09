import React, { useState,useEffect } from "react";
import { useSelector } from "react-redux";
import { setS3bucketList, setS3bucketMessage,resetS3bucketToInit } from "redux/actions";
import { RootState } from "redux/reducers";
import { useAppDispatch } from "redux/store";
import { getS3bucket,filterS3bucketWithColumns } from "services/s3bucketService";




import ApiIcon from "@mui/icons-material/Api";
import Layout from "template";
import { Constant } from "template/Constant";
import { S3bucketForm } from "./form";
import { S3bucketTable } from "./table";
import { Container, Box, Alert, Button } from '@mui/material';
import {
  useRowSelector,
  getRowElement,
} from "Dnd/Dnd Designer/Utility/constants";


export const S3bucketDefaultView = (props) => {
  let config = props.config;
    const [loading,setLoading] = useState(false);
  const dispatch = useAppDispatch();
    if (config === undefined) config = {};
  const s3bucketData = useSelector((state: RootState) => state.s3bucket);
  useEffect(() => {
    if (s3bucketData && s3bucketData.list && s3bucketData.list.length === 0 && (s3bucketData.message !== "No Record Found After Filter")) {
      dispatch(resetS3bucketToInit());
      getS3bucket(Constant.defaultPageNumber, Constant.defaultDropdownPageSize, "").then((response) => {
        if (response && response.records) {
          dispatch(
            setS3bucketList({
              pageNo: Constant.defaultPageNumber,
              pageSize: Constant.defaultDropdownPageSize,
              list: response.records,
              totalCount: response.totalRecords,
              searchKey: "",
            })
          );
        } else {
          dispatch(setS3bucketMessage(`No Record Found For S3bucket`));
        }
      });
    }
  }, [s3bucketData.list.length]);
  

  


    useEffect(() => {
    dispatch(resetS3bucketToInit());
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

  const rData = useSelector((state: RootState) => state.s3bucket);
  const [row, setRow] = useState(undefined);
    const [action, setAction] = useState(
    config["ui_mode"] !== "Edit mode" ? "" : "add"
  );

   useEffect(() => {
    if (rData.message) {
      const timeoutId = setTimeout(() => {
        dispatch(setS3bucketMessage(""));
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
      filterS3bucketWithColumns(eval(config.filtercondition)).then(
        (response: any) => {
          console.log("Filter condition returns ------------------->");
          console.log(response);
          if(response && response.totalCount === 0){
            dispatch(setS3bucketMessage("No Record Found After Filter"));
          }
          if (response && response.records) {
            dispatch(
              setS3bucketList({
                pageNo: page,
                pageSize: pageSize,
                list: response.records,
                totalCount: response.total_count,
                searchKey: searchKey,
              })
            );
          } else {
            dispatch(setS3bucketMessage("No Record Found"));
          }
        }
      );
      } else{
        console.log("No filter condition ----------------->");
      getS3bucket(page, pageSize, searchKey).then((response) => {
        if (response && response.records) {
          dispatch(
            setS3bucketList({
              pageNo: page,
              pageSize: pageSize,
              list: response.records,
              totalCount: response.total_count,
              searchKey: searchKey,
            })
          );
        } else {
          dispatch(setS3bucketMessage("No Record Found"));
        }
      });
      }
    } else {
      getS3bucket(page, pageSize, searchKey).then((response) => {
        if (response && response.records) {
          dispatch(
            setS3bucketList({
              pageNo: page,
              pageSize: pageSize,
              list: response.records,
              totalCount: response.totalRecords,
              searchKey: searchKey,
            })
          );
        } else {
          dispatch(setS3bucketMessage("No Record Found"));
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
    dispatch(setS3bucketMessage(''));
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
const columnDefinitions=[{field:"bucket_id",headerName: config.bucket_id_new_name ? config.bucket_id_new_name:'bucket_id',sortable: true,hide: config.hasOwnProperty("bucket_id_visible") ? !config.bucket_id_visible: false,flex:1,renderCell: (params) => (<RowSelectorComponent inputType={config["bucket_id_control"]} field={params.row.bucket_id} display_control={config["bucket_id_display_control"]} bucket_id={config["bucket_id_bucket_name"]} bucket_folder={config["bucket_id_bucket_folder"]} />),},
{field:"bucket_name",headerName: config.bucket_name_new_name ? config.bucket_name_new_name:'bucket_name',sortable: true,hide: config.hasOwnProperty("bucket_name_visible") ? !config.bucket_name_visible: false,flex:1,renderCell: (params) => (<RowSelectorComponent inputType={config["bucket_name_control"]} field={params.row.bucket_name} display_control={config["bucket_name_display_control"]} bucket_id={config["bucket_name_bucket_name"]} bucket_folder={config["bucket_name_bucket_folder"]} />),},
{field:"bucket_url",headerName: config.bucket_url_new_name ? config.bucket_url_new_name:'bucket_url',sortable: true,hide: config.hasOwnProperty("bucket_url_visible") ? !config.bucket_url_visible: false,flex:1,renderCell: (params) => (<RowSelectorComponent inputType={config["bucket_url_control"]} field={params.row.bucket_url} display_control={config["bucket_url_display_control"]} bucket_id={config["bucket_url_bucket_name"]} bucket_folder={config["bucket_url_bucket_folder"]} />),},
{field:"modifiedBy",headerName: config.modifiedBy_new_name ? config.modifiedBy_new_name:'modifiedBy',sortable: true,hide: config.hasOwnProperty("modifiedBy_visible") ? !config.modifiedBy_visible: false,flex:1,renderCell: (params) => (<RowSelectorComponent inputType={config["modifiedBy_control"]} field={params.row.modifiedBy} display_control={config["modifiedBy_display_control"]} bucket_id={config["modifiedBy_bucket_name"]} bucket_folder={config["modifiedBy_bucket_folder"]} />),},
{field:"createdBy",headerName: config.createdBy_new_name ? config.createdBy_new_name:'createdBy',sortable: true,hide: config.hasOwnProperty("createdBy_visible") ? !config.createdBy_visible: false,flex:1,renderCell: (params) => (<RowSelectorComponent inputType={config["createdBy_control"]} field={params.row.createdBy} display_control={config["createdBy_display_control"]} bucket_id={config["createdBy_bucket_name"]} bucket_folder={config["createdBy_bucket_folder"]} />),},
{field:"modifiedAt",headerName: config.modifiedAt_new_name ? config.modifiedAt_new_name:'modifiedAt',sortable: true,hide: config.hasOwnProperty("modifiedAt_visible") ? !config.modifiedAt_visible: false,flex:1,renderCell: (params) => (<RowSelectorComponent inputType={config["modifiedAt_control"]} field={params.row.modifiedAt} display_control={config["modifiedAt_display_control"]} bucket_id={config["modifiedAt_bucket_name"]} bucket_folder={config["modifiedAt_bucket_folder"]} />),},
{field:"createdAt",headerName: config.createdAt_new_name ? config.createdAt_new_name:'createdAt',sortable: true,hide: config.hasOwnProperty("createdAt_visible") ? !config.createdAt_visible: false,flex:1,renderCell: (params) => (<RowSelectorComponent inputType={config["createdAt_control"]} field={params.row.createdAt} display_control={config["createdAt_display_control"]} bucket_id={config["createdAt_bucket_name"]} bucket_folder={config["createdAt_bucket_folder"]} />),},
{field:"isActive",headerName: config.isActive_new_name ? config.isActive_new_name:'isActive',sortable: true,hide: config.hasOwnProperty("isActive_visible") ? !config.isActive_visible: false,flex:1,renderCell: (params) => (<RowSelectorComponent inputType={config["isActive_control"]} field={params.row.isActive} display_control={config["isActive_display_control"]} bucket_id={config["isActive_bucket_name"]} bucket_folder={config["isActive_bucket_folder"]} />),},
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
              onClose={() => dispatch(setS3bucketMessage(''))}
              sx={{ mb: 2 }}
            >
              {rData.message}
              <Button
                color="inherit"
                size="small"
                onClick={() => dispatch(setS3bucketMessage(''))}
              >
                &times;
              </Button>
            </Alert>
          ) : null}
          {action ? (
            <S3bucketForm
              hideShowForm={setAction}
              action={action}
              row={row}
              config={config}
              getData={getData}
            />
          ) : (
            <>
              {!loading ? (
                <S3bucketTable
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
