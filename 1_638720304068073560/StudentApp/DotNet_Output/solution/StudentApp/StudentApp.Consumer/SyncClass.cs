using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentApp.Model;
using Newtonsoft.Json;

namespace StudentApp.Consumer
{
    public class SyncClass
    {
        public void syncMethod()
        {
            var factory = new ConnectionFactory
            {
                UserName = "guest",
                Password = "guest",
                HostName = "localhost"
            };
            //Create the RabbitMQ connection using connection factory details as i mentioned above
            var connection = factory.CreateConnection();
            //Here we create channel with session and model
            using
            var syncChannel = connection.CreateModel();
            //declare the queue after mentioning name and a few property related to that
            syncChannel.QueueDeclare("myeshop.API", exclusive: false);
            //Set Event object which listen message from chanel which is sent by producer
            var consumer = new EventingBasicConsumer(syncChannel);
            consumer.Received += (model, eventArgs) => {
                //Console.WriteLine("event args:{0}", eventArgs);
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);


                PacketModel packet = JsonConvert.DeserializeObject<PacketModel>(message);
                if (packet == null)
                    Console.WriteLine("exception thrown...packet is null");
                else
                {
                    Console.WriteLine(packet.className);
                    //Console.WriteLine(message);
                    byte[] utf8Bytes = Encoding.UTF8.GetBytes(packet.className);

                    //Converting to Unicode from UTF8 bytes
                    byte[] unicodeBytes = Encoding.Convert(Encoding.UTF8, Encoding.Unicode, utf8Bytes);

                    //Getting string from Unicode bytes
                    string classN = Encoding.Unicode.GetString(unicodeBytes);
                    //Console.WriteLine(msg);
                    if (Equals(classN, "Attendance_recordsModel"))
                    {

                            Attendance_recordsModel attendance_recordsModel = JsonConvert.DeserializeObject<Attendance_recordsModel>(packet.classString);

                            if (attendance_recordsModel != null)
                            {
                                Console.WriteLine("product object :{0}\t{1}", packet.classString
                            
                        ,attendance_recordsModel.employee_id);}}
else if (Equals(classN, "Dnd_ui_versionsModel"))
                    {

                            Dnd_ui_versionsModel dnd_ui_versionsModel = JsonConvert.DeserializeObject<Dnd_ui_versionsModel>(packet.classString);

                            if (dnd_ui_versionsModel != null)
                            {
                                Console.WriteLine("product object :{0}\t{1}", packet.classString,dnd_ui_versionsModel.layout);}}
else if (Equals(classN, "EmployeesModel"))
                    {

                            EmployeesModel employeesModel = JsonConvert.DeserializeObject<EmployeesModel>(packet.classString);

                            if (employeesModel != null)
                            {
                                Console.WriteLine("product object :{0}\t{1}", packet.classString,employeesModel.first_name);}}
else if (Equals(classN, "EntitiesModel"))
                    {

                            EntitiesModel entitiesModel = JsonConvert.DeserializeObject<EntitiesModel>(packet.classString);

                            if (entitiesModel != null)
                            {
                                Console.WriteLine("product object :{0}\t{1}", packet.classString,entitiesModel.entity_name);}}
else if (Equals(classN, "Leave_requestsModel"))
                    {

                            Leave_requestsModel leave_requestsModel = JsonConvert.DeserializeObject<Leave_requestsModel>(packet.classString);

                            if (leave_requestsModel != null)
                            {
                                Console.WriteLine("product object :{0}\t{1}", packet.classString,leave_requestsModel.employee_id);}}
else if (Equals(classN, "MessagequeueModel"))
                    {

                            MessagequeueModel messagequeueModel = JsonConvert.DeserializeObject<MessagequeueModel>(packet.classString);

                            if (messagequeueModel != null)
                            {
                                Console.WriteLine("product object :{0}\t{1}", packet.classString,messagequeueModel.queueName);}}
else if (Equals(classN, "PermissionmatrixModel"))
                    {

                            PermissionmatrixModel permissionmatrixModel = JsonConvert.DeserializeObject<PermissionmatrixModel>(packet.classString);

                            if (permissionmatrixModel != null)
                            {
                                Console.WriteLine("product object :{0}\t{1}", packet.classString,permissionmatrixModel.role_id);}}
else if (Equals(classN, "Project_dnd_ui_versionsModel"))
                    {

                            Project_dnd_ui_versionsModel project_dnd_ui_versionsModel = JsonConvert.DeserializeObject<Project_dnd_ui_versionsModel>(packet.classString);

                            if (project_dnd_ui_versionsModel != null)
                            {
                                Console.WriteLine("product object :{0}\t{1}", packet.classString,project_dnd_ui_versionsModel.project_id);}}
else if (Equals(classN, "RolesModel"))
                    {

                            RolesModel rolesModel = JsonConvert.DeserializeObject<RolesModel>(packet.classString);

                            if (rolesModel != null)
                            {
                                Console.WriteLine("product object :{0}\t{1}", packet.classString,rolesModel.role_name);}}
else if (Equals(classN, "S3bucketModel"))
                    {

                            S3bucketModel s3bucketModel = JsonConvert.DeserializeObject<S3bucketModel>(packet.classString);

                            if (s3bucketModel != null)
                            {
                                Console.WriteLine("product object :{0}\t{1}", packet.classString,s3bucketModel.bucket_name);}}
else if (Equals(classN, "S3bucket_foldersModel"))
                    {

                            S3bucket_foldersModel s3bucket_foldersModel = JsonConvert.DeserializeObject<S3bucket_foldersModel>(packet.classString);

                            if (s3bucket_foldersModel != null)
                            {
                                Console.WriteLine("product object :{0}\t{1}", packet.classString,s3bucket_foldersModel.folder_name);}}
else if (Equals(classN, "UsersModel"))
                    {

                            UsersModel usersModel = JsonConvert.DeserializeObject<UsersModel>(packet.classString);

                            if (usersModel != null)
                            {
                                Console.WriteLine("product object :{0}\t{1}", packet.classString,usersModel.username);}}
else if (Equals(classN, "WorkflowModel"))
                    {

                            WorkflowModel workflowModel = JsonConvert.DeserializeObject<WorkflowModel>(packet.classString);

                            if (workflowModel != null)
                            {
                                Console.WriteLine("product object :{0}\t{1}", packet.classString,workflowModel.steps);}}
else if (Equals(classN, "WorkflowsModel"))
                    {

                            WorkflowsModel workflowsModel = JsonConvert.DeserializeObject<WorkflowsModel>(packet.classString);

                            if (workflowsModel != null)
                            {
                                Console.WriteLine("product object :{0}\t{1}", packet.classString,workflowsModel.workflow_name);}}
else if (Equals(classN, "Workflows_projectsModel"))
                    {

                            Workflows_projectsModel workflows_projectsModel = JsonConvert.DeserializeObject<Workflows_projectsModel>(packet.classString);

                            if (workflows_projectsModel != null)
                            {
                                Console.WriteLine("product object :{0}\t{1}", packet.classString,workflows_projectsModel.modifiedBy);}}
else if (Equals(classN, "Workflow_buildsModel"))
                    {

                            Workflow_buildsModel workflow_buildsModel = JsonConvert.DeserializeObject<Workflow_buildsModel>(packet.classString);

                            if (workflow_buildsModel != null)
                            {
                                Console.WriteLine("product object :{0}\t{1}", packet.classString,workflow_buildsModel.workflow_id);}}
else if (Equals(classN, "Workflow_deploymentsModel"))
                    {

                            Workflow_deploymentsModel workflow_deploymentsModel = JsonConvert.DeserializeObject<Workflow_deploymentsModel>(packet.classString);

                            if (workflow_deploymentsModel != null)
                            {
                                Console.WriteLine("product object :{0}\t{1}", packet.classString,workflow_deploymentsModel.workflow_run_id);}}
else if (Equals(classN, "Workflow_runsModel"))
                    {

                            Workflow_runsModel workflow_runsModel = JsonConvert.DeserializeObject<Workflow_runsModel>(packet.classString);

                            if (workflow_runsModel != null)
                            {
                                Console.WriteLine("product object :{0}\t{1}", packet.classString,workflow_runsModel.workflow_build_id);}}
else if (Equals(classN, "Workflow_triggersModel"))
                    {

                            Workflow_triggersModel workflow_triggersModel = JsonConvert.DeserializeObject<Workflow_triggersModel>(packet.classString);

                            if (workflow_triggersModel != null)
                            {
                                Console.WriteLine("product object :{0}\t{1}", packet.classString,workflow_triggersModel.workflow_id);}}
else if (Equals(classN, "Workflow_trigger_conditionsModel"))
                    {

                            Workflow_trigger_conditionsModel workflow_trigger_conditionsModel = JsonConvert.DeserializeObject<Workflow_trigger_conditionsModel>(packet.classString);

                            if (workflow_trigger_conditionsModel != null)
                            {
                                Console.WriteLine("product object :{0}\t{1}", packet.classString,workflow_trigger_conditionsModel.condition_type);}}

                    else
                    {
                        Console.WriteLine("No model present with this name:{0}", classN);
                    }
                }
            };
            //read the message
            syncChannel.BasicConsume(queue: "myeshop.API", autoAck: true, consumer: consumer);
            Console.ReadKey();
        }
    }
}
