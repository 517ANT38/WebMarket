using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMarket.Infrastructure;
using WebMarket.Models;
using WebMarket.Models.Adapters;

namespace WebMarket.Controllers
{
    
    [MarkAuth]
    [RoleAuthorization("admin")]
    public class AdminController : Controller   {
        // GET: Admin
        
        
        public ActionResult AdminPage()
        {
            IAdapterOrder adapter=new AdapterOrder();
            var r = adapter.Select();
            return View(r);
        }
        public ActionResult AddEditProduct()
        {
            IAdapter<Product> adapter=new AdapterProducts();
            var r = adapter.Select();
            return View(r);
        }
        [HttpPost]
        public ActionResult Edit(int count, int Id_product)
        {
            IAdapterProducts adapter = new AdapterProducts();
            adapter.Update(count, Id_product);
            return JavaScript("alert('Изменения внесены')");
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(NewProduct newProduct)
        {
            IAdapterProducts adapter = new AdapterProducts();
            adapter.Insert(newProduct);
            return RedirectToAction("AddEditProduct");
        }
    }
}