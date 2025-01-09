import * as React from 'react';
import { useState, useEffect } from 'react';
import { useAppDispatch } from 'redux/store';
import { getAllAttendance_records } from 'services/attendance_recordsService';
import { registerLicense } from '@syncfusion/ej2-base';
import { Table, TableContainer, TableHead, TableRow, TableCell, TableBody } from '@mui/material';
registerLicense('Mgo+DSMBaFt/QHRqVVhlXFpHaV5LQmFJfFBmRGlcfFRzcEU3HVdTRHRcQl9iQX5Sc0VhWHdWeXA=;Mgo+DSMBPh8sVXJ0S0J+XE9BdlRBQmJAYVF2R2BJflR1d19FZEwgOX1dQl9gSXxSfkViXH9ccX1VRGQ=;ORg4AjUWIQA/Gnt2VVhkQlFac19JXnxId0x0RWFab196cVZMY1hBNQtUQF1hSn5Rd01jWHpecnVcR2ZV;MTE1MzkzNkAzMjMwMmUzNDJlMzBGTWdkM2pidVlJUk5mdTM3TDcyd3JObitGMEdObjNqT3hVTTN2aUxMWVg0PQ==;MTE1MzkzN0AzMjMwMmUzNDJlMzBtY2FVUzZnbEJSdFpzOHVHWG1ocjlsY1BkZkhINkIvL2VOd1M3dEdHbmdRPQ==;NRAiBiAaIQQuGjN/V0Z+WE9EaFpCVmBWf1ppR2NbfE5xflBFal9VVAciSV9jS31TdERrWX5bcHZUT2ddUg==;MTE1MzkzOUAzMjMwMmUzNDJlMzBneTgxVUV3ZG1MSitWaDJocjQ3am41RVBFWU81ZXJoTmVHOUw0U1dreVBnPQ==;MTE1Mzk0MEAzMjMwMmUzNDJlMzBXa3M3Vm9YZ2wwaXM4L2pnbjlrakVXNFptbi8zQkxhN3JxZHhHWlhFWWFnPQ==;Mgo+DSMBMAY9C3t2VVhkQlFac19JXnxId0x0RWFab196cVZMY1hBNQtUQF1hSn5Rd01jWHpecnZVRGRf;MTE1Mzk0MkAzMjMwMmUzNDJlMzBmNGtXcEN1YnQ1ODFNQjZpZFArV3NCd05HNm5uQXVGa3NhdmphQThITUhRPQ==;MTE1Mzk0M0AzMjMwMmUzNDJlMzBmVTF5Y0VCV2ZheHdlQzN6dVN3K3lUWDF0VURUZzVaUUdubWhYMmJCN3VJPQ==;MTE1Mzk0NEAzMjMwMmUzNDJlMzBneTgxVUV3ZG1MSitWaDJocjQ3am41RVBFWU81ZXJoTmVHOUw0U1dreVBnPQ==');


export const Attendance_recordsGridView: React.FC = () => {
    const dispatch = useAppDispatch();
    const [data, setData] = useState([]);

    useEffect(() => {
        getAllAttendance_records(1, 100).then(
            data => {
                setData(data.data.document.records);
            }
        )
    }, [data.length])


    const columns = [
        {
            field: 'attendance_id',
            headerText: 'attendance_id',
            type:'Number',
            isPrimaryKey: true
         },
	
         {
            field: 'employee_id',
            type:'Number',
            headerText: 'employee_id',
         },
	
         {
            field: 'attendance_date',
            type:'Date',
            headerText: 'attendance_date',
         },
	
         {
            field: 'status',
            type:'String',
            headerText: 'status',
         },
	
         {
            field: 'remarks',
            type:'String',
            headerText: 'remarks',
         },
	
         {
            field: 'isActive',
            type:'Number',
            headerText: 'isActive',
         },
	
         {
            field: 'createdBy',
            type:'String',
            headerText: 'createdBy',
         },
	
         {
            field: 'modifiedBy',
            type:'String',
            headerText: 'modifiedBy',
         },
	
         {
            field: 'createdAt',
            type:'Date',
            headerText: 'createdAt',
         },
	
         {
            field: 'modifiedAt',
            type:'Date',
            headerText: 'modifiedAt',
         }
	];

    return (
        /*<>
            <h1>Attendance_records</h1>
            <GridComponent dataSource={data}
                allowPaging={true}
                pageSettings={pageSettings}
                editSettings={editOptions}
                toolbar={toolbarOptions}
                actionComplete={actionComplete}
				allowSorting={true}
				allowGrouping={true}
				allowFiltering={true}
            >
                <ColumnsDirective>
                    {columns.map((column, index) => (
                        <ColumnDirective key={index} field={column.field} headerText={column.headerText} isPrimaryKey={column.isPrimaryKey} width='150' textAlign='Left' />
                    ))}
                </ColumnsDirective>
                <Inject services={[Page, Edit, Toolbar,Sort,Group,Filter]} />
            </GridComponent>
        </>*/
        <>
      <h1>Attendance_records</h1>
      <TableContainer>
        <Table>
          <TableHead>
            <TableRow>
              {columns.map((column, index) => (
                <TableCell key={index}>{column.headerText}</TableCell>
              ))}
            </TableRow>
          </TableHead>
          <TableBody>
            {data.map((item, index) => (
              <TableRow key={index}>
                {columns.map((column, columnIndex) => (
                  <TableCell key={columnIndex}>{item[column.field]}</TableCell>
                ))}
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    </>
    )
}
