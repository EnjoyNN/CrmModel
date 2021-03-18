using CrmBuisnessLogic.Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CrmUI
{
    public partial class ModelForm : Form
    {
        ShopComputerModel model = new ShopComputerModel();
        public ModelForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var cashDescViews = new List<CashDescView>();

            for (int i = 0; i < model.cashDescs.Count; i++)
            {
                var cash = new CashDescView(model.cashDescs[i], i, 10, 26 * i);

                cashDescViews.Add(cash);
                Controls.Add(cash.cashDescName);
                Controls.Add(cash.price);
                Controls.Add(cash.queueLength);
                Controls.Add(cash.leaveCustomersCount);
            }

            model.Start();
        }

        private void ModelForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            model.stop();
        }

        private void ModelForm_Load(object sender, EventArgs e)
        {
            numericUpDown1.Value = model.customerSpeed;
            numericUpDown2.Value = model.cashDescSpeed;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            model.customerSpeed = (int)numericUpDown1.Value;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            model.cashDescSpeed = (int)numericUpDown2.Value;
        }
    }
}
