using CrmBuisnessLogic.Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CrmUI
{
    public partial class CustomerForm : Form
    {
        public Customer Customer { get; set; }
        public CustomerForm()
        {
            InitializeComponent();
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Customer = new Customer
            {
                Name = textBox1.Text
            };
            Close();
        }
    }
}
