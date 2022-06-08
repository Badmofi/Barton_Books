using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Barton_Books.Models;

namespace Barton_Books.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        /// <summary>
        /// View to return list of all products
        /// </summary>
        /// <param name="sortByProducts"></param>
        /// <param name="id"></param>
        /// <returns>List of products with sort/search functionalities</returns>
        public ActionResult All(string id, int sortByProducts = 0)
        {
            var context = new BooksEntities1();
            List<Product> products;
            //sort items in list
            switch (sortByProducts)
            {
                case 0:
                default:
                    products = context.Products.OrderBy(p => p.ProductCode).ToList();
                    break;
                case 1:
                    products = context.Products.OrderBy(p => p.Description).ToList();
                    break;
                case 2:
                    products = context.Products.OrderBy(p => p.UnitPrice).ToList();
                    break;
                case 3:
                    products = context.Products.OrderBy(p => p.OnHandQuantity).ToList();
                    break;
            }
            //search implementation
            if (!string.IsNullOrWhiteSpace(id))
            {
                id = id.Trim().ToLower();
                products = products.Where(p =>
                p.ProductCode.ToLower().Contains(id) ||
                p.Description.ToLower().Contains(id)
                ).ToList();
            }
            return View(products);
        }

        /// <summary>
        /// HTTP GET: works in conjuction with HTTP POST to allow users to edit/add entries to database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Upsert view</returns>
        [HttpGet]
        public ActionResult Upsert(string id)
        {
            var context = new BooksEntities1();
            Product product = context.Products.Where(p => p.ProductCode == id).FirstOrDefault();
            return View(product);
        }

        /// <summary>
        /// HTTP POST: works in conjuction with HTTP GET to allow users to edit/add entries to database
        /// </summary>
        /// <param name="newProduct"></param>
        /// <returns>Upsert view</returns>
        [HttpPost]
        public ActionResult Upsert(Product newProduct)
        {
            var context = new BooksEntities1();
            try
            {
                if (context.Products.Where(p => p.ProductCode == newProduct.ProductCode).Count() > 0)
                {
                    var productToSave = context.Products.Where(p => p.ProductCode == newProduct.ProductCode).ToList()[0];
                    productToSave.Description = newProduct.Description;
                    productToSave.UnitPrice = newProduct.UnitPrice;
                    productToSave.OnHandQuantity = newProduct.OnHandQuantity;
                }
                else
                {
                    context.Products.Add(newProduct);
                }
                context.SaveChanges();

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("All");
        }
    }
}