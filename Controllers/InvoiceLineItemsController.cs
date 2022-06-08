using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Barton_Books.Models;

namespace Barton_Books.Controllers
{
    public class InvoiceLineItemsController : Controller
    {
        // GET: InvoiceLineItems
        /// <summary>
        /// View to return list of all invoice line items
        /// </summary>
        /// <param name="sortByLineItems"></param>
        /// <param name="id"></param>
        /// <returns>List of invoice line items with sort/search functionalities</returns>
        public ActionResult All(string id, int sortByLineItems = 0)
        {
            var context = new BooksEntities1();
            List<InvoiceLineItem> lineItems;
            //sort items in list
            switch (sortByLineItems)
            {
                case 0:
                default:
                    {
                        lineItems = context.InvoiceLineItems.OrderBy(l => l.InvoiceID).ToList();
                        break;
                    }
                case 1:
                    {
                        lineItems = context.InvoiceLineItems.OrderBy(l => l.ProductCode).ToList();
                        break;
                    }
                case 2:
                    {
                        lineItems = context.InvoiceLineItems.OrderBy(l => l.UnitPrice).ToList();
                        break;
                    }
                case 3:
                    {
                        lineItems = context.InvoiceLineItems.OrderBy(l => l.Quantity).ToList();
                        break;
                    }
                case 4:
                    {
                        lineItems = context.InvoiceLineItems.OrderBy(l => l.ItemTotal).ToList();
                        break;
                    }
            }
            //search implementation
            if (!string.IsNullOrWhiteSpace(id))
            {
                id = id.Trim().ToLower();
                int idLookup = 0;
                int quantityLookup = 0;
                if (int.TryParse(id, out idLookup))
                {
                    lineItems = lineItems.Where(l => l.Quantity == quantityLookup).ToList();
                }
                else
                {
                    lineItems = lineItems.Where(l =>
                    l.ProductCode.ToLower().Contains(id)
                    ).ToList();
                }
            }
                return View(lineItems);
        }

        /// <summary>
        /// HTTP GET: works in conjuction with HTTP POST to allow users to edit/add entries to database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Upsert view</returns>
        [HttpGet]
        public ActionResult Upsert(int id)
        {
            var context = new BooksEntities1();
           InvoiceLineItem item = context.InvoiceLineItems.Where(i => i.InvoiceID == id).FirstOrDefault();
            return View(item);
        }

        /// <summary>
        /// HTTP POST: works in conjuction with HTTP GET to allow users to edit/add entries to database
        /// </summary>
        /// <param name="newItem"></param>
        /// <returns>Upsert view</returns>
        [HttpPost]
        public ActionResult Upsert(InvoiceLineItem newItem)
        {
            var context = new BooksEntities1();
            try
            {
                if (context.InvoiceLineItems.Where(i => i.InvoiceID == newItem.InvoiceID).Count() > 0)
                {
                    var itemToSave = context.InvoiceLineItems.Where(i => i.InvoiceID == newItem.InvoiceID).ToList()[0];
                    itemToSave.ProductCode = newItem.ProductCode;
                    itemToSave.UnitPrice = newItem.UnitPrice;
                    itemToSave.Quantity = newItem.Quantity;
                    itemToSave.ItemTotal = newItem.ItemTotal;
                }
                else
                {
                    context.InvoiceLineItems.Add(newItem);
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