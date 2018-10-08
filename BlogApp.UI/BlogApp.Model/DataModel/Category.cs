using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Model.DataModel
{
    public class Category
    {
        public Category()
        {
            Posts = new HashSet<Post>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime InsertedDate { get; set; }
        public bool IsActive { get; set; }
        public int ParentCategoryId { get; set; }
        public int RootLevel { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
