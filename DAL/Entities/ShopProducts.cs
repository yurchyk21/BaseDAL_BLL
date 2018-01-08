using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ShopProducts
    {
        public int Id { get; set; }
        public int ShopId { get; set; }
        public int ProductId { get; set; }
        public DateTime IncomeDate  { get; set; }
        public double QuantityCurrent { get; set; }
        public bool IsAvailable { get; set; }
    }
}
