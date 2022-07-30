using System.ComponentModel.DataAnnotations;

namespace WebApiTest.Dtos; 

public class ProductCreateDto {
    [Required]
    public string Name { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    public int Count { get; set; }
}