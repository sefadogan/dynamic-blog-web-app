using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Model.DataModel
{
    public class User
    {
        public User()
        {
            Comments = new HashSet<Comment>();
            Posts = new HashSet<Post>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public bool IsActive { get; set; }
        public DateTime InsertedDate { get; set; }
        public int RoleId { get; set; }
        public string Password { get; set; }
        
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual RRole Role { get; set; }
    }
}
