import React, { useEffect, useState } from "react";
import shortid from "shortid";
import GridViewIcon from '@mui/icons-material/GridView';
import ApiIcon from '@mui/icons-material/Api';
import GridOnIcon from '@mui/icons-material/GridOn';
import ListIcon from '@mui/icons-material/List';
import CropSquareIcon from '@mui/icons-material/CropSquare';
import FormatColorTextIcon from '@mui/icons-material/FormatColorText';
import BadgeIcon from '@mui/icons-material/Badge';
import EmailIcon from '@mui/icons-material/Email';
import PhoneIcon from '@mui/icons-material/Phone';
import ImageIcon from '@mui/icons-material/Image';
import TitleIcon from '@mui/icons-material/Title';
import AddCardIcon from '@mui/icons-material/AddCard';
import {Card} from 'Dnd/Draggable Components/Previous_Components/CustomComponents/Card/card'
import Heading from 'Dnd/Draggable Components/Previous_Components/CustomComponents/Heading/heading';
import { GetDataUrl } from "services/fileUploadService";
import DataGridlist from "Dnd/Draggable Components/Previous_Components/CustomComponents/GridList/DataGrid"
import NestedList from 'Dnd/Draggable Components/Previous_Components/CustomComponents/List/List';
import CustomButton from "Dnd/Draggable Components/Previous_Components/CustomComponents/Button";
import TabsView from "Dnd/Draggable Components/Previous_Components/CustomComponents/Tabs";
import CustomTextField from 'Dnd/Draggable Components/Previous_Components/CustomComponents/TextField/TextField';
import Grid from 'Dnd/Draggable Components/Previous_Components/SyncFusionComponents/Grid'
import { DropDownList } from "Dnd/Draggable Components/Previous_Components/CustomComponents/DropDownList";
import MenuBar from 'Dnd/Draggable Components/Previous_Components/CustomComponents/MenuBar';
import {Attendance_recordsDropDownList} from "../../Draggable Components/syncfusion_components/DropDownList/attendance_recordsDropDownList";
import {Attendance_recordsGridView} from "../../Draggable Components/syncfusion_components/Grid/attendance_recordsGridView";
import {Attendance_recordsListView} from "../../Draggable Components/syncfusion_components/ListView/attendance_recordsListView";
import {Attendance_recordsAutoComplete} from "Dnd/Draggable Components/syncfusion_components/Autocomplete/attendance_recordsAutoComplete";
import {Attendance_recordsQueryBuilder} from "Dnd/Draggable Components/syncfusion_components/QueryBuilder/attendance_recordsQueryBuilder";
import {Attendance_records} from "Dnd/Draggable Components/Previous_Components/CustomComponents/Attendance_records";
import {EmployeesDropDownList} from "../../Draggable Components/syncfusion_components/DropDownList/employeesDropDownList";
import {EmployeesGridView} from "../../Draggable Components/syncfusion_components/Grid/employeesGridView";
import {EmployeesListView} from "../../Draggable Components/syncfusion_components/ListView/employeesListView";
import {EmployeesAutoComplete} from "Dnd/Draggable Components/syncfusion_components/Autocomplete/employeesAutoComplete";
import {EmployeesQueryBuilder} from "Dnd/Draggable Components/syncfusion_components/QueryBuilder/employeesQueryBuilder";
import {Employees} from "Dnd/Draggable Components/Previous_Components/CustomComponents/Employees";
import {Leave_requestsDropDownList} from "../../Draggable Components/syncfusion_components/DropDownList/leave_requestsDropDownList";
import {Leave_requestsGridView} from "../../Draggable Components/syncfusion_components/Grid/leave_requestsGridView";
import {Leave_requestsListView} from "../../Draggable Components/syncfusion_components/ListView/leave_requestsListView";
import {Leave_requestsAutoComplete} from "Dnd/Draggable Components/syncfusion_components/Autocomplete/leave_requestsAutoComplete";
import {Leave_requestsQueryBuilder} from "Dnd/Draggable Components/syncfusion_components/QueryBuilder/leave_requestsQueryBuilder";
import {Leave_requests} from "Dnd/Draggable Components/Previous_Components/CustomComponents/Leave_requests";
import {S3bucketDropDownList} from "../../Draggable Components/syncfusion_components/DropDownList/s3bucketDropDownList";
import {S3bucketGridView} from "../../Draggable Components/syncfusion_components/Grid/s3bucketGridView";
import {S3bucketListView} from "../../Draggable Components/syncfusion_components/ListView/s3bucketListView";
import {S3bucketAutoComplete} from "Dnd/Draggable Components/syncfusion_components/Autocomplete/s3bucketAutoComplete";
import {S3bucketQueryBuilder} from "Dnd/Draggable Components/syncfusion_components/QueryBuilder/s3bucketQueryBuilder";
import {S3bucket} from "Dnd/Draggable Components/Previous_Components/CustomComponents/S3bucket";
import {S3bucket_foldersDropDownList} from "../../Draggable Components/syncfusion_components/DropDownList/s3bucket_foldersDropDownList";
import {S3bucket_foldersGridView} from "../../Draggable Components/syncfusion_components/Grid/s3bucket_foldersGridView";
import {S3bucket_foldersListView} from "../../Draggable Components/syncfusion_components/ListView/s3bucket_foldersListView";
import {S3bucket_foldersAutoComplete} from "Dnd/Draggable Components/syncfusion_components/Autocomplete/s3bucket_foldersAutoComplete";
import {S3bucket_foldersQueryBuilder} from "Dnd/Draggable Components/syncfusion_components/QueryBuilder/s3bucket_foldersQueryBuilder";
import {S3bucket_folders} from "Dnd/Draggable Components/Previous_Components/CustomComponents/S3bucket_folders";

// import NavBar from '../../Draggable Components/Previous_Components/CustomComponents/Canvas/Navbar';
// import SideBar from '../../Draggable Components/Previous_Components/CustomComponents/Canvas/Sidebar';
import { ChartIndex } from "Dnd/Draggable Components/Previous_Components/CustomComponents/charts/chartIndex";
import QRCode from "react-qr-code";
import Barcode from "react-barcode";


export const SIDEBAR_ITEM = "sidebarItem";
export const ROW = "row";
export const COLUMN = "column";
export const COMPONENT = "component";
export const SIDEBAR_ITEM_CRUD = "sidebarItemCRUD";

export const getColumnNameList = (slices) => {
  return Configurations[slices[0].toUpperCase() + slices.slice(1)]["row"][
    "columns-list"
  ];
};
export const getRefSlice = (slices, columnName) => {
 //console.log(columnName);
  if (slices !== undefined) {
    const temp = Configurations[slices[0].toUpperCase() + slices.slice(1)][
      "columns"
    ]["columns-list"].find((column) => column.name === columnName);
    return temp?.slice;
  }
  return false;
};
export const getGridListData = [
  "1",
  "2",
  "3",
  "4",
  "5",
  "6",
  "7",
  "8",
  "9",
  "10",
  "11",
  "12",
];

export const getNavNameList = () => {
  return ["page redirect", "popup", "slide in drawer", "components Pages"];
};

export const displayControlList = {
  file: ["image", "audio", "video", "pdf"],
  url: [
    "image",
    "audio",
    "video",
    "Object(ex.pdf)",
    "webpages",
    "Embed audio",
    "Embed video",
    "QR code",
    "Barcode",
  ],
  signature: ["image"],
  code: ["QR code", "Barcode"],
};

