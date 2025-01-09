import { AutoCompleteComponent } from '@syncfusion/ej2-react-dropdowns';
import { useEffect, useState } from 'react';
import { getAllLeave_requests } from 'services/leave_requestsService';
import './style.css'
import { DndProvider } from 'react-dnd';
import { HTML5Backend } from 'react-dnd-html5-backend';

export const Leave_requestsAutoComplete: React.FC = () => {
   
    const [data, setData] = useState([]);
    const [content, setContent] = useState([]);
    
    useEffect(() => {
        getAllLeave_requests(1, 100).then(
            data => {
                setData(data.data.document.records);
                setContent((data.data.document.records).map(obj => obj.employee_id));
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
