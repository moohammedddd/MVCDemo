using AspNetCoreGeneratedDocument;
using DataAccessLayer.Models.Employees;
using Demo.BusinessLogic.DTOs.DepartmentDtos;
using Demo.BusinessLogic.DTOs.EmployeeDtos;
using Demo.BusinessLogic.Services;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.PresentationLayer.ViewModel.DepartmentViews;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection.Metadata.Ecma335;

namespace Demo.PresentationLayer.Controllers
{
    public class EmployeesController(IEmployeeServices employeeServices,
       ILogger<EmployeesController> _logger,
        IWebHostEnvironment _enviroment) : Controller
    {
        public IActionResult Index()
        {
            var employees = employeeServices.GetAllDepartment();
            return View(employees);

        }

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateEmployeeDto createEmployeeDto)

        {
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
            var employeeDetails = employeeServices.GetDepartmentById(id.Value);
            return employeeDetails is null ? NotFound() : View(employeeDetails);
        }
        #endregion

        #region Edit 
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(!id.HasValue) return BadRequest();
            var employee = employeeServices.GetDepartmentById(id.Value);
            // Map From EmployeeDetails to UpdateEmployeeDto
            var UpdatedEmployee = new UpdateEmployeeDto()
            {
                Id = id.Value,
                Name = employee.Name,
                Age = employee.Age,
                Address = employee.Address,
                Salary = employee.Salary,
                IsActive = employee.IsActive,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                HiringDate = employee.HiringDate,
                Gender = Enum.Parse<Gender>(employee.Gender),
                EmployeeType = Enum.Parse<EmployeeType>(employee.EmployeeType)


            };
           return View(UpdatedEmployee);


        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int Id, UpdateEmployeeDto updateEmployeeDto)
        {

            if (!ModelState.IsValid) return View(updateEmployeeDto);
            try
            {
                var result = employeeServices.UpdateEmployeeDto(updateEmployeeDto);
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
            return View(updateEmployeeDto);

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
