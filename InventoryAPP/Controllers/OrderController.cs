using InventoryAPP.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryAPP.Controllers
{
    public class OrderController : Controller
    {
        InventoryDB _db = new InventoryDB();

        // GET: Order
        public ActionResult Index()
        {
            var orderList = _db.Inventories.ToList();
            return View(orderList);
        }
        public ActionResult Create()
        {
            InventoryViewModel model = new InventoryViewModel();
            DDL(model);
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            InventoryViewModel model = new InventoryViewModel();
            List<InventoryProductViewModel> pList = new List<InventoryProductViewModel>();
            var inventory = _db.Inventories.Find(id);
            model.Id = inventory.Id ;
            model.BillNo = inventory.BillNo;
            model.Date = inventory.Date;
            model.CustomerId = inventory.CustomerId;
            model.DueAmount = inventory.DueAmount;
            model.TotalBillAmount = inventory.TotalBillAmount;
            model.PaidAmount = inventory.PaidAmount;
            model.TotalDiscount = inventory.TotalDiscount;
            foreach(var item in inventory.InventoryProducts)
            {
                var product = new InventoryProductViewModel();
                product.Id = item.Id;
                product.InventoryId = item.InventoryId;
                product.ProductId = item.ProductId;
                product.ProductName = item.Product.Name;
                product.Qty = item.Qty;
                product.Rate = item.Rate;
                product.Discount = item.Discount;
                product.TotalAmount = item.Rate * item.Qty;
                product.NetTotal = (item.Rate * item.Qty) - item.Discount;
                pList.Add(product);
            }
            model.InventoryProductList = pList;
            DDL(model);
            return View("Create",model);
        }

        [HttpPost]
        public ActionResult Create(InventoryViewModel model)
        {
            Inventory inventory = new Inventory();

            foreach (var item in model.InventoryProductList)
            {
                InventoryProduct inventoryProduct = new InventoryProduct();
                inventoryProduct.Id = item.Id;
                inventoryProduct.InventoryId = item.InventoryId;
                inventoryProduct.ProductId = item.ProductId;
                inventoryProduct.Qty = item.Qty;
                inventoryProduct.Rate = item.Rate;
                inventoryProduct.Discount = item.Discount;

                inventory.InventoryProducts.Add(inventoryProduct);
            }
            inventory.Date = model.Date;
            inventory.BillNo = model.BillNo;
            inventory.CustomerId = model.CustomerId;
            inventory.DueAmount = model.DueAmount;
            inventory.PaidAmount = model.PaidAmount;
            inventory.TotalBillAmount = model.TotalBillAmount;
            inventory.TotalDiscount = model.TotalDiscount;

            _db.Inventories.Add(inventory);
            _db.SaveChanges();
            return Json("Ok", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit(InventoryViewModel model)
        {
            Inventory inventory = new Inventory();

            foreach (var item in model.InventoryProductList)
            {
                InventoryProduct inventoryProduct = new InventoryProduct();
                inventoryProduct.Id = item.Id;
                inventoryProduct.InventoryId = model.Id;
                inventoryProduct.ProductId = item.ProductId;
                inventoryProduct.Qty = item.Qty;
                inventoryProduct.Rate = item.Rate;
                inventoryProduct.Discount = item.Discount;

                // _db.InventoryProducts.Add(inventoryProduct);
                if(item.Id == 0)
                {
                    _db.InventoryProducts.Add(inventoryProduct);
                    _db.SaveChanges();
                }
                else
                {
                    _db.Entry(inventoryProduct).State = EntityState.Modified;
                    _db.SaveChanges();
                }
                inventory.InventoryProducts.Add(inventoryProduct);
                
            }
            inventory.Id = model.Id;
            inventory.Date = model.Date;
            inventory.BillNo = model.BillNo;
            inventory.CustomerId = model.CustomerId;
            inventory.DueAmount = model.DueAmount;
            inventory.PaidAmount = model.PaidAmount;
            inventory.TotalBillAmount = model.TotalBillAmount;
            inventory.TotalDiscount = model.TotalDiscount;
            //inventory.InventoryProducts = model.InventoryProductList;

            //if (inventory.Id == 0)
            //    _db.Inventories.Add(inventory);
            //else
            _db.Entry(inventory).State = EntityState.Modified;

            _db.SaveChanges();
            return Json("Ok", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int id)
        {
            var product = _db.Inventories.Find(id);
            var InvProducts = _db.InventoryProducts.Where(x=>x.InventoryId == id);
            foreach (var item in InvProducts)
            {
                _db.InventoryProducts.Remove(item);
            }
            _db.Inventories.Remove(product);
            _db.SaveChanges();
            return RedirectToAction("index");
        }
        public void DDL(InventoryViewModel model)
        {
            model.ProductList = _db.Products.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            model.CustomerList = _db.Customers.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
        }

        public JsonResult GetProduct(int? id)
        {
            var product = _db.Products.Find(id);
            var p = new Product();
            p.Id = product.Id;
            p.Name = product.Name;
            p.Rate = product.Rate;
            return Json(p, JsonRequestBehavior.AllowGet);
        }
    }
}