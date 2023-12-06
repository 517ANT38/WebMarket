using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using WebMarket.Infrastructure;
using WebMarket.Models;
using WebMarket.Models.Adapters;

namespace WebMarket.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [HttpGet]
        public ActionResult Index()
        {

            IAdapter<Product> adapter = new AdapterProducts();
            return View(adapter.Select());
        }
        [HttpPost]
        public ActionResult CheckCount(int count, int Id_product)
        {

            ICheckDataInDB checkDataInDB = new CheckAdapterCount();

            if (!checkDataInDB.IsChecked(Id_product, count))
            {
                return JavaScript("alert('На складе не достаточно товара')");
            }
            else
            {
                IAdapter<Product> adapter = new AdapterProducts();

                Product product = adapter.Get(Id_product);
                product.Count = Convert.ToInt32(count);
                if (User.Identity.IsAuthenticated)
                {
                    IAdapterBasketProduct adapterBasket = new AdapterBasketProduct();
                    adapterBasket.InsertSafe(new ProductBasket
                    {
                        Description = product.Description,
                        Price = product.Price,
                        Count = product.Count,
                        Id = Id_product,
                        Name = product.Name,
                        Email = User.Identity.Name

                    });
                }
                else
                    Session[Id_product.ToString()] = product;
                return JavaScript($"alert('Товар {product.Name}  добавлен в корзину.')");
            }


        }



        [HttpPost]
        public ActionResult CheckCountM(int count, int Id_product)
        {

            ICheckDataInDB checkDataInDB = new CheckAdapterCount();

            if (!checkDataInDB.IsChecked(Id_product, count))
            {
                return JavaScript("alert('На складе не достаточно товара')");
            }
            else
            {
                if (User.Identity.IsAuthenticated)
                {
                    IAdapterBasketProduct adapterBasket = new AdapterBasketProduct();
                    adapterBasket.UpdateCount(Id_product, User.Identity.Name, count);
                }
                else ((Product)Session[Id_product.ToString()]).Count = count;
                return new EmptyResult();
            }


        }
        [HttpPost]
        public ActionResult DecCount(int count, string Id_product)
        {
            IAdapterBasketProduct adapterBasket = new AdapterBasketProduct();
            if (count == 0)
            {
                if (User.Identity.IsAuthenticated)
                {
                    adapterBasket.Delete(User.Identity.Name, Convert.ToInt32(Id_product));
                }
                else
                    Session.Remove(Id_product.ToString());
                return JavaScript("clearProduct();");
            }
            if (User.Identity.IsAuthenticated)
            {
                adapterBasket.UpdateCount(Convert.ToInt32(Id_product), User.Identity.Name, count);
            }
            else
                 ((Product)Session[Id_product.ToString()]).Count = count;
            return new EmptyResult();

        }


        [HttpGet]
        public ActionResult Basket()
        {
            IList<Product> products;
            if (!User.Identity.IsAuthenticated)
            {
                IAdapter<int> adapter = new AdapterProductId();
                var list = adapter.Select();
                products = new List<Product>();
                foreach (var item in Session.Keys)
                {
                    int id = Convert.ToInt32(item);
                    if (list.Contains(id))
                    {
                        products.Add((Product)Session[item.ToString()]);
                    }
                }
            }
            else
            {
                IAdapterBasketProduct adapterBasket = new AdapterBasketProduct();
                products = new List<Product>(adapterBasket.Select(User.Identity.Name));
            }
            return View(products);
        }


        [MarkAuth]
        [CustomAuth(true)]
        public ActionResult Arrange()
        {
            ICheckDataInDB checkDataInDB = new CheckAdapterCount();
            IAdapterBasketProduct adapterBasket = new AdapterBasketProduct();
            List<Product> products = new List<Product>(adapterBasket.Select(User.Identity.Name));
            foreach (var item in products)
            {
                if (!checkDataInDB.IsChecked(item.Id,item.Count))
                {
                    return JavaScript("start();");
                }
            }
            return View();
        }

        [MarkAuth]
        [CustomAuth(true)]
        [HttpPost]
        public ActionResult Arrange(NewOrder newOrder)
        {
            ;
            if (ModelState.IsValid)
            {
                IAdapterClient adapterClient = new AdapterClient();
                var v = adapterClient.Get(User.Identity.Name);
                OrderWriteDb order = new OrderWriteDb
                {
                    Id_client = v.Id,
                    DateDelivery = newOrder.DateDelivery,
                    MethodOfIssue = newOrder.MethodOfIssue,
                    DateOrder = newOrder.DateOrder
                };

                
                IAdapterBasketProduct adapterBasketProduct = new AdapterBasketProduct();
                IList<Product> products=new List<Product>(adapterBasketProduct.Select(v.Email));
                IAdapterOrder adapterOrder = new AdapterOrder();
                int id=adapterOrder.Insert(order);
                adapterOrder.InsertProductInOrder(products, id);
                IAdapterProducts adapterProducts = new AdapterProducts();
                var list=products.Select(product => (product.Id,product.Count)).ToList();
                foreach (var item in list)
                {
                    adapterProducts.Update(item.Id, item.Count);
                }
                
                adapterBasketProduct.Delete(v.Email);
                return View("Thanks",v);
            }
            return View();
        }
    }
}