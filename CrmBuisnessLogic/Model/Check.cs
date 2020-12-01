using System;
using System.Collections.Generic;

namespace CrmBuisnessLogic.Model
{
    public class Check
    {
        public int CheckId { get; set; }

        //relationships for entity framework
        public int CustomerId  { get; set; }

        public virtual Customer Customer { get; set; }

        public int SellerId { get; set; }

        public virtual Seller Seller { get; set; } 

        public DateTime Created { get; set; }

        public virtual ICollection<Sell> Sells { get; set; }

        public override string ToString()
        {
            return $"№{CheckId} от {Created.ToString("dd.MM.yy hh:mm:ss")}";
        }
    }
}
