using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WDDNProject.Data;
using WDDNProject.Models;
using WDDNProject.Repository.Interfaces;

namespace WDDNProject.Repository.Implementations
{
    public class ResultRepository : IResultRepository
    {
        private readonly AuthDbContext _context;

        public ResultRepository(AuthDbContext context)
        {
            _context = context;
        }

        public async Task<Result> GetResultById(int id)
        {
            return await this._context.Results.Include(r => r.AppUser)
                                              .Include(r => r.Exam)
                                              .FirstOrDefaultAsync(r => r.id == id);
        }
        public async Task<IEnumerable<Result>> GetResultsByAppUserId(String AppUserId)
        {
            return await this._context.Results.Include(r => r.AppUser)
                                              .Include(r => r.Exam)
                                              .Where(r => r.AppUserId == AppUserId)
                                              .ToListAsync();
        }
        public async Task<int> CreateResult(Result result)
        {
            this._context.Add(result);
            return await this._context.SaveChangesAsync();
        }
        public async Task<bool> UpdateResult(Result result)
        {
            try
            {
                _context.Update(result);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResultExists(result.id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }
        public async Task<int> DeleteResult(int id)
        {
            var result = await this._context.Results.FindAsync(id);
            this._context.Remove(result);
            return await this._context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Result>> GetResultsByExamId(int id)
        {
            return await this._context.Results.Include(r => r.AppUser)
                                              .Include(r => r.Exam)
                                              .Where(r => r.ExamId == id)
                                              .ToListAsync();
        }

        public bool ResultExists(int id)
        {
            return _context.Results.Any(r => r.id == id);
        }
    }
}