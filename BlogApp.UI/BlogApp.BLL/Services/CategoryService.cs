using BlogApp.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.BLL.Services
{
    public class CategoryService : BaseService
    {
        public bool Add(Category data)
        {
            try
            {
                Context.Categories.Add(data);
                Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                // Exception ı da göndermek istesem nasıl yapabilirim?
                return false;
            }
        }
        public bool Update(Category data)
        {
            try
            {
                Context.Categories.Attach(data);
                Context.Entry(data).State = EntityState.Modified;
                Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                // Exception ı da göndermek istesem nasıl yapabilirim?
                return false;
            }

        }
        public bool Delete(int id)
        {
            try
            {
                Category category = Context.Categories.Find(id);
                //Context.Categories.Remove(category);
                category.IsActive = false;
                Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public Category BringById(int id)
        {
            return Context.Categories.Find(id);
        }
        public List<Category> ListAll()
        {
            return Context.Categories.ToList();
        }
    }
}
