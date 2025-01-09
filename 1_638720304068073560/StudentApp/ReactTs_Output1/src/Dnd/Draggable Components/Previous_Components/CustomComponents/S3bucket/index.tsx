import { S3bucketDefaultView } from "./DefaultView";


export const S3bucket = (props) => {
  const config = props.config;
  switch (config.selectedView) {
    case "defaultView":
      return <S3bucketDefaultView {...props} />;
    default:
      return <S3bucketDefaultView {...props} />;
  }
};
