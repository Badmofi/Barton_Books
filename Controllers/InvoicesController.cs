using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Barton_Books.Models;

namespace Barton_Books.Controllers
{
    public class InvoicesController : Controller
    {
        // GET: Invoices
        /// <summary>
        /// View to return list of all invoices
        /// </summary>
        /// <param name="sortByInvoices"></param>
        /// <param name="id"></param>
        /// <returns>List of invoices with sort/search functionalities</returns>
        public ActionResult All(string id, int sortByInvoices = 0)
        {
            var context = new BooksEntities1();
            List<Invoice> invoices;
            //sort items in list
            switch (sortByInvoices)
            {
                case 0:
                default:
                    {
                        invoices = context.Invoices.OrderBy(i => i.InvoiceID).ToList();
                        break;
                    }
                case 1:
                    {
                        invoices = context.Invoices.OrderBy(i => i.CustomerID).ToList();
                        break;
                    }
                case 2:
                    {
                        invoices = context.Invoices.OrderBy(i => i.InvoiceDate).ToList();
                        break;
                    }
                case 3:
                    {
                        invoices = context.Invoices.OrderBy(i => i.ProductTotal).ToList();
                        break;
                    }
                case 4:
                    {
                        invoices = context.Invoices.OrderBy(i => i.SalesTax).ToList();
                        break;
                    }
                case 5:
                    {
                        invoices = context.Invoices.OrderBy(i => i.Shipping).ToList();
                        break;
                    }
                case 6:
                    {
                        invoices = context.Invoices.OrderBy(i => i.InvoiceTotal).ToList();
                        break;
                    }
            }
            //search implementation
            if (!string.IsNullOrWhiteSpace(id))
            {
                id = id.Trim().ToLower();
                int idLookup = 0;
                DateTime dateLookUp;
                if (int.TryParse(id, out idLookup))
                {
                    invoices = invoices.Where(i => i.CustomerID == idLookup).ToList();
                }
                else if (DateTime.TryParse(id, out dateLookUp))
                {
                    invoices = invoices.Where(i => i.InvoiceDate == dateLookUp).ToList();
                }
            }
            return View(invoices);
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
            Invoice invoice = context.Invoices.Where(i => i.InvoiceID == id).FirstOrDefault();
            return View(invoice);
        }

        /// <summary>
        /// HTTP POST: works in conjuction with HTTP GET to allow users to edit/add entries to database
        /// </summary>
        /// <param name="newInvoice"></param>
        /// <returns>Upsert view</returns>
        [HttpPost]
        public ActionResult Upsert(Invoice newInvoice)
        {
            var context = new BooksEntities1();
            try
            {
                if (context.Invoices.Where(i => i.InvoiceID == newInvoice.InvoiceID).Count() > 0)
                {
                    var invoiceToSave = context.Invoices.Where(i => i.InvoiceID == newInvoice.InvoiceID).ToList()[0];
                    invoiceToSave.CustomerID = newInvoice.CustomerID;
                    invoiceToSave.InvoiceDate = newInvoice.InvoiceDate;
                    invoiceToSave.ProductTotal = newInvoice.ProductTotal;
                    invoiceToSave.SalesTax = newInvoice.SalesTax;
                    invoiceToSave.Shipping = newInvoice.Shipping;
                    invoiceToSave.InvoiceTotal = newInvoice.InvoiceTotal;
                }
                else
                {
                    context.Invoices.Add(newInvoice);
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