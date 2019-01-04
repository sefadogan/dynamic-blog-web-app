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
    public class CommentManager : ICommentManager
    {
        private readonly ICommentRepository _repository;

        public CommentManager(ICommentRepository repository)
        {
            _repository = repository;
        }

        public bool Add(Comment data)
        {
            return _repository.Add(data);
        }

        public Comment BringById(int id)
        {
            return _repository.BringById(id);
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }

        public List<Comment> ListAll()
        {
            return _repository.ListAll();
        }

        public bool Update(Comment data)
        {
            return _repository.Update(data);
        }
    }
}
