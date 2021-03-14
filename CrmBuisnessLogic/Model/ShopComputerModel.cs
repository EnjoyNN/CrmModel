using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmBuisnessLogic.Model
{
    public class ShopComputerModel
    {
        Generator generator = new Generator();
        Random rnd = new Random();
        public List<CashDesc> cashDescs { get; set; } = new List<CashDesc>();
        public List<Cart> carts { get; set; } = new List<Cart>();
        public List<Check> checks { get; set; } = new List<Check>();
        public List<Sell> sells { get; set; } = new List<Sell>();
        public Queue<Seller> sellers { get; set; } = new Queue<Seller>();

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

        public void Start()
        {
            var customers = generator.getNewCustomers(10);
            var carts = new Queue<Cart>();

            foreach(var customer in customers)
            {
                var cart = new Cart(customer);

                foreach(var prod in generator.getRandomProducts(10, 30))
                {
                    cart.addProduct(prod);
                }
                carts.Enqueue(cart);
            }

            while (carts.Count > 0)
            //расставляем корзины по кассам
            {
                var cash = cashDescs[rnd.Next(cashDescs.Count - 1)];
                cash.Enqueue(carts.Dequeue());
            }

            //начинаем обслуживать кассы
            while(true)
            {
                var cash = cashDescs[rnd.Next(cashDescs.Count - 1)];
                var money = cash.Dequeue();
            }
        }
    }
}
