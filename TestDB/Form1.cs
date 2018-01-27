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
            this.Text = "Отдел Кадров v 0.5";
        }

        private CEmployee EMPOLOYEE = new CEmployee();

        private void SetNodes(TreeView treeView, List<CDepartment> Departments, CDepartment DepartmentRoot)
        {

            List<CDepartment> SubDepartments = CBranch.GetDepartmentsByParentDepartmentID(Departments, DepartmentRoot.ID).ToList();
            TreeNode node = new TreeNode(DepartmentRoot.Name);
            node.Tag = DepartmentRoot.ID;
            treeView.BeginUpdate();
            treeView.Nodes.Add(node);
            FillNode(node, Departments, DepartmentRoot);
            treeView.EndUpdate();

        }

        private void FillNode(TreeNode parentNode, List<CDepartment> Departments, CDepartment DepartmentRoot)
        {
            List<CDepartment> SubDepartments = CBranch.GetDepartmentsByParentDepartmentID(Departments, DepartmentRoot.ID).ToList();
            foreach (CDepartment D in SubDepartments)
            {


                TreeNode treeNodeSub = new TreeNode(D.Name);
                treeNodeSub.Tag = D.ID;

                parentNode.Nodes.Add(treeNodeSub);
                FillNode(treeNodeSub, Departments, D);

            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
           // SetListEmployees(CSQL.GetEmployees());
           
            //treeView.Nodes.Clear();
            List<CDepartment> Departments = CSQL.GetDepartments();
            SetNodes(treeView1, Departments,
            CBranch.GetDepartmentsByParentDepartmentID(Departments, "").ToList()[0] as CDepartment
             );
            treeView1.SelectedNode = treeView1.Nodes[0];
            //EMPOLOYEE=;
            UpdListEmployee(CSQL.GetEmployeesByDepartmentID(treeView1.SelectedNode.Tag.ToString()), 0);
            //SetTextBoxes(CSQL.GetEmployees());
        }

        private void SetTextInTextBoxes(CEmployee employee)
        {
            List<string> values = CBranch.GetEmployeeClassPropertes(employee);
            for (int i = 0; i < values.Count; i++)
            {
                TextBox Tb = this.Controls.Find(values[i], true).FirstOrDefault() as TextBox;
                Tb.Text = listViewEmployee.SelectedItems[0].SubItems[i].Text;
            }
        }

        private void SetAgeText(CEmployee employee)
        {
            
            int x = 10, y = 230;

            
            Label Label = this.Controls.Find("age", true).FirstOrDefault() as Label;

            if (Label != null)
                Label.Dispose();

             Label = new Label();

            Label.Location = new System.Drawing.Point(x, y );
            Label.Name = "age";
            Label.Text = "Возраст: "+employee.GetAge();
            Label.Size = new System.Drawing.Size(75, 23);
            Label.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            splitContainer1.Panel2.Controls.Add(Label);
           


        }

        private void SetTextBoxes(CEmployee employee)
        {

            List<string> values = CBranch.GetEmployeeClassPropertes(employee);

            int position = 0;
            int x = 10, y = 300, moveX, moveY;
            moveX = x;
            moveY = y;

            for (int i = 0; i < values.Count; i++)
            {
                TextBox textBoxSection = new TextBox();

                textBoxSection.Location = new System.Drawing.Point(moveX, moveY);

                textBoxSection.Name = values[i];
                textBoxSection.Size = new System.Drawing.Size(150, 23);
                textBoxSection.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
                splitContainer1.Panel2.Controls.Add(textBoxSection);



                Label Label = new Label();
                Label.Location = new System.Drawing.Point(moveX, moveY - 30);
                Label.Name = values[i];
                Label.Text = values[i];
                Label.Size = new System.Drawing.Size(75, 23);
                Label.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
                splitContainer1.Panel2.Controls.Add(Label);

                if (position != 2)
                {

                    moveX = moveX + 200;
                    position++;
                }
                else
                {
                    position = 0;
                    moveX = x;
                    moveY = moveY + 60; ;
                }
            }


        }

        private void SetListEmployees(List<CEmployee> Employees)
        {
            listViewEmployee.Clear();
            listViewEmployee.View = View.Details;

            CEmployee employee = new CEmployee();

            List<string> values = CBranch.GetEmployeeClassPropertes(employee);

            for (int i=0;i<values.Count;i++)
            {
                listViewEmployee.Columns.Add(values[i], 150);
            }

            listViewEmployee.FullRowSelect = true;
            listViewEmployee.GridLines = true;
            listViewEmployee.MultiSelect = false;

            foreach (CEmployee E in Employees)
            {

                

                values = CBranch.GetEmployeeValues(E);

               
              // values.Remove(values.Last() );

                


                ListViewItem listitem = new ListViewItem(values.ToArray());
                listViewEmployee.Items.Add(listitem);

            }
        }


        private void UpdListEmployee(List<CEmployee> employees, int positionInListEmployee)
        {

            SetListEmployees(employees);
            listViewEmployee.Select();
            listViewEmployee.Items[positionInListEmployee].Focused = true;
            listViewEmployee.Items[positionInListEmployee].Selected = true;
            listViewEmployee.FocusedItem = listViewEmployee.Items[positionInListEmployee];
            listViewEmployee.EnsureVisible(positionInListEmployee);
            EMPOLOYEE = employees[positionInListEmployee];
            SetAgeText(EMPOLOYEE);
            SetTextBoxes(EMPOLOYEE);
            SetTextInTextBoxes(EMPOLOYEE);
            //SetTextInTextBoxes(CSQL.GetEmployeesFromDepartment(treeView1.SelectedNode.Tag.ToString()));
            //  SetTextInTextBoxes(CSQL.GetEmployeesByDepartmentID(treeView1.SelectedNode.Tag.ToString()));
        }




        private CEmployee GetDateFromEditors()
        {
            CEmployee employee = new CEmployee();
            List<string> values = CBranch.GetEmployeeClassPropertes(employee);
            Dictionary<string, string> emp = new Dictionary<string, string>();
            for (int i = 0; i < values.Count; i++)
            {
                TextBox tb = this.Controls.Find(values[i], true).FirstOrDefault() as TextBox;
                employee[i] = tb.Text;
            }

           employee.SetDepartmentID(EMPOLOYEE.GetDepartmentID());
           return employee;
        }
       
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {

            
            treeView1.SelectedNode=e.Node;
            //string SelTrN = treeView1.SelectedNode.Tag.ToString();
            //List<CEmployee> emp = CSQL.GetEmployeesByDepartmentID(SelTrN);
            //UpdListEmployee(emp);
            //SetListEmployees(CSQL.GetEmployeesByDepartmentID(treeView1.SelectedNode.Tag.ToString()));
            UpdListEmployee(CSQL.GetEmployeesByDepartmentID(treeView1.SelectedNode.Tag.ToString()),0);
        }

        private void listViewEmployee_Click(object sender, EventArgs e)
        {
            if (listViewEmployee.SelectedItems.Count != 0)
            { 
                UpdListEmployee(CSQL.GetEmployeesByDepartmentID(treeView1.SelectedNode.Tag.ToString()), listViewEmployee.SelectedItems[0].Index);
            }
        }

        private void buttonInsEmp_Click(object sender, EventArgs e)
        {
            CSQL.InsertIntoEmployee(GetDateFromEditors());
            UpdListEmployee(CSQL.GetEmployeesByDepartmentID(treeView1.SelectedNode.Tag.ToString()), listViewEmployee.SelectedItems[0].Index);
        }

        private void buttonUpbEmp_Click(object sender, EventArgs e)
        {
            CSQL.UpdateEmployee(GetDateFromEditors());
            UpdListEmployee(CSQL.GetEmployeesByDepartmentID(treeView1.SelectedNode.Tag.ToString()), listViewEmployee.SelectedItems[0].Index);
        }

        private void ButtonDelEmp_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить выбранного сотрудника?", "Удаление", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                CSQL.DeleteFromEmployee(GetDateFromEditors());
                UpdListEmployee(CSQL.GetEmployeesByDepartmentID(treeView1.SelectedNode.Tag.ToString()), listViewEmployee.SelectedItems[0].Index);
            }
        }

        private void buttonUpdDeptToEmp_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel2.Enabled = false;
            //treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(treeView1_NodeMouseDeptToEmp_Click);
            treeView1.NodeMouseClick += treeView1_NodeMouseDeptToEmp_Click;
            treeView1.NodeMouseClick -= treeView1_NodeMouseClick;
            Button button = new Button();
            button.Name = "UpdDept";
            button.Text = "Изменить подразделение";

            button.Location = new System.Drawing.Point(3, treeView1.Size.Height + 30);
            button.Size = new System.Drawing.Size(150, 23);
            button.Anchor = (AnchorStyles.Bottom   | AnchorStyles.Left);
            button.Click += ButtonUpdDept_Click;
            splitContainer1.Panel1.Controls.Add(button);

        }
        private void treeView1_NodeMouseDeptToEmp_Click(object sender, TreeNodeMouseClickEventArgs e)
        {
            //MessageBox.Show("Изменили обработчик");
            // Button bn = this.Controls.Find("UpdDept", true).FirstOrDefault() as Button;
            // bn.Dispose();
            // treeView1.NodeMouseClick += treeView1_NodeMouseClick;
            treeView1.SelectedNode = e.Node;
            EMPOLOYEE.SetDepartmentID(treeView1.SelectedNode.Tag.ToString());

        }

        private void ButtonUpdDept_Click(object sender, EventArgs e)
        {
            CSQL.UpdateDeptIDInEmployee(EMPOLOYEE);
            treeView1.NodeMouseClick -= treeView1_NodeMouseDeptToEmp_Click;
            treeView1.NodeMouseClick += treeView1_NodeMouseClick;
            Button bn = this.Controls.Find("UpdDept", true).FirstOrDefault() as Button;
            splitContainer1.Panel2.Enabled = true;
            UpdListEmployee(CSQL.GetEmployeesByDepartmentID(treeView1.SelectedNode.Tag.ToString()), listViewEmployee.SelectedItems[0].Index);
            
            bn.Dispose();

        }

        }
}

