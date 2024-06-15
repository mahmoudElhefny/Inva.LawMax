using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Inva.LawMax.Cases
{
    public interface ICasRepository: IRepository<Case, Guid>
    {
        Task<int> CountAsync();
        Task<List<Case>> GetPagedListAsync(int skipCount, int maxResultCount,string sorting);
    }
}
