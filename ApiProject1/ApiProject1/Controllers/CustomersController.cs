using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Description;
using ApiProject1.CalculatorService;
using ApiProject1.DilbertService;
using ApiProject1.Models;

namespace ApiProject1.Controllers
{
    public class CustomersController : ApiController
    {
        private BDforAPIEntities db = new BDforAPIEntities();

        // GET: api/Customers
        public IQueryable<Customer> GetCustomers()
        {
            return db.Customers;
        }

        // GET: api/Customers/5
        [ResponseType(typeof(Customer))]
        public IHttpActionResult GetCustomer(int id)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // PUT: api/Customers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.Id)
            {
                return BadRequest();
            }

            db.Entry(customer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Customers
        [ResponseType(typeof(Customer))]
        public IHttpActionResult PostCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Customers.Add(customer);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = customer.Id }, customer);
        }

        // POST: api/Login
        [Route("api/Login")]
        [ResponseType(typeof(Customer))]
        public IHttpActionResult Login(Login login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (string.IsNullOrEmpty( login.Password) || string.IsNullOrEmpty(login.UserName))
            {
                return StatusCode(HttpStatusCode.NotAcceptable);
            }

            foreach (var n in db.Customers)
            {
                if(n.UserName == login.UserName && n.Password == login.Password)
                {
                    Console.WriteLine("logged in OK as " + n.UserName);
                    return Ok("true");
                }
            }
            return Unauthorized();
        }

        // POST: api/Add
        [Route("api/Add")]
        [ResponseType(typeof(Customer))]
        public IHttpActionResult FunctionThatAccessesSOAP(TwoIntegers integers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var client = new CalculatorSoapClient();
            int resultInt = client.Add(integers.int1, integers.int2);
            var result = new IntegerResult(resultInt, "Operation Succesful");
            return Ok(result);
        }
        [Route("api/Dilbert")]
        [ResponseType(typeof(Customer))]
        public IHttpActionResult FunctionDilbert(Date date )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var client = new DilbertSoapClient();
            DateTime fecha = new DateTime(date.year, date.month, date.day);
            Debug.WriteLine("probando");
            string resultString = client.DailyDilbert(fecha);
            Debug.WriteLine(resultString);
            var result = new StringResult(resultString, "Operation Succesful");
            return Ok(result);
        }

        // DELETE: api/Customers/5
        [ResponseType(typeof(Customer))]
        public IHttpActionResult DeleteCustomer(int id)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            db.Customers.Remove(customer);
            db.SaveChanges();

            return Ok(customer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerExists(int id)
        {
            return db.Customers.Count(e => e.Id == id) > 0;
        }
    }
}