using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Shop
    {
        public int Id { get; }
        public int CityId { get; set; }
        public string Name { get; set; }
        public bool IsWork { get; set; }

    }
}
