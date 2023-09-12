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
        DataTable taskList = new DataTable();
        Boolean isEditing = false;
        string filePath = "taskList.csv";
        public mainForm()
        {
            InitializeComponent();

            try
            {
                loadDataTableFromCSV(filePath);
            }
            catch (Exception e) 
            {
                //first time create table
                taskList.Columns.Add("Task Title");
                taskList.Columns.Add("Description");
                taskList.Columns.Add("Date");
                taskList.Columns.Add("Status");
            }
        }
        private void clearField()
        {
            tbTitle.Text = "";
            tbDesc.Text = "";
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
                        writer.Write(","); // Add a comma if it's not the last column
                    }
                }
                writer.WriteLine(); // Move to the next line

                // Write the data rows
                foreach (DataRow row in dataTable.Rows)
                {
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        writer.Write(row[i].ToString());
                        if (i < dataTable.Columns.Count - 1)
                        {
                            writer.Write(","); // Add a comma if it's not the last column
                        }
                    }
                    writer.WriteLine(); // Move to the next line
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
            dgvTaskList.DataSource = taskList;
       
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DateTime currentDateTime = DateTime.Now;
            taskList.Rows.Add(tbTitle.Text, tbDesc.Text, currentDateTime, "Pending");

            clearField();

            saveDataTableToCSV(taskList, filePath);
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
