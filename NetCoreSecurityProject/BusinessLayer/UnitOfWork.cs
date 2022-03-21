﻿using DataAccessLayer;
using EntityLayer;
using System;
using static DataAccesLayer.IRepository;

namespace BusinessLayer
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        private readonly ApiDbContext _context;

        public UnitOfWork(ApiDbContext context)
        {
            _context = context;
            Repository = new Repository<T>(_context);
            RepositoryUser = new RepositoryUser<T>(_context);

        }
        public IRepository<T> Repository { get; private set; }
        public IRepositoryUser<T> RepositoryUser { get; private set; }

        public int Complete()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return 15;
            }
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}