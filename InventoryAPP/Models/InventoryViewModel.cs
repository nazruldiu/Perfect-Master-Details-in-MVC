using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryAPP.Models
{
    public class InventoryViewModel
    {
        public InventoryViewModel()
        {
            this.ProductList = new List<SelectListItem>();
            this.CustomerList = new List<SelectListItem>();
            this.InventoryProductList = new List<InventoryProductViewModel>();
        }
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }
        [DisplayName("Bill No.")]
        public string BillNo { get; set; }
        [DisplayName("Customer")]
        public int? CustomerId { get; set; }
        [DisplayName("Product")]
        public int? ProductId { get; set; }
        [DisplayName("Total Discount")]
        public decimal? TotalDiscount { get; set; }
        [DisplayName("Total Bill Amount")]
        public decimal? TotalBillAmount { get; set; }
        [DisplayName("Due Amount")]
        public decimal? DueAmount { get; set; }
        [DisplayName("Paid Amount")]
        public decimal? PaidAmount { get; set; }

        public List<SelectListItem> ProductList { get; set; }
        public List<SelectListItem> CustomerList { get; set; }

        public List<InventoryProductViewModel> InventoryProductList { get; set; }
    }
}