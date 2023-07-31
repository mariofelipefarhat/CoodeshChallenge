using System.ComponentModel.DataAnnotations;

namespace Coodesh.Infrastructure.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? LastName { get; set; }
    }
}
