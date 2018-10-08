using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Model.DataModel
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Text { get; set; }
        public byte Status { get; set; }
        public DateTime InsertedDate { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public int ParentCommentId { get; set; }

        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}
