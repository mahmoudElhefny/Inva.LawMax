using Inva.LawMax.GenricDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Inva.LawMax.LawyerCases
{
    public interface ILawyerCaseAppService : IApplicationService
    {
        Task<Response<PagedResultDto<LawyerCaseDTO>>> GetListAsync(GetPaginationTypeInput input);
        Task<Response<LawyerCaseDTO>> GetAsync(Guid id);
        Task<Response<bool>> DeleteAsync(Guid id);
        Task<Response<LawyerCaseDTO>> CreateAsync(CreateUpdateLawyerCase input);
        //Task<bool> IsExistName(int? id, string name);
        //Task<bool> IsExistNameAr(int? id, string nameAr);
        //Task<bool> IsExistCode(int? id, string code);


        Task<CreateUpdateLawyerCase> UpdateAsync(int id, CreateUpdateLawyerCase input);

        //Task<List<LookupTypeDto>> GetListDDLAsync();
    }
    
}
