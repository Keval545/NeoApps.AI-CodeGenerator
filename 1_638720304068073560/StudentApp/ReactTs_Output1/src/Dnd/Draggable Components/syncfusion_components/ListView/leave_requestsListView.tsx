import { ListViewComponent } from '@syncfusion/ej2-react-lists';
import { useEffect, useState } from 'react';
import { getAllLeave_requests } from 'services/leave_requestsService';
import './style.css';

export const Leave_requestsListView = () => {
    const [data, setData] = useState([]);
    let selectedItems = [];
    const fields: object = { text: 'employee_id',  groupBy: 'createdBy' };

    useEffect(() => {
        getAllLeave_requests(1, 100).then(
            data => {
                setData(data.data.document.records);
                ////console.log("Data Fetched : ", data.data.document.records);
            }
        )
    }, [])

    function groupTemplate(data: any): JSX.Element {
        return (
            <div>
                <span className='category'>{data.employee_id}</span>
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
            headerTitle="Leave_requests"
            showCheckBox={true}
            checkBoxPosition="Right"
            width='350px'
            groupTemplate={groupTemplate as any}
            select={onSelect}
        />
    );
}

