using System.ComponentModel.DataAnnotations;

namespace CRUDwithImage.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public string? Color { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public string? Image { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
