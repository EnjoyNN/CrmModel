using Microsoft.VisualStudio.TestTools.UnitTesting;
using CrmBuisnessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmBuisnessLogic.Model.Tests
{
    [TestClass()]
    public class CashDescTests
    {
        [TestMethod()]
        public void CashDescTest()
        {
            //arrange
            var customer1 = new Customer()
            {
                Name = "testUser1",
                CustomerId = 1
            };

            var customer2 = new Customer()
            {
                Name = "testUser2",
                CustomerId = 2
            };

            var seller = new Seller()
            {
                Name = "testSeller",
                SellerId = 1
            };

            var product1 = new Product()
            {
                ProductId = 1,
                Name = "testProduct1",
                Price = 100,
                Count = 10
            };

            var product2 = new Product()
            {
                ProductId = 2,
                Name = "testProduct2",
                Price = 150,
                Count = 66
            };

            var cart1 = new Cart(customer1);

            cart1.addProduct(product1);
            cart1.addProduct(product1);
            cart1.addProduct(product2);

            var cart2 = new Cart(customer2);

            cart2.addProduct(product1);
            cart2.addProduct(product2);
            cart2.addProduct(product2);

            var cashDesc = new CashDesc(1, seller);
            cashDesc.maxQueueLength = 10;
            cashDesc.Enqueue(cart1);
            cashDesc.Enqueue(cart2);

            var cart1ExpectedResult = 350;
            var cart2ExpectedResult = 400;

            //act
            //извлечение товаров из очереди и подсчет
            var cart1ActualResult = cashDesc.Dequeue();
            var cart2ActualResult = cashDesc.Dequeue();

            //assert
            Assert.AreEqual(cart1ExpectedResult, cart1ActualResult);
            Assert.AreEqual(cart2ExpectedResult, cart2ActualResult);
            //проверяем что количетсво товара на складе уменьшилось. было 10 одного и 66 другого. поэтому минус 3 у каждого.
            Assert.AreEqual(7, product1.Count);
            Assert.AreEqual(63, product2.Count);

        }

    }
}