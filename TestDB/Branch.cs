using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

namespace Branch
{
    class CDepartment
    {
        public string ID;
        public string ParentDepartmentID;
        public string Code;
        public string Name;
        //public List<CDepartment> Department;
        //public List<CEmployee> Employees;
        //public List<CDepartment> Department = new List<CDepartment>();
        //public List<CEmployee> Employees = new List<CEmployee>();

        //public CDepartment(string iD, string parentDepartmentID, string code, string name, 
        //    List<CDepartment> department, List<CEmployee> employees)
        public CDepartment(string iD, string parentDepartmentID, string code, string name)
        {
          ID=iD;
          ParentDepartmentID=parentDepartmentID;
          Code=code;
          Name=name;
          //Department =  new List <CDepartment>();
         // Employees = new List<CEmployee>();
        }

        public CDepartment()
        {
        }
    }

    class CEmployee 
    {
        public string ID { get; set; }
        public string DepartmentID { get; set; }
        public string SurName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string DateOfBirth { get; set; }
        public string DocSeries { get; set; }
        public string DocNumber { get; set; }
        public string Position { get; set; }

        public CEmployee(string iD, 
             string departmentID, 
             string surName,
             string firstName,
             string patronymic,
             string dateOfBirth,
             string docSeries,
             string docNumber,
             string position
            )
    {
         ID=iD;
         DepartmentID=departmentID;
         SurName=surName;
         FirstName=firstName;
         Patronymic=patronymic;
         DateOfBirth=dateOfBirth;
         DocSeries=docSeries;
         DocNumber=docNumber;
         Position=position;

    }

        public CEmployee()
        {
        }
    }



    class CBranch: CReadBase
    {

        public CBranch(string SQLstr)
            : base(SQLstr)
    {
    }
        public List<CDepartment> GetDepartments()
        {
            List<CDepartment> Departments= new List<CDepartment>();


            
           SqlDataReader reader = mCmd.ExecuteReader();
            while (reader.Read())
            {
                //CDepartment Department = new CDepartment( reader["CHTO"].ToString() ?? "", 
                //                                          reader["KUDA"].ToString() ?? "",
                //                                          "", 
                //                                          reader["DeptName"].ToString() ?? "");        
                CDepartment Department = new CDepartment(reader["ID"].ToString() ?? "",
                                                        reader["ParentDepartmentID"].ToString() ?? "",
                                                        reader["Code"].ToString() ?? "",
                                                        reader["Name"].ToString() ?? "");

                Departments.Add(Department);
            }
            reader.Dispose();

            

            return Departments;
        }

        public List<CEmployee> GetEmployees()
        {
            List<CEmployee> Employees = new List<CEmployee>();

            SqlDataReader reader = mCmd.ExecuteReader();
            while (reader.Read())
            {
                //CEmployee Employee = new CEmployee(reader["UID"].ToString() ?? "", 
                //                                   reader["okDeptId"].ToString() ?? "", 
                //                                   reader["LN"].ToString() ?? "", 
                //                                   reader["FN"].ToString() ?? "", 
                //                                   reader["SN"].ToString() ?? "",
                //                                   reader["BirthDay"].ToString() ?? "",
                //                                   "",
                //                                   "", 
                //                                   reader["okJobId"].ToString() ?? "");
                CEmployee Employee = new CEmployee(reader["ID"].ToString() ?? "",
                                   reader["DepartmentID"].ToString() ?? "",
                                   reader["FirstName"].ToString() ?? "",
                                   reader["SurName"].ToString() ?? "",
                                   reader["Patronymic"].ToString() ?? "",
                                   reader["DateOfBirth"].ToString() ?? "",
                                   "",
                                   "",
                                   reader["Position"].ToString() ?? "");


                Employees.Add(Employee);
            }
            reader.Dispose();



            return Employees;
        }

    }

    public class CReadBase : IDisposable
    {
        private SqlConnection mConn;
        protected SqlCommand mCmd;
        private bool mDisposed;

        public CReadBase(string SQLstr)
        {
            string connstr = GetConnectionString();
            mConn = new SqlConnection(connstr);
            mConn.Open();
            mCmd = new SqlCommand(SQLstr, mConn);            
            mDisposed = false;
        }

        private string GetConnectionString()
        {
            try
            {
                string returnValue = null;
                ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["ConnStr"];
                if (settings != null)
                    returnValue = settings.ConnectionString;
                return returnValue;
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка обращения к БД " +  "(" + ex + ")");                
            }
        }

        public void Dispose()
        {
            if (mDisposed)
                return;
            mConn.Close();
            mDisposed = true;
        }



    }
}
