using Entity;
using System.ComponentModel.DataAnnotations;

namespace baskidabaski.Models
{
    public class ProductModel
    {


        public int ProductId { get; set; }
        //[Display(Name="Name",Prompt ="Enter productName")]

        [Required(ErrorMessage = "Name alanı boş geçilemez.")]
        [StringLength(60, MinimumLength = 5, ErrorMessage = "name alanı 5-60 karakter arasında olmalı.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price alanı boş geçilemez.")]

        public double Price { get; set; }
        [Required(ErrorMessage = "Description alanı boş geçilemez.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "name alanı 5-100 karakter arasında olmalı.")]
        public string Description { get; set; }
        public IFormFile? Image1 { get; set; }

        public IFormFile? Image2 { get; set; }
        public string? Image1String { get; set; }
        public string? Image2String { get; set; }

        public bool IsApproved { get; set; }
        public bool IsHome { get; set; }
        public List<Category>? SelectedCategories { get; set; }

     
    }
}
