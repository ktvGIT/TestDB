using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Reflection;
using System.Collections;

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
        private string DepartmentID { get; set; }
        public string SurName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string DateOfBirth { get; set; }
        public string DocSeries { get; set; }
        public string DocNumber { get; set; }
        public string Position { get; set; }
        public string DeptName { get; set; }

        public string GetDepartmentID()
        { return DepartmentID; }

        public void SetDepartmentID(string pepartmentID)
        {  DepartmentID= pepartmentID; }

    


        public string this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0: return this.ID;
                    //case 1: return this.DepartmentID;
                    case 1: return this.SurName;
                    case 2: return this.FirstName;
                    case 3: return this.Patronymic;
                    case 4: return this.DateOfBirth;
                    case 5: return this.DocSeries;
                    case 6: return this.DocNumber;
                    case 7: return this.Position;
                    case 8: return this.DeptName;

                    default: throw new ArgumentOutOfRangeException("Out of Index");
                }
            }

            set
            {
                switch (i)
                {
                    case 0: { this.ID = value; break; }
                    //case 1: { this.DepartmentID = value; break; }
                    case 1: { this.SurName = value; break; }
                    case 2: { this.FirstName = value; break; }
                    case 3: { this.Patronymic = value; break; }
                    case 4: { this.DateOfBirth = value; break; }
                    case 5: { this.DocSeries = value; break; }
                    case 6: { this.DocNumber = value; break; }
                    case 7: { this.Position = value; break; }
                    case 8: { this.DeptName = value; break; }

                    default: throw new ArgumentOutOfRangeException("Out of Index");
                }
            }

        }
        public CEmployee(string iD, 
             string departmentID, 
             string surName,
             string firstName,
             string patronymic,
             string dateOfBirth,
             string docSeries,
             string docNumber,
             string position,
             string deptName
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
         DeptName = deptName;
       

    }
        public string GetAge()
        {
            string age = "01.01.1900";
            DateTime birthDate= Convert.ToDateTime(DateOfBirth);
            DateTime dateNow = DateTime.Now;
            int year = dateNow.Year - birthDate.Year;
            if (dateNow.Month < birthDate.Month || (dateNow.Month == birthDate.Month && dateNow.Day < birthDate.Day))
                year--;
            age = year.ToString();
            return age;
        }

        public void GetDeptName(string deptname)
        {
            DeptName = deptname;
        }

        public void SetEmployee(string[] values)
        {
            CEmployee e = new CEmployee();
           
        }



        public CEmployee()
        {
        }
    }

    class CSQL
    {
        public static List<CDepartment> GetDepartments()
        {
            List<CDepartment> Departments = new List<CDepartment>();
            //string Sql = @" SELECT  DeptName, CHTO, KUDA
            //                            FROM t_departments";
            string Sql = @" SELECT  Name, ID, ParentDepartmentID,Code
                                         FROM Department";
            CBranch Branch = new CBranch(Sql);
            Departments = Branch.GetDepartments();
            Branch.Dispose();
            return Departments;

        }

        //public static List<CEmployee> GetEmployees()
        //{
        //    List<CEmployee> Employees = new List<CEmployee>();
        //    //string Sql = @" SELECT TOP (200) UID, UID,okDeptId,  LN, FN, SN,  BirthDay,  
        //    //                okJobId FROM t_UsersInfo";

        //    //string Sql = @"SELECT ID, FirstName, SurName, Patronymic, DateOfBirth, DocSeries, 
        //    //                          DocNumber, Position, DepartmentID
        //    //                          FROM  Empoyee";

        //    string Sql = @"SElect E.*,D.Name AS DeptName from Empoyee AS E
        //                        Join  Department AS D ON D.ID = E.DepartmentID";

        //    CBranch Branch = new CBranch(Sql);
        //    Employees = Branch.GetEmployees();
        //    Branch.Dispose();
        //    return Employees;

        //}

        

        public static List<CEmployee> GetEmployeesByDepartmentID(string DepartmentID)
        {
            List<CEmployee> Employees = new List<CEmployee>();
            //string Sql = @" SELECT TOP (200) UID, UID,okDeptId,  LN, FN, SN,  BirthDay,  
            //                okJobId FROM t_UsersInfo";
        
            string Sql = string.Format(@"SElect E.*,D.Name AS DeptName from Empoyee AS E
                                        Join  Department AS D ON D.ID = E.DepartmentID 
                                            where DepartmentID='{0}'", DepartmentID) ;

            CBranch Branch = new CBranch(Sql);
            Employees = Branch.GetEmployees();
            Branch.Dispose();
            return Employees;

        }

        public static void DeleteFromEmployee(CEmployee employee)
        {
            string Sql = string.Format(@"DELETE FROM [dbo].[Empoyee]
                                                        WHERE ID='{0}'
                                                        ",employee.ID);
            CBranch Branch = new CBranch(Sql);
            Branch.ExecSql();
            Branch.Dispose();
        }

            public static void InsertIntoEmployee(CEmployee employee)
        {
                string Sql = string.Format(@"  INSERT INTO[dbo].[Empoyee]
                                                        ([FirstName]
                                                          ,[SurName]
                                                          ,[Patronymic]
                                                          ,[DateOfBirth]
                                                          ,[DocSeries]
                                                          ,[DocNumber]
                                                          ,[Position]
                                                          ,[DepartmentID])
                                                    VALUES (
                                                            '{0}',
                                                            '{1}',
                                                            '{2}',
                                                            '{3}',
                                                            '{4}',
                                                            '{5}',
                                                            '{6}',
                                                            '{7}')
                                                            ",
                                                            employee.FirstName,
                                                            employee.SurName,
                                                            employee.Patronymic,
                                                            Convert.ToDateTime(employee.DateOfBirth),
                                                            employee.DocSeries,
                                                            employee.DocNumber,
                                                            employee.Position,
                                                            employee.GetDepartmentID());
            CBranch Branch = new CBranch(Sql);
            Branch.ExecSql();
            Branch.Dispose();
        }

        public static void UpdateDeptIDInEmployee(CEmployee employee)
        {
            string Sql = string.Format(@"UPDATE [dbo].[Empoyee]
                                           SET [DepartmentID] = '{0}'
                                         WHERE ID='{1}'
                                                            ",
                                                          
                                                            employee.GetDepartmentID(),
                                                            employee.ID);
            CBranch Branch = new CBranch(Sql);
            Branch.ExecSql();
            Branch.Dispose();
        }

        public static void UpdateEmployee(CEmployee employee)
        {
            string Sql = string.Format(@"UPDATE [dbo].[Empoyee]
                                           SET [FirstName] = '{0}'
                                               ,[SurName] = '{1}'
                                              ,[Patronymic] ='{2}'
                                              ,[DateOfBirth] = '{3}'
                                              ,[DocSeries] = '{4}'
                                              ,[DocNumber] ='{5}'
                                              ,[Position] = '{6}'
                                              ,[DepartmentID] = '{7}'
                                         WHERE ID='{8}'
                                                            ",
                                                            employee.FirstName,
                                                            employee.SurName,
                                                            employee.Patronymic,
                                                            Convert.ToDateTime(employee.DateOfBirth),
                                                            employee.DocSeries,
                                                            employee.DocNumber,
                                                            employee.Position,
                                                            employee.GetDepartmentID(),
                                                            employee.ID);
            CBranch Branch = new CBranch(Sql);
            Branch.ExecSql();
            Branch.Dispose();
        }

    }

    class CBranch: CReadBase
    {

        public CBranch(string SQLstr)
            : base(SQLstr)
    {
    }

        public static  CDepartment SearchInDepartmentList(List<CDepartment> Departments, string ID)
        {
            CDepartment department = new CDepartment("", "", "", "");
            foreach (CDepartment D in Departments)
            {
                if (D.ID == ID)
                    return D;
            }
            return department;
        }

        public static  IEnumerable<CDepartment> GetDepartmentsByParentDepartmentID(List<CDepartment> Departments, string RootId)
        {
            return from t in Departments
                   orderby t.ID
                   where t.ParentDepartmentID == RootId
                   select t;
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

        public static List<string> GetEmployeeClassPropertes(CEmployee employee)
        {
            List<string> values = new List<string>();
            Type t = employee.GetType();
            //fieldNames = t.GetFields();
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
            //fieldNames = t.GetFields();
            PropertyInfo[] properties = t.GetProperties();
            foreach (PropertyInfo fil in properties)
            {
                if(fil.Name!="Item")
                values.Add(fil.GetValue(employee,null).ToString() );
            }
            return values;
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
                                   //reader["DateOfBirth"].ToString() ?? "",
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

        public void ExecSql()
        {
            mCmd.ExecuteNonQuery();
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
