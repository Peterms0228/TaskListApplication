using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskListApplication
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        DataTable taskList = new DataTable();
        Boolean isEditing = false;

        private void clearField()
        {
            tbTitle.Text = "";
            tbDesc.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //create column
            taskList.Columns.Add("Task Title");
            taskList.Columns.Add("Description");
            taskList.Columns.Add("Date");

            dgvTaskList.DataSource = taskList;
       
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DateTime currentDateTime = DateTime.Now;
            taskList.Rows.Add(tbTitle.Text, tbDesc.Text, currentDateTime);

            clearField();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!isEditing)
            {
                //grad data from list to text box
                tbTitle.Text = taskList.Rows[dgvTaskList.CurrentCell.RowIndex].ItemArray[0].ToString();
                tbDesc.Text = taskList.Rows[dgvTaskList.CurrentCell.RowIndex].ItemArray[1].ToString();

                isEditing = true;
            }
            else
            {
                taskList.Rows[dgvTaskList.CurrentCell.RowIndex]["Task Title"] = tbTitle.Text;
                taskList.Rows[dgvTaskList.CurrentCell.RowIndex]["Description"] = tbDesc.Text;

                //end of editing
                isEditing = false;
                clearField();
            }
        }
    }
}
