using Inva.LawMax.EntityFrameworkCore;
using Inva.LawMax.Lawyers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Inva.LawMax.LawyerCases
{
    public class EfCoreLawyerCaseRepository : EfCoreRepository<LawMaxDbContext, LawyerCase, Guid>, ILawyerCaseRepository
    {
        public EfCoreLawyerCaseRepository(IDbContextProvider<LawMaxDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<int> CountAsync()
        {
            return await DbContext.Set<LawyerCase>().CountAsync();
        }
        public async Task<List<LawyerCase>> GetPagedListAsync(int skipCount, int maxResultCount)
        {
            try
            {
                return await DbContext.Set<LawyerCase>()
                                     .Skip(skipCount)
                                     .Take(maxResultCount)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately (e.g., logging)
                throw new Exception("Error occurred while fetching paged list of cases.", ex);
            }
        }
    }
}
