using Demo.BusinessLogic.Services;
using DataAccessLayer.Repositiories;
using Microsoft.AspNetCore.Mvc;
using Demo.BusinessLogic.DTOs;
using Demo.PresentationLayer.ViewModel.DepartmentViews;

namespace Demo.PresentationLayer.Controllers
{
    public class DepartmentController(IDepartmentServices departmentServices,
       ILogger<DepartmentController> _logger,
        IWebHostEnvironment _enviroment) : Controller
        
    {
        public IActionResult Index()

        {
            var departments = departmentServices.GetAllDepartments();
            return View(departments);
        }

        [HttpGet]
        public IActionResult Create()

        {
            //var departments = departmentServices.CreateDepartment();
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateDepartmantDto createDepartmantDto)

        {
            if (ModelState.IsValid)
            {
                try
                {
                    int? result = departmentServices.CreateDepartment(createDepartmantDto);
                    if(result > 0) return RedirectToAction(nameof(Index)); //Back to List
                    else
                    {
                        ModelState.AddModelError(string.Empty,"Department cant be created");
                        return View(createDepartmantDto);

                    }
                }
                catch (Exception ex)
                {
                    // Log the exception
                   if(_enviroment.IsDevelopment())
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "An error occurred while creating the department.");
                    }
                    return View(createDepartmantDto);
                }
            }
           
                return View(createDepartmantDto);
            
        }
        
        public IActionResult Details(int? Id)
        {
            if(!Id.HasValue) return BadRequest();
            var department = departmentServices.GetDepartmentById(Id.Value);
            if (department == null) return NotFound();
            return View(department);
        }
        
        [HttpGet] // Get : /Department/Edit/Id?
        public IActionResult Edit(int? Id)
        {
            if (!Id.HasValue) return BadRequest();
            var department = departmentServices.GetDepartmentById(Id.Value);

            //Map From DepartmentDetailsDto To DepartmentEditViewModel
           
            if (department == null) return NotFound();
            else
            {
                var departmentEditViewModel = new DepartmentEditViewModel()
                {
                    Name = department.Name,
                    Code = department.Code,
                    Description = department.Description,
                    DateOfCreation = department.DateOfCreation
                };
                return View(departmentEditViewModel);
            }
          
        }

        [HttpPost ]
        public IActionResult Edit([FromRoute]int  Id ,DepartmentEditViewModel departmentEditViewModel)
        {

            if(!ModelState.IsValid) return View(departmentEditViewModel);
            try
            {
             
                        // Map From DepartmentEdit ViewModel To UpdateDepartmentDto
                var UpdatedDept = new UpdateDepartmetnDto()
                {
                    Id = Id,
                    Name = departmentEditViewModel.Name,
                    Code = departmentEditViewModel.Code,
                    Description = departmentEditViewModel.Description,
                    DateOfCreation = departmentEditViewModel.DateOfCreation
                };
                var result = departmentServices.UpdateDepartment(UpdatedDept);
                if (result > 0) return RedirectToAction(nameof(Index)); //Back to List
                else
                {
                    ModelState.AddModelError(string.Empty, "Department cant be updated");
                    return View(departmentEditViewModel);
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
            return View(departmentEditViewModel);

        }
    
    }


}
