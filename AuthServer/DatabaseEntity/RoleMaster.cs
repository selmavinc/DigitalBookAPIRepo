﻿using System;
using System.Collections.Generic;

namespace AuthServer.DatabaseEntity
{
    public partial class RoleMaster
    {
        public RoleMaster()
        {
            Users = new HashSet<User>();
        }

        public int RoleId { get; set; }
        public string? RoleName { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
