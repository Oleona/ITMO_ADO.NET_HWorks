using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
namespace Test3
{
    public partial class CourseViewer : Form
    {
        private SchoolEntities schoolContext;


        public CourseViewer()
        {
            InitializeComponent();
        }

        private void CourseViewer_Load(object sender, EventArgs e)
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

        private void comboBoxCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                Course course = (Course)this.comboBoxCourses.SelectedItem;
                this.courseGridView.DataSource = course.People.ToList();
                courseGridView.Columns["HireDate"].Visible = false;
                courseGridView.Columns["EnrollmentDate"].Visible = false;
                courseGridView.Columns["OfficeAssignment"].Visible = false;
                dataGridView2.DataSource = schoolContext.Courses.Local.ToBindingList();

                courseGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Output()
        {
            var oncourses = schoolContext.OnlineCourses.OrderBy(c => c.CourseID);
            var ofcourses = schoolContext.OnsiteCourses.OrderBy(c => c.CourseID);
            if (this.radioButton1.Checked == true)
            {
                onOffGridView.DataSource = oncourses.ToList();
                onOffGridView.Columns["URL"].Visible = false;
            }
            else if (this.radioButton2.Checked == true)
                onOffGridView.DataSource = ofcourses.ToList();
            onOffGridView.Columns["Course"].Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Output();

        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
            schoolContext.Dispose();           Application.Exit();

        }

        private void saveChanges_Click(object sender, EventArgs e)
        {
            try
            {
                schoolContext.SaveChanges();
                MessageBox.Show("Changes saved to the database.");
                this.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
