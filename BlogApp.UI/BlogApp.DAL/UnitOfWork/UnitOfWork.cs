using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApp.DAL.Abstract;
using BlogApp.DAL.Concrete;
using BlogApp.DAL.Entity;

namespace BlogApp.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BlogAppEntities _context;
        private ICategoryRepository categoryRepository;
        private ICommentRepository commentRepository;
        private IPostRepository postRepository;
        private IRoleRepository roleRepository;
        private IUserRepository userRepository;

        public UnitOfWork(BlogAppEntities context)
        {
            _context = context;
        }

        public ICategoryRepository CategoryRepository => categoryRepository ?? (categoryRepository = new CategoryRepository(_context));
        public ICommentRepository CommentRepository => commentRepository ?? (commentRepository = new CommentRepository(_context));
        public IRoleRepository RoleRepository => roleRepository ?? (roleRepository = new RoleRepository(_context));
        public IUserRepository UserRepository => userRepository ?? (userRepository = new UserRepository(_context));
        public IPostRepository PostRepository => postRepository ?? (postRepository = new PostRepository(_context));

        public void Dispose()
        {
            if(_context != null)
            {
                _context.Dispose();
                GC.SuppressFinalize(this);
            }
        }
        public bool SaveChanges()
        {
            return (_context.SaveChanges() == 1) ? true : false;
        }
    }
}
