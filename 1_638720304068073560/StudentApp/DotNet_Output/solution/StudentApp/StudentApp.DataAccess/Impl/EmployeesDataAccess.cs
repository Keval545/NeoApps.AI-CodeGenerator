using MySql.Data.MySqlClient;
using StudentApp.DataAccess.Interface;
using StudentApp.Model;
using StudentApp.Utility;
using System.Collections.Generic;
using System;
using System.Globalization;

namespace StudentApp.DataAccess.Impl
{
    public class EmployeesDataAccess : IEmployeesDataAccess
    {
        private MySqlDatabaseConnector  mySqlDatabaseConnector { get; set; }
        public EmployeesDataAccess(MySqlDatabaseConnector _mySqlDatabaseConnector)
        {
            mySqlDatabaseConnector = _mySqlDatabaseConnector;
        }
		public int GetAllTotalRecordEmployees()
        {
            try{
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT count(*) TotalCount FROM employees t WHERE t.isActive=1";
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
        public int GetAllTotalRecordEmployeesByCreatedBy(string ownername)
        {
            try{
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT count(*) TotalCount FROM employees t WHERE t.isActive=1 AND t.createdBy=@ownername";
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
        public int GetSearchTotalRecordEmployees(string searchKey)
        {
            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT count(*) TotalCount FROM employees t WHERE t.isActive=1 AND t.first_name LIKE CONCAT('%',@SearchKey,'%') OR t.last_name LIKE CONCAT('%',@SearchKey,'%') OR t.email LIKE CONCAT('%',@SearchKey,'%') OR t.phone_number LIKE CONCAT('%',@SearchKey,'%') OR t.department LIKE CONCAT('%',@SearchKey,'%') OR t.createdBy LIKE CONCAT('%',@SearchKey,'%') OR t.modifiedBy LIKE CONCAT('%',@SearchKey,'%')";
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
        public int GetSearchTotalRecordEmployeesByCreatedBy(string ownername,string searchKey)
        {
            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT count(*) TotalCount FROM employees t WHERE t.isActive=1 AND t.createdBy=@ownername AND t.first_name LIKE CONCAT('%',@SearchKey,'%') OR t.last_name LIKE CONCAT('%',@SearchKey,'%') OR t.email LIKE CONCAT('%',@SearchKey,'%') OR t.phone_number LIKE CONCAT('%',@SearchKey,'%') OR t.department LIKE CONCAT('%',@SearchKey,'%') OR t.createdBy LIKE CONCAT('%',@SearchKey,'%') OR t.modifiedBy LIKE CONCAT('%',@SearchKey,'%')";
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
        public List<EmployeesModel> GetAllEmployees(int page=1,int itemsPerPage=100,List<OrderByModel> orderBy = null)
        {
            var ret = new List<EmployeesModel>();
			int offset = (page - 1) * itemsPerPage;
            try{
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT  t.* FROM employees t  WHERE t.isActive=1 ORDER BY column LIMIT @Offset, @ItemsPerPage";
                    cmd.CommandText = Helper.ConverOrderListToSQL(cmd.CommandText, orderBy);
                    cmd.Parameters.AddWithValue("@Offset", offset);
                    cmd.Parameters.AddWithValue("@ItemsPerPage", itemsPerPage);
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            var t = new EmployeesModel()
                            {
                            employee_id= reader.GetValue<Int32>("employee_id"),
first_name= reader.GetValue<String>("first_name"),
last_name= reader.GetValue<String>("last_name"),
email= reader.GetValue<String>("email"),
phone_number= reader.GetValue<String>("phone_number"),
department= reader.GetValue<String>("department"),
isActive= reader.GetValue<SByte>("isActive"),
createdBy= reader.GetValue<String>("createdBy"),
modifiedBy= reader.GetValue<String>("modifiedBy"),
createdAt= reader.GetValue<DateTime>("createdAt").ToString(),
modifiedAt= reader.GetValue<DateTime>("modifiedAt").ToString(),
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

        public List<EmployeesModel> GetAllEmployeesByCreatedBy(string ownername,int page=1,int itemsPerPage=100,List<OrderByModel> orderBy = null)
        {
            var ret = new List<EmployeesModel>();
			int offset = (page - 1) * itemsPerPage;
            try{
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT  t.* FROM employees t  WHERE t.isActive=1 AND t.createdBy=@ownername ORDER BY column LIMIT @Offset, @ItemsPerPage";
                    cmd.CommandText = Helper.ConverOrderListToSQL(cmd.CommandText, orderBy);
                    cmd.Parameters.AddWithValue("@Offset", offset);
                    cmd.Parameters.AddWithValue("@ownername", ownername);
                    cmd.Parameters.AddWithValue("@ItemsPerPage", itemsPerPage);
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            var t = new EmployeesModel()
                            {
                            employee_id= reader.GetValue<Int32>("employee_id"),
first_name= reader.GetValue<String>("first_name"),
last_name= reader.GetValue<String>("last_name"),
email= reader.GetValue<String>("email"),
phone_number= reader.GetValue<String>("phone_number"),
department= reader.GetValue<String>("department"),
isActive= reader.GetValue<SByte>("isActive"),
createdBy= reader.GetValue<String>("createdBy"),
modifiedBy= reader.GetValue<String>("modifiedBy"),
createdAt= reader.GetValue<DateTime>("createdAt").ToString(),
modifiedAt= reader.GetValue<DateTime>("modifiedAt").ToString(),
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
		public List<EmployeesModel> SearchEmployees(string searchKey, int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null)
        {
            var ret = new List<EmployeesModel>();
            int offset = (page - 1) * itemsPerPage;
            try{
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT t.* FROM employees t WHERE t.isActive=1 AND t.first_name LIKE CONCAT('%',@SearchKey,'%') OR t.last_name LIKE CONCAT('%',@SearchKey,'%') OR t.email LIKE CONCAT('%',@SearchKey,'%') OR t.phone_number LIKE CONCAT('%',@SearchKey,'%') OR t.department LIKE CONCAT('%',@SearchKey,'%') OR t.createdBy LIKE CONCAT('%',@SearchKey,'%') OR t.modifiedBy LIKE CONCAT('%',@SearchKey,'%') ORDER BY column LIMIT @Offset, @ItemsPerPage";
                    cmd.CommandText = Helper.ConverOrderListToSQL(cmd.CommandText, orderBy);
                    cmd.Parameters.AddWithValue("@SearchKey", searchKey);
                    cmd.Parameters.AddWithValue("@Offset", offset);
                    cmd.Parameters.AddWithValue("@ItemsPerPage", itemsPerPage);
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            var t = new EmployeesModel()
                            {
                            employee_id= reader.GetValue<Int32>("employee_id"),
first_name= reader.GetValue<String>("first_name"),
last_name= reader.GetValue<String>("last_name"),
email= reader.GetValue<String>("email"),
phone_number= reader.GetValue<String>("phone_number"),
department= reader.GetValue<String>("department"),
isActive= reader.GetValue<SByte>("isActive"),
createdBy= reader.GetValue<String>("createdBy"),
modifiedBy= reader.GetValue<String>("modifiedBy"),
createdAt= reader.GetValue<DateTime>("createdAt").ToString(),
modifiedAt= reader.GetValue<DateTime>("modifiedAt").ToString(),
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
        public List<EmployeesModel> SearchEmployeesByCreatedBy(string ownername,string searchKey, int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null)
        {
            var ret = new List<EmployeesModel>();
            int offset = (page - 1) * itemsPerPage;
            try{
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT t.* FROM employees t WHERE t.isActive=1 AND t.createdBy=@ownername AND t.first_name LIKE CONCAT('%',@SearchKey,'%') OR t.last_name LIKE CONCAT('%',@SearchKey,'%') OR t.email LIKE CONCAT('%',@SearchKey,'%') OR t.phone_number LIKE CONCAT('%',@SearchKey,'%') OR t.department LIKE CONCAT('%',@SearchKey,'%') OR t.createdBy LIKE CONCAT('%',@SearchKey,'%') OR t.modifiedBy LIKE CONCAT('%',@SearchKey,'%') ORDER BY column LIMIT @Offset, @ItemsPerPage";
                    cmd.CommandText = Helper.ConverOrderListToSQL(cmd.CommandText, orderBy);
                    cmd.Parameters.AddWithValue("@SearchKey", searchKey);
                    cmd.Parameters.AddWithValue("@ownername", ownername);
                    cmd.Parameters.AddWithValue("@Offset", offset);
                    cmd.Parameters.AddWithValue("@ItemsPerPage", itemsPerPage);
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            var t = new EmployeesModel()
                            {
                            employee_id= reader.GetValue<Int32>("employee_id"),
first_name= reader.GetValue<String>("first_name"),
last_name= reader.GetValue<String>("last_name"),
email= reader.GetValue<String>("email"),
phone_number= reader.GetValue<String>("phone_number"),
department= reader.GetValue<String>("department"),
isActive= reader.GetValue<SByte>("isActive"),
createdBy= reader.GetValue<String>("createdBy"),
modifiedBy= reader.GetValue<String>("modifiedBy"),
createdAt= reader.GetValue<DateTime>("createdAt").ToString(),
modifiedAt= reader.GetValue<DateTime>("modifiedAt").ToString(),
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
        public EmployeesModel GetEmployeesByID(int employee_id)
        {
            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT  t.* FROM employees t  WHERE t.isActive=1 AND t.employee_id= @employee_id";
                    cmd.Parameters.AddWithValue("@employee_id",employee_id);
                    
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            return new EmployeesModel()
                            {
                                employee_id= reader.GetValue<Int32>("employee_id"),
first_name= reader.GetValue<String>("first_name"),
last_name= reader.GetValue<String>("last_name"),
email= reader.GetValue<String>("email"),
phone_number= reader.GetValue<String>("phone_number"),
department= reader.GetValue<String>("department"),
isActive= reader.GetValue<SByte>("isActive"),
createdBy= reader.GetValue<String>("createdBy"),
modifiedBy= reader.GetValue<String>("modifiedBy"),
createdAt= reader.GetValue<DateTime>("createdAt").ToString(),
modifiedAt= reader.GetValue<DateTime>("modifiedAt").ToString(),
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

        public EmployeesModel GetEmployeesByIDByCreatedBy(string ownername,int employee_id)
        {

            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT  t.* FROM employees t  WHERE t.isActive=1 AND t.createdBy=@ownername AND t.employee_id= @employee_id";
                    cmd.Parameters.AddWithValue("@employee_id",employee_id);
                    cmd.Parameters.AddWithValue("@ownername", ownername);
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            return new EmployeesModel()
                            {
                                employee_id= reader.GetValue<Int32>("employee_id"),
first_name= reader.GetValue<String>("first_name"),
last_name= reader.GetValue<String>("last_name"),
email= reader.GetValue<String>("email"),
phone_number= reader.GetValue<String>("phone_number"),
department= reader.GetValue<String>("department"),
isActive= reader.GetValue<SByte>("isActive"),
createdBy= reader.GetValue<String>("createdBy"),
modifiedBy= reader.GetValue<String>("modifiedBy"),
createdAt= reader.GetValue<DateTime>("createdAt").ToString(),
modifiedAt= reader.GetValue<DateTime>("modifiedAt").ToString(),
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
        

        

        

        public bool UpdateEmployees(EmployeesModel model)
        {
            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE employees SET employee_id=@employee_id,first_name=@first_name,last_name=@last_name,email=@email,phone_number=@phone_number,department=@department,isActive=@isActive,modifiedBy=@modifiedBy,modifiedAt=@modifiedAt WHERE employee_id=@employee_id";
                    cmd.Parameters.AddWithValue("@employee_id", model.employee_id);
cmd.Parameters.AddWithValue("@first_name", model.first_name);
cmd.Parameters.AddWithValue("@last_name", model.last_name);
cmd.Parameters.AddWithValue("@email", model.email);
cmd.Parameters.AddWithValue("@phone_number", model.phone_number);
cmd.Parameters.AddWithValue("@department", model.department);
cmd.Parameters.AddWithValue("@isActive", 1);
cmd.Parameters.AddWithValue("@modifiedBy", model.modifiedBy);
cmd.Parameters.AddWithValue("@modifiedAt", DateTime.UtcNow);
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

        public long AddEmployees(EmployeesModel model)
        {
            try
            {
		
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO employees (employee_id,first_name,last_name,email,phone_number,department,isActive,createdBy,modifiedBy,createdAt,modifiedAt) Values (@employee_id,@first_name,@last_name,@email,@phone_number,@department,@isActive,@createdBy,@modifiedBy,@createdAt,@modifiedAt);";
                    cmd.Parameters.AddWithValue("@employee_id", model.employee_id);
cmd.Parameters.AddWithValue("@first_name", model.first_name);
cmd.Parameters.AddWithValue("@last_name", model.last_name);
cmd.Parameters.AddWithValue("@email", model.email);
cmd.Parameters.AddWithValue("@phone_number", model.phone_number);
cmd.Parameters.AddWithValue("@department", model.department);
cmd.Parameters.AddWithValue("@isActive", 1);
cmd.Parameters.AddWithValue("@createdBy", model.createdBy);
cmd.Parameters.AddWithValue("@modifiedBy", model.modifiedBy);
cmd.Parameters.AddWithValue("@createdAt", DateTime.UtcNow);
cmd.Parameters.AddWithValue("@modifiedAt", DateTime.UtcNow);
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

        public bool DeleteEmployees(int employee_id)
        {
            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE employees SET isActive=0 Where employee_id=@employee_id";
                    cmd.Parameters.AddWithValue("@employee_id",employee_id);
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
		public List<EmployeesModel> FilterEmployees(string ownername,List<FilterModel> filterBy, string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy)
        {
            var ret = new List<EmployeesModel>();
            int offset = (page - 1) * itemsPerPage;
            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT  t.* FROM Employees t {filterColumns} ORDER BY column LIMIT @Offset, @ItemsPerPage ";
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
                            var t = new EmployeesModel()
                            {
                                employee_id= reader.GetValue<Int32>("employee_id"),
first_name= reader.GetValue<String>("first_name"),
last_name= reader.GetValue<String>("last_name"),
email= reader.GetValue<String>("email"),
phone_number= reader.GetValue<String>("phone_number"),
department= reader.GetValue<String>("department"),
isActive= reader.GetValue<SByte>("isActive"),
createdBy= reader.GetValue<String>("createdBy"),
modifiedBy= reader.GetValue<String>("modifiedBy"),
createdAt= reader.GetValue<DateTime>("createdAt").ToString(),
modifiedAt= reader.GetValue<DateTime>("modifiedAt").ToString(),
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

        public int GetFilterTotalRecordEmployees(string ownername,List<FilterModel> filterBy, string andOr)
        {
            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT count(*) TotalRecord FROM Employees t {filterColumns}";
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
		public bool DeleteMultipleEmployees(List<DeleteMultipleModel> deleteParam, string andOr)
        {
            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    MySqlTransaction transaction = connection.BeginTransaction();
                    cmd.Transaction = transaction;
                    cmd.CommandText = @"UPDATE Employees SET isActive=0 Where";
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
