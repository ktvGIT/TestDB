namespace TestDB
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.buttonUpdDeptToEmp = new System.Windows.Forms.Button();
            this.buttonDelEmp = new System.Windows.Forms.Button();
            this.buttonInsEmp = new System.Windows.Forms.Button();
            this.buttonUpbEmp = new System.Windows.Forms.Button();
            this.listViewEmployee = new System.Windows.Forms.ListView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            this.splitContainer1.Panel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.buttonUpdDeptToEmp);
            this.splitContainer1.Panel2.Controls.Add(this.buttonDelEmp);
            this.splitContainer1.Panel2.Controls.Add(this.buttonInsEmp);
            this.splitContainer1.Panel2.Controls.Add(this.buttonUpbEmp);
            this.splitContainer1.Panel2.Controls.Add(this.listViewEmployee);
            this.splitContainer1.Size = new System.Drawing.Size(1044, 549);
            this.splitContainer1.SplitterDistance = 345;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.Location = new System.Drawing.Point(3, 3);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(337, 471);
            this.treeView1.TabIndex = 3;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // buttonUpdDeptToEmp
            // 
            this.buttonUpdDeptToEmp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUpdDeptToEmp.Location = new System.Drawing.Point(524, 187);
            this.buttonUpdDeptToEmp.Name = "buttonUpdDeptToEmp";
            this.buttonUpdDeptToEmp.Size = new System.Drawing.Size(158, 23);
            this.buttonUpdDeptToEmp.TabIndex = 4;
            this.buttonUpdDeptToEmp.Text = "Изменить подразделение";
            this.buttonUpdDeptToEmp.UseVisualStyleBackColor = true;
            this.buttonUpdDeptToEmp.Click += new System.EventHandler(this.buttonUpdDeptToEmp_Click);
            // 
            // buttonDelEmp
            // 
            this.buttonDelEmp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDelEmp.Location = new System.Drawing.Point(537, 510);
            this.buttonDelEmp.Name = "buttonDelEmp";
            this.buttonDelEmp.Size = new System.Drawing.Size(145, 23);
            this.buttonDelEmp.TabIndex = 3;
            this.buttonDelEmp.Text = "Удалить выбранного";
            this.buttonDelEmp.UseVisualStyleBackColor = true;
            this.buttonDelEmp.Click += new System.EventHandler(this.ButtonDelEmp_Click);
            // 
            // buttonInsEmp
            // 
            this.buttonInsEmp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonInsEmp.Location = new System.Drawing.Point(537, 481);
            this.buttonInsEmp.Name = "buttonInsEmp";
            this.buttonInsEmp.Size = new System.Drawing.Size(145, 23);
            this.buttonInsEmp.TabIndex = 2;
            this.buttonInsEmp.Text = "Сохранить как нового";
            this.buttonInsEmp.UseVisualStyleBackColor = true;
            this.buttonInsEmp.Click += new System.EventHandler(this.buttonInsEmp_Click);
            // 
            // buttonUpbEmp
            // 
            this.buttonUpbEmp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUpbEmp.Location = new System.Drawing.Point(537, 452);
            this.buttonUpbEmp.Name = "buttonUpbEmp";
            this.buttonUpbEmp.Size = new System.Drawing.Size(145, 23);
            this.buttonUpbEmp.TabIndex = 1;
            this.buttonUpbEmp.Text = "Сохранить изменения";
            this.buttonUpbEmp.UseVisualStyleBackColor = true;
            this.buttonUpbEmp.Click += new System.EventHandler(this.buttonUpbEmp_Click);
            // 
            // listViewEmployee
            // 
            this.listViewEmployee.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewEmployee.Location = new System.Drawing.Point(3, -1);
            this.listViewEmployee.Name = "listViewEmployee";
            this.listViewEmployee.Size = new System.Drawing.Size(687, 182);
            this.listViewEmployee.TabIndex = 0;
            this.listViewEmployee.UseCompatibleStateImageBehavior = false;
            this.listViewEmployee.Click += new System.EventHandler(this.listViewEmployee_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1044, 549);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ListView listViewEmployee;
        private System.Windows.Forms.Button buttonUpbEmp;
        private System.Windows.Forms.Button buttonInsEmp;
        private System.Windows.Forms.Button buttonDelEmp;
        private System.Windows.Forms.Button buttonUpdDeptToEmp;
    }
}

