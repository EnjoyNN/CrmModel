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
    public class CartTests
    {
        [TestMethod()]
        public void CartTest()
        {
            //arrange
            var customer = new Customer()
            {
                CustomerId = 1,
                Name = "testUser"
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

            var cart = new Cart(customer);

            var expectedResult = new List<Product>()
            {
                product1, product1, product2
            };

            //act 
            cart.addProduct(product1);
            cart.addProduct(product1);
            cart.addProduct(product2);

            var cartResult = cart.GetAll();

            //assert
            //сначала количество сравниваем
            Assert.AreEqual(expectedResult.Count, cart.GetAll().Count);

            for (int i = 0; i < expectedResult.Count; i++)
            {
                Assert.AreEqual(expectedResult[i], cartResult[i]);
            }
        }
    }
}