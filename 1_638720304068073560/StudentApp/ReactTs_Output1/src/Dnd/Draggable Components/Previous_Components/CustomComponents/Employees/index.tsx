import { EmployeesDefaultView } from "./DefaultView";


export const Employees = (props) => {
  const config = props.config;
  switch (config.selectedView) {
    case "defaultView":
      return <EmployeesDefaultView {...props} />;
    default:
      return <EmployeesDefaultView {...props} />;
  }
};
