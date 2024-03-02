﻿using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystem.Infrastructure.Data.Common
{
    public class Repository : IRepository
    {
        private readonly HouseRentingDbContext _context;

        public Repository(HouseRentingDbContext context)
        {
            _context = context;
        }

        private DbSet<T> DbSet<T>() where T : class
        {
            return _context.Set<T>();
        }

        public IQueryable<T> All<T>() where T : class
        {
            return DbSet<T>();
        }

        public IQueryable<T> AllReadonly<T>() where T : class
        {
            return DbSet<T>()
                .AsNoTracking();
        }
    }
}
