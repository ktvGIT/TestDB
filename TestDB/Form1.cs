using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Branch;
using System.Reflection;
//using System.Collections;

namespace TestDB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            List<CDepartment> Departments = GetDepartments();

           


            GetNode(treeView1, Departments,
              // SearchInDepartmentList(Departments, "fb9d1a43-5796-4190-abd4-39ffd8c87476") );
              GetSubDepartment(Departments, "").ToList()[0] as CDepartment
               );
            TextBox textBoxSection = new TextBox();
            textBoxSection.Location = new System.Drawing.Point(38, 350);
            textBoxSection.Name = "textBoxSection1";
            textBoxSection.Size =  new System.Drawing.Size(75, 23);
            textBoxSection.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            //splitContainer1.Panel1.
            splitContainer1.Panel2.Controls.Add(textBoxSection);
            // button4.Location.Y + 10;

            //splitContainer1.Panel1
        }

        private List <CDepartment> GetDepartments()
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

        private List<CEmployee> GetEmployees()
        {
            List<CEmployee> Employees = new List<CEmployee>();
            //string Sql = @" SELECT TOP (200) UID, UID,okDeptId,  LN, FN, SN,  BirthDay,  
            //                okJobId FROM t_UsersInfo";

            string Sql = @"SELECT ID, FirstName, SurName, Patronymic, DateOfBirth, DocSeries, 
                                      DocNumber, Position, DepartmentID
                                      FROM  Empoyee";

            CBranch Branch = new CBranch(Sql);
            Employees = Branch.GetEmployees();
            Branch.Dispose();
            return Employees;

        }

        private void FillTreeView2(TreeView treeView, List<CDepartment> Departments, string RootId)
        {
            treeView.Nodes.Clear();
            foreach (CDepartment D in Departments)
            {

                string id;
                string parentId;
                string name;
                //if(parentId==RootId)
                //{
                //    treeView.Nodes.Add(FillNode(id,name))
                //}

            }
        }
        private void FillTreeVew(TreeView treeView, List<CDepartment> Departments, string RootId)
        {

        }

        //private TreeNode GetSubTree(List<CDepartment> Departments, TreeNode treeNode)
        //{
        //    List<CDepartment> SubDepartments = GetSubDepartment(Departments, Department.ID).ToList();
        //    foreach (CDepartment D in Departments)
        //    {
        //        GetSubTree( Departments, D);
        //    }
        //}

        private void GetNode(TreeView treeView,List<CDepartment> Departments, CDepartment DepartmentRoot)
        {
            //treeView.Nodes.Clear();
            List<CDepartment> SubDepartments = GetSubDepartment(Departments, DepartmentRoot.ID).ToList();
            TreeNode node = new TreeNode(DepartmentRoot.Name);
            node.Tag = DepartmentRoot.ID;
            treeView.Nodes.Add(node);

            //foreach (CDepartment D in SubDepartments)
            // {
            //TreeNode node = new TreeNode(DepartmentRoot.Name);
            //node.Tag = DepartmentRoot.Name;
            //treeView.Nodes.Add(node);
            // FillNode(node, Departments,D);

            // }
            FillNode(node, Departments, DepartmentRoot);
            // return SubDepartments;
        }

        private void FillNode(TreeNode parentNode, List<CDepartment> Departments, CDepartment DepartmentRoot)
        {
            List<CDepartment> SubDepartments = GetSubDepartment(Departments, DepartmentRoot.ID).ToList();
            //if(SubDepartments.Count==0)

            

            //TreeNode treeNode = new TreeNode(DepartmentRoot.Name);
           // treeNode.Tag = DepartmentRoot.ID;
            
           // if (parentNode.Tag.ToString()!= treeNode.Tag.ToString() )
           // {

            //TreeNode[] treeNodes = parentNode.Nodes.Find(DepartmentRoot.ID, true);
            //if (treeNodes.Length != 0)
            //    (treeNodes.GetValue(0) as TreeNode).Nodes.Add(treeNode);

          //  parentNode.Nodes.Add(treeNode);
           // }


            foreach (CDepartment D in SubDepartments)
                {
               

                TreeNode treeNodeSub = new TreeNode(D.Name);
                    treeNodeSub.Tag = D.ID;
                //treeNode.Nodes.Add(treeNodeSub);
                parentNode.Nodes.Add(treeNodeSub);
                    FillNode(treeNodeSub, Departments, D);

                }
            
        }


        private TreeNode FillNode(string id, string name)
        {

            TreeNode treeNode = new TreeNode();

            treeNode.Tag = id;
            treeNode.Name = id.ToString();
            treeNode.Text = name;
            return treeNode;

        }

        private TreeNode FillNode(string id, string name, TreeNode RootNode)
        {
            TreeNode treeNode = new TreeNode();

            treeNode.Tag = id;
            treeNode.Name = id.ToString();
            treeNode.Text = name;
            RootNode.Nodes.Add(treeNode);
            return RootNode;

        }
        private CDepartment SearchInDepartmentList(List<CDepartment> Departments,string ID)
        {
            CDepartment department = new CDepartment("","","","");
            foreach (CDepartment D in Departments)
            {
                if (D.ID == ID)
                    return D;
            }
            return department;
        }




        private  IEnumerable<CDepartment> GetSubDepartment(List<CDepartment> Departments, string RootId)
        {
            return from t in Departments
                   orderby t.ID
                   where t.ParentDepartmentID == RootId
                   select t;
        }

        
        
            private void Form1_Load(object sender, EventArgs e)
        {
            listViewEmployee.Clear();
            listViewEmployee.View = View.Details;

            CEmployee Employee = new CEmployee();

            Type t = Employee.GetType();
            //FieldInfo[] fieldNames = t.GetFields();
            PropertyInfo[] properties = t.GetProperties();
            //foreach (PropertyInfo fil in properties)

            foreach (PropertyInfo fil in properties)
            {
                listViewEmployee.Columns.Add(fil.Name, 150);
            }

            listViewEmployee.FullRowSelect = true;
            listViewEmployee.GridLines = true;
            listViewEmployee.MultiSelect = false;


            List<CEmployee> Employees = GetEmployees();
            foreach (CEmployee E in Employees)
            {

                List<string> values = new List<string>();
                t = E.GetType();
                //fieldNames = t.GetFields();
                properties = t.GetProperties();
                foreach (PropertyInfo fil in properties)
                {
                    values.Add(fil.GetValue(E, null).ToString());
                }

                string[] list = values.ToArray();

                ListViewItem listitem = new ListViewItem(list);
                listViewEmployee.Items.Add(listitem);

            }

        }

        private void treeView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show(treeView1.SelectedNode.Tag.ToString());
        }
    }
}
