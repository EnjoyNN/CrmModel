using System;
using System.Collections.Generic;

namespace CrmBuisnessLogic.Model
{
    /// <summary>
    /// Касса это виртуальный объект для моедлирования. его мы не храним в базе. Это как определенный контроллер для логики продажи. Как у нас будут перемещаться объекты
    /// </summary>
    public class CashDesc
    {
        CrmContext db = new CrmContext();
        public Seller Seller { get; set; }
        public Queue<Cart> Queue { get; set; }
        public int numberDesc { get; set; }
        public int maxQueueLength { get; set; }
        /// <summary>
        /// Счетчик для учета того, будет ли уходить человек из магазины из за превышения длины очереди. Это счетчик простыми словами
        /// </summary>
        public int exitCustomer { get; set; }
        /// <summary>
        /// Свойство чтобы понимать нужно ли сохранять в базу данных, либо это беовая работа
        /// </summary>
        public bool isModel { get; set; }
        public int count => Queue.Count;

        public event EventHandler<Check> checkClosed;

        public CashDesc(int number, Seller seller)
        {
            numberDesc = number;
            Seller = seller;
            Queue = new Queue<Cart>();
            isModel = true;
            maxQueueLength = 10;
        }

        //добавление человека в очередь
        public void Enqueue(Cart cart)
        {
            if (Queue.Count <= maxQueueLength)
            {
                Queue.Enqueue(cart);
            }
            else
            {
                exitCustomer++;
            }
        }

        public decimal Dequeue()
        {
            decimal sum = 0;
            if (Queue.Count == 0)
                return 0;
            var card = Queue.Dequeue();

            if (card != null)
            {
                //задаем параметры для чека
                var check = new Check()
                {
                    SellerId = Seller.SellerId,
                    Seller = Seller,
                    CustomerId = card.Customer.CustomerId,
                    Customer = card.Customer,
                    Created = DateTime.Now
                };

                //если не модель сохраним в базу
                if (!isModel)
                {
                    db.Checks.Add(check);
                    db.SaveChanges();
                }
                else
                {
                    check.CheckId = 0;
                }

                var sells = new List<Sell>();
                //теперь перебираем все элементы из корзины
                foreach (Product product in card)
                {
                    //условия, что товар есть на складе. считаем с нулем, а не с count, потому что считаем по единично. те, которых нет на складе не идут в продажу
                    if (product.Count > 0)
                    {
                        var sell = new Sell()
                        {
                            CheckId = check.CheckId,
                            Check = check,
                            ProductId = product.ProductId,
                            Product = product
                        };

                        sells.Add(sell);

                        if (!isModel)
                        {
                            db.Sells.Add(sell);
                        }

                        product.Count--;
                        sum += product.Price;
                    }
                }
                check.price = sum;

                if (!isModel)
                {
                    db.SaveChanges();
                }

                checkClosed?.Invoke(this, check);
            }

            return sum;
        }

        public override string ToString()
        {
            return $"Касса №{numberDesc}";
        }
    }
}
