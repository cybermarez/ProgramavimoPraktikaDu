using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace Data.Models
{
    public class User //čia yra klasė
    {
        public int Id { get; set; } //klasės properties
        //^ access modifieris
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Birthddate { get; set; }

        public string Password { get; set; }

        public UserType UserType { get ;set; }

        public List<WishList> WishList { get; set; } = new List<WishList>();

        public List<ProductCart> ProductCart { get; set; } = new List<ProductCart>();
    }
}
//Visa tai cia yra modelis^