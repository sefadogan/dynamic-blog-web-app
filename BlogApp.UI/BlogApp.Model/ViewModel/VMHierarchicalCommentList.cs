using BlogApp.Model.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Model.ViewModel
{
    public class VMHierarchicalCommentList
    {
        public int CommentId { get; set; }
        public string Text { get; set; }
        public DateTime InsertedDate { get; set; }
        public int UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }

        public List<VMHierarchicalCommentList> ChildComments = new List<VMHierarchicalCommentList>();
        
    }
}
