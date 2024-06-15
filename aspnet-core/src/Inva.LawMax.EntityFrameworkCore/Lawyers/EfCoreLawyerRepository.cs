using Inva.LawMax.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Inva.LawMax.Lawyers
{
    public class EfCoreLawyerRepository : EfCoreRepository<LawMaxDbContext, Lawyer, Guid>, IlawyerRepository
    {
        public EfCoreLawyerRepository(IDbContextProvider<LawMaxDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<int> CountAsync()
        {
            return await DbContext.Set<Lawyer>().CountAsync();
        }
        public async Task<List<Lawyer>> GetPagedListAsync(int skipCount, int maxResultCount, string sorting)
        {
            try
            {
                return await DbContext.Set<Lawyer>()
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
