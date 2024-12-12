using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Categories
    {
        [Key] // Explicitly mark this as the Primary Key
        public int categoryid { get; set; }

        [Required] // Optional: Mark it as required
        public string categoryname { get; set; }

        public string description { get; set; }
    }
}