using DemoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DemoAPI.Controllers
{
    /// <summary>
    /// Information about people
    /// </summary>
    public class EmployeeController : ApiController
    {
        List<Employee> employees = new List<Employee>();

        public EmployeeController()
        {
            employees.Add(new Employee { EmployeeId = 1, FirstName = "Anser", LastName = "Nawaz", DOB = "01/02/1990", StartDate = "02/03/2018", HomeTown = "Manchester", Department = "Research"});
            employees.Add(new Employee { EmployeeId = 2, FirstName = "Jack", LastName = "Smith", DOB = "01/02/1991", StartDate = "03/04/2019", HomeTown = "Salford", Department = "Marketing" });
            employees.Add(new Employee { EmployeeId = 3, FirstName = "Sue", LastName = "Fitzgerald", DOB = "21/07/1994", StartDate = "05/06/2018", HomeTown = "Bury", Department = "Sales" });

        }

        // GET: api/People
        public List<Employee> Get()
        {
            return employees;
        }

        // GET: api/People/5
        public Employee Get(int id)
        {
            return employees.Where(x => x.EmployeeId == id).FirstOrDefault();
        }

        /// <summary>
        /// Gets a list of first name of all users
        /// </summary>
        /// <param name="userId">Unique identifier for this person</param>
        /// <param name="age">We want to know their age</param>
        /// <returns>A list of first names</returns>
        [Route("api/Employee/GetFirstNames")]
        [HttpGet]
        public List<string> GetFirstNames(int userId, int age)
        {
            List<string> output = new List<string>();

            foreach (var p in employees)
            {
                output.Add(p.FirstName);
            }
            return output;
        }

        // POST: api/People
        public void Post(Employee val)
        {
            employees.Add(val);
        }

        // PUT: api/People/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/People/5
        public void Delete(int id)
        {
        }
    }
}