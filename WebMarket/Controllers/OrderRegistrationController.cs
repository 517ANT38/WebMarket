using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMarket.Models;
using WebMarket.Models.Adapters;

namespace WebMarket.Controllers
{
    public class OrderRegistrationController : Controller
    {
        // GET: OrderRegistration
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
               
        public ActionResult Registration(NewClient client)
        {
            if (ModelState.IsValid)
            {
                IAdapter<Client> adapter = new AdapterClient();
                var cl = new Client()
                {
                    Family = client.Family,
                    Name = client.Name,
                    Patronymic = (client.Patronymic == null) ? "NULL" : client.Patronymic,
                    Phone = client.Phone,
                    Email = client.Email,
                    Password = client.Password,
                    Role = "user"
                };
                bool x=adapter.Insert(cl);
                if (!x)
                {
                    
                    ModelState.AddModelError("", "Пользователь с таким email уже есть");
                    return View();
                }
                InsertProductBasket(client.Email);
                FormsAuthentication.SetAuthCookie(cl.Email, false);
                return View("ConfirmView");
            }
            else
            {
                return View();
            }
        }


        [HttpGet]
        public ActionResult Authorization()
        {
            return View();
        }
        [HttpPost]
        
        public ActionResult Authorization(EmailPassword emailPassword)        
        {
            
            if (ModelState.IsValid)
            {
                IAdapterFind adapter = new AdapterFindClient();
                AuthorizedUser res = adapter.findCheck(emailPassword);
                if (res!=null)
                {
                                        
                    
                    
                    FormsAuthentication.SetAuthCookie(emailPassword.Email, false);
                    InsertProductBasket(emailPassword.Email);

                    
                    return View("ConfirmView");
                }
            }
            ModelState.AddModelError("", "Некорректное имя пользователя или пароль");
            return View();

        }

        private void InsertProductBasket(string email)
        {
            IList<ProductBasket> products = new List<ProductBasket>();
            IAdapter<int> adapter = new AdapterProductId();
            var list = adapter.Select();
            
            foreach (var item in Session.Keys)
            {
                int id = Convert.ToInt32(item);
                if (list.Contains(id))
                {
                    Product product = (Product)Session[item.ToString()];
                    
                    products.Add(new ProductBasket
                    {
                        Id = id,
                        Name=product.Name,
                        Description=product.Description,
                        Price=product.Price,
                        Count=product.Count,
                        Email=email
                    });
                }
            }
            foreach(var item in list)
            {
                Session.Remove(item.ToString());
            }
            IAdapterBasketProduct adapterBasket=new AdapterBasketProduct();
            adapterBasket.InsertSafe(products.ToArray());
        }
    }
}