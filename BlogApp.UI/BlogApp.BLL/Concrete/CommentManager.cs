using BlogApp.BLL.Abstract;
using BlogApp.DAL.Abstract;
using BlogApp.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BlogApp.BLL.Concrete
{
    public class CommentManager : ICommentService
    {
        private ICommentDal _commentDal;

        public CommentManager(ICommentDal commentDal)
        {
            _commentDal = commentDal;
        }

        public bool Add(Comment comment)
        {
            return _commentDal.Add(comment);
        }
        public bool Delete(int id)
        {
            return _commentDal.Delete(id);
        }
        public Comment Get(Expression<Func<Comment, bool>> filter)
        {
            return _commentDal.Get(filter);
        }
        public List<Comment> GetList(Expression<Func<Comment, bool>> filter = null)
        {
            return filter == null
                ? _commentDal.GetList()
                : _commentDal.GetList(filter);
        }
        public bool Update(Comment comment)
        {
            return _commentDal.Update(comment);
        }
    }
}
