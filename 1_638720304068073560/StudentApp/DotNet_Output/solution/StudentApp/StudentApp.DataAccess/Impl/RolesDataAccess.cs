using MySql.Data.MySqlClient;
using StudentApp.DataAccess.Interface;
using StudentApp.Model;
using StudentApp.Utility;
using System.Collections.Generic;
using System;
using System.Globalization;

namespace StudentApp.DataAccess.Impl
{
    public class RolesDataAccess : IRolesDataAccess
    {
        private MySqlDatabaseConnector  mySqlDatabaseConnector { get; set; }
        public RolesDataAccess(MySqlDatabaseConnector _mySqlDatabaseConnector)
        {
            mySqlDatabaseConnector = _mySqlDatabaseConnector;
        }
		public int GetAllTotalRecordRoles()
        {
            try{
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT count(*) TotalCount FROM roles t WHERE t.isActive=1";
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
        public int GetAllTotalRecordRolesByCreatedBy(string ownername)
        {
            try{
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT count(*) TotalCount FROM roles t WHERE t.isActive=1 AND t.createdBy=@ownername";
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
        public int GetSearchTotalRecordRoles(string searchKey)
        {
            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT count(*) TotalCount FROM roles t WHERE t.isActive=1 AND t.role_name LIKE CONCAT('%',@SearchKey,'%') OR t.createdBy LIKE CONCAT('%',@SearchKey,'%') OR t.modifiedBy LIKE CONCAT('%',@SearchKey,'%')";
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
        public int GetSearchTotalRecordRolesByCreatedBy(string ownername,string searchKey)
        {
            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT count(*) TotalCount FROM roles t WHERE t.isActive=1 AND t.createdBy=@ownername AND t.role_name LIKE CONCAT('%',@SearchKey,'%') OR t.createdBy LIKE CONCAT('%',@SearchKey,'%') OR t.modifiedBy LIKE CONCAT('%',@SearchKey,'%')";
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
        public List<RolesModel> GetAllRoles(int page=1,int itemsPerPage=100,List<OrderByModel> orderBy = null)
        {
            var ret = new List<RolesModel>();
			int offset = (page - 1) * itemsPerPage;
            try{
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT  t.* FROM roles t  WHERE t.isActive=1 ORDER BY column LIMIT @Offset, @ItemsPerPage";
                    cmd.CommandText = Helper.ConverOrderListToSQL(cmd.CommandText, orderBy);
                    cmd.Parameters.AddWithValue("@Offset", offset);
                    cmd.Parameters.AddWithValue("@ItemsPerPage", itemsPerPage);
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            var t = new RolesModel()
                            {
                            role_id= reader.GetValue<Int32>("role_id"),
role_name= reader.GetValue<String>("role_name"),
createdBy= reader.GetValue<String>("createdBy"),
modifiedBy= reader.GetValue<String>("modifiedBy"),
createdAt= reader.GetValue<DateTime>("createdAt").ToString(),
modifiedAt= reader.GetValue<DateTime>("modifiedAt").ToString(),
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

        public List<RolesModel> GetAllRolesByCreatedBy(string ownername,int page=1,int itemsPerPage=100,List<OrderByModel> orderBy = null)
        {
            var ret = new List<RolesModel>();
			int offset = (page - 1) * itemsPerPage;
            try{
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT  t.* FROM roles t  WHERE t.isActive=1 AND t.createdBy=@ownername ORDER BY column LIMIT @Offset, @ItemsPerPage";
                    cmd.CommandText = Helper.ConverOrderListToSQL(cmd.CommandText, orderBy);
                    cmd.Parameters.AddWithValue("@Offset", offset);
                    cmd.Parameters.AddWithValue("@ownername", ownername);
                    cmd.Parameters.AddWithValue("@ItemsPerPage", itemsPerPage);
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            var t = new RolesModel()
                            {
                            role_id= reader.GetValue<Int32>("role_id"),
role_name= reader.GetValue<String>("role_name"),
createdBy= reader.GetValue<String>("createdBy"),
modifiedBy= reader.GetValue<String>("modifiedBy"),
createdAt= reader.GetValue<DateTime>("createdAt").ToString(),
modifiedAt= reader.GetValue<DateTime>("modifiedAt").ToString(),
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
		public List<RolesModel> SearchRoles(string searchKey, int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null)
        {
            var ret = new List<RolesModel>();
            int offset = (page - 1) * itemsPerPage;
            try{
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT t.* FROM roles t WHERE t.isActive=1 AND t.role_name LIKE CONCAT('%',@SearchKey,'%') OR t.createdBy LIKE CONCAT('%',@SearchKey,'%') OR t.modifiedBy LIKE CONCAT('%',@SearchKey,'%') ORDER BY column LIMIT @Offset, @ItemsPerPage";
                    cmd.CommandText = Helper.ConverOrderListToSQL(cmd.CommandText, orderBy);
                    cmd.Parameters.AddWithValue("@SearchKey", searchKey);
                    cmd.Parameters.AddWithValue("@Offset", offset);
                    cmd.Parameters.AddWithValue("@ItemsPerPage", itemsPerPage);
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            var t = new RolesModel()
                            {
                            role_id= reader.GetValue<Int32>("role_id"),
role_name= reader.GetValue<String>("role_name"),
createdBy= reader.GetValue<String>("createdBy"),
modifiedBy= reader.GetValue<String>("modifiedBy"),
createdAt= reader.GetValue<DateTime>("createdAt").ToString(),
modifiedAt= reader.GetValue<DateTime>("modifiedAt").ToString(),
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
        public List<RolesModel> SearchRolesByCreatedBy(string ownername,string searchKey, int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null)
        {
            var ret = new List<RolesModel>();
            int offset = (page - 1) * itemsPerPage;
            try{
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT t.* FROM roles t WHERE t.isActive=1 AND t.createdBy=@ownername AND t.role_name LIKE CONCAT('%',@SearchKey,'%') OR t.createdBy LIKE CONCAT('%',@SearchKey,'%') OR t.modifiedBy LIKE CONCAT('%',@SearchKey,'%') ORDER BY column LIMIT @Offset, @ItemsPerPage";
                    cmd.CommandText = Helper.ConverOrderListToSQL(cmd.CommandText, orderBy);
                    cmd.Parameters.AddWithValue("@SearchKey", searchKey);
                    cmd.Parameters.AddWithValue("@ownername", ownername);
                    cmd.Parameters.AddWithValue("@Offset", offset);
                    cmd.Parameters.AddWithValue("@ItemsPerPage", itemsPerPage);
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            var t = new RolesModel()
                            {
                            role_id= reader.GetValue<Int32>("role_id"),
role_name= reader.GetValue<String>("role_name"),
createdBy= reader.GetValue<String>("createdBy"),
modifiedBy= reader.GetValue<String>("modifiedBy"),
createdAt= reader.GetValue<DateTime>("createdAt").ToString(),
modifiedAt= reader.GetValue<DateTime>("modifiedAt").ToString(),
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
        public RolesModel GetRolesByID(int role_id)
        {
            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT  t.* FROM roles t  WHERE t.isActive=1 AND t.role_id= @role_id";
                    cmd.Parameters.AddWithValue("@role_id",role_id);
                    
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            return new RolesModel()
                            {
                                role_id= reader.GetValue<Int32>("role_id"),
role_name= reader.GetValue<String>("role_name"),
createdBy= reader.GetValue<String>("createdBy"),
modifiedBy= reader.GetValue<String>("modifiedBy"),
createdAt= reader.GetValue<DateTime>("createdAt").ToString(),
modifiedAt= reader.GetValue<DateTime>("modifiedAt").ToString(),
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

        public RolesModel GetRolesByIDByCreatedBy(string ownername,int role_id)
        {

            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT  t.* FROM roles t  WHERE t.isActive=1 AND t.createdBy=@ownername AND t.role_id= @role_id";
                    cmd.Parameters.AddWithValue("@role_id",role_id);
                    cmd.Parameters.AddWithValue("@ownername", ownername);
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            return new RolesModel()
                            {
                                role_id= reader.GetValue<Int32>("role_id"),
role_name= reader.GetValue<String>("role_name"),
createdBy= reader.GetValue<String>("createdBy"),
modifiedBy= reader.GetValue<String>("modifiedBy"),
createdAt= reader.GetValue<DateTime>("createdAt").ToString(),
modifiedAt= reader.GetValue<DateTime>("modifiedAt").ToString(),
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
        

        

        

        public bool UpdateRoles(RolesModel model)
        {
            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE roles SET role_id=@role_id,role_name=@role_name,modifiedBy=@modifiedBy,modifiedAt=@modifiedAt,isActive=@isActive WHERE role_id=@role_id";
                    cmd.Parameters.AddWithValue("@role_id", model.role_id);
cmd.Parameters.AddWithValue("@role_name", model.role_name);
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

        public long AddRoles(RolesModel model)
        {
            try
            {
		
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO roles (role_id,role_name,createdBy,modifiedBy,createdAt,modifiedAt,isActive) Values (@role_id,@role_name,@createdBy,@modifiedBy,@createdAt,@modifiedAt,@isActive);";
                    cmd.Parameters.AddWithValue("@role_id", model.role_id);
cmd.Parameters.AddWithValue("@role_name", model.role_name);
cmd.Parameters.AddWithValue("@createdBy", model.createdBy);
cmd.Parameters.AddWithValue("@modifiedBy", model.modifiedBy);
cmd.Parameters.AddWithValue("@createdAt", DateTime.UtcNow);
cmd.Parameters.AddWithValue("@modifiedAt", DateTime.UtcNow);
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

        public bool DeleteRoles(int role_id)
        {
            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE roles SET isActive=0 Where role_id=@role_id";
                    cmd.Parameters.AddWithValue("@role_id",role_id);
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
		public List<RolesModel> FilterRoles(string ownername,List<FilterModel> filterBy, string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy)
        {
            var ret = new List<RolesModel>();
            int offset = (page - 1) * itemsPerPage;
            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT  t.* FROM Roles t {filterColumns} ORDER BY column LIMIT @Offset, @ItemsPerPage ";
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
                            var t = new RolesModel()
                            {
                                role_id= reader.GetValue<Int32>("role_id"),
role_name= reader.GetValue<String>("role_name"),
createdBy= reader.GetValue<String>("createdBy"),
modifiedBy= reader.GetValue<String>("modifiedBy"),
createdAt= reader.GetValue<DateTime>("createdAt").ToString(),
modifiedAt= reader.GetValue<DateTime>("modifiedAt").ToString(),
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

        public int GetFilterTotalRecordRoles(string ownername,List<FilterModel> filterBy, string andOr)
        {
            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT count(*) TotalRecord FROM Roles t {filterColumns}";
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
		public bool DeleteMultipleRoles(List<DeleteMultipleModel> deleteParam, string andOr)
        {
            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    MySqlTransaction transaction = connection.BeginTransaction();
                    cmd.Transaction = transaction;
                    cmd.CommandText = @"UPDATE Roles SET isActive=0 Where";
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
