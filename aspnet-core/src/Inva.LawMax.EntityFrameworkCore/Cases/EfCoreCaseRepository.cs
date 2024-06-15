using Inva.LawMax.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Inva.LawMax.Cases
{
    public class EfCoreCaseRepository : EfCoreRepository<LawMaxDbContext, Case, Guid>, ICasRepository
    {
        public EfCoreCaseRepository(IDbContextProvider<LawMaxDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<int> CountAsync()
        {
            return await DbContext.Set<Case>().CountAsync();
        }
        public async Task<List<Case>> GetPagedListAsync(int skipCount, int maxResultCount,string sorting)
        {
            try
            {
                return await DbContext.Set<Case>()
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

        public async Task<List<Case>> GetListAsync(
            string filterText = null,
            int? number = null,
            int? year = null,
            string litigationDegree = null,
            string finalVerdict = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
                {
            var query = ApplyFilter(DbSet, filterText, number, year, litigationDegree, finalVerdict);

            //if (!string.IsNullOrWhiteSpace(sorting))
            //{
            //    query = query.OrderBy();
            //}
            //else
            //{
                query = query.OrderBy(c => c.Number); // Default sorting by Number, change as needed
            //}

            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        private IQueryable<Case> ApplyFilter(
        IQueryable<Case> query,
        string filterText,
        int? number,
        int? year,
        string litigationDegree,
        string finalVerdict)
        {
            if (!string.IsNullOrWhiteSpace(filterText))
            {
                query = query.Where(c =>
                    
                    c.LitigationDegree.Contains(filterText) ||
                    c.FinalVerdict.Contains(filterText));
            }

            if (number.HasValue)
            {
                query = query.Where(c => c.Number==number);
            }

            if (year.HasValue)
            {
                query = query.Where(c => c.Year.ToString() == year.Value.ToString());
            }

            if (!string.IsNullOrWhiteSpace(litigationDegree))
            {
                query = query.Where(c => c.LitigationDegree.Contains(litigationDegree));
            }

            if (!string.IsNullOrWhiteSpace(finalVerdict))
            {
                query = query.Where(c => c.FinalVerdict.Contains(finalVerdict));
            }

            return query;
        }

    }
}
