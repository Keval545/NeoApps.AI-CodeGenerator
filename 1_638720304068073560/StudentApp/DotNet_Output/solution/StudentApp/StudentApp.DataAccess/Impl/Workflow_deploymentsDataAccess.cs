using MySql.Data.MySqlClient;
using StudentApp.DataAccess.Interface;
using StudentApp.Model;
using StudentApp.Utility;
using System.Collections.Generic;
using System;
using System.Globalization;

namespace StudentApp.DataAccess.Impl
{
    public class Workflow_deploymentsDataAccess : IWorkflow_deploymentsDataAccess
    {
        private MySqlDatabaseConnector  mySqlDatabaseConnector { get; set; }
        public Workflow_deploymentsDataAccess(MySqlDatabaseConnector _mySqlDatabaseConnector)
        {
            mySqlDatabaseConnector = _mySqlDatabaseConnector;
        }
		public int GetAllTotalRecordWorkflow_deployments()
        {
            try{
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT count(*) TotalCount FROM workflow_deployments t WHERE t.isActive=1";
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        { 
                            return reader.GetInt32("TotalCount");
                        }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { mySqlDatabaseConnector.CloseConnection(); }
            return 0;
        }
        public int GetAllTotalRecordWorkflow_deploymentsByCreatedBy(string ownername)
        {
            try{
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT count(*) TotalCount FROM workflow_deployments t WHERE t.isActive=1 AND t.createdBy=@ownername";
                    cmd.Parameters.AddWithValue("@ownername", ownername);
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        { 
                            return reader.GetInt32("TotalCount");
                        }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { mySqlDatabaseConnector.CloseConnection(); }
            return 0;
        }
        public int GetSearchTotalRecordWorkflow_deployments(string searchKey)
        {
            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT count(*) TotalCount FROM workflow_deployments t WHERE t.isActive=1 AND t.workflow_deployment_status LIKE CONCAT('%',@SearchKey,'%') OR t.modifiedBy LIKE CONCAT('%',@SearchKey,'%') OR t.createdBy LIKE CONCAT('%',@SearchKey,'%')";
                    cmd.Parameters.AddWithValue("@SearchKey", searchKey);
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        { 
                            return reader.GetInt32("TotalCount");
                        }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { mySqlDatabaseConnector.CloseConnection(); }

            return 0;
        }
        public int GetSearchTotalRecordWorkflow_deploymentsByCreatedBy(string ownername,string searchKey)
        {
            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT count(*) TotalCount FROM workflow_deployments t WHERE t.isActive=1 AND t.createdBy=@ownername AND t.workflow_deployment_status LIKE CONCAT('%',@SearchKey,'%') OR t.modifiedBy LIKE CONCAT('%',@SearchKey,'%') OR t.createdBy LIKE CONCAT('%',@SearchKey,'%')";
                    cmd.Parameters.AddWithValue("@SearchKey", searchKey);
                    cmd.Parameters.AddWithValue("@ownername", ownername);
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        { 
                            return reader.GetInt32("TotalCount");
                        }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { mySqlDatabaseConnector.CloseConnection(); }

            return 0;
        }
        public List<Workflow_deploymentsModel> GetAllWorkflow_deployments(int page=1,int itemsPerPage=100,List<OrderByModel> orderBy = null)
        {
            var ret = new List<Workflow_deploymentsModel>();
			int offset = (page - 1) * itemsPerPage;
            try{
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT  t.* FROM workflow_deployments t  WHERE t.isActive=1 ORDER BY column LIMIT @Offset, @ItemsPerPage";
                    cmd.CommandText = Helper.ConverOrderListToSQL(cmd.CommandText, orderBy);
                    cmd.Parameters.AddWithValue("@Offset", offset);
                    cmd.Parameters.AddWithValue("@ItemsPerPage", itemsPerPage);
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            var t = new Workflow_deploymentsModel()
                            {
                            workflow_deployment_id= reader.GetValue<Int32>("workflow_deployment_id"),
workflow_run_id= reader.GetValue<Int32>("workflow_run_id"),
workflow_deployment_status= reader.GetValue<String>("workflow_deployment_status"),
workflow_deployment_start_time= reader.GetValue<DateTime>("workflow_deployment_start_time").ToString(),
workflow_deployment_end_time= reader.GetValue<DateTime>("workflow_deployment_end_time").ToString(),
modifiedBy= reader.GetValue<String>("modifiedBy"),
createdBy= reader.GetValue<String>("createdBy"),
modifiedAt= reader.GetValue<DateTime>("modifiedAt").ToString(),
createdAt= reader.GetValue<DateTime>("createdAt").ToString(),
isActive= reader.GetValue<SByte>("isActive"),
                            };

                            ret.Add(t);
                        }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { mySqlDatabaseConnector.CloseConnection(); }
            return ret;

        }

        public List<Workflow_deploymentsModel> GetAllWorkflow_deploymentsByCreatedBy(string ownername,int page=1,int itemsPerPage=100,List<OrderByModel> orderBy = null)
        {
            var ret = new List<Workflow_deploymentsModel>();
			int offset = (page - 1) * itemsPerPage;
            try{
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT  t.* FROM workflow_deployments t  WHERE t.isActive=1 AND t.createdBy=@ownername ORDER BY column LIMIT @Offset, @ItemsPerPage";
                    cmd.CommandText = Helper.ConverOrderListToSQL(cmd.CommandText, orderBy);
                    cmd.Parameters.AddWithValue("@Offset", offset);
                    cmd.Parameters.AddWithValue("@ownername", ownername);
                    cmd.Parameters.AddWithValue("@ItemsPerPage", itemsPerPage);
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            var t = new Workflow_deploymentsModel()
                            {
                            workflow_deployment_id= reader.GetValue<Int32>("workflow_deployment_id"),
workflow_run_id= reader.GetValue<Int32>("workflow_run_id"),
workflow_deployment_status= reader.GetValue<String>("workflow_deployment_status"),
workflow_deployment_start_time= reader.GetValue<DateTime>("workflow_deployment_start_time").ToString(),
workflow_deployment_end_time= reader.GetValue<DateTime>("workflow_deployment_end_time").ToString(),
modifiedBy= reader.GetValue<String>("modifiedBy"),
createdBy= reader.GetValue<String>("createdBy"),
modifiedAt= reader.GetValue<DateTime>("modifiedAt").ToString(),
createdAt= reader.GetValue<DateTime>("createdAt").ToString(),
isActive= reader.GetValue<SByte>("isActive"),
                            };

                            ret.Add(t);
                        }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { mySqlDatabaseConnector.CloseConnection(); }
            return ret;

        }
		public List<Workflow_deploymentsModel> SearchWorkflow_deployments(string searchKey, int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null)
        {
            var ret = new List<Workflow_deploymentsModel>();
            int offset = (page - 1) * itemsPerPage;
            try{
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT t.* FROM workflow_deployments t WHERE t.isActive=1 AND t.workflow_deployment_status LIKE CONCAT('%',@SearchKey,'%') OR t.modifiedBy LIKE CONCAT('%',@SearchKey,'%') OR t.createdBy LIKE CONCAT('%',@SearchKey,'%') ORDER BY column LIMIT @Offset, @ItemsPerPage";
                    cmd.CommandText = Helper.ConverOrderListToSQL(cmd.CommandText, orderBy);
                    cmd.Parameters.AddWithValue("@SearchKey", searchKey);
                    cmd.Parameters.AddWithValue("@Offset", offset);
                    cmd.Parameters.AddWithValue("@ItemsPerPage", itemsPerPage);
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            var t = new Workflow_deploymentsModel()
                            {
                            workflow_deployment_id= reader.GetValue<Int32>("workflow_deployment_id"),
workflow_run_id= reader.GetValue<Int32>("workflow_run_id"),
workflow_deployment_status= reader.GetValue<String>("workflow_deployment_status"),
workflow_deployment_start_time= reader.GetValue<DateTime>("workflow_deployment_start_time").ToString(),
workflow_deployment_end_time= reader.GetValue<DateTime>("workflow_deployment_end_time").ToString(),
modifiedBy= reader.GetValue<String>("modifiedBy"),
createdBy= reader.GetValue<String>("createdBy"),
modifiedAt= reader.GetValue<DateTime>("modifiedAt").ToString(),
createdAt= reader.GetValue<DateTime>("createdAt").ToString(),
isActive= reader.GetValue<SByte>("isActive"),
                            };

                            ret.Add(t);
                        }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { mySqlDatabaseConnector.CloseConnection(); }
            return ret;
        }
        public List<Workflow_deploymentsModel> SearchWorkflow_deploymentsByCreatedBy(string ownername,string searchKey, int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null)
        {
            var ret = new List<Workflow_deploymentsModel>();
            int offset = (page - 1) * itemsPerPage;
            try{
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT t.* FROM workflow_deployments t WHERE t.isActive=1 AND t.createdBy=@ownername AND t.workflow_deployment_status LIKE CONCAT('%',@SearchKey,'%') OR t.modifiedBy LIKE CONCAT('%',@SearchKey,'%') OR t.createdBy LIKE CONCAT('%',@SearchKey,'%') ORDER BY column LIMIT @Offset, @ItemsPerPage";
                    cmd.CommandText = Helper.ConverOrderListToSQL(cmd.CommandText, orderBy);
                    cmd.Parameters.AddWithValue("@SearchKey", searchKey);
                    cmd.Parameters.AddWithValue("@ownername", ownername);
                    cmd.Parameters.AddWithValue("@Offset", offset);
                    cmd.Parameters.AddWithValue("@ItemsPerPage", itemsPerPage);
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            var t = new Workflow_deploymentsModel()
                            {
                            workflow_deployment_id= reader.GetValue<Int32>("workflow_deployment_id"),
workflow_run_id= reader.GetValue<Int32>("workflow_run_id"),
workflow_deployment_status= reader.GetValue<String>("workflow_deployment_status"),
workflow_deployment_start_time= reader.GetValue<DateTime>("workflow_deployment_start_time").ToString(),
workflow_deployment_end_time= reader.GetValue<DateTime>("workflow_deployment_end_time").ToString(),
modifiedBy= reader.GetValue<String>("modifiedBy"),
createdBy= reader.GetValue<String>("createdBy"),
modifiedAt= reader.GetValue<DateTime>("modifiedAt").ToString(),
createdAt= reader.GetValue<DateTime>("createdAt").ToString(),
isActive= reader.GetValue<SByte>("isActive"),
                            };

                            ret.Add(t);
                        }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { mySqlDatabaseConnector.CloseConnection(); }
            return ret;
        }
        public Workflow_deploymentsModel GetWorkflow_deploymentsByID(int workflow_deployment_id)
        {
            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT  t.* FROM workflow_deployments t  WHERE t.isActive=1 AND t.workflow_deployment_id= @workflow_deployment_id";
                    cmd.Parameters.AddWithValue("@workflow_deployment_id",workflow_deployment_id);
                    
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            return new Workflow_deploymentsModel()
                            {
                                workflow_deployment_id= reader.GetValue<Int32>("workflow_deployment_id"),
workflow_run_id= reader.GetValue<Int32>("workflow_run_id"),
workflow_deployment_status= reader.GetValue<String>("workflow_deployment_status"),
workflow_deployment_start_time= reader.GetValue<DateTime>("workflow_deployment_start_time").ToString(),
workflow_deployment_end_time= reader.GetValue<DateTime>("workflow_deployment_end_time").ToString(),
modifiedBy= reader.GetValue<String>("modifiedBy"),
createdBy= reader.GetValue<String>("createdBy"),
modifiedAt= reader.GetValue<DateTime>("modifiedAt").ToString(),
createdAt= reader.GetValue<DateTime>("createdAt").ToString(),
isActive= reader.GetValue<SByte>("isActive"),
                            };
                        }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { mySqlDatabaseConnector.CloseConnection(); }
            return null;
        }

        public Workflow_deploymentsModel GetWorkflow_deploymentsByIDByCreatedBy(string ownername,int workflow_deployment_id)
        {

            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT  t.* FROM workflow_deployments t  WHERE t.isActive=1 AND t.createdBy=@ownername AND t.workflow_deployment_id= @workflow_deployment_id";
                    cmd.Parameters.AddWithValue("@workflow_deployment_id",workflow_deployment_id);
                    cmd.Parameters.AddWithValue("@ownername", ownername);
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            return new Workflow_deploymentsModel()
                            {
                                workflow_deployment_id= reader.GetValue<Int32>("workflow_deployment_id"),
workflow_run_id= reader.GetValue<Int32>("workflow_run_id"),
workflow_deployment_status= reader.GetValue<String>("workflow_deployment_status"),
workflow_deployment_start_time= reader.GetValue<DateTime>("workflow_deployment_start_time").ToString(),
workflow_deployment_end_time= reader.GetValue<DateTime>("workflow_deployment_end_time").ToString(),
modifiedBy= reader.GetValue<String>("modifiedBy"),
createdBy= reader.GetValue<String>("createdBy"),
modifiedAt= reader.GetValue<DateTime>("modifiedAt").ToString(),
createdAt= reader.GetValue<DateTime>("createdAt").ToString(),
isActive= reader.GetValue<SByte>("isActive"),
                            };
                        }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { mySqlDatabaseConnector.CloseConnection(); }
            return null;
        }
        

        

        

