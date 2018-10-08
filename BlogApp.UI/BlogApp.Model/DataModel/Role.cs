﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Model.DataModel
{
    public class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public int RoleId { get; set; }
        public string Name { get; set; }
        public DateTime InsertedDate { get; set; }
        
        public virtual ICollection<User> Users { get; set; }
    }
}
