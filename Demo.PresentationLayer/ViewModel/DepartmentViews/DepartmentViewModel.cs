using System.ComponentModel.DataAnnotations;
namespace Demo.PresentationLayer.ViewModel.DepartmentViews

{
    public class DepartmentViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime? DateOfCreation { get; set; }

    }
}