        public bool UpdateWorkflow_deployments(Workflow_deploymentsModel model)
        {
            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE workflow_deployments SET workflow_deployment_id=@workflow_deployment_id,workflow_run_id=@workflow_run_id,workflow_deployment_status=@workflow_deployment_status,workflow_deployment_start_time=@workflow_deployment_start_time,workflow_deployment_end_time=@workflow_deployment_end_time,modifiedBy=@modifiedBy,modifiedAt=@modifiedAt,isActive=@isActive WHERE workflow_deployment_id=@workflow_deployment_id";
                    cmd.Parameters.AddWithValue("@workflow_deployment_id", model.workflow_deployment_id);
cmd.Parameters.AddWithValue("@workflow_run_id", model.workflow_run_id);
cmd.Parameters.AddWithValue("@workflow_deployment_status", model.workflow_deployment_status);
cmd.Parameters.AddWithValue("@workflow_deployment_start_time", model.workflow_deployment_start_time);
cmd.Parameters.AddWithValue("@workflow_deployment_end_time", model.workflow_deployment_end_time);
cmd.Parameters.AddWithValue("@modifiedBy", model.modifiedBy);
cmd.Parameters.AddWithValue("@modifiedAt", DateTime.UtcNow);
cmd.Parameters.AddWithValue("@isActive", 1);
                    var recs = cmd.ExecuteNonQuery();
                    if (recs > 0)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { mySqlDatabaseConnector.CloseConnection(); }
            return false;
        }

