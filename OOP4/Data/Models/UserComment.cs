using System;

namespace Data.Models
{
    public class UserComment
    {
        public int Id { get; set; }

        public string Comment { get; set; }

        public DateTime DateAdded { get; set; }

        public int UserId { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
