using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Model.DataModel
{
    public class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
            PostTags = new HashSet<PostTag>();
        }

        public int PostId { get; set; }
        public string Title { get; set; }
        public string BodySummary { get; set; }
        public string Body { get; set; }
        public short? TimesDisplayed { get; set; }
        public DateTime InsertedDate { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public string MetaKeywords { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<PostTag> PostTags { get; set; }
    }
}
