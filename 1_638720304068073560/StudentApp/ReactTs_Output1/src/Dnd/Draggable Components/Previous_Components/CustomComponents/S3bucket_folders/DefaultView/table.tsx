import React, { useEffect, useState } from "react";
import { Toast } from 'utils/Toast';
import DataTable from "react-data-table-component";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { IconName } from "@fortawesome/fontawesome-svg-core";
import { library } from '@fortawesome/fontawesome-svg-core';
import { fas } from '@fortawesome/free-solid-svg-icons';
import {
  Box,
  Button,
  Card,
  CardHeader,
  CardContent,
  TextField,
  InputAdornment,
  IconButton,
  Grid,
  Typography,
  Pagination,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
} from "@mui/material";
import {
  CustomGridLayout,
  CustomGridLayoutDynamic,
  CustomGridLayoutDynamicV2,
  CustomGridLayoutDynamicV3,
  DefaultGridTest,
} from "Dnd/Dnd Designer/Components/DefaultGridTest";
import { useSelector } from "react-redux";
import { RootState } from "redux/reducers";
import { useAppDispatch } from "redux/store";
import { resetS3bucket_foldersToInit, setS3bucket_foldersMessage } from "redux/actions";
import { Constant } from "template/Constant";
import ConfirmationModal from "template/ConfirmationModal";
import { deleteS3bucket_folders } from "services/s3bucket_foldersService";
import { setS3bucket_foldersList } from "redux/actions";
import { getS3bucket_folders } from "services/s3bucket_foldersService";
import AddIcon from '@mui/icons-material/Add';
import RefreshIcon from '@mui/icons-material/Refresh';
import { DataGrid, GridToolbar } from "@mui/x-data-grid";
import { makeStyles } from "@mui/styles";





library.add(fas);
const useStyles = makeStyles({
  root: {
    "& .MuiDataGrid-cell": {
      display: "flex",
      alignItems: "center",
    },
    "& .MuiDataGrid-root": {
      color: "#000000", // Black text color
      backgroundColor: "transparent", // or "white" if you prefer white background
    },
    "& .MuiDataGrid-columnsContainer": {
      color: "#000000", // Black text color for header
      backgroundColor: "transparent", // or "white" if you prefer white background
    },
    // Additional styling to ensure consistent colors throughout the grid
    "& .MuiDataGrid-row": {
      color: "#000000",
      backgroundColor: "transparent", // or "white"
      borderBottom: "1px solid rgba(0, 0, 0, 0.2)", // Add underline to each row
    },
    "& .MuiDataGrid-columnHeaders": {
      color: "#000000",
      backgroundColor: "transparent", // or "white"
      borderBottom: "2px solid rgba(0, 0, 0, 0.3)", // Slightly thicker underline for header
    },
    "& .MuiDataGrid-footerContainer": {
      color: "#000000",
      backgroundColor: "transparent", // or "white"
      borderTop: "1px solid rgba(0, 0, 0, 0.2)", // Add line above footer
    }
  },
});
type Props = {
  hideShowForm: (action: string) => void;
  handleRowEdit: (row: any) => void;
  getData: (page: number, pageSize: number, searchKey: string) => void;
  config: any;
  openLink: any;
  columnDefinitions: any[];
};

