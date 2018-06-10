using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Branch;
using System.Text.RegularExpressions;


namespace TestDB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = "Отдел Кадров v 0.5";
        }

        //Объект для связи приватных свойст с интерфейсом (временное решение, большая вероятность рассинхронизации)
        private CEmployee EMPOLOYEE = new CEmployee();
        
        //Сдесь зачатки локализации
        private CEmployee EMPLOEEYEEINRUS =new CEmployee("Номер","ID подразделения", "Фамилия", "Имя", "Отчество", "День рождения", "Серия документа", "Номер документа", "Должность","Подразделение");

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

            List<CDepartment> Departments = CSQL.GetDepartments();
            SetNodes(treeView1, Departments,
            CBranch.GetDepartmentsByParentDepartmentID(Departments, "").ToList()[0] as CDepartment);
            treeView1.SelectedNode = treeView1.Nodes[0];
            UpdListEmployee(CSQL.GetEmployeesByDepartmentID(treeView1.SelectedNode.Tag.ToString()), 0);

        }

        private void SetTextInTextBoxes(CEmployee employee)
        {
            List<string> values = CBranch.GetEmployeeClassPropertes(employee);
            for (int i = 0; i < values.Count; i++)
            {
                TextBox tb = this.Controls.Find(values[i], true).FirstOrDefault() as TextBox;
                tb.Text = employee[i];//listViewEmployee.SelectedItems[0].SubItems[i].Text;
            }
        }

        private void SetAgeText(CEmployee employee, int x, int y)
        {                       
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

        private void SetTextBoxes(CEmployee employee,int x, int y)
        {
            List<string> values = CBranch.GetEmployeeClassPropertes(employee);
            List<string> valuesInRus = CBranch.GetEmployeeValues(EMPLOEEYEEINRUS);

            int position = 0;
            int moveX, moveY;
            moveX = x;
            moveY = y;

            for (int i = 0; i < values.Count; i++)
            {
                TextBox textBoxSection = new TextBox();
                textBoxSection.Location = new System.Drawing.Point(moveX, moveY);
                textBoxSection.Name = values[i];
                textBoxSection.Size = new System.Drawing.Size(170, 23);
                textBoxSection.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
                splitContainer1.Panel2.Controls.Add(textBoxSection);

                Label Label = new Label();
                Label.Location = new System.Drawing.Point(moveX, moveY - 30);
                Label.Name = values[i];
                //Label.Text = values[i];
                Label.Text = valuesInRus[i];
                Label.Size = new System.Drawing.Size(105, 23);
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

        private void SetEmployeesColums(CEmployee employee)
        {
            listViewEmployee.Clear();
            listViewEmployee.View = View.Details;
            //List<string> values = CBranch.GetEmployeeClassPropertes(employee);
            List<string> values = CBranch.GetEmployeeValues(employee);

            for (int i = 0; i < values.Count; i++)
            {
                listViewEmployee.Columns.Add(values[i], 150);
            }

            listViewEmployee.FullRowSelect = true;
            listViewEmployee.GridLines = true;
            listViewEmployee.MultiSelect = false;
        }

        private void SetListEmployees(List<CEmployee> Employees)
        {
            

            foreach (CEmployee E in Employees)
            {
                List<string> values  = CBranch.GetEmployeeValues(E);
                ListViewItem listitem = new ListViewItem(values.ToArray());
                listViewEmployee.Items.Add(listitem);
            }
        }


        private void UpdListEmployee(List<CEmployee> employees, int positionInListEmployee)
        {
            SetEmployeesColums(EMPLOEEYEEINRUS);
            if (employees.Count != 0)
            {
                SetListEmployees(employees);
                listViewEmployee.Select();
                listViewEmployee.Items[positionInListEmployee].Focused = true;
                listViewEmployee.Items[positionInListEmployee].Selected = true;
                listViewEmployee.FocusedItem = listViewEmployee.Items[positionInListEmployee];
                listViewEmployee.EnsureVisible(positionInListEmployee);
                EMPOLOYEE = employees[positionInListEmployee];
                SetAgeText(EMPOLOYEE, 10, 230);
                SetTextBoxes(EMPOLOYEE, 10, 300);
                SetTextInTextBoxes(EMPOLOYEE);
            }
            else
                SetTextInTextBoxes(new CEmployee());
        }

        private CEmployee GetDateFromEditors()
        {
            CEmployee employee = new CEmployee();
            List<string> values = CBranch.GetEmployeeClassPropertes(employee);
            Dictionary<string, string> emp = new Dictionary<string, string>();
            for (int i = 0; i < values.Count; i++)
            {
                TextBox tb = this.Controls.Find(values[i], true).FirstOrDefault() as TextBox;
                if (tb.Text != "")
                {
                    employee[i] = tb.Text;
                }
            }
           
            string pattern = @"(?'date'(?:[0-9]?\d{2}\.[0-9]?\d{2}\.[0-9]?\d{4}))";
            Regex newReg = new Regex(pattern);
            MatchCollection matches = newReg.Matches(employee.DateOfBirth);
            if (matches.Count == 0 && employee.DateOfBirth != "")
            {
                // throw new Exception("День рождения Должен быть в формате: дд.ММ.YYYY");
                MessageBox.Show("День рождения Должен быть в формате: дд.ММ.YYYY");
                employee = new CEmployee();
                return employee;
            }
            employee.SetDepartmentID(EMPOLOYEE.GetDepartmentID());

           return employee;
        }
       
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {            
            treeView1.SelectedNode=e.Node;
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
            if (listViewEmployee.SelectedItems.Count != 0)
            {
                CSQL.InsertIntoEmployee(GetDateFromEditors());
                UpdListEmployee(CSQL.GetEmployeesByDepartmentID(treeView1.SelectedNode.Tag.ToString()), listViewEmployee.SelectedItems[0].Index);
            }
            else
                MessageBox.Show("Нет данных для обновленя");
        }

        private void buttonUpbEmp_Click(object sender, EventArgs e)
        {
            if (GetDateFromEditors().ID != "")
            {
                try
                {
                    CSQL.UpdateEmployee(GetDateFromEditors());
                    UpdListEmployee(CSQL.GetEmployeesByDepartmentID(treeView1.SelectedNode.Tag.ToString()), listViewEmployee.SelectedItems[0].Index);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
                MessageBox.Show("Нет данных для сохранения");
            
        }

        private void ButtonDelEmp_Click(object sender, EventArgs e)
        {
            if (listViewEmployee.SelectedItems.Count != 0)
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить выбранного сотрудника?", "Удаление", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        CSQL.DeleteFromEmployee(GetDateFromEditors());
                        UpdListEmployee(CSQL.GetEmployeesByDepartmentID(treeView1.SelectedNode.Tag.ToString()), listViewEmployee.SelectedItems[0].Index);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

            else
                MessageBox.Show("Нет данных для удаления");


        }

        private void buttonUpdDeptToEmp_Click(object sender, EventArgs e)
        {
            if (GetDateFromEditors().ID != "")
            {
                splitContainer1.Panel2.Enabled = false;
                treeView1.NodeMouseClick += treeView1_NodeMouseDeptToEmp_Click;
                treeView1.NodeMouseClick -= treeView1_NodeMouseClick;
                Button button = new Button();
                button.Name = "UpdDept";
                button.Text = "Изменить подразделение";

                button.Location = new System.Drawing.Point(3, treeView1.Size.Height + 30);
                button.Size = new System.Drawing.Size(150, 23);
                button.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
                button.Click += ButtonUpdDept_Click;
                splitContainer1.Panel1.Controls.Add(button);
            }
            else
                MessageBox.Show("Нет данных для обновленя");

        }
        private void treeView1_NodeMouseDeptToEmp_Click(object sender, TreeNodeMouseClickEventArgs e)
        {
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