export const getGridList = (name) => {
  switch (name) {
    case "tableView1":
      return [
        "ID Sortable",
        "ID",
        "Name",
        "Email",
        "SimpleView",
        "SimpleText",
        "Status",
        "Currency",
        "Date Time",
        "Date",
      ];
      break;
    case "tableView2":
      return [
        "Avatar",
        "ID",
        "ID Sortable",
        "Name",
        "Email",
        "SimpleView",
        "SimpleText",
        "Currency",
        "Date Time",
        "Date",
      ];
      break;
    case "tableView3":
      return [
        "ID",
        "Name",
        "Email",
        "SimpleView",
        "SimpleText",
        "Currency",
        "Status",
        "Date Time",
        "Date",
        "Location",
      ];
      break;
    case "tableView4":
      return [
        "ID",
        "Date Time",
        "Name",
        "Email",
        "SimpleView",
        "SimpleText",
        "Date",
        "Status",
        "Currency",
        "",
      ];
      break;
    case "tableView5":
      return [
        "Image",
        "ID",
        "SimpleView",
        "SimpleText",
        "Currency",
        "Status",
        "Date Time",
        "Date",
      ];
      break;
    case "tableView6":
      return [
        "Name",
        "Email",
        "SimpleView",
        "SimpleText",
        "Currency",
        "Status",
        "Date Time",
        "Date",
      ];
      break;
    case "tableView7":
      return [
        "SimpleView",
        "SimpleText",
        "Number-formatted",
        "Date Time",
        "Date",
      ];
      break;
    case "tableView8":
      return [
        "Name",
        "SimpleView",
        "SimpleText",
        "Number-formatted",
        "Date Time",
        "Date",
      ];
      break;
    case "tableView9":
      return [
        "Name",
        "Status",
        "SimpleView",
        "SimpleText",
        "Number-formatted",
        "Date Time",
        "Date",
      ];
      break;
    case "tableView10":
      return [
        "Name",
        "Status",
        "SimpleView",
        "SimpleText",
        "Number-formatted",
        "Date Time",
        "Date",
      ];
      break;
    case "tableView11":
      return [
        "ID",
        "Name",
        "Status",
        "SimpleView",
        "SimpleText",
        "Number-formatted",
        "Date Time",
        "Date",
      ];
      break;
    case "gridView1":
      return [
        "avtar",
        "name",
        "category",
        "cover",
        "publishedAt",
        "readTime",
        "shortDescription",
        "title",
      ];
      break;
    case "gridView2":
      return [
        "avatar",
        "username",
        "cover",
        "profile_url",
        "follower_count",
        "updatedAt",
      ];
      break;
    case "gridView3":
      return ["mimeType", "name", "size", "url"];
      break;
    case "gridView4":
      return ["avatar", "username", "cover", "profile_url", "platform"];
      break;
    case "gridView5":
      return ["avatar", "name", "createdAt", "likes", "media", "message"];
      break;
    case "gridView6":
      return ["avatar", "username", "follower_count", "email"];
      break;

    case "groupList1":
      return ["customerAvatar", "customerName", "description", "createdAt"];
      break;

    case "groupList2":
      return ["username", "email", "profile_url"];
      break;
    case "groupList3":
      return ["title"];
      break;

    case "groupList4":
      return ["value", "type", "message"];
      break;
    case "groupList5":
      return ["image", "name", "sales", "profit", "currency", "conversionRate"];

      break;

    case "groupList6":
      return ["date", "sender", "type", "amount", "currency"];
      break;

    case "groupList7":
      return ["username", "email", "avatar", "date"];
      break;
    case "groupList8":
      return ["avatar", "name", "job"];
      break;
    case "groupList9":
      return ["avatar", "rating", "media_url", "text", "date"];
      break;
    case "groupList10":
      return ["avatar", "subject", "description", "createdAt"];
      break;
    case "groupList11":
      return [
        "campaign_name",
        "campaign_description",
        "start_date",
        "campaign_status",
        "influencer_name",
        "end_date",
      ];
      break;
    case "detailList1":
      return ["impressions", "likes", "comments", "shares"];
      break;
    case "detailList2":
      return [
        "email",
        "username",
        "password",
        "city",
        "address",
        "address1",
        "address2",
      ];
      break;
    case "detailList3":
      return ["profile_url", "username", "platform", "created_by"];
      break;
    case "detailList6":
      return ["media_url", "schedule_date_time", "text"];
      break;
    case "detailList7":
      return [
        "username",
        "campaign_name",
        "campaign_description",
        "start_date",
        "end_date",
      ];
      break;
    case "Calendar":
      return [
        "eventid",
        "title",
        "description",
        "recurrenceFrequency",
        "allDay",
        "start_date",
        "end_date",
        "location",
      ];
      break;
          case "Kanban":
      return [
        "temp_id",
        "TaskName",
        "description",
        "Status",
      ];
      break;
    default:
      return ["null"];
      break;
  }
};

export const getView = [
  "defaultView",
  "Calendar",
  "Kanban",
  "gridView1",
  "gridView2",
  "gridView3",
  "gridView4",
  "gridView5",
  "gridView6",
  "groupList1",
  "groupList2",
  "groupList3",
  "groupList4",
  "groupList5",
  "groupList6",
  "groupList7",
  "groupList8",
  "groupList9",
  "groupList10",
  "groupList11",
  "detailList1",
  "detailList2",
  "detailList3",
  "detailList4",
  "detailList5",
  "detailList6",
  "detailList7",
    "tableView1",
  "tableView2",
  "tableView3",
  "tableView4",
  "tableView6",
  "tableView7",
  "tableView8",
  "tableView9",
  "tableView10",
  "tableView11",
];


export const getViewList = (tableName) => {
  switch (tableName) {
    case "attendance_records":
      return ["defaultView"];
      break;
    case "dnd_ui_versions":
      return ["defaultView"];
      break;
    case "employees":
      return ["defaultView"];
      break;
    case "entities":
      return ["defaultView"];
      break;
    case "leave_requests":
      return ["defaultView"];
      break;
    case "messagequeue":
      return ["defaultView"];
      break;
    case "permissionmatrix":
      return ["defaultView"];
      break;
    case "project_dnd_ui_versions":
      return ["defaultView"];
      break;
    case "roles":
      return ["defaultView"];
      break;
    case "s3bucket":
      return ["defaultView"];
      break;
    case "s3bucket_folders":
      return ["defaultView"];
      break;
    case "users":
      return ["defaultView"];
      break;
    case "workflow":
      return ["defaultView"];
      break;
    case "workflows":
      return ["defaultView"];
      break;
    case "workflows_projects":
      return ["defaultView"];
      break;
    case "workflow_builds":
      return ["defaultView"];
      break;
    case "workflow_deployments":
      return ["defaultView"];
      break;
    case "workflow_runs":
      return ["defaultView"];
      break;
    case "workflow_triggers":
      return ["defaultView"];
      break;
    case "workflow_trigger_conditions":
      return ["defaultView"];
      break;
    default:
      return ["defaultView"];
  }
};





export const getRowElement = (
  inputType: string,
  field: any,
  display_control?: string,
  srcData?: string
) => {
  if (inputType === "file" || inputType === "signature") {
    switch (display_control) {
      case "image":
        return (
          <>
            {srcData ? (
              <img
                src={srcData}
                alt="row content"
                style={{ width: "100%", height: "100%" }}
              />
            ) : (
              <>
                <ImageIcon fontSize="small" />
              </>
            )}
          </>
        );

        break;
      case "audio":
        return (
          <audio controls>
            <source
              src={srcData}
              type="audio/mpeg"
              style={{ width: "100%", height: "100%" }}
            />
            Your browser does not support the audio element.
          </audio>
        );
        break;
      case "video":
        return (
          <video controls>
            <source
              src={srcData}
              type="video/mp4"
              style={{ width: "100%", height: "100%" }}
            />
            Your browser does not support the video tag.
          </video>
        );
        break;
      case "pdf":
        return (
          <iframe src={srcData} style={{ width: "100%", height: "100%" }}>
            Your browser does not support the iframe tag.
          </iframe>
        );
        break;
      default:
        break;
    }
  } else if (inputType === "url") {
    switch (display_control) {
      case "image":
        return (
          <>
            {field ? (
              <img
                src={field}
                alt="row content"
                style={{ width: "100%", height: "100%" }}
              />
            ) : (
              <>
                <ImageIcon fontSize="small" />
              </>
            )}
          </>
        );
        break;
      case "audio":
        return (
          <audio controls>
            <source src={field} type="audio/mpeg" />
            Your browser does not support the audio element.
          </audio>
        );
        break;
      case "video":
        return (
          <video controls>
            <source
              src={field}
              type="video/mp4"
              style={{ width: "100%", height: "100%" }}
            />
            Your browser does not support the video tag.
          </video>
        );
        break;
      case "Object(ex.pdf)":
        return (
          <object
            data={field}
            style={{ width: "100%", height: "100%" }}
          ></object>
        );
        break;
      case "webpages":
        return (
          <iframe
            src={field}
            style={{ width: "100%", height: "100%" }}
          ></iframe>
        );
        break;
      case "Embed audio":
        return (
          <embed
            src={field}
            type="audio/mpeg"
            style={{ width: "100%", height: "100%" }}
          />
        );
        break;
      case "Embed video":
        return (
          <embed
            src={field}
            type="video/mp4"
            style={{ width: "100%", height: "100%" }}
          />
        );
        break;
      case "QR code":
        return (
          <QRCode
            size={256}
            style={{ height: "100%", maxWidth: "100%", width: "100%" }}
            value={field}
            viewBox={`0 0 256 256`}
          />
        );
        break;
      case "Barcode":
        return (
          <div style={{ height: "100%", maxWidth: "100%", width: "100%" }}>
            <Barcode value={field} />
          </div>
        );
        break;
      default:
        break;
    }
  } else if (inputType === "rich text editor") {
    return <div dangerouslySetInnerHTML={{ __html: field }} />;
  } else if (inputType === "url") {
    return (
      <iframe
        src={field}
        title="URL Display"
        width="100%"
        height="600px"
        style={{ border: 0 }}
      >
        Your browser does not support iframes.
      </iframe>
    );
  } else {
    if (display_control === "QR code") {
      return (
        <QRCode
          size={256}
          style={{ height: "100%", maxWidth: "100%", width: "100%" }}
          value={field}
          viewBox={`0 0 256 256`}
        />
      );
    } else if (display_control === "Barcode") {
      return (
        <div style={{ height: "100%", maxWidth: "100%", width: "100%" }}>
          <Barcode value={field} />
        </div>
      );
    } else {
      return field;
    }
  }
};

