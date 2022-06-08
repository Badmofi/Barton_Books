using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Barton_Books.Models;

namespace Barton_Books.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        /// <summary>
        /// View to return list of all customers
        /// </summary>
        /// <param name="sortByCustomers"></param>
        /// <param name="id"></param>
        /// <returns>List of customers with sort/search functionalities</returns>
        public ActionResult All(string id, int sortByCustomers = 0)
        {
            var context = new BooksEntities1();
            List<Customer> customers;
            //sort items in list
            switch (sortByCustomers)
            {
                case 0:
                default:
                    {
                        customers = context.Customers.OrderBy(c => c.CustomerID).ToList();
                        break;
                    }
                case 1:
                    {
                        customers = context.Customers.OrderBy(c => c.Name).ToList();
                        break;
                    }
                case 2:
                    {
                        customers = context.Customers.OrderBy(c => c.Address).ToList();
                        break;
                    }
                case 3:
                    {
                        customers = context.Customers.OrderBy(c => c.City).ToList();
                        break;
                    }
                case 4:
                    {
                        customers = context.Customers.OrderBy(c => c.State).ToList();
                        break;
                    }
                case 5:
                    {
                        customers = context.Customers.OrderBy(c => c.ZipCode).ToList();
                        break;
                    }
            }
            //search implementation
            if (!string.IsNullOrWhiteSpace(id))
            {
                id = id.Trim().ToLower();
                customers = customers.Where(c =>
                c.Name.ToLower().Contains(id) ||
                c.Address.ToLower().Contains(id) ||
                c.City.ToLower().Contains(id) ||
                c.State.ToLower().Contains(id) ||
                c.ZipCode.ToLower().Contains(id)
                ).ToList();
            }
            return View(customers);
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
            Customer customer = context.Customers.Where(c => c.CustomerID == id).FirstOrDefault();
            return View(customer);
        }

        /// <summary>
        /// HTTP POST: works in conjuction with HTTP GET to allow users to edit/add entries to database
        /// </summary>
        /// <param name="newCustomer"></param>
        /// <returns>Upsert view</returns>
        [HttpPost]
        public ActionResult Upsert(Customer newCustomer)
        {
            var context = new BooksEntities1();
            try
            {
                if (context.Customers.Where(c => c.CustomerID == newCustomer.CustomerID).Count() > 0)
                {
                    var customerToSave = context.Customers.Where(c => c.CustomerID == newCustomer.CustomerID).ToList()[0];
                    customerToSave.Name = newCustomer.Name;
                    customerToSave.Address = newCustomer.Address;
                    customerToSave.City = newCustomer.City;
                    customerToSave.State = newCustomer.State;
                    customerToSave.ZipCode = newCustomer.ZipCode;
                }
                else
                {
                    context.Customers.Add(newCustomer);
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