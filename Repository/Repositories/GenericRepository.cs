using core.Models;
using core.Repositories;
using Microsoft.EntityFrameworkCore;
using Repository.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel

    {
        private readonly VezeetaProjectDbContext _context;


        public GenericRepository(VezeetaProjectDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync(int page, int pageSize, string search)
        {
            var entities = await _context.Set<T>()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return entities;
        }

        public async Task<IEnumerable<T>> GetAllAsync(int page, int pageSize)
        {
            var entities = await _context.Set<T>()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return entities;
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
       
        public async Task<T> AddAsync(T item)
        {
                await _context.Set<T>().AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<T> UpdateAsync(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return item;
        }


      
         public async Task<bool> RemoveAsync(T item)
         {
            _context.Set<T>().Remove(item);
            await _context.SaveChangesAsync();
            return true;
         }
      

        
    }
    
}
