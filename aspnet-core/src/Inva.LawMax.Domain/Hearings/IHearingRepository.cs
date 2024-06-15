using Inva.LawMax.Lawyers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Inva.LawMax.Hearings
{
    public interface IHearingRepository : IRepository<Hearing, Guid>
    {
        Task<int> CountAsync();
        Task<List<Hearing>> GetPagedListAsync(int skipCount, int maxResultCount,string sorting);
       // Task<Hearing> UpdateAsync(Hearing entity, bool autoSave = false);
    }
}
