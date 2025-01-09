import { ListViewComponent } from '@syncfusion/ej2-react-lists';
import { useEffect, useState } from 'react';
import { getAllEmployees } from 'services/employeesService';
import './style.css';

export const EmployeesListView = () => {
    const [data, setData] = useState([]);
    let selectedItems = [];
    const fields: object = { text: 'first_name',  groupBy: 'createdBy' };

    useEffect(() => {
        getAllEmployees(1, 100).then(
            data => {
                setData(data.data.document.records);
                ////console.log("Data Fetched : ", data.data.document.records);
            }
        )
    }, [])

    function groupTemplate(data: any): JSX.Element {
        return (
            <div>
                <span className='category'>{data.first_name}</span>
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
            headerTitle="Employees"
            showCheckBox={true}
            checkBoxPosition="Right"
            width='350px'
            groupTemplate={groupTemplate as any}
            select={onSelect}
        />
    );
}

