﻿using System;
using System.Collections.Generic;

namespace AuthorService.DatabaseEntity
{
    public partial class User
    {
        public User()
        {
            Books = new HashSet<Book>();
        }

        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? EmailId { get; set; }
        public string? Password { get; set; }
        public int? RoleId { get; set; }
        public bool? Active { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public virtual RoleMaster? Role { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}
