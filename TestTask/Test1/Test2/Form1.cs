using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void FillDataButton_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = schoolDataSet1.Person;            sqlDataAdapter1.Fill(schoolDataSet1.Person);
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            sqlDataAdapter1.Update(schoolDataSet1);
        }
    }
}
