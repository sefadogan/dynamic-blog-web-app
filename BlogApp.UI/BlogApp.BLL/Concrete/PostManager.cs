using BlogApp.BLL.Abstract;
using BlogApp.DAL.Abstract;
using BlogApp.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BlogApp.BLL.Concrete
{
    public class PostManager : IPostService
    {
        private IPostDal _postDal;

        public PostManager(IPostDal postDal)
        {
            _postDal = postDal;
        }

        public bool Add(Post post)
        {
            return _postDal.Add(post);
        }
        public bool Delete(int id)
        {
            return _postDal.Delete(id);
        }
        public Post Get(Expression<Func<Post, bool>> filter)
        {
            return _postDal.Get(filter);
        }
        public List<Post> GetList(Expression<Func<Post, bool>> filter = null)
        {
            return filter == null
                ? _postDal.GetList()
                : _postDal.GetList(filter);
        }
        public bool Update(Post post)
        {
            return _postDal.Update(post);
        }
    }
}
