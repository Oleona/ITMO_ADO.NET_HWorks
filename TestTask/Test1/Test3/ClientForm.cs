using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test3
{
    public partial class ClientForm : Form
    {
        private SchoolEntities schoolContext;

        public ClientForm()
        {
            InitializeComponent();
        }

        

        
        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
            
            Application.Exit();
        }

        private void ClientForm_Load_1(object sender, EventArgs e)
        {
           
                schoolContext = new SchoolEntities();

                var courses = schoolContext.Courses.OrderBy(c => c.Title);
                try
                {
                    this.comboBoxCourses.DisplayMember = "Title";
                    this.comboBoxCourses.DataSource = courses.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        private void comboBoxCourses_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {

                Course course = (Course)this.comboBoxCourses.SelectedItem;
                this.courseGridView.DataSource = course.People.ToList();
                courseGridView.Columns["HireDate"].Visible = false;
                courseGridView.Columns["EnrollmentDate"].Visible = false;
                courseGridView.Columns["OfficeAssignment"].Visible = false;

                courseGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
    }

