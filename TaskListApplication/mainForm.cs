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

        //file path of the data (.txt or .csv)
        string filePath = "TaskListData.csv";

        //edit variable
        Boolean isEditing = false;
        int editingCell = 0;
        
        public mainForm()
        {
            InitializeComponent();
            try
            {
                //load data
                loadDataTableFromCSV(filePath);
            }
            catch (Exception ex) 
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
            //reset field
            tbTitle.Text = "";
            tbDesc.Text = "";
            tbDuration.Text = "0";
            cbPriority.SelectedItem = Priority.Medium;
            dtpDueDate.Value = DateTime.Now;
            cbStatus.SelectedItem = Status.Pending;

            //reset edit status
            isEditing = false;
            btnEdit.Text = "Edit";
            btnCancelEditing.Visible = false;

        }

        //check is the string null
        private Boolean isStringNull(string str)
        {
            if (str.Length <= 0 || str == null || str.Equals(""))
            {
                return true;
            }
            return false;
        }

        //save data to CSV file
        private void saveDataTableToCSV(DataTable dataTable, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                //write the column names as headers
                foreach (DataColumn column in dataTable.Columns)
                {
                    writer.Write(column.ColumnName);
                    if (column.Ordinal < dataTable.Columns.Count - 1)
                    {
                        writer.Write(","); // Add a ',' if it's not the last column
                    }
                }
                writer.WriteLine(); 

                //write the data rows
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

        //load data from CSV file
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
            //check is title null
            if (isStringNull(tbTitle.Text))
            {
                MessageBox.Show("Empty Task Title");
            }
            else
            {
                //save data to a data row
                DataRow newDataRow = taskList.NewRow();
                newDataRow["Task Title"] = tbTitle.Text;
                newDataRow["Description"] = tbDesc.Text;
                newDataRow["Priority"] = cbPriority.SelectedItem;
                newDataRow["Due Date"] = dtpDueDate.Value.ToString("dd/MM/yyyy");
                newDataRow["Status"] = cbStatus.SelectedValue;

                taskList.Rows.InsertAt(newDataRow, 0);
                clearField();
                saveDataTableToCSV(taskList, filePath);               
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!isEditing)
            {
                editingCell = dgvTaskList.CurrentCell.RowIndex;

                //grab data from list to text box
                tbTitle.Text = taskList.Rows[editingCell]["Task Title"].ToString();
                tbDesc.Text = taskList.Rows[editingCell]["Description"].ToString();
                var strPriority = taskList.Rows[editingCell]["Priority"].ToString();
                cbPriority.SelectedItem = (Priority)Enum.Parse(typeof(Priority), strPriority);
                var strDueDate = taskList.Rows[editingCell]["Due Date"].ToString();
                dtpDueDate.Value = DateTime.Parse(strDueDate);
                var strStatus = taskList.Rows[editingCell]["Status"].ToString();
                cbStatus.SelectedItem = (Status)Enum.Parse(typeof(Status), strStatus);

                //editing
                isEditing = true;
                btnEdit.Text = "Save";
                btnCancelEditing.Visible = true;
            }
            else
            {
                //set data to list
                taskList.Rows[editingCell]["Task Title"] = tbTitle.Text;
                taskList.Rows[editingCell]["Description"] = tbDesc.Text;
                taskList.Rows[editingCell]["Priority"] = cbPriority.Text;
                taskList.Rows[editingCell]["Due Date"] = dtpDueDate.Value.ToString("dd/MM/yyyy");
                taskList.Rows[editingCell]["Status"] = cbStatus.Text;

                //end of editing
                clearField();
                saveDataTableToCSV(taskList, filePath);
            }
        }

        private void tbDuration_TextChanged(object sender, EventArgs e)
        {
            DateTime estimateDueDate = dtpDueDate.Value;
            try
            {
                //calc due date = now + duration
                int.TryParse(tbDuration.Text, out int numberOfDays);
                estimateDueDate = DateTime.Now.AddDays(numberOfDays);
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
            dtpDueDate.Text = estimateDueDate.ToString();
        }
        
        private void tbDuration_KeyPress(object sender, KeyPressEventArgs e)
        {
            //check textbox duration only allow integer
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedCell = dgvTaskList.CurrentCell.RowIndex;
                var title = taskList.Rows[selectedCell]["Task Title"].ToString(); ;
                DialogResult dialogResult = MessageBox.Show("Sure to delete \"" + title + "\" ?", "Alert", MessageBoxButtons.YesNoCancel);
                if (dialogResult == DialogResult.Yes)
                {
                    taskList.Rows[selectedCell].Delete(); 
                    saveDataTableToCSV(taskList, filePath);
                }
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
            clearField();
        }
        private void btnCancelEditing_Click(object sender, EventArgs e)
        {
            clearField();
        }
    }
}
