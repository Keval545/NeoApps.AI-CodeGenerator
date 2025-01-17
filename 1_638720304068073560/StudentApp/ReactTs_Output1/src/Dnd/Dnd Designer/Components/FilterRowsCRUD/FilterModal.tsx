import React, { useState } from "react";
import {  Row, Col, Form } from "react-bootstrap";
import { RootState } from "redux/reducers";
import { useSelector } from "react-redux";
import styles from "./SlideDrawer.module.css";
import { MdHelp } from "react-icons/md";
import "react-tooltip/dist/react-tooltip.css";
// import { Tooltip } from "react-tooltip";
import { IAttendance_recordsiData } from "redux/slices/attendance_records";
import { IEmployeesiData } from "redux/slices/employees";
import { ILeave_requestsiData } from "redux/slices/leave_requests";
import { IS3bucketiData } from "redux/slices/s3bucket";
import { IS3bucket_foldersiData } from "redux/slices/s3bucket_folders";

import Tooltip from "@mui/material/Tooltip";
import IconButton from "@mui/material/IconButton";
import {
Button,
  Typography,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Drawer,
  Grid,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  TextField,
  Box,
} from "@mui/material";

interface FilterModalProps {
  show: boolean;
  handleClose: () => void;
  onSubmit: (output: string) => void;
  componentName;
}

interface FilterRow {
  selectedColumn: string;
  selectedOperator: string;
  inputValue: string;
  logicalOperator: string;
}

