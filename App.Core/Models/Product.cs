using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Models
{
    public class Product
    {        
        public string Name { get; set; }

        public int? Category_Id { get; set; }        
        
        public int Id { get; set; }

        public string Description { get; set; }

        public string Photo { get; set; }

        public decimal Price { get; set; }
    }
}