const dataCache = {};

export function useRowSelector(inputType, field, display_control, bucket_id,bucket_folder) {
  const [srcData, setSrcData] = useState(null);

  useEffect(() => {
    const fetchData = async () => {
      if (dataCache[field]) {
        // If the data for this field exists in the cache, use it directly
        setSrcData(dataCache[field]);
      } else if (inputType === "file" || inputType === "signature") {
        try {
          const formData = new FormData();
          formData.append("Key", field);
          formData.append("BucketId", bucket_id);
          formData.append("folderselected", bucket_folder);
          const response = await GetDataUrl(formData);
          const newData = response.data.document;
          // Cache the fetched data for future use
          dataCache[field] = newData;
          setSrcData(newData);
        } catch {
         //console.log("Bucket Id or bucket Name may not selected");
        }
      }
    };

    fetchData();
  }, [inputType, field, bucket_id]);

  return srcData;
}


export const ErrorControlList = {
  email: "email",
  text: "text",
  url: "url",
  number: { min: "minimum 8 digit is required", max: "maximum limit 20 digit" },
  password: {
    min: "minimum length 8 is required",
    max: "maximum limit 20 character",
    upperCase: "Must contain 1 uppercase letter",
    lowerCase: "Must contain 1 lowercase letter",
  },
  code: "Only QR code, barcode, or URL",
};

export const ValidationControl = (value, m1, m2) => {
  switch (m1) {
    case "password":
      switch (m2) {
        case "max":
          var regexp = /^.{,20}$/i;
          let maxvalid = regexp.test(value);
          return maxvalid
            ? {
                isValid: true,
              }
            : {
                isValid: false,
                errorMessage: ErrorControlList["password"]["max"],
              };
          break;
        case "min":
          var regexp = /^.{8,}$/i;
          let minvalid = regexp.test(value);
          return minvalid
            ? {
                isValid: true,
              }
            : {
                isValid: false,
                errorMessage: ErrorControlList["password"]["min"],
              };
          break;
        case "upperCase":
          var regexp = /^(?=.*[A-Z])/;
          let upvalid = regexp.test(value);
          return upvalid
            ? {
                isValid: true,
              }
            : {
                isValid: false,
                errorMessage: ErrorControlList["password"]["upperCase"],
              };

          break;
        case "lowerCase":
          var regexp = /^(?=.*[a-z])/;
          let lowvalid = regexp.test(value);
          return lowvalid
            ? {
                isValid: true,
              }
            : {
                isValid: false,
                errorMessage: ErrorControlList["password"]["min"],
              };
          break;
        default:
          return {
            isValid: true,
          };
          break;
      }
      break;
    case "number":
      switch (m2) {
        case "max":
          var regexp = /^.{,20}$/i;
          let maxvalid = regexp.test(value);
          return maxvalid
            ? {
                isValid: true,
              }
            : {
                isValid: false,
                errorMessage: ErrorControlList["number"]["max"],
              };
          break;
        case "min":
          var regexp = /^.{8,}$/i;
          let minvalid = regexp.test(value);
          return minvalid
            ? {
                isValid: true,
              }
            : {
                isValid: false,
                errorMessage: ErrorControlList["number"]["min"],
              };
          break;
        default:
          return {
            isValid: true,
          };
          break;
      }
      break;
    case "email":
      var regexp = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/g;
      let emvalid = regexp.test(value);
      return emvalid
        ? {
            isValid: true,
          }
        : {
            isValid: false,
            errorMessage: m2,
          };
      break;
    case "text":
      var regexp = /^[^\s]+$/;
      let maxvalid = regexp.test(value);
      return maxvalid
        ? {
            isValid: true,
          }
        : {
            isValid: false,
            errorMessage: m2,
          };
      break;
    case "url":
      var urlRegexp = new RegExp(
        "^" +
          // protocol identifier (optional)
          "(?:(?:https?|ftp)://)" +
          // user:pass authentication (optional)
          "(?:\\S+(?::\\S*)?@)?" +
          "(?:" +
          // IP address (simple regex, not exact)
          "\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}|" +
          // host & domain names, may end with dot
          // can contain dashes, dots, percent, and unicode characters
          "(?:[\\p{L}\\p{N}][-\\p{L}\\p{N}\\.]*(?:[\\p{L}\\p{N}]|\\p{L})\\.?|" +
          // or localhost without domain
          "localhost" +
          ")" +
          // port number (optional)
          "(?::\\d{2,5})?" +
          // resource path (optional)
          "(?:[/?#]\\S*)?" +
          "$",
        "iu"
      );
      let urlvalid = urlRegexp.test(value);
      return urlvalid
        ? {
            isValid: true,
          }
        : {
            isValid: false,
            errorMessage: m2,
          };
      break;
    case "code" :
      let regexpcode = /^[a-zA-Z0-9:/._-]+$/;
      let codeValid = regexpcode.test(value);
          return codeValid ?
          {
            isValid : true,
          } :
          {
            isValid : false,
            errorMessage : ErrorControlList["code"],
          }
    default:
      return {
        isValid: true,
      };
      break;
  }
};

export interface ISidebar_Items {
    id: string;
    type: string;
    component: {
        type: string;
        content: JSX.Element | string | React.FC | any;
        icon: JSX.Element;
        component_name?: string;
        icon_name?: string;
    },
}

export const getList = (params: string, fkey?: Boolean) => {
  if (fkey) {
    return ["radio", "dropdown"];
  }
  if (
    params === "int" ||
    params === "decimal" ||
    params === "bigint" ||
    params === "double" ||
    params === "smallint" ||
    params === "tinyint" ||
    params === "enum" ||
    params === "mediumint" ||
    params === "float" ||
    params === "binary" ||
    params === "varbinary" ||
    params === "boolean"
  )
    return ["text"];
  if (
    params === "date" ||
    params === "timestamp" ||
    params === "datetime" ||
    params === "time" ||
    params === "year"
  )
    return ["date", "datetime"];
  if (
    params === "varchar" ||
    params === "text" ||
    params === "char" ||
    params === "blob" ||
    params === "tinyblob" ||
    params === "mediumblob" ||
    params === "longblob" ||
    params === "set"
  )
    return ["text", "file", "rich text editor", "url","signature","code"];
};
export const getType = (params: string, fkey?: boolean): string => {
  if (fkey) {
    //return "dropdown";
    return "text";
  }
  if (
    params === "int" ||
    params === "decimal" ||
    params === "bigint" ||
    params === "double" ||
    params === "smallint" ||
    params === "tinyint" ||
    params === "enum" ||
    params === "mediumint" ||
    params === "float" ||
    params === "binary" ||
    params === "varbinary" ||
    params === "boolean" ||
    params === "varchar" ||
    params === "text" ||
    params === "char" ||
    params === "blob" ||
    params === "tinyblob" ||
    params === "mediumblob" ||
    params === "longblob" ||
    params === "set"
  )
    return "text";
  if (
    params === "date" 
    
  )
    return "date";
  if (
    params === "timestamp" ||
    params === "datetime" ||
    params === "time" ||
    params === "year"
  )
    return "datetime";

  // If none of the conditions match, you may want to handle this case accordingly.
  return ""; // or throw an error, depending on your use case
};

