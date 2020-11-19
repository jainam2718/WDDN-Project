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
    public class ExamRepository:IExamRepository
    {
        private readonly AuthDbContext _context;

        public ExamRepository(AuthDbContext context)
        {
            this._context = context;
        }

        public async Task<Exam> GetExamById(int id)
        {
            return await _context.Exams
                .Include(e => e.AppUser)
                .Include(e => e.Group)
                .Include(e => e.Questions)
                .FirstOrDefaultAsync(m => m.id == id);
            
        }

        public async Task<IEnumerable<Exam>> GetExamsByAppUserId(String AppUserId)
        {
            return await _context.Exams.Include(e => e.AppUser)
                                              .Include(e => e.Group)
                                              .Include(e => e.Questions)
                                              .Where(e => e.AppUserId == AppUserId)
                                              .ToListAsync();

        }

        public void Save()
        {
        }

        public async Task<int> CreateExam(Exam exam)
        {
            _context.Add(exam);
            return await _context.SaveChangesAsync();
        }
        public async Task<bool> UpdateExam(Exam exam)
        {
            try
            {
                _context.Update(exam);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamExists(exam.id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<int> DeleteExam(int id)
        {
            var exam = await _context.Exams.FindAsync(id);
            _context.Exams.Remove(exam);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Exam>> GetExamsByGroupId(int id)
        {
            return await _context.Exams.Include(e => e.AppUser)
                                              .Include(e => e.Group)
                                              .Include(e => e.Questions)
                                              .Where(e => e.GroupId == id)
                                              .ToListAsync();
        } 
        public bool ExamExists(int id)
        {
            return _context.Exams.Any(e => e.id == id);
        }

        
    }
}
