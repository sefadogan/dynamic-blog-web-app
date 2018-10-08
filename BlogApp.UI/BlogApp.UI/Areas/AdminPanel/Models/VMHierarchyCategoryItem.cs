using BlogApp.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogApp.UI.Areas.AdminPanel.Models
{
    public class VMHierarchyCategoryItem
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public List<VMHierarchyCategoryItem> ChildCategories = new List<VMHierarchyCategoryItem>();
    }
}