export const S3bucket_foldersTable: React.FC<Props> = ({
  hideShowForm,
  handleRowEdit,
  getData,
  columnDefinitions,
  config,
  openLink,
}) => {
    const classes = useStyles(config);
  const dispatch = useAppDispatch();
  const [search, setSearch] = useState("");
  const [showDelete, setShowDelete] = useState(false);
  const [rowData, setRowData] = useState<any>(undefined);
  const [page, setPage] = useState(Constant.defaultPageNumber); // Page index for DataGrid starts at 0
  const [pageSize, setPageSize] = useState(Constant.defaultPageSize);
  const [totalRecords, setTotalCount] = useState(0); // To store total number of items
  const rData = useSelector((state: RootState) => state.s3bucket_folders);
  const S3bucket_foldersData = useSelector((state: RootState) => state.s3bucket_folders);
  

  


  useEffect(() => {
    if (S3bucket_foldersData && S3bucket_foldersData.list && S3bucket_foldersData.list.length === 0 && (S3bucket_foldersData.message !== "No Record Found After Filter")) {
      dispatch(resetS3bucket_foldersToInit());
      getS3bucket_folders(Constant.defaultPageNumber, Constant.defaultDropdownPageSize, "").then((response) => {
        if (response && response.records) {
           // Assuming the response structure includes pageNumber, pageSize, totalCount, and records
          setPage(response.pageNumber - 1); // Adjust if your component/page state is 0-based and API is 1-based
          setPageSize(response.pageSize);
          setTotalCount(response.totalRecords);
          dispatch(
            setS3bucket_foldersList({
              pageNo: Constant.defaultPageNumber,
              pageSize: Constant.defaultDropdownPageSize,
              list: response.records,
              totalCount: response.totalRecords,
              searchKey: "",
            })
          );
        } else {
          dispatch(setS3bucket_foldersMessage(`No Record Found For S3bucket_folders`));
        }
      });
    }
  }, [S3bucket_foldersData.list.length]);

 const handleSearch = () => {
    if (search.length > 0) {
      // Reset to the first page and fetch data with the search term
      setPage(Constant.defaultPageNumber); // Reset page number to the first page
      getData(Constant.defaultPageNumber, pageSize, search); // Fetch data starting from the first page
    }
  };

  const handlePageChange = (newPage: number) => {
    setPage(newPage); // Optionally reset to the first page on page size change
   //console.log("page changed", newPage);
    getData(newPage + 1, pageSize, search); // Fetch new data for the page
  };

  // Handle page size change
  const handlePageSizeChange = (newPageSize: number) => {
    setPageSize(newPageSize);
    setPage(Constant.defaultPageNumber); // Optionally reset to the first page on page size change
    getData(Constant.defaultPageNumber, newPageSize, search); // Reset to first page with new page size
  };

  const handleRowDeleteClick = (row: any) => {
    setRowData(row);
    setShowDelete(true);
  };

  useEffect(() => {
    if (rData && rData.list && rData.list.length === 0) {
      dispatch(resetS3bucket_foldersToInit());
      getData(page, pageSize, "");
    }
  }, [rData.list.length]);

  const handleReset = () => {
    setSearch("");
    dispatch(resetS3bucket_foldersToInit());
    getData(page, pageSize, "");
  };

  const handleServerDelete = async () => {
    if (rowData) {
      const response = await deleteS3bucket_folders(rowData.folder_id);
      if (response) {
        dispatch(resetS3bucket_foldersToInit());
        dispatch(setS3bucket_foldersMessage("Deleted Successfully"));
        getData(page, pageSize, "");
        setShowDelete(false);
      } else {
        dispatch(setS3bucket_foldersMessage("Some error occurred!"));
      }
    }
  };

  const handleRowSelection = (row: any) => {
   //console.log(row); // Row Selection Functionality can be written here
  };

  const handleAddButtonClick = () => {
    dispatch(setS3bucket_foldersMessage(''));
    hideShowForm('add');
  };

const columns = [
    ...columnDefinitions,
    {
      field: "edit",
      headerName: "",
      sortable: false,
      width: 100,
      renderCell: (params) => (
        <Button
          variant="contained"
          size="small"
          onClick={() => handleRowEdit(params.row)}
        sx={{
          backgroundColor: 'black',
          color: 'white',
          '&:hover': {
            backgroundColor: '#333333', // slightly lighter black on hover
          }
        }}
        >
          Edit
        </Button>
      ),
            hide:         config["edit_button_visible1"] !== undefined
        ? !config["edit_button_visible1"]
        : false,
    },
    {
      field: "delete",
      headerName: "",
      sortable: false,
      width: 100,
      renderCell: (params) => (
        <Button
          variant="contained"
          color="secondary"
          size="small"
          onClick={() => handleRowDeleteClick(params.row)}
          sx={{
            backgroundColor: 'black',
            color: 'white',
            '&:hover': {
              backgroundColor: '#333333', // slightly lighter black on hover
            }
          }}
        >
          Delete
        </Button>
      ),
            hide:         config["delete_button_visible1"] !== undefined
        ? !config["delete_button_visible1"]
        : false,
    },
  ];
   let forwardButton;
  const handleButtonClick = (params, passValue, type) => {};
  if (config["nav_count"] !== undefined) {
    for (let i = 0; i < config.nav_count; i++) {
      if (config[`nav_${i}_button_name`] === undefined) continue;
      const buttonName = config[`nav_${i}_button_name`];
      const passValue = config[`nav_${i}_pass_value`];
      const column = config[`nav_${i}_column`];
      const type = config[`nav_${i}_type`];
      const buttonColor = config[`nav_${i}_button_color`];
      const textColor = config[`nav_${i}_text_color`];
      const icon = config[`nav_${i}_icon`];

      forwardButton = ({
        buttonName,
        passValue,
        column,
        rowData,
        config,
        openLink,
        i,
      }) => {
        return (
          <Button
            style={{
              whiteSpace: "normal",
              wordWrap: "break-word",
            }}
            sx={{
              backgroundColor: buttonColor ? buttonColor : "primary",
              color: textColor ? textColor : "primary",
            }}
            variant="contained"
            color="primary"
            size="small"
            endIcon={
              icon ? (
                <FontAwesomeIcon
                  icon={["fas", icon as IconName]}
                  color={textColor}
                />
              ) : null
            }
            onClick={() => {
              if (passValue) {
                const condition = {
                  columnName: column,
                  columnCondition: 1,
                  columnValue: rowData[column].toString(),
                };
                openLink(config[`nav_${i}_page`], condition);
              } else {
                openLink(config[`nav_${i}_page`]);
              }
            }}
          >
            {buttonName}
          </Button>
        );
      };
      const newButtonColumn = {
        field: buttonName,
        headerName: "",
        sortable: false,
        // Remove fixed width or make it flexible
        flex: 1, // This allows the column to grow
        minWidth: 120, // Minimum width to prevent too narrow columns
        renderCell: (params) => (
          <Button
	    style={{
              whiteSpace: "normal", // Allows the text to wrap
              wordWrap: "break-word", // Ensures long words are broken and wrapped
              width: '100%', // Takes full width of the cell
              height: 'auto', // Adjusts height based on content
              padding: '8px', // Add some padding
            }}
            sx={{
              backgroundColor: buttonColor ? buttonColor : "primary",
              color: textColor ? textColor : "primary",
              textAlign: 'center',
              '& .MuiButton-endIcon': {
                marginLeft: 'auto', // Properly aligns the icon
              }
            }}
            endIcon={
              icon ? (
                <FontAwesomeIcon
                  icon={["fas", icon as IconName]}
                  color={textColor}
                />
              ) : null
            }
            variant="contained"
            color="primary"
            size="small"
            onClick={() => {
             
              if (passValue) {
                const condition = {
                  columnName: column,
                  columnCondition: 1,
                  columnValue: params.row[column].toString(),
                };
                openLink(config[`nav_${i}_page`], condition);
              } else {
                openLink(config[`nav_${i}_page`]);
              }
            }}
          >
            {buttonName}
          </Button>
        ),
      };

      columns.push(newButtonColumn);
    }
  }
  const filteredColumns = columns; //.filter((column) => column.visible);
  //Experimental Code -----------------------------------------------------
  interface LayoutItem {
    i: string;
    x: number;
    y: number;
    w: number;
    h: number;
    minW: number;
    maxW: number;
    minH: number;
    maxH: number;
  }

  
  // Generate a standard layout for a row
  const generateStandardLayout = () => {
    return filteredColumns.map((col, index) => ({
      i: col.field,
      x: index * 2, // Example positioning, adjust as needed
      y: 0,
      w: 2, // Standard width for all cells
      h: 1, // Standard height for all cells
    }));
  };

  const standardLayout = generateStandardLayout();
  const generateInitialLayout = () => {
    return filteredColumns.map((col, index) => ({
      i: col.field,
      x: index * 2, // Initial positioning
      y: 0,
      w: 2, // Initial width
      h: 1, // Initial height
    }));
  };

  const [layout, setLayout] = useState(generateInitialLayout());

  // Callback function to update layout
  const onLayoutChange = (newLayout) => {
    setLayout(newLayout);
  };
  const pageCount = Math.ceil(totalRecords / pageSize);
  const gridSize = config[`BYOView_Number_of_DbTable_Rows`]
    ? 12 / config[`BYOView_Number_of_DbTable_Rows`]
    : 12;
  /////////////End of Experimental code /////////////////////////////
  return (
   
     <Card className="shadow mb-4">
    <CardHeader
style={{
          backgroundColor:
            config["tableHeadBackgroundColor"] !== undefined
              ? config["tableHeadBackgroundColor"]
              : "white",
          color:
            config["HeadColor"] !== undefined ? config["HeadColor"] : "black",
        }}
         title={
            <>
              {config["heading2_visible1"] && (
                <div>
                  <span>
                    {config["tableHeading"] !== undefined
                      ? config["tableHeading"]
                      : "Users List"}{" "}
                    ({rData.list.length})
                  </span>
                  <IconButton onClick={handleReset}>
                    <RefreshIcon
                      style={{
                        color:
                          config["HeadColor"] !== undefined
                            ? config["HeadColor"]
                            : "black",
                      }}
                    />
                  </IconButton>
                </div>
              )}
            </>
          }
      action={
          <>
            {(config[`add_new_button_visible1`] || config[`add_new_button_visible1`] === undefined) && (
              <Button
                className="btn-icon-split float-right"
                onClick={handleAddButtonClick}
                variant="contained"
               endIcon={<AddIcon sx={{ color: 'white' }} />}
               sx={{
                 backgroundColor: 'black',
                 color: 'white',
                 '&:hover': {
                   backgroundColor: '#333333', // slightly lighter black on hover
                 }
               }}
              >
                {config[`add_new_button_new_name1`] !== undefined
                  ? config[`add_new_button_new_name1`]
                  : "Add New"}
              </Button>
            )}
          </>
        }
    />
    <CardContent>
    <Grid container spacing={2}>
        <Grid item xs={12} md={3}>
              <TextField
                placeholder="Search"
                variant="outlined"
                fullWidth
                value={search}
                onChange={(e) => setSearch(e.target.value)}
      sx={{
        '& .MuiOutlinedInput-root': {
          height: '40px', // Reduced height
          width: '300px', // Increased width for textbox
        }
      }}
                InputProps={{
                  endAdornment: (
                    <InputAdornment position="end">
                      <Button
                        disabled={search.length <= 2}
                        variant="contained"
                        onClick={handleSearch}
              sx={{
                backgroundColor: 'black',
                color: 'white',
                height: '30px', // Reduced button height
                '&:hover': {
                  backgroundColor: '#333333', // slightly lighter black on hover
                },
                '&.Mui-disabled': {
                  backgroundColor: '#666666', // grey when disabled
                  color: '#cccccc'
                }
              }}
                      >
                        Search
                      </Button>
                    </InputAdornment>
                  ),
                }}
              />
        </Grid>
      </Grid>
      <Box mt={2}></Box>
      <Box mt={2}>
          {}

          {}
          {config["BYOViewConfig"] !== undefined ? (
            <>
              <Grid container spacing={2}>
                {rData.list.map((row, rowIndex) => (
                  <Grid item xs={12} sm={gridSize} key={rowIndex}>
                    <CustomGridLayoutDynamicV2
                      rowData={row}
                      layoutItems={config["BYOViewConfig"]} // Assuming this is your layout configuration
                      filteredColumns={filteredColumns}
                      onLayoutChange={onLayoutChange} // Handle layout changes
                      config={config}
                    />
                  </Grid>
                ))}
              </Grid>
              <Box
                display="flex"
                justifyContent="space-between"
                alignItems="center"
                padding="20px"
                pt={10}
              >
                <FormControl variant="outlined" size="small">
                  <InputLabel>Rows per page</InputLabel>
                  <Select
                    label="Rows per page"
                    value={pageSize}
                    onChange={(e) =>
                      handlePageSizeChange(Number(e.target.value))
                    }
                  >
                    {Constant.paginationRowsPerPageOptions.map((option) => (
                      <MenuItem key={option} value={option}>
                        {option}
                      </MenuItem>
                    ))}
                  </Select>
                </FormControl>
                {/* Pagination Component */}
                <Pagination
                  count={pageCount}
                  page={page + 1} // MUI Pagination is 1-based
                  onChange={(event, value) => handlePageChange(value - 1)} // Convert back to 0-based page index
                  color="primary"
                />
              </Box>
            </>
          ) : (
            // Fallback content or component if config["BYOViewConfig"] is undefined
            <div
              style={{ height: 800, width: "100%" }}
              className={classes.root}
            >
              <DataGrid
                rows={rData.list}
                columns={filteredColumns}
                getRowId={(row) => row.folder_id}
                pagination
                paginationMode="server" // Enable server-side pagination
                pageSize={pageSize}
                onPageSizeChange={(newPageSize) =>
                  handlePageSizeChange(newPageSize)
                }
                rowCount={totalRecords} // Total row count from server
                page={page}
                rowsPerPageOptions={Constant.paginationRowsPerPageOptions}
                onPageChange={(newPage) => handlePageChange(newPage)}
                components={{
                  Toolbar: GridToolbar,
                }}
                className={classes.root}
                componentsProps={{
                  toolbar: {
                    sx: {
                      backgroundColor: 'white',
                      borderBottom: '1px solid rgba(0, 0, 0, 0.12)',
                      '& .MuiButton-root': { // Filter buttons
                        color: 'black',
                        '&:hover': {
                          backgroundColor: 'rgba(0, 0, 0, 0.04)'
                        }
                      },
                      '& .MuiFormControl-root': { // Search input
                        '& .MuiInputBase-root': {
                          color: 'black',
                          '& .MuiOutlinedInput-notchedOutline': {
                            borderColor: 'rgba(0, 0, 0, 0.23)'
                          },
                          '&:hover .MuiOutlinedInput-notchedOutline': {
                            borderColor: 'black'
                          }
                        }
                      }
                    }
                  }
                }}
              />
            </div>
          )}
        </Box>
    </CardContent>
    <ConfirmationModal
      buttonNegative="Cancel"
      buttonPositive="Delete"
      title="Delete Confirmation"
      show={showDelete}
      body="Are you sure?"
      onNegative={() => setShowDelete(false)}
      onPositive={handleServerDelete}
    />
  </Card>
  );
};
