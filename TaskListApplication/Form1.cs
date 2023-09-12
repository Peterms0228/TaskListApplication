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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

    }
}
