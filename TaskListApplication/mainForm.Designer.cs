namespace TaskListApplication
{
    partial class mainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnAdd = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tbTitle = new System.Windows.Forms.TextBox();
            this.tbDesc = new System.Windows.Forms.TextBox();
            this.dgvTaskList = new System.Windows.Forms.DataGridView();
            this.btnEdit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaskList)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnAdd.Location = new System.Drawing.Point(795, 68);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(128, 55);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add New";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitle.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(911, 45);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Task List Application";
            // 
            // tbTitle
            // 
            this.tbTitle.Location = new System.Drawing.Point(12, 68);
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.PlaceholderText = "Task Title";
            this.tbTitle.Size = new System.Drawing.Size(776, 31);
            this.tbTitle.TabIndex = 2;
            // 
            // tbDesc
            // 
            this.tbDesc.Location = new System.Drawing.Point(12, 105);
            this.tbDesc.Multiline = true;
            this.tbDesc.Name = "tbDesc";
            this.tbDesc.PlaceholderText = "Description";
            this.tbDesc.Size = new System.Drawing.Size(776, 80);
            this.tbDesc.TabIndex = 2;
            // 
            // dgvTaskList
            // 
            this.dgvTaskList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTaskList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTaskList.Location = new System.Drawing.Point(13, 202);
            this.dgvTaskList.Name = "dgvTaskList";
            this.dgvTaskList.RowHeadersWidth = 62;
            this.dgvTaskList.Size = new System.Drawing.Size(910, 436);
            this.dgvTaskList.TabIndex = 3;
            this.dgvTaskList.Text = "dataGridView1";
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(795, 129);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(128, 55);
            this.btnEdit.TabIndex = 4;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(935, 650);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.dgvTaskList);
            this.Controls.Add(this.tbDesc);
            this.Controls.Add(this.tbTitle);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnAdd);
            this.Name = "mainForm";
            this.Text = "Yokogawa task";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaskList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox tbTitle;
        private System.Windows.Forms.TextBox tbDesc;
        private System.Windows.Forms.DataGridView dgvTaskList;
        private System.Windows.Forms.Button btnEdit;
    }
}

