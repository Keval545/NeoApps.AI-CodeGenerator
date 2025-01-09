using MySql.Data.MySqlClient;
using StudentApp.DataAccess.Interface;
using StudentApp.Model;
using StudentApp.Utility;
using System.Collections.Generic;
using System;
using System.Globalization;

namespace StudentApp.DataAccess.Impl
{
    public class Attendance_recordsDataAccess : IAttendance_recordsDataAccess
    {
        private MySqlDatabaseConnector  mySqlDatabaseConnector { get; set; }
        public Attendance_recordsDataAccess(MySqlDatabaseConnector _mySqlDatabaseConnector)
        {
            mySqlDatabaseConnector = _mySqlDatabaseConnector;
        }
		public int GetAllTotalRecordAttendance_records()
        {
            try{
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT count(*) TotalCount FROM attendance_records t WHERE t.isActive=1";
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
        public int GetAllTotalRecordAttendance_recordsByCreatedBy(string ownername)
        {
            try{
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT count(*) TotalCount FROM attendance_records t WHERE t.isActive=1 AND t.createdBy=@ownername";
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
        public int GetSearchTotalRecordAttendance_records(string searchKey)
        {
            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT count(*) TotalCount FROM attendance_records t WHERE t.isActive=1 AND t.remarks LIKE CONCAT('%',@SearchKey,'%') OR t.createdBy LIKE CONCAT('%',@SearchKey,'%') OR t.modifiedBy LIKE CONCAT('%',@SearchKey,'%')";
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
        public int GetSearchTotalRecordAttendance_recordsByCreatedBy(string ownername,string searchKey)
        {
            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT count(*) TotalCount FROM attendance_records t WHERE t.isActive=1 AND t.createdBy=@ownername AND t.remarks LIKE CONCAT('%',@SearchKey,'%') OR t.createdBy LIKE CONCAT('%',@SearchKey,'%') OR t.modifiedBy LIKE CONCAT('%',@SearchKey,'%')";
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
        public List<Attendance_recordsModel> GetAllAttendance_records(int page=1,int itemsPerPage=100,List<OrderByModel> orderBy = null)
        {
            var ret = new List<Attendance_recordsModel>();
			int offset = (page - 1) * itemsPerPage;
            try{
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT  t.* FROM attendance_records t  WHERE t.isActive=1 ORDER BY column LIMIT @Offset, @ItemsPerPage";
                    cmd.CommandText = Helper.ConverOrderListToSQL(cmd.CommandText, orderBy);
                    cmd.Parameters.AddWithValue("@Offset", offset);
                    cmd.Parameters.AddWithValue("@ItemsPerPage", itemsPerPage);
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            var t = new Attendance_recordsModel()
                            {
                            attendance_id= reader.GetValue<Int32>("attendance_id"),
employee_id= reader.GetValue<Int32>("employee_id"),
attendance_date= reader.GetValue<DateTime>("attendance_date").ToString(),
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

        public List<Attendance_recordsModel> GetAllAttendance_recordsByCreatedBy(string ownername,int page=1,int itemsPerPage=100,List<OrderByModel> orderBy = null)
        {
            var ret = new List<Attendance_recordsModel>();
			int offset = (page - 1) * itemsPerPage;
            try{
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT  t.* FROM attendance_records t  WHERE t.isActive=1 AND t.createdBy=@ownername ORDER BY column LIMIT @Offset, @ItemsPerPage";
                    cmd.CommandText = Helper.ConverOrderListToSQL(cmd.CommandText, orderBy);
                    cmd.Parameters.AddWithValue("@Offset", offset);
                    cmd.Parameters.AddWithValue("@ownername", ownername);
                    cmd.Parameters.AddWithValue("@ItemsPerPage", itemsPerPage);
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            var t = new Attendance_recordsModel()
                            {
                            attendance_id= reader.GetValue<Int32>("attendance_id"),
employee_id= reader.GetValue<Int32>("employee_id"),
attendance_date= reader.GetValue<DateTime>("attendance_date").ToString(),
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
		public List<Attendance_recordsModel> SearchAttendance_records(string searchKey, int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null)
        {
            var ret = new List<Attendance_recordsModel>();
            int offset = (page - 1) * itemsPerPage;
            try{
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT t.* FROM attendance_records t WHERE t.isActive=1 AND t.remarks LIKE CONCAT('%',@SearchKey,'%') OR t.createdBy LIKE CONCAT('%',@SearchKey,'%') OR t.modifiedBy LIKE CONCAT('%',@SearchKey,'%') ORDER BY column LIMIT @Offset, @ItemsPerPage";
                    cmd.CommandText = Helper.ConverOrderListToSQL(cmd.CommandText, orderBy);
                    cmd.Parameters.AddWithValue("@SearchKey", searchKey);
                    cmd.Parameters.AddWithValue("@Offset", offset);
                    cmd.Parameters.AddWithValue("@ItemsPerPage", itemsPerPage);
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            var t = new Attendance_recordsModel()
                            {
                            attendance_id= reader.GetValue<Int32>("attendance_id"),
employee_id= reader.GetValue<Int32>("employee_id"),
attendance_date= reader.GetValue<DateTime>("attendance_date").ToString(),
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
        public List<Attendance_recordsModel> SearchAttendance_recordsByCreatedBy(string ownername,string searchKey, int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy = null)
        {
            var ret = new List<Attendance_recordsModel>();
            int offset = (page - 1) * itemsPerPage;
            try{
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT t.* FROM attendance_records t WHERE t.isActive=1 AND t.createdBy=@ownername AND t.remarks LIKE CONCAT('%',@SearchKey,'%') OR t.createdBy LIKE CONCAT('%',@SearchKey,'%') OR t.modifiedBy LIKE CONCAT('%',@SearchKey,'%') ORDER BY column LIMIT @Offset, @ItemsPerPage";
                    cmd.CommandText = Helper.ConverOrderListToSQL(cmd.CommandText, orderBy);
                    cmd.Parameters.AddWithValue("@SearchKey", searchKey);
                    cmd.Parameters.AddWithValue("@ownername", ownername);
                    cmd.Parameters.AddWithValue("@Offset", offset);
                    cmd.Parameters.AddWithValue("@ItemsPerPage", itemsPerPage);
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            var t = new Attendance_recordsModel()
                            {
                            attendance_id= reader.GetValue<Int32>("attendance_id"),
employee_id= reader.GetValue<Int32>("employee_id"),
attendance_date= reader.GetValue<DateTime>("attendance_date").ToString(),
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
        public Attendance_recordsModel GetAttendance_recordsByID(int attendance_id)
        {
            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT  t.* FROM attendance_records t  WHERE t.isActive=1 AND t.attendance_id= @attendance_id";
                    cmd.Parameters.AddWithValue("@attendance_id",attendance_id);
                    
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            return new Attendance_recordsModel()
                            {
                                attendance_id= reader.GetValue<Int32>("attendance_id"),
employee_id= reader.GetValue<Int32>("employee_id"),
attendance_date= reader.GetValue<DateTime>("attendance_date").ToString(),
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

        public Attendance_recordsModel GetAttendance_recordsByIDByCreatedBy(string ownername,int attendance_id)
        {

            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT  t.* FROM attendance_records t  WHERE t.isActive=1 AND t.createdBy=@ownername AND t.attendance_id= @attendance_id";
                    cmd.Parameters.AddWithValue("@attendance_id",attendance_id);
                    cmd.Parameters.AddWithValue("@ownername", ownername);
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            return new Attendance_recordsModel()
                            {
                                attendance_id= reader.GetValue<Int32>("attendance_id"),
employee_id= reader.GetValue<Int32>("employee_id"),
attendance_date= reader.GetValue<DateTime>("attendance_date").ToString(),
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
        public Attendance_recordsRelationalModel GetAttendance_recordsRelational(string ownername,int attendance_id)
{
    try
    {
        mySqlDatabaseConnector.OpenConnection();
        var connection = mySqlDatabaseConnector.GetConnection();
        using (var cmd = connection.CreateCommand())
        {
            cmd.CommandText = @"SELECT  t.* FROM attendance_records t  WHERE t.isActive=1 AND t.createdBy=@ownername AND t.attendance_id= @attendance_id";
            cmd.Parameters.AddWithValue("@ownername", ownername);
            cmd.Parameters.AddWithValue("@attendance_id",attendance_id);
            Attendance_recordsRelationalModel attendance_records = null;
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    attendance_records = new Attendance_recordsRelationalModel()
                    {
                        attendance_id= reader.GetValue<Int32>("attendance_id"),
employee_id= reader.GetValue<Int32>("employee_id"),
attendance_date= reader.GetValue<DateTime>("attendance_date").ToString(),
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

            

            if (attendance_records != null)
{
    cmd.Parameters.Clear();
    cmd.CommandText = @"SELECT  t.* FROM employees t  WHERE t.isActive=1 AND t.createdBy=@ownername AND t.employee_id= @employee_id";
    cmd.Parameters.AddWithValue("@ownername", ownername);
    cmd.Parameters.AddWithValue("@employee_id",attendance_records.employee_id);

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

            attendance_records.employee_id_Employees = employees;
        }
    }
}


            return attendance_records;
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

public List<Attendance_recordsRelationalModel> GetAllAttendance_recordsRelational(string ownername,int page=1,int itemsPerPage=100,List<OrderByModel> orderBy = null)
{
    int offset = (page - 1) * itemsPerPage;
    try
    {
        mySqlDatabaseConnector.OpenConnection();
        var connection = mySqlDatabaseConnector.GetConnection();
        using (var cmd = connection.CreateCommand())
        {
            cmd.CommandText = @"SELECT  t.* FROM attendance_records t  WHERE t.isActive=1 AND t.createdBy=@ownername ORDER BY column LIMIT @Offset, @ItemsPerPage";
            cmd.Parameters.AddWithValue("@ownername", ownername);
            cmd.CommandText = Helper.ConverOrderListToSQL(cmd.CommandText, orderBy);
            cmd.Parameters.AddWithValue("@Offset", offset);
            cmd.Parameters.AddWithValue("@ItemsPerPage", itemsPerPage);
            List<Attendance_recordsRelationalModel> attendance_records = new List<Attendance_recordsRelationalModel>();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Attendance_recordsRelationalModel res = new Attendance_recordsRelationalModel()
                    {
                        attendance_id= reader.GetValue<Int32>("attendance_id"),
employee_id= reader.GetValue<Int32>("employee_id"),
attendance_date= reader.GetValue<DateTime>("attendance_date").ToString(),
status= reader.GetValue<String>("status"),
remarks= reader.IsDBNull(Helper.GetColumnOrder(reader,"remarks")) ? (String?)null : reader.GetString("remarks"),
isActive= reader.GetValue<SByte>("isActive"),
createdBy= reader.GetValue<String>("createdBy"),
modifiedBy= reader.GetValue<String>("modifiedBy"),
createdAt= reader.GetValue<DateTime>("createdAt").ToString(),
modifiedAt= reader.GetValue<DateTime>("modifiedAt").ToString(),
                        
                    };

                    attendance_records.Add(res);
                }
            }

            

            for (int i = 0; i < attendance_records.Count; i++)
{
    cmd.Parameters.Clear();
    cmd.CommandText = @"SELECT  t.* FROM employees t  WHERE t.isActive=1 AND t.createdBy=@ownername AND t.employee_id= @employee_id";
    cmd.Parameters.AddWithValue("@ownername", ownername);
    cmd.Parameters.AddWithValue("@employee_id",attendance_records[i].employee_id);

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

            attendance_records[i].employee_id_Employees = employees;
        }
    }
}


            return attendance_records;
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

        public Attendance_recordsReportingModel GetAttendance_recordsReporting(string ownername,int attendance_id)
{
    try
    {
        mySqlDatabaseConnector.OpenConnection();
        var connection = mySqlDatabaseConnector.GetConnection();
        using (var cmd = connection.CreateCommand())
        {
            cmd.CommandText = @"SELECT  t.* FROM attendance_records t  WHERE t.isActive=1 AND t.createdBy=@ownername AND t.attendance_id= @attendance_id";
            cmd.Parameters.AddWithValue("@ownername", ownername);
            cmd.Parameters.AddWithValue("@attendance_id",attendance_id);
            Attendance_recordsReportingModel attendance_records = null;
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    attendance_records = new Attendance_recordsReportingModel()
                    {
                        attendance_id= reader.GetValue<Int32>("attendance_id"),
employee_id= reader.GetValue<Int32>("employee_id"),
attendance_date= reader.GetValue<DateTime>("attendance_date").ToString(),
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

            if (attendance_records != null)
{
    cmd.Parameters.Clear();
    cmd.CommandText = @"SELECT  t.* FROM employees t  WHERE t.isActive=1 AND t.createdBy=@ownername AND t.employee_id= @employee_id";
    cmd.Parameters.AddWithValue("@ownername", ownername);
    cmd.Parameters.AddWithValue("@employee_id",attendance_records.employee_id);

    using (var reader = cmd.ExecuteReader())
    {
        while (reader.Read())
        {
            attendance_records.employee_id_Employees_employee_id= reader.GetValue<Int32>("employee_id");
attendance_records.employee_id_Employees_first_name= reader.GetValue<String>("first_name");
attendance_records.employee_id_Employees_last_name= reader.GetValue<String>("last_name");
attendance_records.employee_id_Employees_email= reader.GetValue<String>("email");
attendance_records.employee_id_Employees_phone_number= reader.GetValue<String>("phone_number");
attendance_records.employee_id_Employees_department= reader.GetValue<String>("department");
attendance_records.employee_id_Employees_isActive= reader.GetValue<SByte>("isActive");
attendance_records.employee_id_Employees_createdBy= reader.GetValue<String>("createdBy");
attendance_records.employee_id_Employees_modifiedBy= reader.GetValue<String>("modifiedBy");
attendance_records.employee_id_Employees_createdAt= reader.GetValue<DateTime>("createdAt").ToString();
attendance_records.employee_id_Employees_modifiedAt= reader.GetValue<DateTime>("modifiedAt").ToString();
        }
    }
}


            return attendance_records;
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

public List<Attendance_recordsReportingModel> GetAllAttendance_recordsReporting(string ownername,int page=1,int itemsPerPage=100,List<OrderByModel> orderBy = null){
    int offset = (page - 1) * itemsPerPage;
    try
    {
        mySqlDatabaseConnector.OpenConnection();
        var connection = mySqlDatabaseConnector.GetConnection();
        using (var cmd = connection.CreateCommand())
        {
            cmd.CommandText = @"SELECT  t.* FROM attendance_records t  WHERE t.isActive=1 AND t.createdBy=@ownername ORDER BY column LIMIT @Offset, @ItemsPerPage";
            cmd.CommandText = Helper.ConverOrderListToSQL(cmd.CommandText, orderBy);
            cmd.Parameters.AddWithValue("@Offset", offset);
            cmd.Parameters.AddWithValue("@ownername", ownername);
            cmd.Parameters.AddWithValue("@ItemsPerPage", itemsPerPage);
            List<Attendance_recordsReportingModel> attendance_records = new List<Attendance_recordsReportingModel>();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Attendance_recordsReportingModel res = new Attendance_recordsReportingModel()
                    {
                        attendance_id= reader.GetValue<Int32>("attendance_id"),
employee_id= reader.GetValue<Int32>("employee_id"),
attendance_date= reader.GetValue<DateTime>("attendance_date").ToString(),
status= reader.GetValue<String>("status"),
remarks= reader.IsDBNull(Helper.GetColumnOrder(reader,"remarks")) ? (String?)null : reader.GetString("remarks"),
isActive= reader.GetValue<SByte>("isActive"),
createdBy= reader.GetValue<String>("createdBy"),
modifiedBy= reader.GetValue<String>("modifiedBy"),
createdAt= reader.GetValue<DateTime>("createdAt").ToString(),
modifiedAt= reader.GetValue<DateTime>("modifiedAt").ToString(),
                    };
                    attendance_records.Add(res);
                }
            }

            for (int i = 0; i < attendance_records.Count; i++)
{
    cmd.Parameters.Clear();
    cmd.CommandText = @"SELECT  t.* FROM employees t  WHERE t.isActive=1 AND t.createdBy=@ownername AND t.employee_id= @employee_id";
    cmd.Parameters.AddWithValue("@ownername", ownername);
    cmd.Parameters.AddWithValue("@employee_id",attendance_records[i].employee_id);

    using (var reader = cmd.ExecuteReader())
    {
        while (reader.Read())
        {
            attendance_records[i].employee_id_Employees_employee_id= reader.GetValue<Int32>("employee_id");
attendance_records[i].employee_id_Employees_first_name= reader.GetValue<String>("first_name");
attendance_records[i].employee_id_Employees_last_name= reader.GetValue<String>("last_name");
attendance_records[i].employee_id_Employees_email= reader.GetValue<String>("email");
attendance_records[i].employee_id_Employees_phone_number= reader.GetValue<String>("phone_number");
attendance_records[i].employee_id_Employees_department= reader.GetValue<String>("department");
attendance_records[i].employee_id_Employees_isActive= reader.GetValue<SByte>("isActive");
attendance_records[i].employee_id_Employees_createdBy= reader.GetValue<String>("createdBy");
attendance_records[i].employee_id_Employees_modifiedBy= reader.GetValue<String>("modifiedBy");
attendance_records[i].employee_id_Employees_createdAt= reader.GetValue<DateTime>("createdAt").ToString();
attendance_records[i].employee_id_Employees_modifiedAt= reader.GetValue<DateTime>("modifiedAt").ToString();
        }
    }
}


            return attendance_records;
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

        

        public bool UpdateAttendance_records(Attendance_recordsModel model)
        {
            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE attendance_records SET attendance_id=@attendance_id,employee_id=@employee_id,attendance_date=@attendance_date,status=@status,remarks=@remarks,isActive=@isActive,modifiedBy=@modifiedBy,modifiedAt=@modifiedAt WHERE attendance_id=@attendance_id";
                    cmd.Parameters.AddWithValue("@attendance_id", model.attendance_id);
cmd.Parameters.AddWithValue("@employee_id", model.employee_id);
cmd.Parameters.AddWithValue("@attendance_date", model.attendance_date);
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

        public long AddAttendance_records(Attendance_recordsModel model)
        {
            try
            {
		
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO attendance_records (attendance_id,employee_id,attendance_date,status,remarks,isActive,createdBy,modifiedBy,createdAt,modifiedAt) Values (@attendance_id,@employee_id,@attendance_date,@status,@remarks,@isActive,@createdBy,@modifiedBy,@createdAt,@modifiedAt);";
                    cmd.Parameters.AddWithValue("@attendance_id", model.attendance_id);
cmd.Parameters.AddWithValue("@employee_id", model.employee_id);
cmd.Parameters.AddWithValue("@attendance_date", model.attendance_date);
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

        public bool DeleteAttendance_records(int attendance_id)
        {
            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE attendance_records SET isActive=0 Where attendance_id=@attendance_id";
                    cmd.Parameters.AddWithValue("@attendance_id",attendance_id);
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
		public List<Attendance_recordsModel> FilterAttendance_records(string ownername,List<FilterModel> filterBy, string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy)
        {
            var ret = new List<Attendance_recordsModel>();
            int offset = (page - 1) * itemsPerPage;
            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT  t.* FROM Attendance_records t {filterColumns} ORDER BY column LIMIT @Offset, @ItemsPerPage ";
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
                            var t = new Attendance_recordsModel()
                            {
                                attendance_id= reader.GetValue<Int32>("attendance_id"),
employee_id= reader.GetValue<Int32>("employee_id"),
attendance_date= reader.GetValue<DateTime>("attendance_date").ToString(),
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

        public int GetFilterTotalRecordAttendance_records(string ownername,List<FilterModel> filterBy, string andOr)
        {
            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT count(*) TotalRecord FROM Attendance_records t {filterColumns}";
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
		public bool DeleteMultipleAttendance_records(List<DeleteMultipleModel> deleteParam, string andOr)
        {
            try
            {
                mySqlDatabaseConnector.OpenConnection();
                var connection = mySqlDatabaseConnector.GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    MySqlTransaction transaction = connection.BeginTransaction();
                    cmd.Transaction = transaction;
                    cmd.CommandText = @"UPDATE Attendance_records SET isActive=0 Where";
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
