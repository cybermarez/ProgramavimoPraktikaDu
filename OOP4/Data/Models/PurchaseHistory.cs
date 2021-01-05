using System;

namespace Data.Models
{
    public class PurchaseHistory
    {
        public int Id { get; set; }

        public Guid PurchaseId { get; set; }

        public DateTime Date { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
