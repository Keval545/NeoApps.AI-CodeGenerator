import { DropDownListComponent } from '@syncfusion/ej2-react-dropdowns';
import { useEffect, useState } from 'react';
import { getAllEmployees } from 'services/employeesService';
import './style.css'
import { registerLicense } from '@syncfusion/ej2-base';
registerLicense('Mgo+DSMBaFt/QHRqVVhlXFpHaV5LQmFJfFBmRGlcfFRzcEU3HVdTRHRcQl9iQX5Sc0VhWHdWeXA=;Mgo+DSMBPh8sVXJ0S0J+XE9BdlRBQmJAYVF2R2BJflR1d19FZEwgOX1dQl9gSXxSfkViXH9ccX1VRGQ=;ORg4AjUWIQA/Gnt2VVhkQlFac19JXnxId0x0RWFab196cVZMY1hBNQtUQF1hSn5Rd01jWHpecnVcR2ZV;MTE1MzkzNkAzMjMwMmUzNDJlMzBGTWdkM2pidVlJUk5mdTM3TDcyd3JObitGMEdObjNqT3hVTTN2aUxMWVg0PQ==;MTE1MzkzN0AzMjMwMmUzNDJlMzBtY2FVUzZnbEJSdFpzOHVHWG1ocjlsY1BkZkhINkIvL2VOd1M3dEdHbmdRPQ==;NRAiBiAaIQQuGjN/V0Z+WE9EaFpCVmBWf1ppR2NbfE5xflBFal9VVAciSV9jS31TdERrWX5bcHZUT2ddUg==;MTE1MzkzOUAzMjMwMmUzNDJlMzBneTgxVUV3ZG1MSitWaDJocjQ3am41RVBFWU81ZXJoTmVHOUw0U1dreVBnPQ==;MTE1Mzk0MEAzMjMwMmUzNDJlMzBXa3M3Vm9YZ2wwaXM4L2pnbjlrakVXNFptbi8zQkxhN3JxZHhHWlhFWWFnPQ==;Mgo+DSMBMAY9C3t2VVhkQlFac19JXnxId0x0RWFab196cVZMY1hBNQtUQF1hSn5Rd01jWHpecnZVRGRf;MTE1Mzk0MkAzMjMwMmUzNDJlMzBmNGtXcEN1YnQ1ODFNQjZpZFArV3NCd05HNm5uQXVGa3NhdmphQThITUhRPQ==;MTE1Mzk0M0AzMjMwMmUzNDJlMzBmVTF5Y0VCV2ZheHdlQzN6dVN3K3lUWDF0VURUZzVaUUdubWhYMmJCN3VJPQ==;MTE1Mzk0NEAzMjMwMmUzNDJlMzBneTgxVUV3ZG1MSitWaDJocjQ3am41RVBFWU81ZXJoTmVHOUw0U1dreVBnPQ==');

export const EmployeesDropDownList: React.FC = () => {
    const [data, setData] = useState([]);
    const [content, setContent] = useState([]);
    const fields: object = { text: 'first_name'};

    useEffect(() => {
        getAllEmployees(1, 100).then(
            data => {
                setData(data.data.document.records);
                setContent((data.data.document.records).map(obj => obj.first_name));
               //console.log(data.data.document.records);
            }
        )
    }, [])

    let headerTemplate = (data: any): JSX.Element => {
        return (
            <div className="header-row">
<div className="header-column">employee_id</div>
<div className="header-column">first_name</div>
<div className="header-column">last_name</div>
<div className="header-column">email</div>
<div className="header-column">phone_number</div>
<div className="header-column">department</div>
<div className="header-column">isActive</div>
<div className="header-column">createdBy</div>
<div className="header-column">modifiedBy</div>
<div className="header-column">createdAt</div>
<div className="header-column">modifiedAt</div>
</div>
        );
    }
    let itemTemplate = (data: any): JSX.Element => {
        return (
            <div className="item-row"><div className="header-column">{data.employee_id}</div>
<div className="header-column">{data.first_name}</div>
<div className="header-column">{data.last_name}</div>
<div className="header-column">{data.email}</div>
<div className="header-column">{data.phone_number}</div>
<div className="header-column">{data.department}</div>
<div className="header-column">{data.isActive}</div>
<div className="header-column">{data.createdBy}</div>
<div className="header-column">{data.modifiedBy}</div>
<div className="header-column">{data.createdAt}</div>
<div className="header-column">{data.modifiedAt}</div>
</div>
        );
    }
    let failureTemplate = (data: any): JSX.Element => {
        return (
            <span className='action-failure'> Data fetch get fails</span>
        );
    }
    const onChange = (args) => {
    }
    return (
        <DropDownListComponent id='ddlelement'
            dataSource={data}
            fields={fields}
            placeholder="Select a Field"
            headerTemplate={headerTemplate = headerTemplate.bind(this)}
            itemTemplate={itemTemplate = itemTemplate.bind(this)}
            actionFailureTemplate={failureTemplate = failureTemplate.bind(this)}
            change={onChange}
        />
    )
}