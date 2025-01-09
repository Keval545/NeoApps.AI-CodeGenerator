using StudentApp.DataAccess.Interface;
using StudentApp.Model;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Collections.Concurrent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;
using StudentApp.Utility;
using System.Collections;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Dynamic;

namespace StudentApp.Consumer
{
    public class ConsumeMessage
    {
        private static int NumThreads = 10;
        private IModel channel;
        private IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
        private IConnection connection { get; set; }

        private string queueName = "DatabaseEventsModel";
        public ConsumeMessage(IConnection c)
        {
            connection = c;
            channel = connection.CreateModel();
            channel.QueueDeclare(queue: queueName,
                                durable: false,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);
        }
        public void consumeMessage()
        {

            //Set Event object which listen message from chanel that is sent by producer
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, eventArgs) =>
            {

                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                ThreadPool.QueueUserWorkItem(async (state) =>
                {

                    channel.BasicAck(deliveryTag: eventArgs.DeliveryTag, multiple: false);

                    PacketModel msg = JsonConvert.DeserializeObject<PacketModel>(message);
                    if (msg == null)
                    {
                        Console.WriteLine("msg is null");
                    }
                    else
                    {
                        //coverting ffrom UTF8 to Unicode
                        byte[] utf8Bytes = Encoding.UTF8.GetBytes(msg.className);
                        byte[] unicodeBytes = Encoding.Convert(Encoding.UTF8, Encoding.Unicode, utf8Bytes);

                        //Getting string from Unicode bytes
                        string className = Encoding.Unicode.GetString(unicodeBytes);
                        var action = "";
                        string token = msg.token;
                        Console.WriteLine("Token Found: " + token);
                        if (msg.id.Values.First() == int.MaxValue)
                        {
                            Console.WriteLine("added model");
                            action = "added";
                        }
                        else if (msg.classString == null)
                        {

                            Console.WriteLine("Deleted model");
                            action = "deleted";
                        }
                        else
                        {
                            Console.WriteLine("updated model");
                            action = "updated";
                        }
                        if (Equals(className, "Attendance_recordsModel")){
                                if (msg.classString != null)
                                {
                            //Deserialized Object into Attendance_recordsModel type
                            Attendance_recordsModel attendance_recordsModel = JsonConvert.DeserializeObject<Attendance_recordsModel>(msg.classString);
                        Console.WriteLine(attendance_recordsModel);

                        //Parsed the Object into JSon Object 
                        var obj = JObject.Parse(msg.classString);
                        //Console.WriteLine(obj);
                        //Console.WriteLine(obj[DataAccess.findPrimaryKey(className)]);
                        var workflowExec = new workflowExecuter();
                               var res = await Task.Run(async () => await workflowExec.TriggerWorkflowAsync(configuration.GetConnectionString("NodeRedEndPoint"), attendance_recordsModel, action, className,token));
                                }
                                else
                                {
                                    dynamic obj = new ExpandoObject();
                                    foreach (var kvp in msg.id)
                                    {
                                        string key = kvp.Key;
                                        int value = kvp.Value;
                                        ((IDictionary<string, object>)obj)[key] = value;

                                    }
                                    var workflowExec = new workflowExecuter();
                                    var res = await Task.Run(async () => await workflowExec.TriggerWorkflowAsync(configuration.GetConnectionString("NodeRedEndPoint"), obj, action, className,token));
                                }

                    }
                    
else if (Equals(className, "Dnd_ui_versionsModel")){
                                    if (msg.classString != null)
                                    {
                            //Deserialized Object into Dnd_ui_versionsModel type
                            Dnd_ui_versionsModel dnd_ui_versionsModel = JsonConvert.DeserializeObject<Dnd_ui_versionsModel>(msg.classString);
                        Console.WriteLine(dnd_ui_versionsModel);

                        //Parsed the Object into JSon Object 
                        var obj = JObject.Parse(msg.classString);
                        //Console.WriteLine(obj);
                        //Console.WriteLine(obj[DataAccess.findPrimaryKey(className)]);
                        var workflowExec = new workflowExecuter();
                               var res = await Task.Run(async () => await workflowExec.TriggerWorkflowAsync(configuration.GetConnectionString("NodeRedEndPoint"), dnd_ui_versionsModel, action, className,token));
                                    }
                                    else
                                    {
                                        dynamic obj = new ExpandoObject();
                                        foreach (var kvp in msg.id)
                                        {
                                            string key = kvp.Key;
                                            int value = kvp.Value;
                                            ((IDictionary<string, object>)obj)[key] = value;

                                        }
                                        var workflowExec = new workflowExecuter();
                                        var res = await Task.Run(async () => await workflowExec.TriggerWorkflowAsync(configuration.GetConnectionString("NodeRedEndPoint"), obj, action, className,token));
                                    }
                    }
                    
else if (Equals(className, "EmployeesModel")){
                                    if (msg.classString != null)
                                    {
                            //Deserialized Object into EmployeesModel type
                            EmployeesModel employeesModel = JsonConvert.DeserializeObject<EmployeesModel>(msg.classString);
                        Console.WriteLine(employeesModel);

                        //Parsed the Object into JSon Object 
                        var obj = JObject.Parse(msg.classString);
                        //Console.WriteLine(obj);
                        //Console.WriteLine(obj[DataAccess.findPrimaryKey(className)]);
                        var workflowExec = new workflowExecuter();
                               var res = await Task.Run(async () => await workflowExec.TriggerWorkflowAsync(configuration.GetConnectionString("NodeRedEndPoint"), employeesModel, action, className,token));
                                    }
                                    else
                                    {
                                        dynamic obj = new ExpandoObject();
                                        foreach (var kvp in msg.id)
                                        {
                                            string key = kvp.Key;
                                            int value = kvp.Value;
                                            ((IDictionary<string, object>)obj)[key] = value;

                                        }
                                        var workflowExec = new workflowExecuter();
                                        var res = await Task.Run(async () => await workflowExec.TriggerWorkflowAsync(configuration.GetConnectionString("NodeRedEndPoint"), obj, action, className,token));
                                    }
                    }
                    
else if (Equals(className, "EntitiesModel")){
                                    if (msg.classString != null)
                                    {
                            //Deserialized Object into EntitiesModel type
                            EntitiesModel entitiesModel = JsonConvert.DeserializeObject<EntitiesModel>(msg.classString);
                        Console.WriteLine(entitiesModel);

                        //Parsed the Object into JSon Object 
                        var obj = JObject.Parse(msg.classString);
                        //Console.WriteLine(obj);
                        //Console.WriteLine(obj[DataAccess.findPrimaryKey(className)]);
                        var workflowExec = new workflowExecuter();
                               var res = await Task.Run(async () => await workflowExec.TriggerWorkflowAsync(configuration.GetConnectionString("NodeRedEndPoint"), entitiesModel, action, className,token));
                                    }
                                    else
                                    {
                                        dynamic obj = new ExpandoObject();
                                        foreach (var kvp in msg.id)
                                        {
                                            string key = kvp.Key;
                                            int value = kvp.Value;
                                            ((IDictionary<string, object>)obj)[key] = value;

                                        }
                                        var workflowExec = new workflowExecuter();
                                        var res = await Task.Run(async () => await workflowExec.TriggerWorkflowAsync(configuration.GetConnectionString("NodeRedEndPoint"), obj, action, className,token));
                                    }
                    }
                    
else if (Equals(className, "Leave_requestsModel")){
                                    if (msg.classString != null)
                                    {
                            //Deserialized Object into Leave_requestsModel type
                            Leave_requestsModel leave_requestsModel = JsonConvert.DeserializeObject<Leave_requestsModel>(msg.classString);
                        Console.WriteLine(leave_requestsModel);

                        //Parsed the Object into JSon Object 
                        var obj = JObject.Parse(msg.classString);
                        //Console.WriteLine(obj);
                        //Console.WriteLine(obj[DataAccess.findPrimaryKey(className)]);
                        var workflowExec = new workflowExecuter();
                               var res = await Task.Run(async () => await workflowExec.TriggerWorkflowAsync(configuration.GetConnectionString("NodeRedEndPoint"), leave_requestsModel, action, className,token));
                                    }
                                    else
                                    {
                                        dynamic obj = new ExpandoObject();
                                        foreach (var kvp in msg.id)
                                        {
                                            string key = kvp.Key;
                                            int value = kvp.Value;
                                            ((IDictionary<string, object>)obj)[key] = value;

                                        }
                                        var workflowExec = new workflowExecuter();
                                        var res = await Task.Run(async () => await workflowExec.TriggerWorkflowAsync(configuration.GetConnectionString("NodeRedEndPoint"), obj, action, className,token));
                                    }
                    }
                    
else if (Equals(className, "MessagequeueModel")){
                                    if (msg.classString != null)
                                    {
                            //Deserialized Object into MessagequeueModel type
                            MessagequeueModel messagequeueModel = JsonConvert.DeserializeObject<MessagequeueModel>(msg.classString);
                        Console.WriteLine(messagequeueModel);

                        //Parsed the Object into JSon Object 
                        var obj = JObject.Parse(msg.classString);
                        //Console.WriteLine(obj);
                        //Console.WriteLine(obj[DataAccess.findPrimaryKey(className)]);
                        var workflowExec = new workflowExecuter();
                               var res = await Task.Run(async () => await workflowExec.TriggerWorkflowAsync(configuration.GetConnectionString("NodeRedEndPoint"), messagequeueModel, action, className,token));
                                    }
                                    else
                                    {
                                        dynamic obj = new ExpandoObject();
                                        foreach (var kvp in msg.id)
                                        {
                                            string key = kvp.Key;
                                            int value = kvp.Value;
                                            ((IDictionary<string, object>)obj)[key] = value;

                                        }
                                        var workflowExec = new workflowExecuter();
                                        var res = await Task.Run(async () => await workflowExec.TriggerWorkflowAsync(configuration.GetConnectionString("NodeRedEndPoint"), obj, action, className,token));
                                    }
                    }
                    
else if (Equals(className, "PermissionmatrixModel")){
                                    if (msg.classString != null)
                                    {
                            //Deserialized Object into PermissionmatrixModel type
                            PermissionmatrixModel permissionmatrixModel = JsonConvert.DeserializeObject<PermissionmatrixModel>(msg.classString);
                        Console.WriteLine(permissionmatrixModel);

                        //Parsed the Object into JSon Object 
                        var obj = JObject.Parse(msg.classString);
                        //Console.WriteLine(obj);
                        //Console.WriteLine(obj[DataAccess.findPrimaryKey(className)]);
                        var workflowExec = new workflowExecuter();
                               var res = await Task.Run(async () => await workflowExec.TriggerWorkflowAsync(configuration.GetConnectionString("NodeRedEndPoint"), permissionmatrixModel, action, className,token));
                                    }
                                    else
                                    {
                                        dynamic obj = new ExpandoObject();
                                        foreach (var kvp in msg.id)
                                        {
                                            string key = kvp.Key;
                                            int value = kvp.Value;
                                            ((IDictionary<string, object>)obj)[key] = value;

                                        }
                                        var workflowExec = new workflowExecuter();
                                        var res = await Task.Run(async () => await workflowExec.TriggerWorkflowAsync(configuration.GetConnectionString("NodeRedEndPoint"), obj, action, className,token));
                                    }
                    }
                    
else if (Equals(className, "Project_dnd_ui_versionsModel")){
                                    if (msg.classString != null)
                                    {
                            //Deserialized Object into Project_dnd_ui_versionsModel type
                            Project_dnd_ui_versionsModel project_dnd_ui_versionsModel = JsonConvert.DeserializeObject<Project_dnd_ui_versionsModel>(msg.classString);
                        Console.WriteLine(project_dnd_ui_versionsModel);

                        //Parsed the Object into JSon Object 
                        var obj = JObject.Parse(msg.classString);
                        //Console.WriteLine(obj);
                        //Console.WriteLine(obj[DataAccess.findPrimaryKey(className)]);
                        var workflowExec = new workflowExecuter();
                               var res = await Task.Run(async () => await workflowExec.TriggerWorkflowAsync(configuration.GetConnectionString("NodeRedEndPoint"), project_dnd_ui_versionsModel, action, className,token));
                                    }
                                    else
                                    {
                                        dynamic obj = new ExpandoObject();
                                        foreach (var kvp in msg.id)
                                        {
                                            string key = kvp.Key;
                                            int value = kvp.Value;
                                            ((IDictionary<string, object>)obj)[key] = value;

                                        }
                                        var workflowExec = new workflowExecuter();
                                        var res = await Task.Run(async () => await workflowExec.TriggerWorkflowAsync(configuration.GetConnectionString("NodeRedEndPoint"), obj, action, className,token));
                                    }
                    }
                    
else if (Equals(className, "RolesModel")){
                                    if (msg.classString != null)
                                    {
                            //Deserialized Object into RolesModel type
                            RolesModel rolesModel = JsonConvert.DeserializeObject<RolesModel>(msg.classString);
                        Console.WriteLine(rolesModel);

                        //Parsed the Object into JSon Object 
                        var obj = JObject.Parse(msg.classString);
                        //Console.WriteLine(obj);
                        //Console.WriteLine(obj[DataAccess.findPrimaryKey(className)]);
                        var workflowExec = new workflowExecuter();
                               var res = await Task.Run(async () => await workflowExec.TriggerWorkflowAsync(configuration.GetConnectionString("NodeRedEndPoint"), rolesModel, action, className,token));
                                    }
                                    else
                                    {
                                        dynamic obj = new ExpandoObject();
                                        foreach (var kvp in msg.id)
                                        {
                                            string key = kvp.Key;
                                            int value = kvp.Value;
                                            ((IDictionary<string, object>)obj)[key] = value;

                                        }
                                        var workflowExec = new workflowExecuter();
                                        var res = await Task.Run(async () => await workflowExec.TriggerWorkflowAsync(configuration.GetConnectionString("NodeRedEndPoint"), obj, action, className,token));
                                    }
                    }
                    
else if (Equals(className, "S3bucketModel")){
                                    if (msg.classString != null)
                                    {
                            //Deserialized Object into S3bucketModel type
                            S3bucketModel s3bucketModel = JsonConvert.DeserializeObject<S3bucketModel>(msg.classString);
                        Console.WriteLine(s3bucketModel);

                        //Parsed the Object into JSon Object 
                        var obj = JObject.Parse(msg.classString);
                        //Console.WriteLine(obj);
                        //Console.WriteLine(obj[DataAccess.findPrimaryKey(className)]);
                        var workflowExec = new workflowExecuter();
                               var res = await Task.Run(async () => await workflowExec.TriggerWorkflowAsync(configuration.GetConnectionString("NodeRedEndPoint"), s3bucketModel, action, className,token));
                                    }
                                    else
                                    {
                                        dynamic obj = new ExpandoObject();
                                        foreach (var kvp in msg.id)
                                        {
                                            string key = kvp.Key;
                                            int value = kvp.Value;
                                            ((IDictionary<string, object>)obj)[key] = value;

                                        }
                                        var workflowExec = new workflowExecuter();
                                        var res = await Task.Run(async () => await workflowExec.TriggerWorkflowAsync(configuration.GetConnectionString("NodeRedEndPoint"), obj, action, className,token));
                                    }
                    }
                    
else if (Equals(className, "S3bucket_foldersModel")){
                                    if (msg.classString != null)
                                    {
                            //Deserialized Object into S3bucket_foldersModel type
                            S3bucket_foldersModel s3bucket_foldersModel = JsonConvert.DeserializeObject<S3bucket_foldersModel>(msg.classString);
                        Console.WriteLine(s3bucket_foldersModel);

                        //Parsed the Object into JSon Object 
                        var obj = JObject.Parse(msg.classString);
                        //Console.WriteLine(obj);
                        //Console.WriteLine(obj[DataAccess.findPrimaryKey(className)]);
                        var workflowExec = new workflowExecuter();
                               var res = await Task.Run(async () => await workflowExec.TriggerWorkflowAsync(configuration.GetConnectionString("NodeRedEndPoint"), s3bucket_foldersModel, action, className,token));
                                    }
                                    else
                                    {
                                        dynamic obj = new ExpandoObject();
                                        foreach (var kvp in msg.id)
                                        {
                                            string key = kvp.Key;
                                            int value = kvp.Value;
                                            ((IDictionary<string, object>)obj)[key] = value;

                                        }
                                        var workflowExec = new workflowExecuter();
                                        var res = await Task.Run(async () => await workflowExec.TriggerWorkflowAsync(configuration.GetConnectionString("NodeRedEndPoint"), obj, action, className,token));
                                    }
                    }
                    
else if (Equals(className, "UsersModel")){
                                    if (msg.classString != null)
                                    {
                            //Deserialized Object into UsersModel type
                            UsersModel usersModel = JsonConvert.DeserializeObject<UsersModel>(msg.classString);
                        Console.WriteLine(usersModel);

                        //Parsed the Object into JSon Object 
                        var obj = JObject.Parse(msg.classString);
                        //Console.WriteLine(obj);
                        //Console.WriteLine(obj[DataAccess.findPrimaryKey(className)]);
                        var workflowExec = new workflowExecuter();
                               var res = await Task.Run(async () => await workflowExec.TriggerWorkflowAsync(configuration.GetConnectionString("NodeRedEndPoint"), usersModel, action, className,token));
                                    }
                                    else
                                    {
                                        dynamic obj = new ExpandoObject();
                                        foreach (var kvp in msg.id)
                                        {
                                            string key = kvp.Key;
                                            int value = kvp.Value;
                                            ((IDictionary<string, object>)obj)[key] = value;

                                        }
                                        var workflowExec = new workflowExecuter();
                                        var res = await Task.Run(async () => await workflowExec.TriggerWorkflowAsync(configuration.GetConnectionString("NodeRedEndPoint"), obj, action, className,token));
                                    }
                    }
                    
else if (Equals(className, "WorkflowModel")){
                                    if (msg.classString != null)
                                    {
                            //Deserialized Object into WorkflowModel type
                            WorkflowModel workflowModel = JsonConvert.DeserializeObject<WorkflowModel>(msg.classString);
                        Console.WriteLine(workflowModel);

                        //Parsed the Object into JSon Object 
                        var obj = JObject.Parse(msg.classString);
                        //Console.WriteLine(obj);
                        //Console.WriteLine(obj[DataAccess.findPrimaryKey(className)]);
                        var workflowExec = new workflowExecuter();
                               var res = await Task.Run(async () => await workflowExec.TriggerWorkflowAsync(configuration.GetConnectionString("NodeRedEndPoint"), workflowModel, action, className,token));
                                    }
                                    else
                                    {
                                        dynamic obj = new ExpandoObject();
                                        foreach (var kvp in msg.id)
                                        {
                                            string key = kvp.Key;
                                            int value = kvp.Value;
                                            ((IDictionary<string, object>)obj)[key] = value;

                                        }
                                        var workflowExec = new workflowExecuter();
                                        var res = await Task.Run(async () => await workflowExec.TriggerWorkflowAsync(configuration.GetConnectionString("NodeRedEndPoint"), obj, action, className,token));
                                    }
                    }
                    
else if (Equals(className, "WorkflowsModel")){
                                    if (msg.classString != null)
                                    {
                            //Deserialized Object into WorkflowsModel type
                            WorkflowsModel workflowsModel = JsonConvert.DeserializeObject<WorkflowsModel>(msg.classString);
                        Console.WriteLine(workflowsModel);

                        //Parsed the Object into JSon Object 
                        var obj = JObject.Parse(msg.classString);
                        //Console.WriteLine(obj);
                        //Console.WriteLine(obj[DataAccess.findPrimaryKey(className)]);
                        var workflowExec = new workflowExecuter();
                               var res = await Task.Run(async () => await workflowExec.TriggerWorkflowAsync(configuration.GetConnectionString("NodeRedEndPoint"), workflowsModel, action, className,token));
                                    }
                                    else
                                    {
                                        dynamic obj = new ExpandoObject();
                                        foreach (var kvp in msg.id)
                                        {
                                            string key = kvp.Key;
                                            int value = kvp.Value;
                                            ((IDictionary<string, object>)obj)[key] = value;

                                        }
                                        var workflowExec = new workflowExecuter();
                                        var res = await Task.Run(async () => await workflowExec.TriggerWorkflowAsync(configuration.GetConnectionString("NodeRedEndPoint"), obj, action, className,token));
                                    }
                    }
                    
else if (Equals(className, "Workflows_projectsModel")){
                                    if (msg.classString != null)
                                    {
                            //Deserialized Object into Workflows_projectsModel type
                            Workflows_projectsModel workflows_projectsModel = JsonConvert.DeserializeObject<Workflows_projectsModel>(msg.classString);
                        Console.WriteLine(workflows_projectsModel);

                        //Parsed the Object into JSon Object 
                        var obj = JObject.Parse(msg.classString);
                        //Console.WriteLine(obj);
                        //Console.WriteLine(obj[DataAccess.findPrimaryKey(className)]);
                        var workflowExec = new workflowExecuter();
                               var res = await Task.Run(async () => await workflowExec.TriggerWorkflowAsync(configuration.GetConnectionString("NodeRedEndPoint"), workflows_projectsModel, action, className,token));
                                    }
                                    else
                                    {
                                        dynamic obj = new ExpandoObject();
                                        foreach (var kvp in msg.id)
                                        {
                                            string key = kvp.Key;
                                            int value = kvp.Value;
                                            ((IDictionary<string, object>)obj)[key] = value;

                                        }
                                        var workflowExec = new workflowExecuter();
                                        var res = await Task.Run(async () => await workflowExec.TriggerWorkflowAsync(configuration.GetConnectionString("NodeRedEndPoint"), obj, action, className,token));
                                    }
                    }
                    
else if (Equals(className, "Workflow_buildsModel")){
                                    if (msg.classString != null)
                                    {
                            //Deserialized Object into Workflow_buildsModel type
                            Workflow_buildsModel workflow_buildsModel = JsonConvert.DeserializeObject<Workflow_buildsModel>(msg.classString);
                        Console.WriteLine(workflow_buildsModel);

                        //Parsed the Object into JSon Object 
                        var obj = JObject.Parse(msg.classString);
                        //Console.WriteLine(obj);
                        //Console.WriteLine(obj[DataAccess.findPrimaryKey(className)]);
                        var workflowExec = new workflowExecuter();
                               var res = await Task.Run(async () => await workflowExec.TriggerWorkflowAsync(configuration.GetConnectionString("NodeRedEndPoint"), workflow_buildsModel, action, className,token));
                                    }
                                    else
                                    {
                                        dynamic obj = new ExpandoObject();
                                        foreach (var kvp in msg.id)
                                        {
                                            string key = kvp.Key;
                                            int value = kvp.Value;
                                            ((IDictionary<string, object>)obj)[key] = value;

                                        }
                                        var workflowExec = new workflowExecuter();
                                        var res = await Task.Run(async () => await workflowExec.TriggerWorkflowAsync(configuration.GetConnectionString("NodeRedEndPoint"), obj, action, className,token));
                                    }
                    }
                    
else if (Equals(className, "Workflow_deploymentsModel")){
                                    if (msg.classString != null)
                                    {
                            //Deserialized Object into Workflow_deploymentsModel type
                            Workflow_deploymentsModel workflow_deploymentsModel = JsonConvert.DeserializeObject<Workflow_deploymentsModel>(msg.classString);
                        Console.WriteLine(workflow_deploymentsModel);

                        //Parsed the Object into JSon Object 
                        var obj = JObject.Parse(msg.classString);
                        //Console.WriteLine(obj);
                        //Console.WriteLine(obj[DataAccess.findPrimaryKey(className)]);
                        var workflowExec = new workflowExecuter();
                               var res = await Task.Run(async () => await workflowExec.TriggerWorkflowAsync(configuration.GetConnectionString("NodeRedEndPoint"), workflow_deploymentsModel, action, className,token));
                                    }
                                    else
                                    {
                                        dynamic obj = new ExpandoObject();
                                        foreach (var kvp in msg.id)
                                        {
                                            string key = kvp.Key;
                                            int value = kvp.Value;
                                            ((IDictionary<string, object>)obj)[key] = value;

                                        }
                                        var workflowExec = new workflowExecuter();
                                        var res = await Task.Run(async () => await workflowExec.TriggerWorkflowAsync(configuration.GetConnectionString("NodeRedEndPoint"), obj, action, className,token));
                                    }
                    }
                    
else if (Equals(className, "Workflow_runsModel")){
                                    if (msg.classString != null)
                                    {
                            //Deserialized Object into Workflow_runsModel type
                            Workflow_runsModel workflow_runsModel = JsonConvert.DeserializeObject<Workflow_runsModel>(msg.classString);
                        Console.WriteLine(workflow_runsModel);

                        //Parsed the Object into JSon Object 
                        var obj = JObject.Parse(msg.classString);
                        //Console.WriteLine(obj);
                        //Console.WriteLine(obj[DataAccess.findPrimaryKey(className)]);
                        var workflowExec = new workflowExecuter();
                               var res = await Task.Run(async () => await workflowExec.TriggerWorkflowAsync(configuration.GetConnectionString("NodeRedEndPoint"), workflow_runsModel, action, className,token));
                                    }
                                    else
                                    {
                                        dynamic obj = new ExpandoObject();
                                        foreach (var kvp in msg.id)
                                        {
                                            string key = kvp.Key;
                                            int value = kvp.Value;
                                            ((IDictionary<string, object>)obj)[key] = value;

                                        }
                                        var workflowExec = new workflowExecuter();
                                        var res = await Task.Run(async () => await workflowExec.TriggerWorkflowAsync(configuration.GetConnectionString("NodeRedEndPoint"), obj, action, className,token));
                                    }
                    }
                    
else if (Equals(className, "Workflow_triggersModel")){
                                    if (msg.classString != null)
                                    {
                            //Deserialized Object into Workflow_triggersModel type
                            Workflow_triggersModel workflow_triggersModel = JsonConvert.DeserializeObject<Workflow_triggersModel>(msg.classString);
                        Console.WriteLine(workflow_triggersModel);

                        //Parsed the Object into JSon Object 
                        var obj = JObject.Parse(msg.classString);
                        //Console.WriteLine(obj);
                        //Console.WriteLine(obj[DataAccess.findPrimaryKey(className)]);
                        var workflowExec = new workflowExecuter();
                               var res = await Task.Run(async () => await workflowExec.TriggerWorkflowAsync(configuration.GetConnectionString("NodeRedEndPoint"), workflow_triggersModel, action, className,token));
                                    }
                                    else
                                    {
                                        dynamic obj = new ExpandoObject();
                                        foreach (var kvp in msg.id)
                                        {
                                            string key = kvp.Key;
                                            int value = kvp.Value;
                                            ((IDictionary<string, object>)obj)[key] = value;

                                        }
                                        var workflowExec = new workflowExecuter();
                                        var res = await Task.Run(async () => await workflowExec.TriggerWorkflowAsync(configuration.GetConnectionString("NodeRedEndPoint"), obj, action, className,token));
                                    }
                    }
                    
else if (Equals(className, "Workflow_trigger_conditionsModel")){
                                    if (msg.classString != null)
                                    {
                            //Deserialized Object into Workflow_trigger_conditionsModel type
                            Workflow_trigger_conditionsModel workflow_trigger_conditionsModel = JsonConvert.DeserializeObject<Workflow_trigger_conditionsModel>(msg.classString);
                        Console.WriteLine(workflow_trigger_conditionsModel);

                        //Parsed the Object into JSon Object 
                        var obj = JObject.Parse(msg.classString);
                        //Console.WriteLine(obj);
                        //Console.WriteLine(obj[DataAccess.findPrimaryKey(className)]);
                        var workflowExec = new workflowExecuter();
                               var res = await Task.Run(async () => await workflowExec.TriggerWorkflowAsync(configuration.GetConnectionString("NodeRedEndPoint"), workflow_trigger_conditionsModel, action, className,token));
                                    }
                                    else
                                    {
                                        dynamic obj = new ExpandoObject();
                                        foreach (var kvp in msg.id)
                                        {
                                            string key = kvp.Key;
                                            int value = kvp.Value;
                                            ((IDictionary<string, object>)obj)[key] = value;

                                        }
                                        var workflowExec = new workflowExecuter();
                                        var res = await Task.Run(async () => await workflowExec.TriggerWorkflowAsync(configuration.GetConnectionString("NodeRedEndPoint"), obj, action, className,token));
                                    }
                    }
                    


                    }
                });
            };


            channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);

            while (true)
            {
                Thread.Sleep(1000);
                ThreadPool.GetAvailableThreads(out var workerThreads, out var ioThreads);
                if (workerThreads == 0 && NumThreads < 100)
                // Increase the number of threads if all threads are busy
                {
                    NumThreads++;
                    ThreadPool.SetMaxThreads(NumThreads, NumThreads);
                }
            }
        }
    }
}
