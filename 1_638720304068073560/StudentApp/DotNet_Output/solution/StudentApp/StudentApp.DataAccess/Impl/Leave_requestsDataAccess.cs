using MySql.Data.MySqlClient;
using StudentApp.DataAccess.Interface;
using StudentApp.Model;
using StudentApp.Utility;
using System.Collections.Generic;
using System;
using System.Globalization;

namespace StudentApp.DataAccess.Impl
{
    public class Leave_requestsDataAccess : ILeave_requestsDataAccess
    {
        private MySqlDatabaseConnector  mySqlDatabaseConnector { get; set; }
        public Leave_requestsDataAccess(MySqlDatabaseConnector _mySqlDatabaseConnector)
        {
            mySqlDatabaseConnector = _mySqlDatabaseConnector;
        }
		public int GetAllTotalRecordLeave_requests()
        {
            try{
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT count(*) TotalCount FROM leave_requests t WHERE t.isActive=1";
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
        public int GetAllTotalRecordLeave_requestsByCreatedBy(string ownername)
        {
            try{
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT count(*) TotalCount FROM leave_requests t WHERE t.isActive=1 AND t.createdBy=@ownername";
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
        public int GetSearchTotalRecordLeave_requests(string searchKey)
        {
            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT count(*) TotalCount FROM leave_requests t WHERE t.isActive=1 AND t.remarks LIKE CONCAT('%',@SearchKey,'%') OR t.createdBy LIKE CONCAT('%',@SearchKey,'%') OR t.modifiedBy LIKE CONCAT('%',@SearchKey,'%')";
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
        public int GetSearchTotalRecordLeave_requestsByCreatedBy(string ownername,string searchKey)
        {
            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT count(*) TotalCount FROM leave_requests t WHERE t.isActive=1 AND t.createdBy=@ownername AND t.remarks LIKE CONCAT('%',@SearchKey,'%') OR t.createdBy LIKE CONCAT('%',@SearchKey,'%') OR t.modifiedBy LIKE CONCAT('%',@SearchKey,'%')";
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
        public List<Leave_requestsModel> GetAllLeave_requests(int page=1,int itemsPerPage=100,List<OrderByModel> orderBy = null)
        {
            var ret = new List<Leave_requestsModel>();
			int offset = (page - 1) * itemsPerPage;
            try{
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT  t.* FROM leave_requests t  WHERE t.isActive=1 ORDER BY column LIMIT @Offset, @ItemsPerPage";
                    cmd.CommandText = Helper.ConverOrderListToSQL(cmd.CommandText, orderBy);
                    cmd.Parameters.AddWithValue("@Offset", offset);
                    cmd.Parameters.AddWithValue("@ItemsPerPage", itemsPerPage);
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            var t = new Leave_requestsModel()
                            {
                            leave_id= reader.GetValue<Int32>("leave_id"),
employee_id= reader.GetValue<Int32>("employee_id"),
leave_start_date= reader.GetValue<DateTime>("leave_start_date").ToString(),
leave_end_date= reader.GetValue<DateTime>("leave_end_date").ToString(),
leave_type= reader.GetValue<String>("leave_type"),
status= reader.GetValue<String>("status"),
remarks= reader.IsDBNull(Helper.GetColumnOrder(reader,"remarks")) ? (String?)null : reader.GetString("remarks"),
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

        public List<Leave_requestsModel> GetAllLeave_requestsByCreatedBy(string ownername,int page=1,int itemsPerPage=100,List<OrderByModel> orderBy = null)
        {
            var ret = new List<Leave_requestsModel>();
			int offset = (page - 1) * itemsPerPage;
            try{
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT  t.* FROM leave_requests t  WHERE t.isActive=1 AND t.createdBy=@ownername ORDER BY column LIMIT @Offset, @ItemsPerPage";
                    cmd.CommandText = Helper.ConverOrderListToSQL(cmd.CommandText, orderBy);
                    cmd.Parameters.AddWithValue("@Offset", offset);
                    cmd.Parameters.AddWithValue("@ownername", ownername);
                    cmd.Parameters.AddWithValue("@ItemsPerPage", itemsPerPage);
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            var t = new Leave_requestsModel()
                            {
                            leave_id= reader.GetValue<Int32>("leave_id"),
employee_id= reader.GetValue<Int32>("employee_id"),
leave_start_date= reader.GetValue<DateTime>("leave_start_date").ToString(),
leave_end_date= reader.GetValue<DateTime>("leave_end_date").ToString(),
leave_type= reader.GetValue<String>("leave_type"),
status= reader.GetValue<String>("status"),
remarks= reader.IsDBNull(Helper.GetColumnOrder(reader,"remarks")) ? (String?)null : reader.GetString("remarks"),
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
		public List<Leave_requestsModel> SearchLeave_requests(string searchKey, int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null)
        {
            var ret = new List<Leave_requestsModel>();
            int offset = (page - 1) * itemsPerPage;
            try{
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT t.* FROM leave_requests t WHERE t.isActive=1 AND t.remarks LIKE CONCAT('%',@SearchKey,'%') OR t.createdBy LIKE CONCAT('%',@SearchKey,'%') OR t.modifiedBy LIKE CONCAT('%',@SearchKey,'%') ORDER BY column LIMIT @Offset, @ItemsPerPage";
                    cmd.CommandText = Helper.ConverOrderListToSQL(cmd.CommandText, orderBy);
                    cmd.Parameters.AddWithValue("@SearchKey", searchKey);
                    cmd.Parameters.AddWithValue("@Offset", offset);
                    cmd.Parameters.AddWithValue("@ItemsPerPage", itemsPerPage);
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            var t = new Leave_requestsModel()
                            {
                            leave_id= reader.GetValue<Int32>("leave_id"),
employee_id= reader.GetValue<Int32>("employee_id"),
leave_start_date= reader.GetValue<DateTime>("leave_start_date").ToString(),
leave_end_date= reader.GetValue<DateTime>("leave_end_date").ToString(),
leave_type= reader.GetValue<String>("leave_type"),
status= reader.GetValue<String>("status"),
remarks= reader.IsDBNull(Helper.GetColumnOrder(reader,"remarks")) ? (String?)null : reader.GetString("remarks"),
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
        public List<Leave_requestsModel> SearchLeave_requestsByCreatedBy(string ownername,string searchKey, int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null)
        {
            var ret = new List<Leave_requestsModel>();
            int offset = (page - 1) * itemsPerPage;
            try{
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT t.* FROM leave_requests t WHERE t.isActive=1 AND t.createdBy=@ownername AND t.remarks LIKE CONCAT('%',@SearchKey,'%') OR t.createdBy LIKE CONCAT('%',@SearchKey,'%') OR t.modifiedBy LIKE CONCAT('%',@SearchKey,'%') ORDER BY column LIMIT @Offset, @ItemsPerPage";
                    cmd.CommandText = Helper.ConverOrderListToSQL(cmd.CommandText, orderBy);
                    cmd.Parameters.AddWithValue("@SearchKey", searchKey);
                    cmd.Parameters.AddWithValue("@ownername", ownername);
                    cmd.Parameters.AddWithValue("@Offset", offset);
                    cmd.Parameters.AddWithValue("@ItemsPerPage", itemsPerPage);
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            var t = new Leave_requestsModel()
                            {
                            leave_id= reader.GetValue<Int32>("leave_id"),
employee_id= reader.GetValue<Int32>("employee_id"),
leave_start_date= reader.GetValue<DateTime>("leave_start_date").ToString(),
leave_end_date= reader.GetValue<DateTime>("leave_end_date").ToString(),
leave_type= reader.GetValue<String>("leave_type"),
status= reader.GetValue<String>("status"),
remarks= reader.IsDBNull(Helper.GetColumnOrder(reader,"remarks")) ? (String?)null : reader.GetString("remarks"),
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
        public Leave_requestsModel GetLeave_requestsByID(int leave_id)
        {
            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT  t.* FROM leave_requests t  WHERE t.isActive=1 AND t.leave_id= @leave_id";
                    cmd.Parameters.AddWithValue("@leave_id",leave_id);
                    
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            return new Leave_requestsModel()
                            {
                                leave_id= reader.GetValue<Int32>("leave_id"),
employee_id= reader.GetValue<Int32>("employee_id"),
leave_start_date= reader.GetValue<DateTime>("leave_start_date").ToString(),
leave_end_date= reader.GetValue<DateTime>("leave_end_date").ToString(),
leave_type= reader.GetValue<String>("leave_type"),
status= reader.GetValue<String>("status"),
remarks= reader.IsDBNull(Helper.GetColumnOrder(reader,"remarks")) ? (String?)null : reader.GetString("remarks"),
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

        public Leave_requestsModel GetLeave_requestsByIDByCreatedBy(string ownername,int leave_id)
        {

            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT  t.* FROM leave_requests t  WHERE t.isActive=1 AND t.createdBy=@ownername AND t.leave_id= @leave_id";
                    cmd.Parameters.AddWithValue("@leave_id",leave_id);
                    cmd.Parameters.AddWithValue("@ownername", ownername);
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            return new Leave_requestsModel()
                            {
                                leave_id= reader.GetValue<Int32>("leave_id"),
employee_id= reader.GetValue<Int32>("employee_id"),
leave_start_date= reader.GetValue<DateTime>("leave_start_date").ToString(),
leave_end_date= reader.GetValue<DateTime>("leave_end_date").ToString(),
leave_type= reader.GetValue<String>("leave_type"),
status= reader.GetValue<String>("status"),
remarks= reader.IsDBNull(Helper.GetColumnOrder(reader,"remarks")) ? (String?)null : reader.GetString("remarks"),
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
        public Leave_requestsRelationalModel GetLeave_requestsRelational(string ownername,int leave_id)
{
    try
    {
        mySqlDatabaseConnector.OpenConnection();
        var connection = mySqlDatabaseConnector.GetConnection();
        using (var cmd = connection.CreateCommand())
        {
            cmd.CommandText = @"SELECT  t.* FROM leave_requests t  WHERE t.isActive=1 AND t.createdBy=@ownername AND t.leave_id= @leave_id";
            cmd.Parameters.AddWithValue("@ownername", ownername);
            cmd.Parameters.AddWithValue("@leave_id",leave_id);
            Leave_requestsRelationalModel leave_requests = null;
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    leave_requests = new Leave_requestsRelationalModel()
                    {
                        leave_id= reader.GetValue<Int32>("leave_id"),
employee_id= reader.GetValue<Int32>("employee_id"),
leave_start_date= reader.GetValue<DateTime>("leave_start_date").ToString(),
leave_end_date= reader.GetValue<DateTime>("leave_end_date").ToString(),
leave_type= reader.GetValue<String>("leave_type"),
status= reader.GetValue<String>("status"),
remarks= reader.IsDBNull(Helper.GetColumnOrder(reader,"remarks")) ? (String?)null : reader.GetString("remarks"),
isActive= reader.GetValue<SByte>("isActive"),
createdBy= reader.GetValue<String>("createdBy"),
modifiedBy= reader.GetValue<String>("modifiedBy"),
createdAt= reader.GetValue<DateTime>("createdAt").ToString(),
modifiedAt= reader.GetValue<DateTime>("modifiedAt").ToString(),
                        
                    };
                }
            }

            

            if (leave_requests != null)
{
    cmd.Parameters.Clear();
    cmd.CommandText = @"SELECT  t.* FROM employees t  WHERE t.isActive=1 AND t.createdBy=@ownername AND t.employee_id= @employee_id";
    cmd.Parameters.AddWithValue("@ownername", ownername);
    cmd.Parameters.AddWithValue("@employee_id",leave_requests.employee_id);

    using (var reader = cmd.ExecuteReader())
    {
        while (reader.Read())
        {
            var employees = new EmployeesRelationalModel()
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

            leave_requests.employee_id_Employees = employees;
        }
    }
}


            return leave_requests;
        }
    }
    catch (Exception ex)
    {
        throw ex;
    }
    finally
    {
        mySqlDatabaseConnector.CloseConnection();
    }
    return null;
}

public List<Leave_requestsRelationalModel> GetAllLeave_requestsRelational(string ownername,int page=1,int itemsPerPage=100,List<OrderByModel> orderBy = null)
{
    int offset = (page - 1) * itemsPerPage;
    try
    {
        mySqlDatabaseConnector.OpenConnection();
        var connection = mySqlDatabaseConnector.GetConnection();
        using (var cmd = connection.CreateCommand())
        {
            cmd.CommandText = @"SELECT  t.* FROM leave_requests t  WHERE t.isActive=1 AND t.createdBy=@ownername ORDER BY column LIMIT @Offset, @ItemsPerPage";
            cmd.Parameters.AddWithValue("@ownername", ownername);
            cmd.CommandText = Helper.ConverOrderListToSQL(cmd.CommandText, orderBy);
            cmd.Parameters.AddWithValue("@Offset", offset);
            cmd.Parameters.AddWithValue("@ItemsPerPage", itemsPerPage);
            List<Leave_requestsRelationalModel> leave_requests = new List<Leave_requestsRelationalModel>();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Leave_requestsRelationalModel res = new Leave_requestsRelationalModel()
                    {
                        leave_id= reader.GetValue<Int32>("leave_id"),
employee_id= reader.GetValue<Int32>("employee_id"),
leave_start_date= reader.GetValue<DateTime>("leave_start_date").ToString(),
leave_end_date= reader.GetValue<DateTime>("leave_end_date").ToString(),
leave_type= reader.GetValue<String>("leave_type"),
status= reader.GetValue<String>("status"),
remarks= reader.IsDBNull(Helper.GetColumnOrder(reader,"remarks")) ? (String?)null : reader.GetString("remarks"),
isActive= reader.GetValue<SByte>("isActive"),
createdBy= reader.GetValue<String>("createdBy"),
modifiedBy= reader.GetValue<String>("modifiedBy"),
createdAt= reader.GetValue<DateTime>("createdAt").ToString(),
modifiedAt= reader.GetValue<DateTime>("modifiedAt").ToString(),
                        
                    };

                    leave_requests.Add(res);
                }
            }

            

            for (int i = 0; i < leave_requests.Count; i++)
{
    cmd.Parameters.Clear();
    cmd.CommandText = @"SELECT  t.* FROM employees t  WHERE t.isActive=1 AND t.createdBy=@ownername AND t.employee_id= @employee_id";
    cmd.Parameters.AddWithValue("@ownername", ownername);
    cmd.Parameters.AddWithValue("@employee_id",leave_requests[i].employee_id);

    using (var reader = cmd.ExecuteReader())
    {
        while (reader.Read())
        {
            var employees = new EmployeesRelationalModel()
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

            leave_requests[i].employee_id_Employees = employees;
        }
    }
}


            return leave_requests;
        }
    }
    catch (Exception ex)
    {
        throw ex;
    }
    finally
    {
        mySqlDatabaseConnector.CloseConnection();
    }
    return null;
}

        public Leave_requestsReportingModel GetLeave_requestsReporting(string ownername,int leave_id)
{
    try
    {
        mySqlDatabaseConnector.OpenConnection();
        var connection = mySqlDatabaseConnector.GetConnection();
        using (var cmd = connection.CreateCommand())
        {
            cmd.CommandText = @"SELECT  t.* FROM leave_requests t  WHERE t.isActive=1 AND t.createdBy=@ownername AND t.leave_id= @leave_id";
            cmd.Parameters.AddWithValue("@ownername", ownername);
            cmd.Parameters.AddWithValue("@leave_id",leave_id);
            Leave_requestsReportingModel leave_requests = null;
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    leave_requests = new Leave_requestsReportingModel()
                    {
                        leave_id= reader.GetValue<Int32>("leave_id"),
employee_id= reader.GetValue<Int32>("employee_id"),
leave_start_date= reader.GetValue<DateTime>("leave_start_date").ToString(),
leave_end_date= reader.GetValue<DateTime>("leave_end_date").ToString(),
leave_type= reader.GetValue<String>("leave_type"),
status= reader.GetValue<String>("status"),
remarks= reader.IsDBNull(Helper.GetColumnOrder(reader,"remarks")) ? (String?)null : reader.GetString("remarks"),
isActive= reader.GetValue<SByte>("isActive"),
createdBy= reader.GetValue<String>("createdBy"),
modifiedBy= reader.GetValue<String>("modifiedBy"),
createdAt= reader.GetValue<DateTime>("createdAt").ToString(),
modifiedAt= reader.GetValue<DateTime>("modifiedAt").ToString(),
                    };
                }
            }

            if (leave_requests != null)
{
    cmd.Parameters.Clear();
    cmd.CommandText = @"SELECT  t.* FROM employees t  WHERE t.isActive=1 AND t.createdBy=@ownername AND t.employee_id= @employee_id";
    cmd.Parameters.AddWithValue("@ownername", ownername);
    cmd.Parameters.AddWithValue("@employee_id",leave_requests.employee_id);

    using (var reader = cmd.ExecuteReader())
    {
        while (reader.Read())
        {
            leave_requests.employee_id_Employees_employee_id= reader.GetValue<Int32>("employee_id");
leave_requests.employee_id_Employees_first_name= reader.GetValue<String>("first_name");
leave_requests.employee_id_Employees_last_name= reader.GetValue<String>("last_name");
leave_requests.employee_id_Employees_email= reader.GetValue<String>("email");
leave_requests.employee_id_Employees_phone_number= reader.GetValue<String>("phone_number");
leave_requests.employee_id_Employees_department= reader.GetValue<String>("department");
leave_requests.employee_id_Employees_isActive= reader.GetValue<SByte>("isActive");
leave_requests.employee_id_Employees_createdBy= reader.GetValue<String>("createdBy");
leave_requests.employee_id_Employees_modifiedBy= reader.GetValue<String>("modifiedBy");
leave_requests.employee_id_Employees_createdAt= reader.GetValue<DateTime>("createdAt").ToString();
leave_requests.employee_id_Employees_modifiedAt= reader.GetValue<DateTime>("modifiedAt").ToString();
        }
    }
}


            return leave_requests;
        }
    }
    catch (Exception ex)
    {
        throw ex;
    }
    finally
    {
        mySqlDatabaseConnector.CloseConnection();
    }
    return null;
}

public List<Leave_requestsReportingModel> GetAllLeave_requestsReporting(string ownername,int page=1,int itemsPerPage=100,List<OrderByModel> orderBy = null){
    int offset = (page - 1) * itemsPerPage;
    try
    {
        mySqlDatabaseConnector.OpenConnection();
        var connection = mySqlDatabaseConnector.GetConnection();
        using (var cmd = connection.CreateCommand())
        {
            cmd.CommandText = @"SELECT  t.* FROM leave_requests t  WHERE t.isActive=1 AND t.createdBy=@ownername ORDER BY column LIMIT @Offset, @ItemsPerPage";
            cmd.CommandText = Helper.ConverOrderListToSQL(cmd.CommandText, orderBy);
            cmd.Parameters.AddWithValue("@Offset", offset);
            cmd.Parameters.AddWithValue("@ownername", ownername);
            cmd.Parameters.AddWithValue("@ItemsPerPage", itemsPerPage);
            List<Leave_requestsReportingModel> leave_requests = new List<Leave_requestsReportingModel>();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Leave_requestsReportingModel res = new Leave_requestsReportingModel()
                    {
                        leave_id= reader.GetValue<Int32>("leave_id"),
employee_id= reader.GetValue<Int32>("employee_id"),
leave_start_date= reader.GetValue<DateTime>("leave_start_date").ToString(),
leave_end_date= reader.GetValue<DateTime>("leave_end_date").ToString(),
leave_type= reader.GetValue<String>("leave_type"),
status= reader.GetValue<String>("status"),
remarks= reader.IsDBNull(Helper.GetColumnOrder(reader,"remarks")) ? (String?)null : reader.GetString("remarks"),
isActive= reader.GetValue<SByte>("isActive"),
createdBy= reader.GetValue<String>("createdBy"),
modifiedBy= reader.GetValue<String>("modifiedBy"),
createdAt= reader.GetValue<DateTime>("createdAt").ToString(),
modifiedAt= reader.GetValue<DateTime>("modifiedAt").ToString(),
                    };
                    leave_requests.Add(res);
                }
            }

            for (int i = 0; i < leave_requests.Count; i++)
{
    cmd.Parameters.Clear();
    cmd.CommandText = @"SELECT  t.* FROM employees t  WHERE t.isActive=1 AND t.createdBy=@ownername AND t.employee_id= @employee_id";
    cmd.Parameters.AddWithValue("@ownername", ownername);
    cmd.Parameters.AddWithValue("@employee_id",leave_requests[i].employee_id);

    using (var reader = cmd.ExecuteReader())
    {
        while (reader.Read())
        {
            leave_requests[i].employee_id_Employees_employee_id= reader.GetValue<Int32>("employee_id");
leave_requests[i].employee_id_Employees_first_name= reader.GetValue<String>("first_name");
leave_requests[i].employee_id_Employees_last_name= reader.GetValue<String>("last_name");
leave_requests[i].employee_id_Employees_email= reader.GetValue<String>("email");
leave_requests[i].employee_id_Employees_phone_number= reader.GetValue<String>("phone_number");
leave_requests[i].employee_id_Employees_department= reader.GetValue<String>("department");
leave_requests[i].employee_id_Employees_isActive= reader.GetValue<SByte>("isActive");
leave_requests[i].employee_id_Employees_createdBy= reader.GetValue<String>("createdBy");
leave_requests[i].employee_id_Employees_modifiedBy= reader.GetValue<String>("modifiedBy");
leave_requests[i].employee_id_Employees_createdAt= reader.GetValue<DateTime>("createdAt").ToString();
leave_requests[i].employee_id_Employees_modifiedAt= reader.GetValue<DateTime>("modifiedAt").ToString();
        }
    }
}


            return leave_requests;
        }
    }
    catch (Exception ex)
    {
        throw ex;
    }
    finally
    {
        mySqlDatabaseConnector.CloseConnection();
    }
    return null;
}

        

        public bool UpdateLeave_requests(Leave_requestsModel model)
        {
            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE leave_requests SET leave_id=@leave_id,employee_id=@employee_id,leave_start_date=@leave_start_date,leave_end_date=@leave_end_date,leave_type=@leave_type,status=@status,remarks=@remarks,isActive=@isActive,modifiedBy=@modifiedBy,modifiedAt=@modifiedAt WHERE leave_id=@leave_id";
                    cmd.Parameters.AddWithValue("@leave_id", model.leave_id);
cmd.Parameters.AddWithValue("@employee_id", model.employee_id);
cmd.Parameters.AddWithValue("@leave_start_date", model.leave_start_date);
cmd.Parameters.AddWithValue("@leave_end_date", model.leave_end_date);
cmd.Parameters.AddWithValue("@leave_type", model.leave_type);
cmd.Parameters.AddWithValue("@status", model.status);
cmd.Parameters.AddWithValue("@remarks", model.remarks);
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

        public long AddLeave_requests(Leave_requestsModel model)
        {
            try
            {
		
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO leave_requests (leave_id,employee_id,leave_start_date,leave_end_date,leave_type,status,remarks,isActive,createdBy,modifiedBy,createdAt,modifiedAt) Values (@leave_id,@employee_id,@leave_start_date,@leave_end_date,@leave_type,@status,@remarks,@isActive,@createdBy,@modifiedBy,@createdAt,@modifiedAt);";
                    cmd.Parameters.AddWithValue("@leave_id", model.leave_id);
cmd.Parameters.AddWithValue("@employee_id", model.employee_id);
cmd.Parameters.AddWithValue("@leave_start_date", model.leave_start_date);
cmd.Parameters.AddWithValue("@leave_end_date", model.leave_end_date);
cmd.Parameters.AddWithValue("@leave_type", model.leave_type);
cmd.Parameters.AddWithValue("@status", model.status);
cmd.Parameters.AddWithValue("@remarks", model.remarks);
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

        public bool DeleteLeave_requests(int leave_id)
        {
            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE leave_requests SET isActive=0 Where leave_id=@leave_id";
                    cmd.Parameters.AddWithValue("@leave_id",leave_id);
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
		public List<Leave_requestsModel> FilterLeave_requests(string ownername,List<FilterModel> filterBy, string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy)
        {
            var ret = new List<Leave_requestsModel>();
            int offset = (page - 1) * itemsPerPage;
            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT  t.* FROM Leave_requests t {filterColumns} ORDER BY column LIMIT @Offset, @ItemsPerPage ";
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
                            var t = new Leave_requestsModel()
                            {
                                leave_id= reader.GetValue<Int32>("leave_id"),
employee_id= reader.GetValue<Int32>("employee_id"),
leave_start_date= reader.GetValue<DateTime>("leave_start_date").ToString(),
leave_end_date= reader.GetValue<DateTime>("leave_end_date").ToString(),
leave_type= reader.GetValue<String>("leave_type"),
status= reader.GetValue<String>("status"),
remarks= reader.IsDBNull(Helper.GetColumnOrder(reader,"remarks")) ? (String?)null : reader.GetString("remarks"),
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

        public int GetFilterTotalRecordLeave_requests(string ownername,List<FilterModel> filterBy, string andOr)
        {
            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT count(*) TotalRecord FROM Leave_requests t {filterColumns}";
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
		public bool DeleteMultipleLeave_requests(List<DeleteMultipleModel> deleteParam, string andOr)
        {
            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    MySqlTransaction transaction = connection.BeginTransaction();
                    cmd.Transaction = transaction;
                    cmd.CommandText = @"UPDATE Leave_requests SET isActive=0 Where";
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
