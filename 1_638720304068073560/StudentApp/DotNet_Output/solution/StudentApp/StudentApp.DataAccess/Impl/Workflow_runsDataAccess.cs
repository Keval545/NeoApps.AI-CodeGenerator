using MySql.Data.MySqlClient;
using StudentApp.DataAccess.Interface;
using StudentApp.Model;
using StudentApp.Utility;
using System.Collections.Generic;
using System;
using System.Globalization;

namespace StudentApp.DataAccess.Impl
{
    public class Workflow_runsDataAccess : IWorkflow_runsDataAccess
    {
        private MySqlDatabaseConnector  mySqlDatabaseConnector { get; set; }
        public Workflow_runsDataAccess(MySqlDatabaseConnector _mySqlDatabaseConnector)
        {
            mySqlDatabaseConnector = _mySqlDatabaseConnector;
        }
		public int GetAllTotalRecordWorkflow_runs()
        {
            try{
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT count(*) TotalCount FROM workflow_runs t WHERE t.isActive=1";
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
        public int GetAllTotalRecordWorkflow_runsByCreatedBy(string ownername)
        {
            try{
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT count(*) TotalCount FROM workflow_runs t WHERE t.isActive=1 AND t.createdBy=@ownername";
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
        public int GetSearchTotalRecordWorkflow_runs(string searchKey)
        {
            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT count(*) TotalCount FROM workflow_runs t WHERE t.isActive=1 AND t.workflow_run_status LIKE CONCAT('%',@SearchKey,'%') OR t.modifiedBy LIKE CONCAT('%',@SearchKey,'%') OR t.createdBy LIKE CONCAT('%',@SearchKey,'%')";
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
        public int GetSearchTotalRecordWorkflow_runsByCreatedBy(string ownername,string searchKey)
        {
            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT count(*) TotalCount FROM workflow_runs t WHERE t.isActive=1 AND t.createdBy=@ownername AND t.workflow_run_status LIKE CONCAT('%',@SearchKey,'%') OR t.modifiedBy LIKE CONCAT('%',@SearchKey,'%') OR t.createdBy LIKE CONCAT('%',@SearchKey,'%')";
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
        public List<Workflow_runsModel> GetAllWorkflow_runs(int page=1,int itemsPerPage=100,List<OrderByModel> orderBy = null)
        {
            var ret = new List<Workflow_runsModel>();
			int offset = (page - 1) * itemsPerPage;
            try{
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT  t.* FROM workflow_runs t  WHERE t.isActive=1 ORDER BY column LIMIT @Offset, @ItemsPerPage";
                    cmd.CommandText = Helper.ConverOrderListToSQL(cmd.CommandText, orderBy);
                    cmd.Parameters.AddWithValue("@Offset", offset);
                    cmd.Parameters.AddWithValue("@ItemsPerPage", itemsPerPage);
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            var t = new Workflow_runsModel()
                            {
                            workflow_run_id= reader.GetValue<Int32>("workflow_run_id"),
workflow_build_id= reader.GetValue<Int32>("workflow_build_id"),
workflow_run_status= reader.GetValue<String>("workflow_run_status"),
workflow_run_start_time= reader.GetValue<DateTime>("workflow_run_start_time").ToString(),
workflow_run_end_time= reader.GetValue<DateTime>("workflow_run_end_time").ToString(),
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

        public List<Workflow_runsModel> GetAllWorkflow_runsByCreatedBy(string ownername,int page=1,int itemsPerPage=100,List<OrderByModel> orderBy = null)
        {
            var ret = new List<Workflow_runsModel>();
			int offset = (page - 1) * itemsPerPage;
            try{
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT  t.* FROM workflow_runs t  WHERE t.isActive=1 AND t.createdBy=@ownername ORDER BY column LIMIT @Offset, @ItemsPerPage";
                    cmd.CommandText = Helper.ConverOrderListToSQL(cmd.CommandText, orderBy);
                    cmd.Parameters.AddWithValue("@Offset", offset);
                    cmd.Parameters.AddWithValue("@ownername", ownername);
                    cmd.Parameters.AddWithValue("@ItemsPerPage", itemsPerPage);
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            var t = new Workflow_runsModel()
                            {
                            workflow_run_id= reader.GetValue<Int32>("workflow_run_id"),
workflow_build_id= reader.GetValue<Int32>("workflow_build_id"),
workflow_run_status= reader.GetValue<String>("workflow_run_status"),
workflow_run_start_time= reader.GetValue<DateTime>("workflow_run_start_time").ToString(),
workflow_run_end_time= reader.GetValue<DateTime>("workflow_run_end_time").ToString(),
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
		public List<Workflow_runsModel> SearchWorkflow_runs(string searchKey, int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null)
        {
            var ret = new List<Workflow_runsModel>();
            int offset = (page - 1) * itemsPerPage;
            try{
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT t.* FROM workflow_runs t WHERE t.isActive=1 AND t.workflow_run_status LIKE CONCAT('%',@SearchKey,'%') OR t.modifiedBy LIKE CONCAT('%',@SearchKey,'%') OR t.createdBy LIKE CONCAT('%',@SearchKey,'%') ORDER BY column LIMIT @Offset, @ItemsPerPage";
                    cmd.CommandText = Helper.ConverOrderListToSQL(cmd.CommandText, orderBy);
                    cmd.Parameters.AddWithValue("@SearchKey", searchKey);
                    cmd.Parameters.AddWithValue("@Offset", offset);
                    cmd.Parameters.AddWithValue("@ItemsPerPage", itemsPerPage);
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            var t = new Workflow_runsModel()
                            {
                            workflow_run_id= reader.GetValue<Int32>("workflow_run_id"),
workflow_build_id= reader.GetValue<Int32>("workflow_build_id"),
workflow_run_status= reader.GetValue<String>("workflow_run_status"),
workflow_run_start_time= reader.GetValue<DateTime>("workflow_run_start_time").ToString(),
workflow_run_end_time= reader.GetValue<DateTime>("workflow_run_end_time").ToString(),
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
        public List<Workflow_runsModel> SearchWorkflow_runsByCreatedBy(string ownername,string searchKey, int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null)
        {
            var ret = new List<Workflow_runsModel>();
            int offset = (page - 1) * itemsPerPage;
            try{
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT t.* FROM workflow_runs t WHERE t.isActive=1 AND t.createdBy=@ownername AND t.workflow_run_status LIKE CONCAT('%',@SearchKey,'%') OR t.modifiedBy LIKE CONCAT('%',@SearchKey,'%') OR t.createdBy LIKE CONCAT('%',@SearchKey,'%') ORDER BY column LIMIT @Offset, @ItemsPerPage";
                    cmd.CommandText = Helper.ConverOrderListToSQL(cmd.CommandText, orderBy);
                    cmd.Parameters.AddWithValue("@SearchKey", searchKey);
                    cmd.Parameters.AddWithValue("@ownername", ownername);
                    cmd.Parameters.AddWithValue("@Offset", offset);
                    cmd.Parameters.AddWithValue("@ItemsPerPage", itemsPerPage);
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            var t = new Workflow_runsModel()
                            {
                            workflow_run_id= reader.GetValue<Int32>("workflow_run_id"),
workflow_build_id= reader.GetValue<Int32>("workflow_build_id"),
workflow_run_status= reader.GetValue<String>("workflow_run_status"),
workflow_run_start_time= reader.GetValue<DateTime>("workflow_run_start_time").ToString(),
workflow_run_end_time= reader.GetValue<DateTime>("workflow_run_end_time").ToString(),
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
        public Workflow_runsModel GetWorkflow_runsByID(int workflow_run_id)
        {
            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT  t.* FROM workflow_runs t  WHERE t.isActive=1 AND t.workflow_run_id= @workflow_run_id";
                    cmd.Parameters.AddWithValue("@workflow_run_id",workflow_run_id);
                    
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            return new Workflow_runsModel()
                            {
                                workflow_run_id= reader.GetValue<Int32>("workflow_run_id"),
workflow_build_id= reader.GetValue<Int32>("workflow_build_id"),
workflow_run_status= reader.GetValue<String>("workflow_run_status"),
workflow_run_start_time= reader.GetValue<DateTime>("workflow_run_start_time").ToString(),
workflow_run_end_time= reader.GetValue<DateTime>("workflow_run_end_time").ToString(),
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

        public Workflow_runsModel GetWorkflow_runsByIDByCreatedBy(string ownername,int workflow_run_id)
        {

            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT  t.* FROM workflow_runs t  WHERE t.isActive=1 AND t.createdBy=@ownername AND t.workflow_run_id= @workflow_run_id";
                    cmd.Parameters.AddWithValue("@workflow_run_id",workflow_run_id);
                    cmd.Parameters.AddWithValue("@ownername", ownername);
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            return new Workflow_runsModel()
                            {
                                workflow_run_id= reader.GetValue<Int32>("workflow_run_id"),
workflow_build_id= reader.GetValue<Int32>("workflow_build_id"),
workflow_run_status= reader.GetValue<String>("workflow_run_status"),
workflow_run_start_time= reader.GetValue<DateTime>("workflow_run_start_time").ToString(),
workflow_run_end_time= reader.GetValue<DateTime>("workflow_run_end_time").ToString(),
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
        

        

        

        public bool UpdateWorkflow_runs(Workflow_runsModel model)
        {
            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE workflow_runs SET workflow_run_id=@workflow_run_id,workflow_build_id=@workflow_build_id,workflow_run_status=@workflow_run_status,workflow_run_start_time=@workflow_run_start_time,workflow_run_end_time=@workflow_run_end_time,modifiedBy=@modifiedBy,modifiedAt=@modifiedAt,isActive=@isActive WHERE workflow_run_id=@workflow_run_id";
                    cmd.Parameters.AddWithValue("@workflow_run_id", model.workflow_run_id);
cmd.Parameters.AddWithValue("@workflow_build_id", model.workflow_build_id);
cmd.Parameters.AddWithValue("@workflow_run_status", model.workflow_run_status);
cmd.Parameters.AddWithValue("@workflow_run_start_time", model.workflow_run_start_time);
cmd.Parameters.AddWithValue("@workflow_run_end_time", model.workflow_run_end_time);
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

        public long AddWorkflow_runs(Workflow_runsModel model)
        {
            try
            {
		
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO workflow_runs (workflow_run_id,workflow_build_id,workflow_run_status,workflow_run_start_time,workflow_run_end_time,modifiedBy,createdBy,modifiedAt,createdAt,isActive) Values (@workflow_run_id,@workflow_build_id,@workflow_run_status,@workflow_run_start_time,@workflow_run_end_time,@modifiedBy,@createdBy,@modifiedAt,@createdAt,@isActive);";
                    cmd.Parameters.AddWithValue("@workflow_run_id", model.workflow_run_id);
cmd.Parameters.AddWithValue("@workflow_build_id", model.workflow_build_id);
cmd.Parameters.AddWithValue("@workflow_run_status", model.workflow_run_status);
cmd.Parameters.AddWithValue("@workflow_run_start_time", model.workflow_run_start_time);
cmd.Parameters.AddWithValue("@workflow_run_end_time", model.workflow_run_end_time);
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

        public bool DeleteWorkflow_runs(int workflow_run_id)
        {
            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE workflow_runs SET isActive=0 Where workflow_run_id=@workflow_run_id";
                    cmd.Parameters.AddWithValue("@workflow_run_id",workflow_run_id);
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
		public List<Workflow_runsModel> FilterWorkflow_runs(string ownername,List<FilterModel> filterBy, string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy)
        {
            var ret = new List<Workflow_runsModel>();
            int offset = (page - 1) * itemsPerPage;
            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT  t.* FROM Workflow_runs t {filterColumns} ORDER BY column LIMIT @Offset, @ItemsPerPage ";
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
                            var t = new Workflow_runsModel()
                            {
                                workflow_run_id= reader.GetValue<Int32>("workflow_run_id"),
workflow_build_id= reader.GetValue<Int32>("workflow_build_id"),
workflow_run_status= reader.GetValue<String>("workflow_run_status"),
workflow_run_start_time= reader.GetValue<DateTime>("workflow_run_start_time").ToString(),
workflow_run_end_time= reader.GetValue<DateTime>("workflow_run_end_time").ToString(),
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

        public int GetFilterTotalRecordWorkflow_runs(string ownername,List<FilterModel> filterBy, string andOr)
        {
            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT count(*) TotalRecord FROM Workflow_runs t {filterColumns}";
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
		public bool DeleteMultipleWorkflow_runs(List<DeleteMultipleModel> deleteParam, string andOr)
        {
            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    MySqlTransaction transaction = connection.BeginTransaction();
                    cmd.Transaction = transaction;
                    cmd.CommandText = @"UPDATE Workflow_runs SET isActive=0 Where";
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
