using CrmBuisnessLogic.Model;
using System;
using System.Windows.Forms;

namespace CrmUI
{
    class CashDescView
    {
        CashDesc cashDesc;
        public Label cashDescName { get; set; }
        public NumericUpDown price { get; set; }
        public ProgressBar queueLength { get; set; }
        public Label leaveCustomersCount { get; set; }

        public CashDescView(CashDesc cashDesc, int number, int x, int y) 
        {
            this.cashDesc = cashDesc;

            cashDescName = new Label();
            price = new NumericUpDown();
            queueLength = new ProgressBar();
            leaveCustomersCount = new Label();

            cashDescName.AutoSize = true;
            cashDescName.Location = new System.Drawing.Point(x, y);
            cashDescName.Name = "label" + number;
            cashDescName.Size = new System.Drawing.Size(35, 13);
            cashDescName.TabIndex = number;
            cashDescName.Text = cashDesc.ToString();

            price.Location = new System.Drawing.Point(x + 70, y);
            price.Name = "numericUpDown" + number; 
            price.Size = new System.Drawing.Size(120, 20);
            price.TabIndex = number;
            price.Maximum = 1000000000000000;

            queueLength.Location = new System.Drawing.Point(x + 250, y);
            queueLength.Maximum = cashDesc.maxQueueLength;
            queueLength.Name = "progressBar" + number;
            queueLength.Size = new System.Drawing.Size(100, 23);
            queueLength.TabIndex = number;
            queueLength.Value = 0;

            leaveCustomersCount.AutoSize = true;
            leaveCustomersCount.Location = new System.Drawing.Point(x + 400, y);
            leaveCustomersCount.Name = "label2" + number;
            leaveCustomersCount.Size = new System.Drawing.Size(35, 13);
            leaveCustomersCount.TabIndex = number;
            leaveCustomersCount.Text = "";

            cashDesc.checkClosed += CashDesc_checkClosed;
        }

        private void CashDesc_checkClosed(object sender, Check e)
        {
            price.Invoke((Action)delegate 
            { 
                price.Value += e.price;
                queueLength.Value = cashDesc.count;
                leaveCustomersCount.Text = cashDesc.exitCustomer.ToString();
            });
        }
    }
}