import { Leave_requestsDefaultView } from "./DefaultView";


export const Leave_requests = (props) => {
  const config = props.config;
  switch (config.selectedView) {
    case "defaultView":
      return <Leave_requestsDefaultView {...props} />;
    default:
      return <Leave_requestsDefaultView {...props} />;
  }
};
