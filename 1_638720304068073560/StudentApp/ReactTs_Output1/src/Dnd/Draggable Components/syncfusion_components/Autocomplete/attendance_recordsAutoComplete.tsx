import { AutoCompleteComponent } from '@syncfusion/ej2-react-dropdowns';
import { useEffect, useState } from 'react';
import { getAllAttendance_records } from 'services/attendance_recordsService';
import './style.css'
import { DndProvider } from 'react-dnd';
import { HTML5Backend } from 'react-dnd-html5-backend';

export const Attendance_recordsAutoComplete: React.FC = () => {
   
    const [data, setData] = useState([]);
    const [content, setContent] = useState([]);
    
    useEffect(() => {
        getAllAttendance_records(1, 100).then(
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
