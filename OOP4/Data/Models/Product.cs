using System.Collections.Generic;

namespace Data.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public byte[] Image { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public List<UserComment> UserComments { get; set; } = new List<UserComment>();
    }
}
