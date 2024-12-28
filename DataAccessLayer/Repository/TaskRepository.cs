using EntityLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class TaskRepository : IRepository<TaskItem>
    {

        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TaskItem entity)
        {
            await _context.TaskItems.AddAsync(entity);
            await _context.SaveChangesAsync();  
        }

        public async Task DeleteAsync(int id)
        {
            var task = await GetByIdAsync(id);
            if (task != null)
            {
                _context.TaskItems.Remove(task);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<TaskItem>> FindAsync(Expression<Func<TaskItem, bool>> predicate)
        {
            return await _context.TaskItems.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
           return await _context.TaskItems.ToListAsync();
        }

        public async Task<TaskItem> GetByIdAsync(int id)
        {
            return await _context.TaskItems.FindAsync(id);
        }

        public async Task UpdateAsync(TaskItem entity)
        {
             _context.TaskItems.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
