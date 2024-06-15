using Inva.LawMax.Cases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Inva.LawMax.Lawyers
{
    public interface IlawyerRepository : IRepository<Lawyer, Guid>
    {
        Task<int> CountAsync();
        Task<List<Lawyer>> GetPagedListAsync(int skipCount, int maxResultCount,string sorting);
    }
}
