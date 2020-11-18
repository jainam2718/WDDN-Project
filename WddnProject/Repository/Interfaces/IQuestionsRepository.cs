using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WDDNProject.Models;

namespace WDDNProject.Repository.Interfaces
{
    public interface IQuestionsRepository
    {
        Task<Questions> GetQuestionsById(int id);
        Task<IEnumerable<Questions>>  GetQuestionsByExamId(int examId);
        void Save();
        Task<int> CreateQuestion(Questions question);
        Task<bool> UpdateQuestion(Questions question);
        Task<int> DeleteQuestion(int id);
    }
}
