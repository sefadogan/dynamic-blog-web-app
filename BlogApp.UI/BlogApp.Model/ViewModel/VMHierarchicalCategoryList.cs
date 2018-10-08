using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Model.ViewModel
{
    public class VMHierarchicalCategoryList
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<VMHierarchicalCategoryList> ChildCategories = new List<VMHierarchicalCategoryList>();
    }
}
