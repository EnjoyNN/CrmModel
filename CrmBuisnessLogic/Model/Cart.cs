using System.Collections;
using System.Collections.Generic;

namespace CrmBuisnessLogic.Model
{
    public class Cart : IEnumerable
    {
        public Customer Customer { get; set; }
        public Dictionary<Product, int> products { get; set; }

        public Cart(Customer customer)
        {
            Customer = customer;
            products = new Dictionary<Product, int>();
        }
        public void addProduct(Product product)
        {
            if (products.TryGetValue(product, out int count))
            {
                //если уже есть, добавлять плюс один в корзину, а не создаем новый
                products[product] = ++count;
            }
            else
            {
                products.Add(product, 1);
            }
        }

        public IEnumerator GetEnumerator()
        {
            foreach (var product in products.Keys)
            {
                for (int i = 0; i < products[product]; i++)
                {
                    yield return product;
                }
            }
        }

        public List<Product> GetAll()
        {
            var result = new List<Product>();
            foreach (Product i in this)
            {
                result.Add(i);
            }
            return result;
        }
    }
}
