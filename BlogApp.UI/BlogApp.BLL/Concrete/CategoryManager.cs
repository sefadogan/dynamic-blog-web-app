using BlogApp.BLL.Abstract;
using BlogApp.DAL.Abstract;
using BlogApp.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.BLL.Concrete
{
    public class CategoryManager : ICategoryManager
    {
        private readonly ICategoryRepository _repository;

        public CategoryManager(ICategoryRepository category)
        {
            _repository = category;
        }

        public bool Add(Category data)
        {
            return _repository.Add(data);
        }

        public bool Update(Category data)
        {
            return _repository.Add(data);
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }

        public Category BringById(int id)
        {
            return _repository.BringById(id);
        }

        public List<Category> ListAll()
        {
            return _repository.ListAll();
        }
    }
}
