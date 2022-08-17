using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryAPP.Models
{
    public class InventoryProductViewModel
    {
        public int Id { get; set; }
        public int InventoryId { get; set; }
        public int? ProductId { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Qty { get; set; }
        public decimal? Discount { get; set; }
        public string ProductName { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? NetTotal { get; set; }
    }
}