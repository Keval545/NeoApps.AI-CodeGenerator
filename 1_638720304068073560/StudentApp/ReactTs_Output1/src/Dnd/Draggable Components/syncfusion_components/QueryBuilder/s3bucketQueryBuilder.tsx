import { AnimationSettingsModel, DialogComponent } from '@syncfusion/ej2-react-popups';
import { QueryBuilderComponent } from '@syncfusion/ej2-react-querybuilder';
import { AxiosResponse } from 'axios';
import { useEffect, useState } from 'react';
import { getAllS3bucket } from 'services/s3bucketService';
import { Button } from '@mui/material';

export const S3bucketQueryBuilder: React.FC = () => {

    let qryBldrObj: QueryBuilderComponent;
    let dialogInstance: DialogComponent;
    let animationSettings: AnimationSettingsModel = { effect: 'Zoom', duration: 400, delay: 0 };
    const [data, setData] = useState<AxiosResponse>(null);

    function getSql(): void {
        dialogInstance.content = qryBldrObj.getSqlFromRules(qryBldrObj.getRules());
        dialogInstance.show();
    }
    function getRule(): void {
        const validRule = qryBldrObj.getValidRules(qryBldrObj.rule);
        dialogInstance.content = '<pre>' + JSON.stringify(validRule, null, 4) + '</pre>';
        dialogInstance.show();
    }

    useEffect(() => {
        getAllS3bucket(1, 100).then(
            data => {
                setData(data.data.document.records);
            }
        )
    }, [])

    return (
        <div>
            <h1>Query Builder</h1>
            < QueryBuilderComponent dataSource={data} enableNotCondition={true} width='100%' summaryView={true} ref={(scope) => { qryBldrObj = scope as QueryBuilderComponent; }} />;
            <br />
            <div className="e-qb-button">
                <Button variant="contained" color="primary" onClick={getSql}>
          To SQL
        </Button>
        <Button variant="contained" color="primary" onClick={getRule}>
          To Rule
        </Button>
            </div>
            
            <DialogComponent id="defaultdialog" showCloseIcon={true} animationSettings={animationSettings} ref={dialog => dialogInstance = dialog as DialogComponent} height='auto' header='Querybuilder' visible={false} width='50%' />
        </div>
    )
}


// bucket_id = "backend_stack_id"
// S3bucket = "Backend_Stacks"
// s3bucket = "backend_stacks"
// S3bucketQueryBuilder = 'QueryBuilder'