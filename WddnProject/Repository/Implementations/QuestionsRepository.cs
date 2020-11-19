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
    public class QuestionsRepository : IQuestionsRepository
    {
        private readonly AuthDbContext _context;

        public QuestionsRepository(AuthDbContext context)
        {
            this._context = context;
        }

        public async Task<Questions> GetQuestionsById(int id)
        {
            return await _context.Questions
                .Include(e => e.Exam)
                .FirstOrDefaultAsync(m => m.id == id);
        }

        public async Task<IEnumerable<Questions>> GetQuestionsByExamId(int examId)
        {
            return await _context.Questions.Include(q => q.Exam)
                                            .Where(q => q.ExamId == examId)
                                            .ToListAsync();
        }

        public  void Save()
        {
        }

        public async Task<int> CreateQuestion(Questions questions)
        {
            _context.Add(questions);
            return await _context.SaveChangesAsync();
        }
        public async Task<bool> UpdateQuestion(Questions questions)
        {
            try
            {
                _context.Update(questions);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionExists(questions.id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<int> DeleteQuestion(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            _context.Questions.Remove(question);
            return await _context.SaveChangesAsync();

        }

        public bool QuestionExists(int id)
        {
            return _context.Questions.Any(e => e.id == id);
        }

    }
}
