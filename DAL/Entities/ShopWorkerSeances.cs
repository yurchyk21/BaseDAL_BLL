using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
   public class ShopWorkerSeances
    {
        public int Id { get; set; }
        public int ShopWorkerId { get; set; }
        public int ShopId { get; set; }
        public DateTime BeginWork { get; set; }
        public DateTime EndWork { get; set; }

    }
}
