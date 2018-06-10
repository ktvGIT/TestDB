using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Reflection;

namespace Branch
{
    class CBranch : CReadBase
    {

        public CBranch(string SQLstr)
            : base(SQLstr)
        {
        }

        public static CDepartment SearchInDepartmentList(List<CDepartment> Departments, string ID)
        {
            CDepartment department = new CDepartment("", "", "", "");
            foreach (CDepartment D in Departments)
            {
                if (D.ID == ID)
                    return D;
            }
            return department;
        }

        public static IEnumerable<CDepartment> GetDepartmentsByParentDepartmentID(List<CDepartment> Departments, string RootId)
        {
            return from t in Departments
                   orderby t.ID
                   where t.ParentDepartmentID == RootId
                   select t;
        }

        public List<CDepartment> GetDepartments()
        {
            List<CDepartment> Departments = new List<CDepartment>();
            SqlDataReader reader = mCmd.ExecuteReader();
            while (reader.Read())
            {
                CDepartment Department = new CDepartment(reader["ID"].ToString() ?? "",
                                                        reader["ParentDepartmentID"].ToString() ?? "",
                                                        reader["Code"].ToString() ?? "",
                                                        reader["Name"].ToString() ?? "");
                Departments.Add(Department);
            }
            reader.Dispose();
            return Departments;
        }

        public static List<string> GetEmployeeClassPropertes(CEmployee employee)
        {
            List<string> values = new List<string>();
            Type t = employee.GetType();
            PropertyInfo[] properties = t.GetProperties();
            foreach (PropertyInfo fil in properties)
            {
                if (fil.Name != "Item")
                    values.Add(fil.Name);
            }
            return values;
        }

        public static List<string> GetEmployeeValues(CEmployee employee)
        {
            List<string> values = new List<string>();
            Type t = employee.GetType();
            PropertyInfo[] properties = t.GetProperties();
            foreach (PropertyInfo fil in properties)
            {
                if (fil.Name != "Item")
                    values.Add(fil.GetValue(employee, null).ToString());
            }
            return values;
        }

        public List<CEmployee> GetEmployees()
        {
            List<CEmployee> Employees = new List<CEmployee>();

            SqlDataReader reader = mCmd.ExecuteReader();
            while (reader.Read())
            {
                CEmployee Employee = new CEmployee(reader["ID"].ToString() ?? "",
                                   reader["DepartmentID"].ToString() ?? "",
                                   reader["FirstName"].ToString() ?? "",
                                   reader["SurName"].ToString() ?? "",
                                   reader["Patronymic"].ToString() ?? "",
                                   (Convert.ToDateTime(reader["DateOfBirth"])).ToString("dd.MM.yyyy") ?? "",
                                   reader["DocSeries"].ToString() ?? "",
                                   reader["DocNumber"].ToString() ?? "",
                                   reader["Position"].ToString() ?? "",
                                   reader["DeptName"].ToString() ?? "");
                Employees.Add(Employee);
            }
            reader.Dispose();

            return Employees;
        }

    }

}
