import { S3bucket_foldersDefaultView } from "./DefaultView";


export const S3bucket_folders = (props) => {
  const config = props.config;
  switch (config.selectedView) {
    case "defaultView":
      return <S3bucket_foldersDefaultView {...props} />;
    default:
      return <S3bucket_foldersDefaultView {...props} />;
  }
};
