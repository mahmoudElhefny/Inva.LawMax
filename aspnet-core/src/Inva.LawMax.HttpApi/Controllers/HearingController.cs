using Inva.LawMax.GenricDTOs;
using Inva.LawMax.Hearings;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Inva.LawMax.Controllers
{
    [Route("api/HearingController/Hearing")]
    [ApiController]
    public class HearingController : AbpController
    {
        private readonly IHearingService _hearingService;

        public HearingController(IHearingService hearingService)
        {
            _hearingService = hearingService;
        }

        [HttpGet]
        public async Task<Response<PagedResultDto<HearingDTO>>> GetListAsync([FromQuery] GetPaginationTypeInput input)
        {
            return await _hearingService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Response<HearingDTO>> GetAsync(Guid id)
        {
            return await _hearingService.GetAsync(id);
        }

        [HttpPost]
        public async Task<Response<HearingDTO>> CreateAsync(CreateUpdateHearingDTO input)
        {
            return await _hearingService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<Response<HearingDTO>> UpdateAsync(CreateUpdateHearingDTO input)
        {
            return await _hearingService.UpdateAsync(input);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<Response<bool>> DeleteAsync(Guid id)
        {
            return await _hearingService.DeleteAsync(id);
        }
    }
}
