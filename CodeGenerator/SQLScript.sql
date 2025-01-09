-- Create the Employees Table
CREATE TABLE employees (
  employee_id INT AUTO_INCREMENT PRIMARY KEY,
  first_name VARCHAR(255) NOT NULL,
  last_name VARCHAR(255) NOT NULL,
  email VARCHAR(255) UNIQUE NOT NULL,
  phone_number VARCHAR(15),
  department VARCHAR(255),
  isActive TINYINT(1) NOT NULL,
  createdBy VARCHAR(255) NOT NULL,
  modifiedBy VARCHAR(255) NOT NULL,
  createdAt DATETIME NOT NULL,
  modifiedAt DATETIME NOT NULL
);

-- Create the Attendance Records Table
CREATE TABLE attendance_records (
  attendance_id INT AUTO_INCREMENT PRIMARY KEY,
  employee_id INT NOT NULL,
  attendance_date DATE NOT NULL,
  status ENUM('Present', 'Absent', 'Leave') NOT NULL,
  remarks TEXT,
  isActive TINYINT(1) NOT NULL,
  createdBy VARCHAR(255) NOT NULL,
  modifiedBy VARCHAR(255) NOT NULL,
  createdAt DATETIME NOT NULL,
  modifiedAt DATETIME NOT NULL,
  FOREIGN KEY (employee_id) REFERENCES employees(employee_id) ON DELETE CASCADE
);

-- Create the Leave Requests Table
CREATE TABLE leave_requests (
  leave_id INT AUTO_INCREMENT PRIMARY KEY,
  employee_id INT NOT NULL,
  leave_start_date DATE NOT NULL,
  leave_end_date DATE NOT NULL,
  leave_type ENUM('Sick Leave', 'Casual Leave', 'Paid Leave') NOT NULL,
  status ENUM('Pending', 'Approved', 'Rejected') NOT NULL,
  remarks TEXT,
  isActive TINYINT(1) NOT NULL,
  createdBy VARCHAR(255) NOT NULL,
  modifiedBy VARCHAR(255) NOT NULL,
  createdAt DATETIME NOT NULL,
  modifiedAt DATETIME NOT NULL,
  FOREIGN KEY (employee_id) REFERENCES employees(employee_id) ON DELETE CASCADE
);
