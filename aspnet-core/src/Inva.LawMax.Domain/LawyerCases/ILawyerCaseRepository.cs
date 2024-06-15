using Inva.LawMax.Lawyers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Inva.LawMax.LawyerCases
{
    public interface ILawyerCaseRepository:IRepository<LawyerCase,Guid>
    {
        Task<int> CountAsync();
        Task<List<LawyerCase>> GetPagedListAsync(int skipCount, int maxResultCount);
    }
}
