using Inva.LawMax.GenricDTOs;
using Inva.LawMax.LawyerCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Inva.LawMax.Hearings
{
    public interface IHearingService : IApplicationService
    {
        Task<Response<PagedResultDto<HearingDTO>>> GetListAsync(GetPaginationTypeInput input);
        Task<Response<HearingDTO>> GetAsync(Guid id);
        Task<Response<bool>> DeleteAsync(Guid id);
        Task<Response<HearingDTO>> CreateAsync(CreateUpdateHearingDTO input);
        Task<Response<HearingDTO>> UpdateAsync(CreateUpdateHearingDTO input);
    }

}
