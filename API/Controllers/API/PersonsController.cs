using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using TestApi1.Models;

namespace TestApi1.Controllers.API
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PersonsController : ApiController
    {
        private ApplicationDbContext _person;

        public PersonsController()
        {
            _person = new ApplicationDbContext();
        }

        // api/persons
        public IEnumerable<Person> GetPersons()
        {
            return _person.Person.ToList();
        }

        // api/persons/{id}
        public IHttpActionResult GetPerson(int id)
        {
            var person = _person.Person.SingleOrDefault(p => p.Id == id);

            if (person == null)
                return NotFound();

            return Ok(person);
        }

        //// api/persons
        //[HttpPost]
        //public Person CreatePerson(Person person)
        //{
        //    if (!ModelState.IsValid)
        //        throw new HttpResponseException(HttpStatusCode.BadRequest);

        //    _person.Person.Add(person);
        //    _person.SaveChanges();

        //    return person;
        //}

        // PUT api/persons/{id}
        [HttpPut]
        public HttpResponseMessage EditPerson(int id, Person person)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var personInDB = _person.Person.SingleOrDefault(p => p.Id == id);

            if(personInDB == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            personInDB.FirstName = person.FirstName;
            personInDB.LastName = person.LastName;
            personInDB.JobTitle = person.JobTitle;

            _person.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK, person);
        }
    }
}
