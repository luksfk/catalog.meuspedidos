using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Models
{
    public class Sale
    {
        public string Name { get; set; }

        public int Category_Id { get; set; }

        public List<SalesPolicy> Policies { get; set; }
    }

    public class SalesPolicy
    {
        public int Min { get; set; }

        public decimal Discount { get; set; }
    }
}
