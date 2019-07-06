using DemoAPI.Common;
using DemoAPI.DataStore;
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
        private static IEmployeeRepository _repository = new EmployeeRepository();
        List<Employee> employeeList = new List<Employee>();

        public EmployeeController()
        {
            //employees.Add(new Employee { EmployeeId = 1, FirstName = "Anser", LastName = "Nawaz", DOB = "01/02/1990", StartDate = "02/03/2018", HomeTown = "Manchester", Department = "Research"});
            //employees.Add(new Employee { EmployeeId = 2, FirstName = "Jack", LastName = "Smith", DOB = "01/02/1991", StartDate = "03/04/2019", HomeTown = "Salford", Department = "Marketing" });
            //employees.Add(new Employee { EmployeeId = 3, FirstName = "Sue", LastName = "Fitzgerald", DOB = "21/07/1994", StartDate = "05/06/2018", HomeTown = "Bury", Department = "Sales" });
            _repository.GetData(employeeList);

        }

        // GET: api/Employee
        public List<Employee> Get()
        {
            return employeeList;
        }

        // GET: api/Employee/5
        public Employee Get(int id)
        {
            return employeeList.Where(x => x.EmployeeId == id).FirstOrDefault();
        }

        [Route("api/Employee/GetEmployeesPerTown")]
        [HttpGet]
        // GET: api/Employee/GetEmployeesPerTown
        public List<string> GetEmployeesPerTown()
        {
            List<string> output = new List<string>();
            var results = employeeList
                          .GroupBy(e => e.HomeTown)
                          .Select(e => new { Hometown = e.Key, NumberOfEmployees = e.Count() });

            //Console.WriteLine("Number of employees per town.");

            foreach (var x in results)
            {
                output.Add(x.ToString());
                //Console.WriteLine($"The number of employees from {x.Hometown} is {x.NumberOfEmployees}");
            }

            return output;
        }

        [Route("api/Employee/GetAverageEmployeeAgeInDept")]
        [HttpGet]
        // GET: api/Employee/GetAverageEmployeeAgeInDept
        public List<string> GetAverageEmployeeAgeInDept()
        {
            List<string> output = new List<string>();
            var results = employeeList
                          .GroupBy(e => e.Department)
                          .Select(e => new { Department = e.Key, NumberOfEmployees = e.Count(), TotalAge = e.Sum(x => x.Age) });

            


            foreach (var x in results)
            {
               // output.Add(x.ToString());
                output.Add($"The average age for {x.Department} is {Math.Round((float)x.TotalAge / (float)x.NumberOfEmployees, 1, MidpointRounding.AwayFromZero)}");
              //  Console.WriteLine($"The average age for {x.Department} is {Math.Round((float)x.TotalAge / (float)x.NumberOfEmployees, 1, MidpointRounding.AwayFromZero)}");
            }
            return output;
        }

        [Route("api/Employee/ShowEmployeeWithAnniversary")]
        [HttpGet]
        public  List<string> ShowEmployeeWithAnniversary()
        {
            List<string> output = new List<string>();
            foreach (var employee in employeeList)
            {
                if (employee.StartDate.CompareStartDate())
                {
                    output.Add($"{employee.FirstName} with a start date of {employee.StartDate} has an anniversary within 30 days");
                   // Console.WriteLine($"{employee.FirstName} with a start date of {employee.StartDate} has an anniversary within 30 days");
                };

            }

            return output;
        }


        /// <summary>
        /// Gets a list of first name of all users
        /// </summary>
        /// <param name="userId">Unique identifier for this person</param>
        /// <param name="age">We want to know their age</param>
        /// <returns>A list of first names</returns>
        [Route("api/Employee/GetFirstNames")]
        [HttpGet]
        public List<string> GetFirstNames()
        {
            List<string> output = new List<string>();

            foreach (var p in employeeList)
            {
                output.Add(p.FirstName);
            }
            return output;
        }

        /// <summary>
        /// Add new employee
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        // POST: api/employee
        public List<Employee> Post([FromBody]Employee val)
        {
             employeeList.Add(val);
            _repository.AddEmployeeManually(employeeList);
            return employeeList;
        }

        /// <summary>
        /// Edit DOB of existing employee
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        // PUT: api/employee/5
        public List<Employee> Put(int id, [FromBody]DateTime value)
        {
            
            var editItem = employeeList.FirstOrDefault(e => e.EmployeeId == id);
            if (editItem != null)
            {
                
               
                editItem.DOB = value;
                
            }
            else
            {
                Console.WriteLine("Record not found");
            }
            return employeeList;
            
        }

        // DELETE: api/employee/5
        public List<Employee> Delete(int id)
        {
            var removeItem = employeeList.FirstOrDefault(e => e.EmployeeId == id);
            if (removeItem != null)
            {
               // Console.WriteLine($"{removeItem.FirstName} with ID {removeItem.EmployeeId} will be removed!");
                 employeeList.Remove(removeItem);
                _repository.RemoveEmployee(employeeList);

            }
            else
            {
                Console.WriteLine("Record not found");
            }
            return employeeList;
        }
    }
}