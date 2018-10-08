using BlogApp.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.BLL.Services
{
    public class BaseService
    {
        protected BlogAppEntities Context { get; set; }

        public BaseService()
        {
            Context = new BlogAppEntities();
        }
    }
}