        public long AddWorkflow_deployments(Workflow_deploymentsModel model)
        {
            try
            {
		
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO workflow_deployments (workflow_deployment_id,workflow_run_id,workflow_deployment_status,workflow_deployment_start_time,workflow_deployment_end_time,modifiedBy,createdBy,modifiedAt,createdAt,isActive) Values (@workflow_deployment_id,@workflow_run_id,@workflow_deployment_status,@workflow_deployment_start_time,@workflow_deployment_end_time,@modifiedBy,@createdBy,@modifiedAt,@createdAt,@isActive);";
                    cmd.Parameters.AddWithValue("@workflow_deployment_id", model.workflow_deployment_id);
cmd.Parameters.AddWithValue("@workflow_run_id", model.workflow_run_id);
cmd.Parameters.AddWithValue("@workflow_deployment_status", model.workflow_deployment_status);
cmd.Parameters.AddWithValue("@workflow_deployment_start_time", model.workflow_deployment_start_time);
cmd.Parameters.AddWithValue("@workflow_deployment_end_time", model.workflow_deployment_end_time);
cmd.Parameters.AddWithValue("@modifiedBy", model.modifiedBy);
cmd.Parameters.AddWithValue("@createdBy", model.createdBy);
cmd.Parameters.AddWithValue("@modifiedAt", DateTime.UtcNow);
cmd.Parameters.AddWithValue("@createdAt", DateTime.UtcNow);
cmd.Parameters.AddWithValue("@isActive", 1);
                    var recs = cmd.ExecuteNonQuery();
                    if (recs ==1)
                    {
                        return cmd.LastInsertedId ;
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { mySqlDatabaseConnector.CloseConnection(); }
            return -1;
          
        }

        public bool DeleteWorkflow_deployments(int workflow_deployment_id)
        {
            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE workflow_deployments SET isActive=0 Where workflow_deployment_id=@workflow_deployment_id";
                    cmd.Parameters.AddWithValue("@workflow_deployment_id",workflow_deployment_id);
                    var recs = cmd.ExecuteNonQuery();
                    if (recs > 0)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { mySqlDatabaseConnector.CloseConnection(); }
            return false;
        }
		public List<Workflow_deploymentsModel> FilterWorkflow_deployments(string ownername,List<FilterModel> filterBy, string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy)
        {
            var ret = new List<Workflow_deploymentsModel>();
            int offset = (page - 1) * itemsPerPage;
            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT  t.* FROM Workflow_deployments t {filterColumns} ORDER BY column LIMIT @Offset, @ItemsPerPage ";
                    cmd.CommandText = Helper.ConverOrderListToSQL(cmd.CommandText, orderBy);

                    if (filterBy != null && filterBy.Count > 0)
                    {
                        var whereClause = string.Empty;
                        int paramCount = 0;
                        foreach (var r in filterBy)
                        {
                            if (!string.IsNullOrEmpty(r.ColumnName))
                            {
                                paramCount++;
                                if (!string.IsNullOrEmpty(whereClause))
                                {
                                    whereClause = whereClause + " " + andOr + " ";
                                }
                                whereClause = whereClause + "t." + r.ColumnName + " " + UtilityCommon.ConvertFilterToSQLString(r.ColumnCondition) + " @" + r.ColumnName + paramCount;
                            }
                        }
                        whereClause = whereClause.Trim();
                        cmd.CommandText = cmd.CommandText.Replace("{filterColumns}", "Where t.isActive=1 AND t.createdBy=@ownername AND " + whereClause);
                    }
                    else
                    {
                        cmd.CommandText = cmd.CommandText.Replace("{filterColumns}", "Where t.isActive=1 AND t.createdBy=@ownername");
                    }
                    if (orderBy != null && orderBy.Count > 0)
                    {
                        cmd.CommandText = Helper.ConverOrderListToSQL(cmd.CommandText, orderBy);
                    }
                    cmd.Parameters.AddWithValue("@Offset", offset);
                    cmd.CommandText = cmd.CommandText.Replace("@Offset", $"{offset}");
                    cmd.Parameters.AddWithValue("@ItemsPerPage", itemsPerPage);
                    cmd.CommandText = cmd.CommandText.Replace("@ItemsPerPage", $"{itemsPerPage}");
                    cmd.Parameters.AddWithValue("@ownername", ownername);
                    if (filterBy != null && filterBy.Count > 0)
                    {
                        int paramCount = 0;
                        foreach (var r in filterBy)
                        {
                            paramCount++;
                            if (!string.IsNullOrEmpty(r.ColumnName))
                            {
                                // Console.WriteLine("Before cmd is : " + cmd.CommandText);
                                // whereClause = whereClause.Replace("@" + r.ColumnName + paramCount, r.ColumnValue);
                                // Console.WriteLine("Replace " + "@" + r.ColumnName + paramCount + " With " + r.ColumnValue);
                                cmd.Parameters.AddWithValue("@" + r.ColumnName + paramCount, r.ColumnValue);
                                cmd.CommandText = cmd.CommandText.Replace("@" + r.ColumnName + paramCount, $"'{r.ColumnValue}'");
                            }
                        }
                    }
                    
                    Console.WriteLine("==================================================");
                    Console.WriteLine("SQL Command : " + cmd.CommandText);
                    Console.WriteLine("==================================================");

                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            var t = new Workflow_deploymentsModel()
                            {
                                workflow_deployment_id= reader.GetValue<Int32>("workflow_deployment_id"),
workflow_run_id= reader.GetValue<Int32>("workflow_run_id"),
workflow_deployment_status= reader.GetValue<String>("workflow_deployment_status"),
workflow_deployment_start_time= reader.GetValue<DateTime>("workflow_deployment_start_time").ToString(),
workflow_deployment_end_time= reader.GetValue<DateTime>("workflow_deployment_end_time").ToString(),
modifiedBy= reader.GetValue<String>("modifiedBy"),
createdBy= reader.GetValue<String>("createdBy"),
modifiedAt= reader.GetValue<DateTime>("modifiedAt").ToString(),
createdAt= reader.GetValue<DateTime>("createdAt").ToString(),
isActive= reader.GetValue<SByte>("isActive"),
                            };

                            ret.Add(t);
                        }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { mySqlDatabaseConnector.CloseConnection(); }
            return ret;
        }

        public int GetFilterTotalRecordWorkflow_deployments(string ownername,List<FilterModel> filterBy, string andOr)
        {
            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT count(*) TotalRecord FROM Workflow_deployments t {filterColumns}";
                    // cmd.CommandText = @"{filterCountQuery}";
                    if (filterBy != null && filterBy.Count > 0)
                    {
                        int paramCount = 0;
                        var whereClause = string.Empty;
                        foreach (var r in filterBy)
                        {
                            if (!string.IsNullOrEmpty(r.ColumnName))
                            {
                                paramCount++;
                                if (!string.IsNullOrEmpty(whereClause))
                                {
                                    whereClause = whereClause + " " + andOr + " ";
                                }
                                whereClause = whereClause + "t." + r.ColumnName + " " + UtilityCommon.ConvertFilterToSQLString(r.ColumnCondition) + " @" + r.ColumnName + paramCount;
                                // cmd.Parameters.AddWithValue("@" + r.ColumnName + paramCount, r.ColumnValue);
                                // Console.WriteLine("whereClause is : " + whereClause);
                                // whereClause = whereClause.Replace("@" + r.ColumnName + paramCount, r.ColumnValue);
                                // Console.WriteLine("Replace " + "@" + r.ColumnName + paramCount + " With " + r.ColumnValue);
                            }
                        }
                        whereClause = whereClause.Trim();
                        cmd.CommandText = cmd.CommandText.Replace("{filterColumns}", "Where t.isActive=1 AND t.createdBy=@ownername AND " + whereClause);
                    }
                    else
                    {
                        cmd.CommandText = cmd.CommandText.Replace("{filterColumns}", "Where t.isActive=1 AND t.createdBy=@ownername");
                    }

                    cmd.Parameters.AddWithValue("@ownername", ownername);
                    if (filterBy != null && filterBy.Count > 0)
                    {
                        int paramCount = 0;
                        foreach (var r in filterBy)
                        {
                            paramCount++;
                            if (!string.IsNullOrEmpty(r.ColumnName))
                            {
                                // Console.WriteLine("Before cmd is : " + cmd.CommandText);
                                // whereClause = whereClause.Replace("@" + r.ColumnName + paramCount, r.ColumnValue);
                                // Console.WriteLine("Replace " + "@" + r.ColumnName + paramCount + " With " + r.ColumnValue);
                                cmd.Parameters.AddWithValue("@" + r.ColumnName + paramCount, r.ColumnValue);
                                cmd.CommandText = cmd.CommandText.Replace("@" + r.ColumnName + paramCount, $"'{r.ColumnValue}'");
                            }
                        }
                    }
                    
                    Console.WriteLine("==================================================");
                    Console.WriteLine("SQL Command : " + cmd.CommandText);
                    Console.WriteLine("==================================================");


                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            Console.WriteLine("==================================================");
                            Console.WriteLine("reader.GetInt32('TotalRecord') : " + reader.GetInt32("TotalRecord"));
                            Console.WriteLine("==================================================");

                            return reader.GetInt32("TotalRecord");
                        }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { mySqlDatabaseConnector.CloseConnection(); }
            return 0;
        }
		public bool DeleteMultipleWorkflow_deployments(List<DeleteMultipleModel> deleteParam, string andOr)
        {
            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    MySqlTransaction transaction = connection.BeginTransaction();
                    cmd.Transaction = transaction;
                    cmd.CommandText = @"UPDATE Workflow_deployments SET isActive=0 Where";
                    int count = 0;

                    foreach (var r in deleteParam)
                    {
                        if (count == 0)
                        {
                            cmd.CommandText = cmd.CommandText + " " + r.ColumnName + "=@" + r.ColumnName;
                        }
                        else
                        {
                            cmd.CommandText = cmd.CommandText + " " + andOr + " " + r.ColumnName + "=@" + r.ColumnName;
                        }
                        cmd.Parameters.AddWithValue("@" + r.ColumnName, r.ColumnValue);
                        count++;
                    }
                    var recs = cmd.ExecuteNonQuery();
                    if (recs > 0)
                    {
                        transaction.Commit();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { mySqlDatabaseConnector.CloseConnection(); }
            return false;
        }
    }
}
