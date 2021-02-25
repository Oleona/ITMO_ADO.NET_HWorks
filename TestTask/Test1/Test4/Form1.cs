using CatCodeFirst;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test4
{
    public partial class Cattery : Form
    {
        CatContext context = new CatContext();

        public List<Order> Orders { get; private set; }

        public Cattery()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Cat cat = new Cat
                {
                    CatName = this.textBox1.Text,
                    CatAge = Int32.Parse(textBox2.Text),
                    CatBreed = this.textBox3.Text,
                    CatColor = this.textBox4.Text,
                    Orders = listBoxOrder.SelectedItems.OfType<Order>().ToList()
                };
                context.Cats.Add(cat);
                context.SaveChanges();

                textBox1.Text = String.Empty;
                textBox2.Text = String.Empty;
                textBox3.Text = String.Empty;
                textBox4.Text = String.Empty;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.ToString());
            }
        }

        private void btnShowCat_Click(object sender, EventArgs e)
        {
            var query = from c in context.Cats
                        orderby c.CatName
                        select c;
            dataGridViewCat.DataSource = query.ToList();
        }

        private void btnAddOrder_Click(object sender, EventArgs e)
        {
            try
            {
                Order order = new Order
                {
                    ProductType = this.textBox5.Text,
                    ProductName = this.textBox6.Text,
                    Quantity = Int32.Parse(textBox7.Text),
                    PurchaseDate = DateTime.Parse(this.textBox8.Text),

                };
                context.Orders.Add(order);
                context.SaveChanges();

                textBox5.Text = String.Empty;
                textBox6.Text = String.Empty;
                textBox7.Text = String.Empty;
                textBox8.Text = String.Empty;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.ToString());
            }
        }

        private void btnShowOrder_Click(object sender, EventArgs e)
        {
            var query = from o in context.Orders
                        orderby o.ProductType
                        select o;
            dataGridViewOrder.DataSource = query.ToList();
            listBoxOrder.DataSource = context.Orders.ToList();
        }

        private void Cattery_Load(object sender, EventArgs e)
        {
            // listBoxOrder.DataSource = context.Orders.ToList();
        }


        private void btnAddOrderForCat_Click(object sender, EventArgs e)
        {
            if (label9.Text == String.Empty) return;
            var id = Convert.ToInt32(label9.Text);
            var cat = context.Cats.Find(id);
            if (cat == null) return;
            
            else
            {       
                cat.Orders.AddRange(listBoxOrder.SelectedItems.OfType<Order>());
                    context.Entry(cat).State = EntityState.Modified;
               
                context.SaveChanges();
                
            }
        }

        private void dataGridViewCat_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewCat.CurrentRow == null) return;
            var cat = dataGridViewCat.CurrentRow.DataBoundItem as Cat;
            if (cat == null) return;
            label9.Text = Convert.ToString(cat.CatId);

        }
    }
}


