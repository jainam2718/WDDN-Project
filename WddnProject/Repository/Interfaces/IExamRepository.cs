using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WDDNProject.Models;

namespace WDDNProject.Repository.Interfaces
{
    public interface IExamRepository
    {
        Task<Exam> GetExamById(int id);
        void Save();
        Task<IEnumerable<Exam>> GetExamsByAppUserId(String AppUserId);
        Task<int> CreateExam(Exam exam);
        Task<bool> UpdateExam(Exam exam);
        Task<int> DeleteExam(int id);
        Task<IEnumerable<Exam>> GetExamsByGroupId(int id);
    }
}
