using Inva.LawMax.GenricDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Inva.LawMax.Lawyers
{
    public interface IlawyerAppService : IApplicationService
    {
        Task<Response<PagedResultDto<LawyerDTO>>> GetListAsync(GetPaginationTypeInput input);
        Task<Response<LawyerDTO>> GetAsync(Guid id);
        Task<Response<bool>> DeleteAsync(Guid id);
        Task<Response<LawyerDTO>> CreateAsync(CreateUpdateLawyerDTO input);
        
        Task<CreateUpdateLawyerDTO> UpdateAsync(int id, CreateUpdateLawyerDTO input);
        Task<Response<List<LawyerDTO>>> GetListAsync();
    }
}
