using System;

namespace Data.Models
{
    public class UsersList
    {
            public int Id { get; set; } //klasės properties
                                        //^ access modifieris
            public string Username { get; set; }

            public string FirstName { get; set; }

            public string LastName { get; set; }

            public DateTime Birthddate { get; set; }
    }
}
