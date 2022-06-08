using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Barton_Books.Models;

namespace Barton_Books.Controllers
{
    public class OrderOptionsController : Controller
    {
        // GET: OrderOptions
        /// <summary>
        /// View to return list of all order options
        /// </summary>
        /// <param name="sortByOrderOptions"></param>
        /// <returns>List of products with sort functionality</returns>
        public ActionResult All(int sortByOrderOptions = 0)
        {
            var context = new BooksEntities1();
            List<OrderOption> options;
            //sort items in list
            switch (sortByOrderOptions)
            {
                case 0:
                default:
                    {
                        options = context.OrderOptions.OrderBy(o => o.SalesTaxRate).ToList();
                        break;
                    }
                case 1:
                    {
                        options = context.OrderOptions.OrderBy(o => o.FirstBookShipCharge).ToList();
                        break;
                    }
                case 2:
                    {
                        options = context.OrderOptions.OrderBy(o => o.AdditionalBookShipCharge).ToList();
                        break;
                    }

            }
            return View(options);
        }
    }
}