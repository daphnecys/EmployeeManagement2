using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EmployeeManagement2.Models;
using EmployeeManagement2.ViewModels;
using System.Reflection.Metadata.Ecma335;

namespace EmployeeManagement2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmployeeRepository _employeeRepository;

        public HomeController(ILogger<HomeController> logger, 
            IEmployeeRepository employeeRepository)
        {
            _logger = logger;
            _employeeRepository = employeeRepository;
        }

        public ViewResult Index()
        {
            // original
            //return _employeeRepository.GetEmployee(1).Name;

            // Part 27: 
            // retrieve all the employees
            var model = _employeeRepository.GetAllEmployees();
            // Pass the list of employees to the view
            return View(model);
        }

        // Part 41: Model Binding
        // Maps data in HTTP Request to controller action method parameters
        // can be integers, strings, Customer, Employee, etc
        //[Route("Home/Details/{id?}")]
        public ViewResult Details(int? id)
        {
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee = _employeeRepository.GetEmployee(id ?? 1),
                PageTitle = "Employee Details"
            };

            // Pass the ViewModel object to the View() helper method
            return View("../Employees/Details", homeDetailsViewModel);
        }

        // Part 41: 2nd example
        //http://localhost:48118/home/details/2?name=pragim
        // try http://localhost:48118/home/details/2?name=pragim&id=3
        //public string Details(int? id, string name)
        //{
        //    return "id = " + id.Value.ToString() + " and name = " + name;
        //}

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        //Part 41: Complex Model Binding
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                Employee newEmployee = _employeeRepository.Add(employee);
                //return RedirectToAction("details", new { id = newEmployee.Id });
            }
            return View();
        }

        // Part 42: Validation
        //[HttpPost]
        //public IActionResult Create(Employee employee)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Employee newEmployee = _employeeRepository.Add(employee);
        //        return RedirectToAction("details", new { id = newEmployee.Id });
        //    }
        //    return View();
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
