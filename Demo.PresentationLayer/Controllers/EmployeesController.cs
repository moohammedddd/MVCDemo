using AspNetCoreGeneratedDocument;
using DataAccessLayer.Models.Employees;
using Demo.BusinessLogic.DTOs.DepartmentDtos;
using Demo.BusinessLogic.DTOs.EmployeeDtos;
using Demo.BusinessLogic.Services;
using Demo.BusinessLogic.Services.AttchmentServices;
using Demo.BusinessLogic.Services.Classes;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.PresentationLayer.ViewModel.DepartmentViews;
using Demo.PresentationLayer.ViewModel.EmployeeViews;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection.Metadata.Ecma335;

namespace Demo.PresentationLayer.Controllers
{
    public class EmployeesController(IEmployeeServices employeeServices,
       ILogger<EmployeesController> _logger,

        IWebHostEnvironment _enviroment,
        IAttchmentServices _attachmentService
        //IDepartmentServices _departmentServices
        ) : Controller
    {
        public IActionResult Index(string? EmployeeSearchByName)
        {
            var employees = employeeServices.GetAllEmployees(EmployeeSearchByName);
            return View(employees);

        }

        #region Create
        public IActionResult Create([FromServices]IDepartmentServices _departmentServices)
        {
            ViewData["Departments"] = _departmentServices.GetAllDepartments();
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeViewModel , [FromServices] IDepartmentServices _departmentServices)

        {
            
            if (ModelState.IsValid)
            {
                try
                {var createEmployeeDto = new CreateEmployeeDto()
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
                Salary = employeeViewModel.Salary,
                Image = employeeViewModel.Image,
               
               
                

            };
                    int? result = employeeServices.CreateEmployee(createEmployeeDto);
                    if (result > 0) return RedirectToAction(nameof(Index)); //Back to List
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Employee cant be created");
                        //return View(createEmployeeDto);

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

                    ViewData["Departments"] = _departmentServices.GetAllDepartments();
                    return View(employeeViewModel);
                }

            }
        
            return View(employeeViewModel);

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
                DepartmentId = employee.DepartmentId,
                ImgUrl = employee.ImageDesc
                

            };

       
        
            ViewData["Departments"] = _departmentServices.GetAllDepartments();

            return View(updatedEmployee);
        }



        [HttpPost]
        #region BAse
        //public IActionResult Edit([FromRoute] int? Id, EmployeeViewModel employeeViewModel)
        //{
        //    if (!Id.HasValue) return BadRequest();

        //    if (!ModelState.IsValid) return View(employeeViewModel);
        //    try
        //    {
        //        var UpdatedEmployee = new UpdateEmployeeDto()
        //        {
        //            Id = Id.Value,
        //            Name = employeeViewModel.Name,
        //            Address = employeeViewModel.Address,
        //            Age = employeeViewModel.Age,
        //            Salary = employeeViewModel.Salary,
        //            IsActive = employeeViewModel.IsActive,
        //            Email = employeeViewModel.Email,
        //            PhoneNumber = employeeViewModel.PhoneNumber,
        //            DepartmentId = employeeViewModel.DepartmentId,
        //            Gender = employeeViewModel.Gender,
        //            EmployeeType = employeeViewModel.EmployeeType,


        //        };
        //        if (employeeViewModel.Image is null)
        //        {
        //            UpdatedEmployee.ImageName = employeeViewModel.ImageDesc; // حط الاسم القديم
        //        }
        //        else
        //        {
        //            // لو فيه صورة جديدة
        //            if (employeeViewModel.ImageDesc is not null)
        //            {
        //                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", "Images");
        //                var filePath = Path.Combine(folderPath, employeeViewModel.ImageDesc);
        //                _attachmentService.Delete(filePath); // امسح القديمة
        //            }

        //            // ارفع الجديدة وخد الاسم
        //            var uploadedFileName = _attachmentService.Upload(employeeViewModel.Image, "Images");
        //            UpdatedEmployee.ImageName = uploadedFileName;
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception
        //        if (_enviroment.IsDevelopment())
        //        {
        //            ModelState.AddModelError(string.Empty, ex.Message);
        //        }
        //        else
        //        {
        //            _logger.LogError(ex.Message);
        //        }

        //    }

        //    return View(employeeViewModel);
        //}
        #endregion


        public IActionResult Edit([FromRoute] int? id, EmployeeViewModel employeeVM)
        {
            if (!id.HasValue) return BadRequest();

            if (!ModelState.IsValid) return View(employeeVM);
            try
            {
                var employeeDto = new UpdateEmployeeDto()
                {
                    Id = id.Value,
                    Name = employeeVM.Name,
                    Address = employeeVM.Address,
                    Age = employeeVM.Age,
                    Email = employeeVM.Email,
                    HiringDate = employeeVM.HiringDate,
                    PhoneNumber = employeeVM.PhoneNumber,
                    IsActive = employeeVM.IsActive,
                    Salary = employeeVM.Salary,
                    Gender = employeeVM.Gender,
                    EmployeeType = employeeVM.EmployeeType,
                    DepartmentId = employeeVM.DepartmentId
                };

                if (employeeVM.Image is null)//no update in image
                {
                    //المفروض الصوره تفضل زي ماهي      
                    employeeDto.ImageName = employeeVM.ImgUrl;
                }
                if (employeeVM.Image is not null)
                {//new image
                    if (employeeVM.ImgUrl is not null)
                    {//Old Image
                        var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", "Images");
                        var filePath = Path.Combine(folderPath, employeeVM.ImgUrl);
                        _attachmentService.Delete(filePath);
                    }

                    employeeDto.ImageName = _attachmentService.Upload(employeeVM.Image, "Images");
                }

                var res = employeeServices.UpdateEmployeeDto(employeeDto);
                if (res > 0) return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee Can't Be Updated");
                }
            }
            catch (Exception ex)
            {

                if (_enviroment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                else
                {
                    _logger.LogError(ex.Message);
                }
            }

            return View(employeeVM);
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
