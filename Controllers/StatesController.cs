using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Barton_Books.Models;

namespace Barton_Books.Controllers
{
    public class StatesController : Controller
    {
        // GET: States
        /// <summary>
        /// View to return list of all states
        /// </summary>
        /// <param name="sortByStates"></param>
        /// <param name="id"></param>
        /// <returns>List of states with sort/search functionalities</returns>
        public ActionResult All(string id, int sortByStates = 0)
        {
            var context = new BooksEntities1();
            List<State> states;
            //sort items in list
            switch (sortByStates)
            {
                case 0:
                default:
                    states = context.States.OrderBy(s => s.StateCode).ToList();
                    break;
                case 1:
                    states = context.States.OrderBy(s => s.StateName).ToList();
                    break;
            }
            //search implementation
            if (!string.IsNullOrWhiteSpace(id))
            {
                id = id.Trim().ToLower();
                states = states.Where(s =>
                s.StateCode.ToLower().Contains(id) ||
                s.StateName.ToLower().Contains(id)
                ).ToList();
            }
            return View(states);
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
            State state = context.States.Where(s => s.StateCode == id).FirstOrDefault();
            return View(state);
        }

        /// <summary>
        /// HTTP POST: works in conjuction with HTTP GET to allow users to edit/add entries to database
        /// </summary>
        /// <param name="newState"></param>
        /// <returns>Upsert view</returns>
        [HttpPost]
        public ActionResult Upsert(State newState)
        {
            var context = new BooksEntities1();
            try
            {
                if (context.States.Where(s => s.StateCode == newState.StateCode).Count() > 0)
                {
                    var stateToSave = context.States.Where(s => s.StateCode == newState.StateCode).ToList()[0];
                    stateToSave.StateName = newState.StateName;
                }
                else
                {
                    context.States.Add(newState);
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