export const SIDEBAR_ITEMS: ISidebar_Items[] = [
	{
        id: shortid.generate(),
        type: SIDEBAR_ITEM,
        component: {
            type: "Heading",
            content: <Heading />,
            icon: < TitleIcon className='dnd sidebarIcon' />,
            component_name: "Heading",
            icon_name: "TitleIcon",
        },
    },
       /*{
        id: shortid.generate(),
        type: SIDEBAR_ITEM,
        component: {
            type: "Attendance_recordsDropDownList",
            content: Attendance_recordsDropDownList,
            icon: < ApiIcon className = 'dnd sidebarIcon' />,
            component_name: "attendance_recordsDropDownList",
            icon_name: "ApiIcon",
        },
    },*/
	/*{
        id: shortid.generate(),
        type: SIDEBAR_ITEM,
        component: {
            type: "Attendance_recordsGridView",
            content: Attendance_recordsGridView,
            icon: < ApiIcon className = 'dnd sidebarIcon' />,
            component_name: "attendance_recordsGridView",
            icon_name: "ApiIcon",
        },
    },*/
	/*{
        id: shortid.generate(),
        type: SIDEBAR_ITEM,
        component: {
            type: "Attendance_recordsListView",
            content: Attendance_recordsListView,
            icon: < ApiIcon className = 'dnd sidebarIcon' />,
            component_name: "attendance_recordsListView",
            icon_name: "ApiIcon",
        },
    },*/
	/*{
        id: shortid.generate(),
        type: SIDEBAR_ITEM,
        component: {
            type: "Attendance_recordsAutoComplete",
            content: Attendance_recordsAutoComplete,
            icon: < ApiIcon className = 'dnd sidebarIcon' />,
            component_name: "attendance_recordsAutoComplete",
            icon_name: "ApiIcon",
        },
    },*/
	/*{
        id: shortid.generate(),
        type: SIDEBAR_ITEM,
        component: {
            type: "Attendance_recordsQueryBuilder",
            content: Attendance_recordsQueryBuilder,
            icon: < ApiIcon className = 'dnd sidebarIcon' />,
            component_name: "attendance_recordsQueryBuilder",
            icon_name: "ApiIcon",
        },
    },*/
	{
        id: shortid.generate(),
        type: SIDEBAR_ITEM,
        component: {
            type: "Attendance_records",
            content: Attendance_records,
            icon: < ApiIcon className = 'dnd sidebarIcon' />,
            component_name: "attendance_records",
            icon_name: "ApiIcon",
        },
    },
	/*{
        id: shortid.generate(),
        type: SIDEBAR_ITEM,
        component: {
            type: "EmployeesDropDownList",
            content: EmployeesDropDownList,
            icon: < ApiIcon className = 'dnd sidebarIcon' />,
            component_name: "employeesDropDownList",
            icon_name: "ApiIcon",
        },
    },*/
	/*{
        id: shortid.generate(),
        type: SIDEBAR_ITEM,
        component: {
            type: "EmployeesGridView",
            content: EmployeesGridView,
            icon: < ApiIcon className = 'dnd sidebarIcon' />,
            component_name: "employeesGridView",
            icon_name: "ApiIcon",
        },
    },*/
	/*{
        id: shortid.generate(),
        type: SIDEBAR_ITEM,
        component: {
            type: "EmployeesListView",
            content: EmployeesListView,
            icon: < ApiIcon className = 'dnd sidebarIcon' />,
            component_name: "employeesListView",
            icon_name: "ApiIcon",
        },
    },*/
	/*{
        id: shortid.generate(),
        type: SIDEBAR_ITEM,
        component: {
            type: "EmployeesAutoComplete",
            content: EmployeesAutoComplete,
            icon: < ApiIcon className = 'dnd sidebarIcon' />,
            component_name: "employeesAutoComplete",
            icon_name: "ApiIcon",
        },
    },*/
	/*{
        id: shortid.generate(),
        type: SIDEBAR_ITEM,
        component: {
            type: "EmployeesQueryBuilder",
            content: EmployeesQueryBuilder,
            icon: < ApiIcon className = 'dnd sidebarIcon' />,
            component_name: "employeesQueryBuilder",
            icon_name: "ApiIcon",
        },
    },*/
	{
        id: shortid.generate(),
        type: SIDEBAR_ITEM,
        component: {
            type: "Employees",
            content: Employees,
            icon: < ApiIcon className = 'dnd sidebarIcon' />,
            component_name: "employees",
            icon_name: "ApiIcon",
        },
    },
	/*{
        id: shortid.generate(),
        type: SIDEBAR_ITEM,
        component: {
            type: "Leave_requestsDropDownList",
            content: Leave_requestsDropDownList,
            icon: < ApiIcon className = 'dnd sidebarIcon' />,
            component_name: "leave_requestsDropDownList",
            icon_name: "ApiIcon",
        },
    },*/
	/*{
        id: shortid.generate(),
        type: SIDEBAR_ITEM,
        component: {
            type: "Leave_requestsGridView",
            content: Leave_requestsGridView,
            icon: < ApiIcon className = 'dnd sidebarIcon' />,
            component_name: "leave_requestsGridView",
            icon_name: "ApiIcon",
        },
    },*/
	/*{
        id: shortid.generate(),
        type: SIDEBAR_ITEM,
        component: {
            type: "Leave_requestsListView",
            content: Leave_requestsListView,
            icon: < ApiIcon className = 'dnd sidebarIcon' />,
            component_name: "leave_requestsListView",
            icon_name: "ApiIcon",
        },
    },*/
	/*{
        id: shortid.generate(),
        type: SIDEBAR_ITEM,
        component: {
            type: "Leave_requestsAutoComplete",
            content: Leave_requestsAutoComplete,
            icon: < ApiIcon className = 'dnd sidebarIcon' />,
            component_name: "leave_requestsAutoComplete",
            icon_name: "ApiIcon",
        },
    },*/
	/*{
        id: shortid.generate(),
        type: SIDEBAR_ITEM,
        component: {
            type: "Leave_requestsQueryBuilder",
            content: Leave_requestsQueryBuilder,
            icon: < ApiIcon className = 'dnd sidebarIcon' />,
            component_name: "leave_requestsQueryBuilder",
            icon_name: "ApiIcon",
        },
    },*/
	{
        id: shortid.generate(),
        type: SIDEBAR_ITEM,
        component: {
            type: "Leave_requests",
            content: Leave_requests,
            icon: < ApiIcon className = 'dnd sidebarIcon' />,
            component_name: "leave_requests",
            icon_name: "ApiIcon",
        },
    },
	/*{
        id: shortid.generate(),
        type: SIDEBAR_ITEM,
        component: {
            type: "S3bucketDropDownList",
            content: S3bucketDropDownList,
            icon: < ApiIcon className = 'dnd sidebarIcon' />,
            component_name: "s3bucketDropDownList",
            icon_name: "ApiIcon",
        },
    },*/
	/*{
        id: shortid.generate(),
        type: SIDEBAR_ITEM,
        component: {
            type: "S3bucketGridView",
            content: S3bucketGridView,
            icon: < ApiIcon className = 'dnd sidebarIcon' />,
            component_name: "s3bucketGridView",
            icon_name: "ApiIcon",
        },
    },*/
	/*{
        id: shortid.generate(),
        type: SIDEBAR_ITEM,
        component: {
            type: "S3bucketListView",
            content: S3bucketListView,
            icon: < ApiIcon className = 'dnd sidebarIcon' />,
            component_name: "s3bucketListView",
            icon_name: "ApiIcon",
        },
    },*/
	/*{
        id: shortid.generate(),
        type: SIDEBAR_ITEM,
        component: {
            type: "S3bucketAutoComplete",
            content: S3bucketAutoComplete,
            icon: < ApiIcon className = 'dnd sidebarIcon' />,
            component_name: "s3bucketAutoComplete",
            icon_name: "ApiIcon",
        },
    },*/
	/*{
        id: shortid.generate(),
        type: SIDEBAR_ITEM,
        component: {
            type: "S3bucketQueryBuilder",
            content: S3bucketQueryBuilder,
            icon: < ApiIcon className = 'dnd sidebarIcon' />,
            component_name: "s3bucketQueryBuilder",
            icon_name: "ApiIcon",
        },
    },*/
	{
        id: shortid.generate(),
        type: SIDEBAR_ITEM,
        component: {
            type: "S3bucket",
            content: S3bucket,
            icon: < ApiIcon className = 'dnd sidebarIcon' />,
            component_name: "s3bucket",
            icon_name: "ApiIcon",
        },
    },
	/*{
        id: shortid.generate(),
        type: SIDEBAR_ITEM,
        component: {
            type: "S3bucket_foldersDropDownList",
            content: S3bucket_foldersDropDownList,
            icon: < ApiIcon className = 'dnd sidebarIcon' />,
            component_name: "s3bucket_foldersDropDownList",
            icon_name: "ApiIcon",
        },
    },*/
	/*{
        id: shortid.generate(),
        type: SIDEBAR_ITEM,
        component: {
            type: "S3bucket_foldersGridView",
            content: S3bucket_foldersGridView,
            icon: < ApiIcon className = 'dnd sidebarIcon' />,
            component_name: "s3bucket_foldersGridView",
            icon_name: "ApiIcon",
        },
    },*/
	/*{
        id: shortid.generate(),
        type: SIDEBAR_ITEM,
        component: {
            type: "S3bucket_foldersListView",
            content: S3bucket_foldersListView,
            icon: < ApiIcon className = 'dnd sidebarIcon' />,
            component_name: "s3bucket_foldersListView",
            icon_name: "ApiIcon",
        },
    },*/
	/*{
        id: shortid.generate(),
        type: SIDEBAR_ITEM,
        component: {
            type: "S3bucket_foldersAutoComplete",
            content: S3bucket_foldersAutoComplete,
            icon: < ApiIcon className = 'dnd sidebarIcon' />,
            component_name: "s3bucket_foldersAutoComplete",
            icon_name: "ApiIcon",
        },
    },*/
	/*{
        id: shortid.generate(),
        type: SIDEBAR_ITEM,
        component: {
            type: "S3bucket_foldersQueryBuilder",
            content: S3bucket_foldersQueryBuilder,
            icon: < ApiIcon className = 'dnd sidebarIcon' />,
            component_name: "s3bucket_foldersQueryBuilder",
            icon_name: "ApiIcon",
        },
    },*/
	{
        id: shortid.generate(),
        type: SIDEBAR_ITEM,
        component: {
            type: "S3bucket_folders",
            content: S3bucket_folders,
            icon: < ApiIcon className = 'dnd sidebarIcon' />,
            component_name: "s3bucket_folders",
            icon_name: "ApiIcon",
        },
    },
	
    /*{
        id: shortid.generate(),
        type: SIDEBAR_ITEM,
        component: {
            type: "Card",
            content: <Card />,
            icon: < AddCardIcon className='dnd sidebarIcon' />,
            component_name: "Card",
            icon_name: "AddCardIcon",
        },
    },
    {
        id: shortid.generate(),
        type: SIDEBAR_ITEM,
        component: {
            type: "SyncFusion GRID",
            content: Grid,
            icon: < GridViewIcon className='dnd sidebarIcon' />,
            component_name: "Grid",
            icon_name: "GridViewIcon",
        },
    },
    {
        id: shortid.generate(),
        type: SIDEBAR_ITEM,
        component: {
            type: "GridList",
            content: < DataGridlist />,
            icon: < GridOnIcon className='dnd sidebarIcon' />,
            component_name: "DataGridlist",
            icon_name: "GridOnIcon",
        },
    },
    {
        id: shortid.generate(),
        type: SIDEBAR_ITEM,
        component: {
            type: "List",
            content: <DropDownList />,
            icon: < ListIcon className='dnd sidebarIcon' />,
            component_name: "NestedList",
            icon_name: "ListIcon",
        },
    },*/
    {
    id: shortid.generate(),
    type: SIDEBAR_ITEM,
    component: {
      type: "BarChart",
      content: ChartIndex,
      icon: <CropSquareIcon className="dnd sidebarIcon" />,
      component_name: "barchart",
      icon_name: "CropSquareIcon",
    },
  },
    {
        id: shortid.generate(),
        type: SIDEBAR_ITEM,
        component: {
            type: "Button",
            content: < CustomButton />,
            icon: < CropSquareIcon className='dnd sidebarIcon' />,
            component_name: "CustomButton",
            icon_name: "CropSquareIcon",
        },
    },
  {
    id: shortid.generate(),
    type: SIDEBAR_ITEM,
    component: {
      type: "Tabs",
      content: <TabsView />,
      icon: <CropSquareIcon className="dnd sidebarIcon" />,
      component_name: "Tabs",
      icon_name: "CropSquareIcon",
    },
  },
   /* {
        id: shortid.generate(),
        type: SIDEBAR_ITEM,
        component: {
            type: "input",
            content: <CustomTextField />,
            icon: < FormatColorTextIcon className='dnd sidebarIcon' />,
            component_name: "CustomTextField",
            icon_name: "FormatColorTextIcon",
        },
    },
    {
        id: shortid.generate(),
        type: SIDEBAR_ITEM,
        component: {
            type: "MenuBar",
            content: <MenuBar />,
            icon: < FormatColorTextIcon className='dnd sidebarIcon' />,
            component_name: "MenuBar",
            icon_name: "FormatColorTextIcon",
        },
    },
    {
        id: shortid.generate(),
        type: SIDEBAR_ITEM,
        component: {
            type: "name",
            content: "Some name",
            icon: < BadgeIcon className='dnd sidebarIcon' />,
            icon_name: "BadgeIcon",
        }
    },
    {
        id: shortid.generate(),
        type: SIDEBAR_ITEM,
        component: {
            type: "email",
            content: "Some email",
            icon: < EmailIcon className='dnd sidebarIcon' />,
            icon_name: "EmailIcon",
        }
    },
    {
        id: shortid.generate(),
        type: SIDEBAR_ITEM,
        component: {
            type: "phone",
            content: "Some phone",
            icon: < PhoneIcon className='dnd sidebarIcon' />,
            icon_name: "PhoneIcon",
        }
    },
    {
        id: shortid.generate(),
        type: SIDEBAR_ITEM,
        component: {
            type: "image",
            content: "Some image",
            icon: < ImageIcon className='dnd sidebarIcon' />,
            icon_name: "ImageIcon",
        }
    }*/
];

