using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Model.DataModel
{
    public class Tag
    {
        public Tag()
        {
            this.PostTags = new HashSet<PostTag>();
        }

        public int TagId { get; set; }
        public string Name { get; set; }
        
        public virtual ICollection<PostTag> PostTags { get; set; }
    }
}
