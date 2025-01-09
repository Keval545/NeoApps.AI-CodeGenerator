import { AutoCompleteComponent } from '@syncfusion/ej2-react-dropdowns';
import { useEffect, useState } from 'react';
import { getAllEmployees } from 'services/employeesService';
import './style.css'
import { DndProvider } from 'react-dnd';
import { HTML5Backend } from 'react-dnd-html5-backend';

export const EmployeesAutoComplete: React.FC = () => {
   
    const [data, setData] = useState([]);
    const [content, setContent] = useState([]);
    
    useEffect(() => {
        getAllEmployees(1, 100).then(
            data => {
                setData(data.data.document.records);
                setContent((data.data.document.records).map(obj => obj.first_name));
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
