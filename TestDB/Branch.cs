using System;

namespace Branch
{
    class CDepartment
    {
        public string ID;
        public string ParentDepartmentID;
        public string Code;
        public string Name;

        public CDepartment(string iD, string parentDepartmentID, string code, string name)
        {
          ID=iD;
          ParentDepartmentID=parentDepartmentID;
          Code=code;
          Name=name;
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


        public CEmployee()
        {
            ID = "";
            DepartmentID = "";
            SurName = "";
            FirstName = "";
            Patronymic = "";
            DateOfBirth = "";
            DocSeries = "";
            DocNumber = "";
            Position = "";
            DeptName = "";
        }
    }

  
   
   
}
