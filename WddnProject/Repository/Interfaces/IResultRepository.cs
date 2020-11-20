using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WDDNProject.Models;

namespace WDDNProject.Repository.Interfaces
{
    public interface IResultRepository
    {
        Task<Result> GetResultById(int id);
        Task<IEnumerable<Result>> GetResultsByAppUserId(String AppUserId);
        Task<int> CreateResult(Result result);
        Task<bool> UpdateResult(Result result);
        Task<int> DeleteResult(int id);
        Task<IEnumerable<Result>> GetResultsByExamId(int id);
    }
}