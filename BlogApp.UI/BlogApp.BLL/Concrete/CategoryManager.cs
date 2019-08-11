using BlogApp.BLL.Abstract;
using BlogApp.DAL.Abstract;
using BlogApp.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BlogApp.BLL.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public bool Add(Category category)
        {
            return _categoryDal.Add(category);
        }
        public bool Delete(int id)
        {
            return _categoryDal.Delete(id);
        }
        public Category Get(Expression<Func<Category, bool>> filter)
        {
            return _categoryDal.Get(filter);
        }
        public List<Category> GetList(Expression<Func<Category, bool>> filter = null)
        {
            return filter == null
                ? _categoryDal.GetList()
                : _categoryDal.GetList(filter);
        }
        public bool Update(Category category)
        {
            return _categoryDal.Update(category);
        }
    }
}
