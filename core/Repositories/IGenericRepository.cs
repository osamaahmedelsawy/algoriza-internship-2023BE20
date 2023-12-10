using core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Repositories
{
    public interface IGenericRepository<T> where T : BaseModel
    {
        public Task<IEnumerable<T>>GetAllAsync(int page, int pageSize, string search);
        public Task<IEnumerable<T>> GetAllAsync(int page, int pageSize);
        public Task<T>GetByIdAsync (int id);
        public Task<T> AddAsync(T item); 
        public Task<T> UpdateAsync (T item);
        public Task<bool> RemoveAsync(T item);

    }
}
