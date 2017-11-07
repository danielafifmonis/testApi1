using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestApi1.Models;

namespace TestApi1.Controllers
{
    public class PersonController : Controller
    {
        private ApplicationDbContext _person;

        public PersonController()
        {
            _person = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _person.Dispose();
        }

        // http://localhost:3928/Person/Index You will get the list of people. In this case we have 2 people
        public ViewResult Index()
        {
            var persons = _person.Person.ToList();

            return View(persons);
        }      

        [HttpPost]
        public ActionResult Save(Person person)
        {
            if (person.Id == 0)
                _person.Person.Add(person);
            else
            {
                var personInDB = _person.Person.Single(p => p.Id == person.Id);
                personInDB.FirstName = person.FirstName;
                personInDB.LastName = person.LastName;
                personInDB.JobTitle = person.JobTitle;
            }
            _person.SaveChanges();

            return RedirectToAction("Index","Person");
        }

        public ActionResult EditPerson(int id)
        {
            var person = _person.Person.SingleOrDefault(p => p.Id == id);

            if (person == null)
                return HttpNotFound();
            
            return View("PersonForm", person);
        }

        //private IEnumerable<Person> GetPersons()
        //{
        //    return new List<Person>
        //    {
        //        new Person { Id = 1, FirstName = "Daniel", LastName= "Monis", JobTitle="Developer" },
        //        new Person { Id = 2, FirstName = "Bill", LastName= "Gates", JobTitle="Manager" }
        //    };
        //}
    }
}