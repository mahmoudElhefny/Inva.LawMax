
using Inva.LawMax.GenricDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Inva.LawMax.Cases
{
    public interface ICaseAppService: IApplicationService
    {
        Task<Response<PagedResultDto<CaseDto>>> GetListAsync(GetPaginationTypeInput input);
        Task<Response<CaseDto>> GetAsync(Guid id);
        Task <Response<bool>>DeleteAsync(Guid id);
        Task<Response<CaseDto>> CreateAsync(CreateUpdateCaseDto input);
        Task<Response<List<CaseDto>>> GetListAsync();
        Task<CreateUpdateCaseDto> UpdateAsync(int id, CreateUpdateCaseDto input);

       
    }
}