const FilterModal: React.FC<FilterModalProps> = ({
  show,
  handleClose,
  onSubmit,
  componentName,
}) => {
  const attendance_recordsData = useSelector((state: RootState) => state.attendance_records);
const employeesData = useSelector((state: RootState) => state.employees);
const leave_requestsData = useSelector((state: RootState) => state.leave_requests);
const s3bucketData = useSelector((state: RootState) => state.s3bucket);
const s3bucket_foldersData = useSelector((state: RootState) => state.s3bucket_folders);

  const rData = useSelector((state: RootState) => state[componentName]);
  const [selectedObject, setSelectedObject] = useState("");
  const [filteredData, setFilteredData] = useState<any[]>([]);
  const [combinedData, setCombinedData] = useState<any[]>([]);

  const [filterRows, setFilterRows] = useState<FilterRow[]>([
    {
      selectedColumn: "",
      selectedOperator: ">=",
      inputValue: "",
      logicalOperator: "&&",
    },
  ]);
  
  function getPropertiesFromObject(obj: object): string[] {
    return Object.keys(obj);
  }
  const [slices, setSlices] = useState<{ [key: string]: string[] }>({
    attendance_records: getPropertiesFromObject(IAttendance_recordsiData),employees: getPropertiesFromObject(IEmployeesiData),leave_requests: getPropertiesFromObject(ILeave_requestsiData),s3bucket: getPropertiesFromObject(IS3bucketiData),s3bucket_folders: getPropertiesFromObject(IS3bucket_foldersiData),
  });
  const updateFilterRow = (
    index: number,
    key: keyof FilterRow,
    value: string
  ) => {
    const newFilterRows = [...filterRows];
    newFilterRows[index][key] = value;
    setFilterRows(newFilterRows);
  };

  const [selectedSlice, setSelectedSlice] = useState("");
  const applyFilterOnSliceSelection = () => {
    if (!selectedReduxSlice) return;

    const filtered = selectedReduxSlice.list.filter((stack: any) => {
      return filterRows.every((row) => {
        if (!row.selectedColumn || !row.inputValue) return true;

        var value;
        var stackValue;
        if (typeof row.inputValue === "string") {
          value = row.inputValue;
          stackValue = stack[row.selectedColumn];
        } else {
          value = parseFloat(row.inputValue);
          stackValue = parseFloat(stack[row.selectedColumn]);
        }

        switch (row.selectedOperator) {
          case ">=":
            return stackValue >= value;
          case "<=":
            return stackValue <= value;
          case "==":
            return stackValue === value;
          case "!=":
            return stackValue !== value;
          default:
            return true;
        }
      });
    });

   //console.log(filtered);
    setFilteredData(filtered);
  };

  const handleSliceSelection = (selectedSlice: string) => {
    setSelectedSlice(selectedSlice);
   //console.log(selectedSlice);
    switch (selectedSlice) {
        case "attendance_records":
setSelectedReduxSlice(attendance_recordsData);
break;case "employees":
setSelectedReduxSlice(employeesData);
break;case "leave_requests":
setSelectedReduxSlice(leave_requestsData);
break;case "s3bucket":
setSelectedReduxSlice(s3bucketData);
break;case "s3bucket_folders":
setSelectedReduxSlice(s3bucket_foldersData);
break;
      default:
        setSelectedReduxSlice(null);
    }
  };
  React.useEffect(() => {
    applyFilterOnSliceSelection();
  }, [selectedSlice, filterRows]);

  //Dev2
  React.useEffect(() => {
    // setCombinedData((prevData) => [...prevData, ...filteredData]);
    setCombinedData(filteredData);
  }, [filteredData]);

  const [selectedReduxSlice, setSelectedReduxSlice] = useState<any>(null);
  const handleDropdownChange = (e: any) => {
   //console.log("handleDropdownChange called");
    const selectedSliceName = e.target.value;
    setSelectedObject(selectedSliceName);

    switch (selectedSliceName) {
      case "attendance_records":
setSelectedReduxSlice(attendance_recordsData);
break;case "employees":
setSelectedReduxSlice(employeesData);
break;case "leave_requests":
setSelectedReduxSlice(leave_requestsData);
break;case "s3bucket":
setSelectedReduxSlice(s3bucketData);
break;case "s3bucket_folders":
setSelectedReduxSlice(s3bucket_foldersData);
break;
      default:
        setSelectedReduxSlice(null);
    }
  };

  const handleApplyFilter = () => {
    if (selectedReduxSlice) {
      console.log("Apply filter clicked", filterRows);
      let filterString = `selectedReduxSlice.list.find(stack => `;
      filterRows.forEach((row, index) => {
        const formattedInputValue =
          typeof row.inputValue === "string"
            ? `"${row.inputValue}"`
            : row.inputValue;
        filterString += `stack.${row.selectedColumn} ${row.selectedOperator} ${formattedInputValue}`;
        if (index < filterRows.length - 1) {
          filterString += ` ${row.logicalOperator} `;
        }
      });
      // filterString += `).${selectedObject}`;
      filterString += `)`;
      if (eval(filterString) !== undefined) {
        filterString += `.${selectedObject}`;
        //console.log("The selected slice is:", selectedSlice);
        //console.log(filterString);
        //console.log(eval(filterString));
        //  console.log(selectedReduxSlice.list);

        onSubmit(JSON.stringify(eval(filterString)));
      } else {
       
        console.log("Filter string : ", filterString);
        var value;
        filterRows.forEach((row, index) => {
          console.log("row,inputvalue : ", row.inputValue);
          // const formattedInputValue =
          //   typeof row.inputValue === "string"
          //     ? `"${row.inputValue}"`
          //     : row.inputValue;
          value = row.inputValue;
        });
        console.log(value);
        onSubmit(JSON.stringify(value));
      }
      // Update the filteredData state

      // Filter the selectedReduxSlice data based on the filterRows conditions
    }
  };
  const renderSelectedData = () => {
    if (combinedData.length === 0) {
      return <div>No data to display.</div>;
    }

    return (
      <TableContainer style={{ maxHeight: '400px', overflowY: 'auto', marginBottom: '90px' }}>
        <Table>
          <TableHead>
            <TableRow>
              {slices[selectedSlice].map((colName) => (
                <TableCell key={colName}>{colName}</TableCell>
              ))}
            </TableRow>
          </TableHead>
          <TableBody>
            {combinedData.map((row, index) => (
              <TableRow key={index}>
                {slices[selectedSlice].map((colName) => (
                  <TableCell key={colName}>{row[colName]}</TableCell>
                ))}
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    );
  };

  return (
    <>
      {show && (
        <div className={styles.drawerOverlay} onClick={handleClose}>
          <div
            className={`${styles.drawerContent} ${
              show ? styles.open : styles.closed
            }`}
            onClick={(e) => e.stopPropagation()}
          >
            <Grid container rowSpacing={2} columnSpacing={{ xs: 1, sm: 2, md: 3 }}>
           
              <Grid item xs={12}>
                <Typography variant="h3">
                  Select Value
                  {/* <Box component="span" ml={1}>
                    <MdHelp id="filter-help-4" />
                    <Tooltip title="configure Data Filter" placement="right">
                      <IconButton size="small">
                        <MdHelp id="filter-help-4" />
                      </IconButton>
                    </Tooltip>
                  </Box> */}
                </Typography>
                <Typography variant="h5">You can filter data by selecting a column and its value. That value will be passed as filter</Typography>
              </Grid>
           
           
              <Grid item xs={12}>
                {filterRows.map((row, index) => (
                  <Grid container spacing={3} key={index}>
                    <Grid item xs={3}>
                      <FormControl fullWidth>
                        <InputLabel>Select Table</InputLabel>
                        <Select
                          value={selectedSlice}
                          onChange={(e) => handleSliceSelection(e.target.value)}
                        >
                          <MenuItem value="">Select Table</MenuItem>
                          {Object.keys(slices).map((sliceName) => (
                            <MenuItem key={sliceName} value={sliceName}>
                              {sliceName}
                            </MenuItem>
                          ))}
                        </Select>
                      </FormControl>
                    </Grid>
                    <Grid item xs={3}>
                      <FormControl fullWidth>
                        <InputLabel>Select column</InputLabel>
                        <Select
                          value={row.selectedColumn}
                          onChange={(e) =>
                            updateFilterRow(
                              index,
                              "selectedColumn",
                              e.target.value
                            )
                          }
                        >
                          <MenuItem value="">Select column</MenuItem>
                          {selectedSlice &&
                            slices[selectedSlice].map((property) => (
                              <MenuItem key={property} value={property}>
                                {property}
                              </MenuItem>
                            ))}
                        </Select>
                      </FormControl>
                    </Grid>
                    <Grid item xs={3}>
                      <FormControl fullWidth>
                        <InputLabel>Select operator</InputLabel>
                        <Select
                          value={row.selectedOperator}
                          onChange={(e) =>
                            updateFilterRow(
                              index,
                              "selectedOperator",
                              e.target.value
                            )
                          }
                        >
                          <MenuItem value=">=">{">="}</MenuItem>
                          <MenuItem value="<=">{"<="}</MenuItem>
                          <MenuItem value="==">{"=="}</MenuItem>
                          <MenuItem value="!=">{"!="}</MenuItem>
                        </Select>
                      </FormControl>
                    </Grid>
                    <Grid item xs={3}>
                      {index > 0 && (
                        <FormControl fullWidth>
                          <InputLabel>Select logical operator</InputLabel>
                          <Select
                            value={row.logicalOperator}
                            onChange={(e) =>
                              updateFilterRow(
                                index,
                                "logicalOperator",
                                e.target.value
                              )
                            }
                          >
                            <MenuItem value="&&">{"&&"}</MenuItem>
                            <MenuItem value="||">{"||"}</MenuItem>
                          </Select>
                        </FormControl>
                      )}
                      <TextField
                        fullWidth
                        label="Enter value"
                        value={row.inputValue}
                        onChange={(e) =>
                          updateFilterRow(index, "inputValue", e.target.value)
                        }
                      />
                    </Grid>
                  </Grid>
                ))}
              </Grid>
           
           
              <Grid item xs={12}>
                <FormControl fullWidth>
                  <InputLabel>
                    Select the column whose value you want to pass.
                  </InputLabel>
                  <Select
                    value={selectedObject}
                    onChange={(e) => setSelectedObject(e.target.value)}
                  >
                    <MenuItem value="">
                    Select the column whose value you want to pass
                    </MenuItem>
                    {selectedSlice &&
                      slices[selectedSlice].map((property) => (
                        <MenuItem key={property} value={property}>
                          {property}
                        </MenuItem>
                      ))}
                  </Select>
                </FormControl>
              </Grid>
           
           
              <Grid item xs={12}>
                <Button onClick={() => handleClose()}>
                  Close
                </Button>
                <Button  onClick={() => handleApplyFilter()}>
                  Apply Filter
                </Button>
              </Grid>
           
           
              <Grid item xs={12}>
                <Typography variant="h4">Selected Table</Typography>
                {renderSelectedData()}
              </Grid>
            </Grid>
           
          </div>
        </div>
      )}
    </>

    // <>
    //   {show && (
    //     <Drawer anchor="right" open={show} onClose={handleClose}>
    //       <Grid
    //         container
    //         spacing={3}
    //         onClick={(e) => e.stopPropagation()}
    //         className={styles.drawerContent}
    //       >
    //         <Grid item xs={12}>
    //           <h3>
    //             Data Source Filter
    //             <small className="p-2">
    //               <MdHelp id="filter-help-4" />
    //             </small>
    //             <ReactTooltip
    //               className="tooltipContent1"
    //               anchorId="filter-help-4"
    //               place="right"
    //               content="configure Data Filter"
    //             />
    //           </h3>
    //         </Grid>
    //         {/* Render the filter rows here */}
    //         {filterRows.map((row, index) => (
    //           <Grid container item xs={12} spacing={3} key={index}>
    //             <Grid item xs={3}>
    //               <FormControl fullWidth>
    //                 <InputLabel>Select Data Source</InputLabel>
    //                 <Select
    //                   value={selectedSlice}
    //                   onChange={(e) => handleSliceSelection(e.target.value)}
    //                 >
    //                   <MenuItem value="">
    //                     <em>None</em>
    //                   </MenuItem>
    //                   {Object.keys(slices).map((sliceName) => (
    //                     <MenuItem value={sliceName} key={sliceName}>
    //                       {sliceName}
    //                     </MenuItem>
    //                   ))}
    //                 </Select>
    //               </FormControl>
    //             </Grid>
    //             <Grid item xs={3}>
    //               <FormControl fullWidth>
    //                 <InputLabel>Select column</InputLabel>
    //                 <Select
    //                   value={row.selectedColumn}
    //                   onChange={(e) =>
    //                     updateFilterRow(index, "selectedColumn", e.target.value)
    //                   }
    //                 >
    //                   <MenuItem value="">
    //                     <em>None</em>
    //                   </MenuItem>
    //                   {selectedSlice &&
    //                     slices[selectedSlice].map((property) => (
    //                       <MenuItem key={property} value={property}>
    //                         {property}
    //                       </MenuItem>
    //                     ))}
    //                 </Select>
    //               </FormControl>
    //             </Grid>
    //             <Grid item xs={3}>
    //               <FormControl fullWidth>
    //                 <InputLabel>Select Operator</InputLabel>
    //                 <Select
    //                   value={row.selectedOperator}
    //                   onChange={(e) =>
    //                     updateFilterRow(
    //                       index,
    //                       "selectedOperator",
    //                       e.target.value
    //                     )
    //                   }
    //                 >
    //                   <MenuItem value=">=">{">="}</MenuItem>
    //                   <MenuItem value="<=">{"<="}</MenuItem>
    //                   <MenuItem value="==">{"=="}</MenuItem>
    //                   <MenuItem value="!=">{"!="}</MenuItem>
    //                 </Select>
    //               </FormControl>
    //             </Grid>
    //             <Grid item xs={3}>
    //               {index > 0 && (
    //                 <FormControl fullWidth>
    //                   <InputLabel>Select Logical Operator</InputLabel>
    //                   <Select
    //                     value={row.logicalOperator}
    //                     onChange={(e) =>
    //                       updateFilterRow(
    //                         index,
    //                         "logicalOperator",
    //                         e.target.value
    //                       )
    //                     }
    //                   >
    //                     <MenuItem value="&&">{"&&"}</MenuItem>
    //                     <MenuItem value="||">{"||"}</MenuItem>
    //                   </Select>
    //                 </FormControl>
    //               )}
    //               <TextField
    //                 type="text"
    //                 placeholder="Enter value"
    //                 value={row.inputValue}
    //                 onChange={(e) =>
    //                   updateFilterRow(index, "inputValue", e.target.value)
    //                 }
    //                 fullWidth
    //               />
    //             </Grid>
    //           </Grid>
    //         ))}
    //         <Grid container spacing={3}>
    //           <Grid item xs={12}>
    //             <FormControl fullWidth>
    //               <InputLabel>
    //                 Select the column which you want to see the value.
    //               </InputLabel>
    //               <Select
    //                 value={selectedObject}
    //                 onChange={(e) => setSelectedObject(e.target.value)}
    //               >
    //                 <MenuItem value="">
    //                   <em>
    //                     Select the column which you want to see the value.
    //                   </em>
    //                 </MenuItem>
    //                 {selectedSlice &&
    //                   slices[selectedSlice].map((property) => (
    //                     <MenuItem key={property} value={property}>
    //                       {property}
    //                     </MenuItem>
    //                   ))}
    //               </Select>
    //             </FormControl>
    //           </Grid>
    //           <Grid item xs={12}>
    //             <Button
    //               variant="contained"
    //               color="secondary"
    //               onClick={handleClose}
    //             >
    //               Close
    //             </Button>
    //             <Button
    //               variant="contained"
    //               color="primary"
    //               onClick={handleApplyFilter}
    //             >
    //               Apply Filter
    //             </Button>
    //           </Grid>
    //         </Grid>
    //         <Grid container spacing={3}>
    //           <Grid item xs={12}>
    //             <Typography variant="h4">Selected Data Source</Typography>
    //             {renderSelectedData()}
    //           </Grid>
    //         </Grid>
    //       </Grid>
    //     </Drawer>
    //   )}
    // </>
  );
};

export default FilterModal;
