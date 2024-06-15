using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Inva.LawMax.GenricDTOs;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Threading;

namespace Inva.LawMax.Cases
{
    [Route("api/Categry/Categry")]
    [ApiController]
    public class CaseController : AbpController
    {
        private readonly ICaseAppService _caseAppService;

        public CaseController(ICaseAppService caseAppService)
        {
            _caseAppService = caseAppService;
        }

        [HttpGet]
        public async Task<Response<PagedResultDto<CaseDto>>> GetListAsync([FromQuery]GetPaginationTypeInput input)
        {
            return await _caseAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Response<CaseDto>> GetAsync(Guid id)
        {
            return await _caseAppService.GetAsync(id);
        }

        [HttpPost]
        public async Task<Response<CaseDto>> CreateAsync(CreateUpdateCaseDto input)
        {
            return await _caseAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<CreateUpdateCaseDto> UpdateAsync(int id, CreateUpdateCaseDto input)
        {
            return await _caseAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<Response<bool>> DeleteAsync(Guid id)
        {
           return await _caseAppService.DeleteAsync(id);
        }
        [HttpGet("GetAllCases")]
        public async Task<Response<List<CaseDto>>> GetListAsync()
        {
            return await _caseAppService.GetListAsync();
        }

    }
}
