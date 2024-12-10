using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Products
    {
        [Key] // Explicitly mark this as the Primary Key
        public int productid { get; set; }

        [Required] // Optional: Mark it as required
        public string productname { get; set; }

        [Required] // This foreign key references Categories
        public int categoryid { get; set; }

        [Required]
        public decimal unitprice { get; set; }

        [Required]
        public int unitsinstock { get; set; }
    }
}