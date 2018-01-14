﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
   public class ShopWorker
    {
        public int Id { get; set; }
        public DateTime HiredDate { get; set; }
        public bool IsLocked { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }

    }
}
