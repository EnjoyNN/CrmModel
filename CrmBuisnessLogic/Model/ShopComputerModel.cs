using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CrmBuisnessLogic.Model
{
    public class ShopComputerModel
    {
        Generator generator = new Generator();
        Random rnd = new Random();
        bool isWorking = false;
        public List<CashDesc> cashDescs { get; set; } = new List<CashDesc>();
        public List<Cart> carts { get; set; } = new List<Cart>();
        public List<Check> checks { get; set; } = new List<Check>();
        public List<Sell> sells { get; set; } = new List<Sell>();
        public Queue<Seller> sellers { get; set; } = new Queue<Seller>();
        public int customerSpeed { get; set; } = 100;
        public int cashDescSpeed { get; set; } = 100;

        public ShopComputerModel()
        {
            var sellersNew= generator.getNewSellers(20);
            generator.getNewProducts(1000);
            generator.getNewCustomers(100);

            foreach (var seller in sellersNew)
            {
                sellers.Enqueue(seller);
            }

            for (int i = 0; i < 3; i++)
            {
                //вытягиваем свободных продавцов. три штуки достаточно
                cashDescs.Add(new CashDesc(cashDescs.Count, sellers.Dequeue()));
            }
        }

        private void CashDeskWork(CashDesc cashDesc, int sleep)
        {
            while (isWorking)
            {
                if (cashDesc.count > 0)
                {
                    cashDesc.Dequeue();
                    Thread.Sleep(sleep);
                }
            }
        }

        public void Start()
        {
            isWorking = true;
            Task.Run(() => createCarts(10, customerSpeed));

            var cashDescTasks = cashDescs.Select(c => new Task(() => CashDeskWork(c, cashDescSpeed)));
            foreach(var task in cashDescTasks)
            {
                task.Start();
            }
        }

        public void stop()
        {
            isWorking = false;
        }

        private void createCarts(int customerCounts, int sleep)
        {
            while (isWorking)
            {
                var customers = generator.getNewCustomers(customerCounts);

                foreach (var customer in customers)
                {
                    var cart = new Cart(customer);

                    foreach (var product in generator.getRandomProducts(10, 30))
                    {
                        cart.addProduct(product);
                    }

                    var cash = cashDescs[rnd.Next(cashDescs.Count - 1)];
                    cash.Enqueue(cart);
                }

                Thread.Sleep(sleep);
            }
        }
    }
}
