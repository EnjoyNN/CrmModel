using CrmBuisnessLogic.Model;
using System;
using System.Data.Entity;
using System.Windows.Forms;

namespace CrmUI
{
    public partial class Catalog<T> : Form
        where T: class
    {
        CrmContext db;
        DbSet<T> set;

        public Catalog(CrmContext db, DbSet<T> set)
        {
            InitializeComponent();

            this.db = db;
            this.set = set;
            //var set = db.Set(typeof(T));
            set.Load();
            dataGridView1.DataSource = set.Local.ToBindingList();
        }


        private void Catalog_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var id = dataGridView1.SelectedRows[0].Cells[0].Value;

            if (typeof(T) == typeof(Product))
            {
                //по идентификатору ищем продукт
                var product = set.Find(id) as Product;
                if (product != null)
                {
                    var form = new ProductForm(product);

                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        product = form.Product;
                        db.SaveChanges();
                        dataGridView1.Update();
                    }
                }
            }
            if (typeof(T) == typeof(Seller))
            {
                //по идентификатору ищем продукт
                var seller = set.Find(id) as Seller;
                if (seller != null)
                {
                    var form = new SellerForm(seller);

                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        seller = form.Seller;
                        db.SaveChanges();
                        dataGridView1.Update();
                    }
                }
            }
            if (typeof(T) == typeof(Customer))
            {
                //по идентификатору ищем продукт
                var customer = set.Find(id) as Customer;
                if (customer != null)
                {
                    var form = new CustomerForm(customer);

                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        customer = form.Customer;
                        db.SaveChanges();
                        dataGridView1.Update();
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
