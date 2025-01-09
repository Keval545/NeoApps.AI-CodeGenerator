import { AutoCompleteComponent } from '@syncfusion/ej2-react-dropdowns';
import { useEffect, useState } from 'react';
import { getAllS3bucket_folders } from 'services/s3bucket_foldersService';
import './style.css'
import { DndProvider } from 'react-dnd';
import { HTML5Backend } from 'react-dnd-html5-backend';

export const S3bucket_foldersAutoComplete: React.FC = () => {
   
    const [data, setData] = useState([]);
    const [content, setContent] = useState([]);
    
    useEffect(() => {
        getAllS3bucket_folders(1, 100).then(
            data => {
                setData(data.data.document.records);
                setContent((data.data.document.records).map(obj => obj.folder_name));
               //console.log("data :- ", data);
            }
        )
    }, [])


    return (
        <DndProvider backend={HTML5Backend}>

            <div>
                <h1>Autocompletion</h1>
                <AutoCompleteComponent id="atcelement"
                    dataSource={content}
                    placeholder="Type a Field Name"
                    width="350px"
                // itemTemplate={itemTemplate = itemTemplate.bind(this)}
                />
                <hr />
            </div>
        </DndProvider>
    )
}
