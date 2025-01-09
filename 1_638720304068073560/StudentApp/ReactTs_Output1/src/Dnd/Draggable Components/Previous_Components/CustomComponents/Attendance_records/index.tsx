import { Attendance_recordsDefaultView } from "./DefaultView";


export const Attendance_records = (props) => {
  const config = props.config;
  switch (config.selectedView) {
    case "defaultView":
      return <Attendance_recordsDefaultView {...props} />;
    default:
      return <Attendance_recordsDefaultView {...props} />;
  }
};
