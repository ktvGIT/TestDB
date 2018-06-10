using System;
using System.Collections.Generic;


namespace Branch
{
    class CSQL
    {
        public static List<CDepartment> GetDepartments()
        {
            List<CDepartment> Departments = new List<CDepartment>();
            string Sql = @" SELECT  Name, ID, ParentDepartmentID,Code
                                         FROM Department";
            CBranch Branch = new CBranch(Sql);
            Departments = Branch.GetDepartments();
            Branch.Dispose();
            return Departments;

        }





        public static List<CEmployee> GetEmployeesByDepartmentID(string DepartmentID)
        {
            List<CEmployee> Employees = new List<CEmployee>();
            string Sql = string.Format(@"SElect E.*,D.Name AS DeptName from Empoyee AS E
                                        Join  Department AS D ON D.ID = E.DepartmentID 
                                            where DepartmentID='{0}'", DepartmentID);

            CBranch Branch = new CBranch(Sql);
            Employees = Branch.GetEmployees();
            Branch.Dispose();
            return Employees;

        }

        public static void DeleteFromEmployee(CEmployee employee)
        {
            string Sql = string.Format(@"DELETE FROM [dbo].[Empoyee]
                                                        WHERE ID='{0}'
                                                        ", employee.ID);
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
                                                            convert(varchar, convert(datetime, '{3}', 104), 121),
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
                                           SET [SurName] = '{0}'
                                               ,[FirstName] = '{1}'
                                              ,[Patronymic] ='{2}'
                                              ,[DateOfBirth] = convert(varchar, convert(datetime, '{3}', 104), 121)
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

}
