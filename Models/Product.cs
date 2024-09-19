using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public double Price { get; set; }

        public string Description { get; set; }

        [ForeignKey("Category")]
        public int? CategoryId {  get; set; }
        public virtual  Category? Category { get; set; }
    }
}
