using AspNetCoreGeneratedDocument;
using DataAccessLayer.Models.Employees;
using Demo.BusinessLogic.DTOs.DepartmentDtos;
using Demo.BusinessLogic.DTOs.EmployeeDtos;
using Demo.BusinessLogic.Services;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.PresentationLayer.ViewModel.DepartmentViews;
using Demo.PresentationLayer.ViewModel.EmployeeViews;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection.Metadata.Ecma335;

namespace Demo.PresentationLayer.Controllers
{
    public class EmployeesController(IEmployeeServices employeeServices,
       ILogger<EmployeesController> _logger,

        IWebHostEnvironment _enviroment
        //IDepartmentServices _departmentServices
        ) : Controller
    {
        public IActionResult Index()
        {
            var employees = employeeServices.GetAllEmployees();
            return View(employees);

        }

        #region Create
        public IActionResult Create([FromServices]IDepartmentServices _departmentServices)
        {
            ViewData["Departments"] = _departmentServices.GetAllDepartments();
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeViewModel)

        {
            var createEmployeeDto = new CreateEmployeeDto()
            {
                Name = employeeViewModel.Name,
                Address = employeeViewModel.Address,
                Age = employeeViewModel.Age,
                DepartmentId = employeeViewModel.DepartmentId,
                Email = employeeViewModel.Email,
                Gender = employeeViewModel.Gender,
                HiringDate = employeeViewModel.HiringDate,
                EmployeeType = employeeViewModel.EmployeeType,
                IsActive = employeeViewModel.IsActive,
                PhoneNumber = employeeViewModel.PhoneNumber,
                Salary = employeeViewModel.Salary


            };

            if (ModelState.IsValid)
            {
                try
                {
                    int? result = employeeServices.CreateEmployee(createEmployeeDto);
                    if (result > 0) return RedirectToAction(nameof(Index)); //Back to List
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Employee cant be created");
                        return View(createEmployeeDto);

                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Exception while creating employee: {ex.InnerException?.Message ?? ex.Message}");

                    var innerMessage = ex.InnerException?.Message ?? ex.Message;

                    if (_enviroment.IsDevelopment())
                    {

                        ModelState.AddModelError(string.Empty, innerMessage);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "An error occurred while creating the employees");
                    }

                    return View(createEmployeeDto);
                }

            }

            return View(createEmployeeDto);

        }
        #endregion

        #region Details
        [HttpGet]
        public IActionResult Details (int? id)
        {
            if(!id.HasValue) return BadRequest();
            var employeeDetails = employeeServices.GetEmployeeById(id.Value);
            return employeeDetails is null ? NotFound() : View(employeeDetails);
        }
        #endregion

        #region Edit 
        [HttpGet]
        public IActionResult Edit(int? id, [FromServices] IDepartmentServices _departmentServices)
        {
            if (!id.HasValue) return BadRequest();

            var employee = employeeServices.GetEmployeeById(id.Value);

            var updatedEmployee = new EmployeeViewModel()
            {
                
                Name = employee.Name,
                Address = employee.Address,
                Age = employee.Age,
                Salary = employee.Salary,
                IsActive = employee.IsActive,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                Gender = Enum.Parse<Gender>(employee.Gender),
                EmployeeType = Enum.Parse<EmployeeType>(employee.EmployeeType),
                DepartmentId = employee.DepartmentId 
            };

        
            ViewData["Departments"] = _departmentServices.GetAllDepartments();

            return View(updatedEmployee);
        }



        [HttpPost]
        public IActionResult Edit([FromRoute]int? Id,EmployeeViewModel employeeViewModel)
        {
            if (!Id.HasValue) return BadRequest();
         
            if (!ModelState.IsValid) return View(employeeViewModel);
            try
            {
                var UpdatedEmployee = new UpdateEmployeeDto()
                {
                    Id = Id.Value,
                    Name = employeeViewModel.Name,
                    Address = employeeViewModel.Address,
                    Age = employeeViewModel.Age,
                    Salary = employeeViewModel.Salary,
                    IsActive = employeeViewModel.IsActive,
                    Email = employeeViewModel.Email,
                    PhoneNumber = employeeViewModel.PhoneNumber,
                    DepartmentId = employeeViewModel.DepartmentId,
                    Gender =employeeViewModel.Gender,
                    EmployeeType =employeeViewModel.EmployeeType,

                };
             
                var result = employeeServices.UpdateEmployeeDto(UpdatedEmployee); // this data from dto
                if (result > 0) return RedirectToAction(nameof(Index)); //Back to List
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee cant be updated");
                   
                }

            }
            catch (Exception ex)
            {
                // Log the exception
                if (_enviroment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                else
                {
                    _logger.LogError(ex.Message);
                }

            }

            return View(employeeViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        public IActionResult Delete(int Id)
        {
            if (Id == 0) return BadRequest();

            try
            {
                var isDeleted = employeeServices.DeleteEmployee(Id);
                if (isDeleted) return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee cant be deleted");
                    return RedirectToAction(nameof(Delete), new { Id });
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                if (_enviroment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                else
                {
                    _logger.LogError(ex.Message);
                }
            }
            return View("Error");
        }
        #endregion

    }
}
