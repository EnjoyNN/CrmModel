﻿using CrmBuisnessLogic.Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CrmUI
{
    public partial class SellerForm : Form
    {
        public Seller Seller { get; set; }
        public SellerForm()
        {
            InitializeComponent();
        }

        public SellerForm(Seller seller) : this()
        {
            Seller = seller ?? new Seller();
            textBox1.Text = Seller.Name;
        }

        private void SellerForm_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Seller = Seller ?? new Seller();
            Seller.Name = textBox1.Text;
            Close();
        }
    }
}
