using System.Collections.Generic;

namespace CrmBuisnessLogic.Model
{
    public class Product
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Count { get; set; }

        public virtual ICollection<Sell> Sells { get; set; }

        //tostring is better for understanding what is the product we have
        public override string ToString()
        {
            return Name;
        }
    }
}