export const functionTOmap = (
   component,
  config,
  openLink?,
  id?,
  handleConfigurationChange?,
  openTabLink?
) => {
    ////console.log("component from functionToMap :", component)
    switch (component) {
		case "Heading":
            return <Heading config={config} />;
            break;
        case "Card":
            return <Card config={config} />
            break;
        case "SyncFusion GRID":
            return <Grid />
            break;
        /*case "Attendance_recordsDropDownList":
            return < Attendance_recordsDropDownList config={config} />
            break;*/ 
	/*case "Attendance_recordsGridView":
            return < Attendance_recordsGridView config={config} />
            break; */
	/*case "Attendance_recordsListView":
            return < Attendance_recordsListView config={config} />
            break; */
	/*case "Attendance_recordsAutoComplete":
            return < Attendance_recordsAutoComplete config={config} />
            break; */
	/*case "Attendance_recordsQueryBuilder":
            return < Attendance_recordsQueryBuilder config={config} />
            break;*/ 
	case "Attendance_records":
            return < Attendance_records config={config} openLink={openLink}
          id={id}
          handleConfigurationChange={handleConfigurationChange}  />
            break; 
	/*case "EmployeesDropDownList":
            return < EmployeesDropDownList config={config} />
            break;*/ 
	/*case "EmployeesGridView":
            return < EmployeesGridView config={config} />
            break; */
	/*case "EmployeesListView":
            return < EmployeesListView config={config} />
            break; */
	/*case "EmployeesAutoComplete":
            return < EmployeesAutoComplete config={config} />
            break; */
	/*case "EmployeesQueryBuilder":
            return < EmployeesQueryBuilder config={config} />
            break;*/ 
	case "Employees":
            return < Employees config={config} openLink={openLink}
          id={id}
          handleConfigurationChange={handleConfigurationChange}  />
            break; 
	/*case "Leave_requestsDropDownList":
            return < Leave_requestsDropDownList config={config} />
            break;*/ 
	/*case "Leave_requestsGridView":
            return < Leave_requestsGridView config={config} />
            break; */
	/*case "Leave_requestsListView":
            return < Leave_requestsListView config={config} />
            break; */
	/*case "Leave_requestsAutoComplete":
            return < Leave_requestsAutoComplete config={config} />
            break; */
	/*case "Leave_requestsQueryBuilder":
            return < Leave_requestsQueryBuilder config={config} />
            break;*/ 
	case "Leave_requests":
            return < Leave_requests config={config} openLink={openLink}
          id={id}
          handleConfigurationChange={handleConfigurationChange}  />
            break; 
	/*case "S3bucketDropDownList":
            return < S3bucketDropDownList config={config} />
            break;*/ 
	/*case "S3bucketGridView":
            return < S3bucketGridView config={config} />
            break; */
	/*case "S3bucketListView":
            return < S3bucketListView config={config} />
            break; */
	/*case "S3bucketAutoComplete":
            return < S3bucketAutoComplete config={config} />
            break; */
	/*case "S3bucketQueryBuilder":
            return < S3bucketQueryBuilder config={config} />
            break;*/ 
	case "S3bucket":
            return < S3bucket config={config} openLink={openLink}
          id={id}
          handleConfigurationChange={handleConfigurationChange}  />
            break; 
	/*case "S3bucket_foldersDropDownList":
            return < S3bucket_foldersDropDownList config={config} />
            break;*/ 
	/*case "S3bucket_foldersGridView":
            return < S3bucket_foldersGridView config={config} />
            break; */
	/*case "S3bucket_foldersListView":
            return < S3bucket_foldersListView config={config} />
            break; */
	/*case "S3bucket_foldersAutoComplete":
            return < S3bucket_foldersAutoComplete config={config} />
            break; */
	/*case "S3bucket_foldersQueryBuilder":
            return < S3bucket_foldersQueryBuilder config={config} />
            break;*/ 
	case "S3bucket_folders":
            return < S3bucket_folders config={config} openLink={openLink}
          id={id}
          handleConfigurationChange={handleConfigurationChange}  />
            break; 
	
     case "BarChart":
        return (
            <ChartIndex
                config={config}
                openLink={openLink}
                id={id}
                handleConfigurationChange={handleConfigurationChange}
            />
        );
      break;
    case "Button":
      return (
        <CustomButton
          config={config}
          openLink={openLink}
          id={id}
          handleConfigurationChange={handleConfigurationChange}
        />
      );
      break;
    case "Tabs":
      return (
        <TabsView
          config={config}
          openLink={openLink}
          id={id}
          handleConfigurationChange={handleConfigurationChange}
          openTabLink={openTabLink}
        />
      );
      break;
        case "input":
            return <CustomTextField config={config} />
            break;
        case "MenuBar":
            return <MenuBar />
            break;
        case "GridList":
            return <DataGridlist />
            break;
        case "List":
            return <DropDownList />
            // return <NestedList config={config} />
            break;
        case "name":
            return <h3>Some Name</h3>
            break;
        case "email":
            return "Some email"
            break;
        case "phone":
            return "Some phone"
            break;
        case "image":
            return "Some image"
            break;
        default:
            return <p>Cant't find any Component</p>
    }
}
export const mapNametoComponent = {
    "Grid": Grid,
    "Attendance_recordsDropDownList": Attendance_recordsDropDownList,
	"Attendance_recordsGridView": Attendance_recordsGridView,
	"Attendance_recordsListView": Attendance_recordsListView,
	"Attendance_recordsAutoComplete": Attendance_recordsAutoComplete,
	"Attendance_recordsQueryBuilder": Attendance_recordsQueryBuilder,
	"Attendance_records": Attendance_records,
	"EmployeesDropDownList": EmployeesDropDownList,
	"EmployeesGridView": EmployeesGridView,
	"EmployeesListView": EmployeesListView,
	"EmployeesAutoComplete": EmployeesAutoComplete,
	"EmployeesQueryBuilder": EmployeesQueryBuilder,
	"Employees": Employees,
	"Leave_requestsDropDownList": Leave_requestsDropDownList,
	"Leave_requestsGridView": Leave_requestsGridView,
	"Leave_requestsListView": Leave_requestsListView,
	"Leave_requestsAutoComplete": Leave_requestsAutoComplete,
	"Leave_requestsQueryBuilder": Leave_requestsQueryBuilder,
	"Leave_requests": Leave_requests,
	"S3bucketDropDownList": S3bucketDropDownList,
	"S3bucketGridView": S3bucketGridView,
	"S3bucketListView": S3bucketListView,
	"S3bucketAutoComplete": S3bucketAutoComplete,
	"S3bucketQueryBuilder": S3bucketQueryBuilder,
	"S3bucket": S3bucket,
	"S3bucket_foldersDropDownList": S3bucket_foldersDropDownList,
	"S3bucket_foldersGridView": S3bucket_foldersGridView,
	"S3bucket_foldersListView": S3bucket_foldersListView,
	"S3bucket_foldersAutoComplete": S3bucket_foldersAutoComplete,
	"S3bucket_foldersQueryBuilder": S3bucket_foldersQueryBuilder,
	"S3bucket_folders": S3bucket_folders,

    "DataGridlist": <DataGridlist />,
    "NestedList": <NestedList />,
    "CustomButton": <CustomButton />,
    "TabsView": <TabsView />,
    "CustomTextFields": <CustomTextField />,
    "Some name": "Some name",
    "Some email": "Some email",
    "Some phone": "Some phone",
    "Some image": "Some image"
}

