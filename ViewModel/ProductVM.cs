using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CRUDwithImage.ViewModel
{
    public class ProductVM
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }

        public string? Description { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public string? Color { get; set; }
        [Required]
        public string? Image { get; set; }
        [Required]
        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> Category { get; set; } = default!;
    }
}
