import { ListViewComponent } from '@syncfusion/ej2-react-lists';
import { useEffect, useState } from 'react';
import { getAllS3bucket_folders } from 'services/s3bucket_foldersService';
import './style.css';

export const S3bucket_foldersListView = () => {
    const [data, setData] = useState([]);
    let selectedItems = [];
    const fields: object = { text: 'folder_name',  groupBy: 'createdBy' };

    useEffect(() => {
        getAllS3bucket_folders(1, 100).then(
            data => {
                setData(data.data.document.records);
                ////console.log("Data Fetched : ", data.data.document.records);
            }
        )
    }, [])

    function groupTemplate(data: any): JSX.Element {
        return (
            <div>
                <span className='category'>{data.folder_name}</span>
                {(fields["groupBy"] === "" || fields["groupBy"] === undefined) ? <span className="count"></span> : data.items.length <= 1 ? <span className="count"> {data.items.length} Item </span> : <span className="count"> {data.items.length} Items</span>}
            </div>
        );
    }
    function onSelect(args) {
        selectedItems.push(args.data);
       //console.log("Selected Items : ", selectedItems);
    }

    return (
        <ListViewComponent
            id="list"
            dataSource={data}
            fields={fields}
            showHeader={true}
            headerTitle="S3bucket_folders"
            showCheckBox={true}
            checkBoxPosition="Right"
            width='350px'
            groupTemplate={groupTemplate as any}
            select={onSelect}
        />
    );
}

