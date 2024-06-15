using Inva.LawMax.Cases;
using Inva.LawMax.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Inva.LawMax.Hearings
{
    public class EfCoreHearingRepository : EfCoreRepository<LawMaxDbContext, Hearing, Guid>, IHearingRepository
    {
        public EfCoreHearingRepository(IDbContextProvider<LawMaxDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<int> CountAsync()
        {
            return await DbContext.Set<Hearing>().CountAsync();
        }
        public async Task<List<Hearing>> GetPagedListAsync(int skipCount, int maxResultCount, string sorting)
            {
            try
            {
                //var query = DbContext.Set<Hearing>().Include(h => h.Case);
                //var sql = query.ToQueryString();
               // Console.WriteLine(sql);
                return await DbContext.Set<Hearing>()
                                     .Include(c => c.Case)
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
