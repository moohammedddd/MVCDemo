using DataAccessLayer.Repositiories;
using Microsoft.AspNetCore.Mvc;
using Demo.BusinessLogic.DTOs;
using Demo.PresentationLayer.ViewModel.DepartmentViews;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.BusinessLogic.DTOs.DepartmentDtos;   


namespace Demo.PresentationLayer.Controllers
{
    public class DepartmentController(IDepartmentServices departmentServices,
                               ILogger<DepartmentController> _logger,
                                IWebHostEnvironment _enviroment) : Controller
        
    {
        public IActionResult Index(string? DepartmentSearch)

        {
            //ViewData["Message1"] = "Welcome to the Department Index Page";  
            //ViewBag.Message1 = "Welcome to the Department Index Page => ViewBag"
            var departments = departmentServices.GetAllDepartments(DepartmentSearch);
            return View(departments);
        }

        #region Create
        [HttpGet]
        public IActionResult Create()

        {
            //var departments = departmentServices.CreateDepartment();
            return View();
        }
        [HttpPost]
        public IActionResult Create(DepartmentViewModel departmentViewModel )

        {
            if (ModelState.IsValid)
            {
                try
                {
                    var createDepartmentDto = new CreateDepartmentDto()
                    {
                        Name = departmentViewModel.Name,
                        Code = departmentViewModel.Code,
                        Description = departmentViewModel.Description,
                        DateOfCreation = departmentViewModel.DateOfCreation
                    };
                    var message = string.Empty;
                    int? result = departmentServices.CreateDepartment(createDepartmentDto);
                    if (result > 0) message = "Department Created successfully "; //Back to List
                    else
                    {
                        message = "Department not Created successfully";


                    }
                    ViewData["SpecialMsg01"] = message;
                    TempData["SpecialMsg04"] = message;
                    return RedirectToAction(nameof(Index));
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
                        ModelState.AddModelError(string.Empty, "An error occurred while creating the department.");
                    }
                    return View(departmentViewModel);
                }
            }

            return View(departmentViewModel);

        }

        #endregion

        #region Details
        public IActionResult Details(int? Id)
        {
            if (!Id.HasValue) return BadRequest();
            var department = departmentServices.GetDepartmentById(Id.Value);
            if (department == null) return NotFound();
            return View(department);
        }
        #endregion



        #region Edit
        [HttpGet] // Get : /Department/Edit/Id?
        public IActionResult Edit(int? Id)
        {
            if (!Id.HasValue) return BadRequest();
            var department = departmentServices.GetDepartmentById(Id.Value);

            //Map From DepartmentDetailsDto To DepartmentEditViewModel

            if (department == null) return NotFound();
            else
            {
                var departmentEditViewModel = new DepartmentViewModel()
                {
                    Name = department.Name,
                    Code = department.Code,
                    Description = department.Description,
                    DateOfCreation = department.DateOfCreation
                };
                return View(departmentEditViewModel);
            }

        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit([FromRoute] int Id, DepartmentViewModel departmentEditViewModel)
        {

            if (!ModelState.IsValid) return View(departmentEditViewModel);
            try
            {

                // Map From DepartmentEdit ViewModel To UpdateDepartmentDto
                var message = string.Empty;
                var UpdatedDept = new UpdateDepartmentDto()
                {
                    Id = Id,
                    Name = departmentEditViewModel.Name,
                    Code = departmentEditViewModel.Code,
                    Description = departmentEditViewModel.Description,
                    DateOfCreation = departmentEditViewModel.DateOfCreation
                };
                var result = departmentServices.UpdateDepartment(UpdatedDept);
                if (result > 0)
                {
                    message = "Department updated Successfully";
                     //Back to List
                    

                }
                else
                {
                    //ModelState.AddModelError(string.Empty, "Department cant be updated");
                    message = "Department not Updated Successfully";
                    return View(departmentEditViewModel);
                }
                ViewData["SpecialMsg01"] = message;
                TempData["SpecialMsg03"] = message;
                return RedirectToAction(nameof(Index));

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
            return View(departmentEditViewModel);

        }
        #endregion



        #region Delete
        [HttpGet]
        //public IActionResult Delete(int? Id)
        //{
        //    if (!Id.HasValue) return BadRequest();
        //    var department = departmentServices.GetDepartmentById(Id.Value);
        //    if (department == null) return NotFound();
        //    return View(department);
        //}
        [HttpPost]
        public IActionResult Delete(int Id)
        {
            if(Id == 0) BadRequest();
            try
            {
                var isDeleted = departmentServices.DeleteDepartment(Id);
                var message = string.Empty;
                if(isDeleted)  message = "Department Deleted Successfully";
                else message = "Department cant be deleted";
                
                   
                    //return View();
                    ViewData["SpecialMsg01"] = message;
                    TempData["SpecialMsg02"] = message;
               
                return RedirectToAction(nameof(Index));
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
            return RedirectToAction(nameof(Delete),new {Id});
        }
        #endregion


    }


}