export const SyncFusion_Component_List = ["SyncFusion GRID"];
export const MUI_Component_List = ["DataGridlist", "NestedList", "CustomButton", "CustomTextFields"];

export const Configurations = {
BarChart : {
    Heading : {
      "input-type" : "text",
    },
    content:{
      "input-type" : "chart",
    }
  },
    Tabs: {
    Heading: {
      "input-type": "text",
    },
    navConfig: {
      "input-type": "tab-nav",
      "nav-list": getNavNameList(),
    },
  },
  Button: {
    navConfig: {
      "input-type": "button-nav",
      "nav-list": getNavNameList(),
    },
  },
    "input": {
        "innerContent": {
            "input-type": "text"
        },
    },
    Heading: {
    innerContent: {
      "input-type": "text",
    },
    headerSize: {
      "input-type": "list",
      options: ["h1", "h2", "h3", "h4", "h5", "h6"],
    },
    backgroundColor: {
      "input-type": "heading-color",
      options: [
        "black",
        "gray",
        "white",
        "Tomato",
        "DodgerBlue",
        "MediumSeaGreen",
        "LightGray",
      ],
    },
    color: {
      "input-type": "heading-color",
      options: ["black", "white", "gray"],
    },
    fontFamily: {
      "input-type": "list",
      options: [
        "Arial",
        "Helvetica",
        "Verdana",
        "Georgia",
        "Courier New",
        "cursive",
      ],
    },
  },
     GlobalConfig: {
innerContent: {
      "input-type": "text",
    },
    backgroundColor: {
      "input-type": "global-configuration",
      options: [
        "black",
        "gray",
        "white",
        "Tomato",
        "DodgerBlue",
        "MediumSeaGreen",
        "LightGray",
      ],
    },
    SidebarBackgroundColor: {
      "input-type": "global-configuration",
      options: [
        "black",
        "gray",
        "white",
        "Tomato",
        "DodgerBlue",
        "MediumSeaGreen",
        "LightGray",
      ],
    },
    SidebarMenuTextColor: {
      "input-type": "global-configuration",
      options: [
        "black",
        "gray",
        "white",
        "Tomato",
        "DodgerBlue",
        "MediumSeaGreen",
        "LightGray",
      ],
    },
    SidebarMenuTextBackgroundColor: {
      "input-type": "global-configuration",
      options: [
        "black",
        "gray",
        "white",
        "Tomato",
        "DodgerBlue",
        "MediumSeaGreen",
        "LightGray",
      ],
    },
    SidebarSubMenuTextColor: {
      "input-type": "global-configuration",
      options: [
        "black",
        "gray",
        "white",
        "Tomato",
        "DodgerBlue",
        "MediumSeaGreen",
        "LightGray",
      ],
    },
    SidebarSubMenuTextBackgroundColor: {
      "input-type": "global-configuration",
      options: [
        "black",
        "gray",
        "white",
        "Tomato",
        "DodgerBlue",
        "MediumSeaGreen",
        "LightGray",
      ],
    },
  },

    "List": {
        "primaryKeyList": {
            "input-type": "list",
            "options": ["backend_stack_id", "backend_stack_name", "createdBy", "modifiedBy", "createdAt", "modifiedAt", "isActive"]
        },
        "secondaryKeyList": {
            "input-type": "list",
            "options": ["backend_stack_id", "backend_stack_name", "createdBy", "modifiedBy", "createdAt", "modifiedAt", "isActive"]
        },
        "functionName": {
            "input-type": "list",
            "options": ["getAllBackend_Stacks", "getOneBackend_Stacks", "searchBackend_Stacks", "addBackend_Stacks", "updateBackend_Stacks", "deleteBackend_Stacks"]
        },
    },
    "SyncFusion GRID": {
        "primaryKeyList": {
            "input-type": "list",
            "options": ["backend_stack_id", "backend_stack_name", "createdBy", "modifiedBy", "createdAt", "modifiedAt", "isActive"]
        },
        "modelName": {
            "input-type": "list",
            "options": ["", "Backend_Stacks"]
        },
        "tableName": {
            "input-type": "list",
            "options": ["", "backend_stacks"]
        },
        "functionName": {
            "input-type": "list",
            "options": ["getAllBackend_Stacks", "getOneBackend_Stacks", "searchBackend_Stacks", "addBackend_Stacks", "updateBackend_Stacks", "deleteBackend_Stacks"]
        },
    },
    "Card": {
        "header": {
            "input-type": "text"
        },
        "content": {
            "input-type": "text"
        }
    },
    Attendance_records :{innerContent:{"input-type":"text",},tableHeading:{"input-type":"text",},addFormHeading:{"input-type":"text",},editFormHeading:{"input-type":"text",},headerSize:{"input-type":"list",options:["h1","h2","h3","h4","h5","h6"],},backgroundColor:{"input-type":"heading-color"},color:{"input-type":"heading-color"},tableHeadBackgroundColor:{"input-type":"table-head-color",},HeadColor:{"input-type":"table-head-color",},tableBackgroundColor:{"input-type":"table-color",},HeadRowBackgroundColor:{"input-type":"table-color",},HeadRowColor:{"input-type":"row-color",},RowBackgroundColor:{"input-type":"row-color",},RowColor:{"input-type":"row-color",},fontFamily:{"input-type":"list",options:["Arial","Helvetica","Verdana","Georgia","CourierNew","cursive",],},columns: {"input-type": "group","columns-list":[{name: 'attendance_id',pkey:true,fkey:false,icontrol: getList('int'),type: 'int',slice: '',},{name: 'employee_id',pkey:false,fkey:true,icontrol: getList('int',true),type: 'int',slice: 'employees',},{name: 'attendance_date',pkey:false,fkey:false,icontrol: getList('date'),type: 'date',slice: '',},{name: 'status',pkey:false,fkey:false,icontrol: getList('enum'),type: 'enum',slice: '',},{name: 'remarks',pkey:false,fkey:false,icontrol: getList('text'),type: 'text',slice: '',},{name: 'isActive',pkey:false,fkey:false,icontrol: getList('tinyint'),type: 'tinyint',slice: '',},{name: 'createdBy',pkey:false,fkey:false,icontrol: getList('varchar'),type: 'varchar',slice: '',},{name: 'modifiedBy',pkey:false,fkey:false,icontrol: getList('varchar'),type: 'varchar',slice: '',},{name: 'createdAt',pkey:false,fkey:false,icontrol: getList('datetime'),type: 'datetime',slice: '',},{name: 'modifiedAt',pkey:false,fkey:false,icontrol: getList('datetime'),type: 'datetime',slice: '',},],"error-control-list": ["password", "email", "text", "number","url"],},row:{"input-type": "filter-form","columns-list": ['attendance_id','employee_id','attendance_date','status','remarks','isActive','createdBy','modifiedBy','createdAt','modifiedAt',],"column-condition": ["==", "!=", ">", "<"],},navConfig : {"input-type": "nav","columns-list": ['attendance_id','employee_id','attendance_date','status','remarks','isActive','createdBy','modifiedBy','createdAt','modifiedAt',],"nav-list":getNavNameList(),}},Employees :{innerContent:{"input-type":"text",},tableHeading:{"input-type":"text",},addFormHeading:{"input-type":"text",},editFormHeading:{"input-type":"text",},headerSize:{"input-type":"list",options:["h1","h2","h3","h4","h5","h6"],},backgroundColor:{"input-type":"heading-color"},color:{"input-type":"heading-color"},tableHeadBackgroundColor:{"input-type":"table-head-color",},HeadColor:{"input-type":"table-head-color",},tableBackgroundColor:{"input-type":"table-color",},HeadRowBackgroundColor:{"input-type":"table-color",},HeadRowColor:{"input-type":"row-color",},RowBackgroundColor:{"input-type":"row-color",},RowColor:{"input-type":"row-color",},fontFamily:{"input-type":"list",options:["Arial","Helvetica","Verdana","Georgia","CourierNew","cursive",],},columns: {"input-type": "group","columns-list":[{name: 'employee_id',pkey:true,fkey:false,icontrol: getList('int'),type: 'int',slice: '',},{name: 'first_name',pkey:false,fkey:false,icontrol: getList('varchar'),type: 'varchar',slice: '',},{name: 'last_name',pkey:false,fkey:false,icontrol: getList('varchar'),type: 'varchar',slice: '',},{name: 'email',pkey:false,fkey:false,icontrol: getList('varchar'),type: 'varchar',slice: '',},{name: 'phone_number',pkey:false,fkey:false,icontrol: getList('varchar'),type: 'varchar',slice: '',},{name: 'department',pkey:false,fkey:false,icontrol: getList('varchar'),type: 'varchar',slice: '',},{name: 'isActive',pkey:false,fkey:false,icontrol: getList('tinyint'),type: 'tinyint',slice: '',},{name: 'createdBy',pkey:false,fkey:false,icontrol: getList('varchar'),type: 'varchar',slice: '',},{name: 'modifiedBy',pkey:false,fkey:false,icontrol: getList('varchar'),type: 'varchar',slice: '',},{name: 'createdAt',pkey:false,fkey:false,icontrol: getList('datetime'),type: 'datetime',slice: '',},{name: 'modifiedAt',pkey:false,fkey:false,icontrol: getList('datetime'),type: 'datetime',slice: '',},],"error-control-list": ["password", "email", "text", "number","url"],},row:{"input-type": "filter-form","columns-list": ['employee_id','first_name','last_name','email','phone_number','department','isActive','createdBy','modifiedBy','createdAt','modifiedAt',],"column-condition": ["==", "!=", ">", "<"],},navConfig : {"input-type": "nav","columns-list": ['employee_id','first_name','last_name','email','phone_number','department','isActive','createdBy','modifiedBy','createdAt','modifiedAt',],"nav-list":getNavNameList(),}},Leave_requests :{innerContent:{"input-type":"text",},tableHeading:{"input-type":"text",},addFormHeading:{"input-type":"text",},editFormHeading:{"input-type":"text",},headerSize:{"input-type":"list",options:["h1","h2","h3","h4","h5","h6"],},backgroundColor:{"input-type":"heading-color"},color:{"input-type":"heading-color"},tableHeadBackgroundColor:{"input-type":"table-head-color",},HeadColor:{"input-type":"table-head-color",},tableBackgroundColor:{"input-type":"table-color",},HeadRowBackgroundColor:{"input-type":"table-color",},HeadRowColor:{"input-type":"row-color",},RowBackgroundColor:{"input-type":"row-color",},RowColor:{"input-type":"row-color",},fontFamily:{"input-type":"list",options:["Arial","Helvetica","Verdana","Georgia","CourierNew","cursive",],},columns: {"input-type": "group","columns-list":[{name: 'leave_id',pkey:true,fkey:false,icontrol: getList('int'),type: 'int',slice: '',},{name: 'employee_id',pkey:false,fkey:true,icontrol: getList('int',true),type: 'int',slice: 'employees',},{name: 'leave_start_date',pkey:false,fkey:false,icontrol: getList('date'),type: 'date',slice: '',},{name: 'leave_end_date',pkey:false,fkey:false,icontrol: getList('date'),type: 'date',slice: '',},{name: 'leave_type',pkey:false,fkey:false,icontrol: getList('enum'),type: 'enum',slice: '',},{name: 'status',pkey:false,fkey:false,icontrol: getList('enum'),type: 'enum',slice: '',},{name: 'remarks',pkey:false,fkey:false,icontrol: getList('text'),type: 'text',slice: '',},{name: 'isActive',pkey:false,fkey:false,icontrol: getList('tinyint'),type: 'tinyint',slice: '',},{name: 'createdBy',pkey:false,fkey:false,icontrol: getList('varchar'),type: 'varchar',slice: '',},{name: 'modifiedBy',pkey:false,fkey:false,icontrol: getList('varchar'),type: 'varchar',slice: '',},{name: 'createdAt',pkey:false,fkey:false,icontrol: getList('datetime'),type: 'datetime',slice: '',},{name: 'modifiedAt',pkey:false,fkey:false,icontrol: getList('datetime'),type: 'datetime',slice: '',},],"error-control-list": ["password", "email", "text", "number","url"],},row:{"input-type": "filter-form","columns-list": ['leave_id','employee_id','leave_start_date','leave_end_date','leave_type','status','remarks','isActive','createdBy','modifiedBy','createdAt','modifiedAt',],"column-condition": ["==", "!=", ">", "<"],},navConfig : {"input-type": "nav","columns-list": ['leave_id','employee_id','leave_start_date','leave_end_date','leave_type','status','remarks','isActive','createdBy','modifiedBy','createdAt','modifiedAt',],"nav-list":getNavNameList(),}},S3bucket :{innerContent:{"input-type":"text",},tableHeading:{"input-type":"text",},addFormHeading:{"input-type":"text",},editFormHeading:{"input-type":"text",},headerSize:{"input-type":"list",options:["h1","h2","h3","h4","h5","h6"],},backgroundColor:{"input-type":"heading-color"},color:{"input-type":"heading-color"},tableHeadBackgroundColor:{"input-type":"table-head-color",},HeadColor:{"input-type":"table-head-color",},tableBackgroundColor:{"input-type":"table-color",},HeadRowBackgroundColor:{"input-type":"table-color",},HeadRowColor:{"input-type":"row-color",},RowBackgroundColor:{"input-type":"row-color",},RowColor:{"input-type":"row-color",},fontFamily:{"input-type":"list",options:["Arial","Helvetica","Verdana","Georgia","CourierNew","cursive",],},columns: {"input-type": "group","columns-list":[{name: 'bucket_id',pkey:true,fkey:false,icontrol: getList('int'),type: 'int',slice: '',},{name: 'bucket_name',pkey:false,fkey:false,icontrol: getList('varchar'),type: 'varchar',slice: '',},{name: 'bucket_url',pkey:false,fkey:false,icontrol: getList('varchar'),type: 'varchar',slice: '',},{name: 'modifiedBy',pkey:false,fkey:false,icontrol: getList('varchar'),type: 'varchar',slice: '',},{name: 'createdBy',pkey:false,fkey:false,icontrol: getList('varchar'),type: 'varchar',slice: '',},{name: 'modifiedAt',pkey:false,fkey:false,icontrol: getList('datetime'),type: 'datetime',slice: '',},{name: 'createdAt',pkey:false,fkey:false,icontrol: getList('datetime'),type: 'datetime',slice: '',},{name: 'isActive',pkey:false,fkey:false,icontrol: getList('tinyint'),type: 'tinyint',slice: '',},],"error-control-list": ["password", "email", "text", "number","url"],},row:{"input-type": "filter-form","columns-list": ['bucket_id','bucket_name','bucket_url','modifiedBy','createdBy','modifiedAt','createdAt','isActive',],"column-condition": ["==", "!=", ">", "<"],},navConfig : {"input-type": "nav","columns-list": ['bucket_id','bucket_name','bucket_url','modifiedBy','createdBy','modifiedAt','createdAt','isActive',],"nav-list":getNavNameList(),}},S3bucket_folders :{innerContent:{"input-type":"text",},tableHeading:{"input-type":"text",},addFormHeading:{"input-type":"text",},editFormHeading:{"input-type":"text",},headerSize:{"input-type":"list",options:["h1","h2","h3","h4","h5","h6"],},backgroundColor:{"input-type":"heading-color"},color:{"input-type":"heading-color"},tableHeadBackgroundColor:{"input-type":"table-head-color",},HeadColor:{"input-type":"table-head-color",},tableBackgroundColor:{"input-type":"table-color",},HeadRowBackgroundColor:{"input-type":"table-color",},HeadRowColor:{"input-type":"row-color",},RowBackgroundColor:{"input-type":"row-color",},RowColor:{"input-type":"row-color",},fontFamily:{"input-type":"list",options:["Arial","Helvetica","Verdana","Georgia","CourierNew","cursive",],},columns: {"input-type": "group","columns-list":[{name: 'folder_id',pkey:true,fkey:false,icontrol: getList('int'),type: 'int',slice: '',},{name: 'folder_name',pkey:false,fkey:false,icontrol: getList('varchar'),type: 'varchar',slice: '',},{name: 'modifiedBy',pkey:false,fkey:false,icontrol: getList('varchar'),type: 'varchar',slice: '',},{name: 'createdBy',pkey:false,fkey:false,icontrol: getList('varchar'),type: 'varchar',slice: '',},{name: 'modifiedAt',pkey:false,fkey:false,icontrol: getList('datetime'),type: 'datetime',slice: '',},{name: 'createdAt',pkey:false,fkey:false,icontrol: getList('datetime'),type: 'datetime',slice: '',},{name: 'isActive',pkey:false,fkey:false,icontrol: getList('tinyint'),type: 'tinyint',slice: '',},],"error-control-list": ["password", "email", "text", "number","url"],},row:{"input-type": "filter-form","columns-list": ['folder_id','folder_name','modifiedBy','createdBy','modifiedAt','createdAt','isActive',],"column-condition": ["==", "!=", ">", "<"],},navConfig : {"input-type": "nav","columns-list": ['folder_id','folder_name','modifiedBy','createdBy','modifiedAt','createdAt','isActive',],"nav-list":getNavNameList(),}},

}


// "SyncFusion GRID": {
//     "heading": {
//         "input-type": "text"
//     },
//     "primaryKeyList": {
//         "input-type": "list",
//         "options": ["backend_stack_id", "backend_stack_name", "createdBy", "modifiedBy", "isActive"]
//     },
//     "modelName": {
//         "input-type": "list",
//         "options": ["Backend_Stacks", "Branches", "Project", "User"]
//     },
//     "componentName": {
//         "input-type": "text"
//     }
// }

// {
//     id: shortid.generate(),
//     type: SIDEBAR_ITEM,
//     component: {
//         type: "NavBar",
//         content: NavBar,
//         icon: < ApiIcon className='dnd sidebarIcon' />,
//         component_name: "NavBar",
//         icon_name: "ApiIcon",
//     },
// },
// {
//     id: shortid.generate(),
//     type: SIDEBAR_ITEM,
//     component: {
//         type: "SideBar",
//         content: SideBar,
//         icon: < ApiIcon className='dnd sidebarIcon' />,
//         component_name: "SideBar",
//         icon_name: "ApiIcon",
//     },
// },

// case "NavBar":
//     return <NavBar />
//     break;
// case "SideBar":
//     return <SideBar />
//     break;