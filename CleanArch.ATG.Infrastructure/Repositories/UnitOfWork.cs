using CleanArch.ATG.Application.Interfaces;
using CleanArch.ATG.Infrastructure.Contexts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.ATG.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ATGDbContext _context;
        private Hashtable _repositories;

        private IProductRepository _ProductRepository;

        public UnitOfWork( ATGDbContext context )
        {
            _context = context;
        }
        public IProductRepository ProductRepository
        {
            get
            {

                if (_ProductRepository == null)
                {
                    _ProductRepository = new ProductRepository(_context);
                }
                return _ProductRepository;
            }
        }

        public IRepository<TEntity> GenericRepository<TEntity>() where TEntity : class
        {
            if (_repositories is null)
                _repositories = new Hashtable();

            var key = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(key))
            {
                var repo = new BaseRepository<TEntity>(_context);
                _repositories.Add(key , repo);
            }
            return _repositories [key] as IRepository<TEntity>;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
    }
}
