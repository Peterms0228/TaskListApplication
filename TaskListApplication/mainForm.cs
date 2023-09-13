using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TaskListApplication
{
    public partial class mainForm : Form
    {
        enum Status { Pending, Completed, Expired };
        enum Priority { High, Medium, Low };

        DataTable taskList = new DataTable();
        Boolean isEditing = false;
        string filePath = "taskList.csv";
        
        public mainForm()
        {
            InitializeComponent();
            try
            {
                //load data
                loadDataTableFromCSV(filePath);
            }
            catch (Exception e) 
            {
                //trigger if first time create table
                taskList.Columns.Add("Task Title");
                taskList.Columns.Add("Description");
                taskList.Columns.Add("Priority");
                taskList.Columns.Add("Due Date");
                taskList.Columns.Add("Status");
            }
        }
        private void clearField()
        {
            tbTitle.Text = "";
            tbDesc.Text = "";
            cbPriority.SelectedItem = Priority.Medium;
            dtpDueDate.Value = DateTime.Now;
            cbStatus.SelectedItem = Status.Pending;
        }

        // Function to save DataTable to a CSV file
        private void saveDataTableToCSV(DataTable dataTable, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Write the column names as headers
                foreach (DataColumn column in dataTable.Columns)
                {
                    writer.Write(column.ColumnName);
                    if (column.Ordinal < dataTable.Columns.Count - 1)
                    {
                        writer.Write(","); // Add a ',' if it's not the last column
                    }
                }
                writer.WriteLine(); 

                // Write the data rows
                foreach (DataRow row in dataTable.Rows)
                {
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        writer.Write(row[i].ToString());
                        if (i < dataTable.Columns.Count - 1)
                        {
                            writer.Write(","); // Add a ',' if it's not the last column
                        }
                    }
                    writer.WriteLine(); 
                }
            }
        }

        private DataTable loadDataTableFromCSV(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                // Read the column names from the first line
                string[] columns = reader.ReadLine().Split(',');

                // Create columns in the DataTable
                foreach (string columnName in columns)
                { 
                    taskList.Columns.Add(columnName);
                }

                // Read and add data rows
                while (!reader.EndOfStream)
                {
                    string[] data = reader.ReadLine().Split(',');
                    taskList.Rows.Add(data);
                }
            }
            return taskList;
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            //inital priority = medium
            cbPriority.DataSource = Enum.GetValues(typeof(Priority));
            cbPriority.SelectedItem = Priority.Medium;

            //initial status = pending
            cbStatus.DataSource = Enum.GetValues(typeof(Status));
            cbStatus.SelectedItem = Status.Pending;

            //inital dueDate = today 
            dtpDueDate.Value = DateTime.Now;

            //set TaskList datasource
            dgvTaskList.DataSource = taskList;
       
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //add data to data list
            var title = tbTitle.Text;
            var desc = tbDesc.Text;
            var prio = cbPriority.SelectedItem;
            var dueDate = dtpDueDate.Value.ToString("dd/MM/yyyy");
            var status = cbStatus.SelectedValue;
            taskList.Rows.Add(title, desc, prio, dueDate, status);

            clearField();
            saveDataTableToCSV(taskList, filePath);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!isEditing)
            {
                //grab data from list to text box
                tbTitle.Text = taskList.Rows[dgvTaskList.CurrentCell.RowIndex]["Task Title"].ToString();
                tbDesc.Text = taskList.Rows[dgvTaskList.CurrentCell.RowIndex]["Description"].ToString();

                var strPriority = taskList.Rows[dgvTaskList.CurrentCell.RowIndex]["Priority"].ToString();
                cbPriority.SelectedItem = (Priority)Enum.Parse(typeof(Priority), strPriority);

                var strDueDate = taskList.Rows[dgvTaskList.CurrentCell.RowIndex]["Due Date"].ToString();
                dtpDueDate.Value = DateTime.Parse(strDueDate);

                cbStatus.Enabled = true;
                var strStatus = taskList.Rows[dgvTaskList.CurrentCell.RowIndex]["Status"].ToString();
                cbStatus.SelectedItem = (Status)Enum.Parse(typeof(Status), strStatus);

                //editing
                isEditing = true;
                btnEdit.Text = "Editing...";
            }
            else
            {
                //set data to list
                taskList.Rows[dgvTaskList.CurrentCell.RowIndex]["Task Title"] = tbTitle.Text;
                taskList.Rows[dgvTaskList.CurrentCell.RowIndex]["Description"] = tbDesc.Text;
                taskList.Rows[dgvTaskList.CurrentCell.RowIndex]["Priority"] = cbPriority.Text;
                taskList.Rows[dgvTaskList.CurrentCell.RowIndex]["Due Date"] = dtpDueDate.Value.ToString("dd/MM/yyyy");
                taskList.Rows[dgvTaskList.CurrentCell.RowIndex]["Status"] = cbStatus.Text;

                //end of editing
                cbStatus.Enabled = false;
                isEditing = false;
                btnEdit.Text = "Edit";
                clearField();
                saveDataTableToCSV(taskList, filePath);
            }
        }
    }
}
