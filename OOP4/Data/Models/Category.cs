using System.Collections.Generic;

namespace Data.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string CategoryName { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
