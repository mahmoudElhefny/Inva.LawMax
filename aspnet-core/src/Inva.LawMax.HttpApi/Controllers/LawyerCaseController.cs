using Inva.LawMax.GenricDTOs;
using Inva.LawMax.LawyerCases;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Inva.LawMax.Controllers
{
    [Route("api/LawyerCaseController/LawyerCase")]
    [ApiController]
    public class LawyerCaseController : AbpController
    {
        private readonly ILawyerCaseAppService _lawyerCaseAppService;

        public LawyerCaseController(ILawyerCaseAppService lawyerCaseAppService)
        {
            _lawyerCaseAppService = lawyerCaseAppService;
        }

        [HttpGet]
        public async Task<Response<PagedResultDto<LawyerCaseDTO>>> GetListAsync([FromQuery] GetPaginationTypeInput input)
        {
            return await _lawyerCaseAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Response<LawyerCaseDTO>> GetAsync(Guid id)
        {
            return await _lawyerCaseAppService.GetAsync(id);
        }

        [HttpPost("Create")]
        public async Task<Response<LawyerCaseDTO>> CreateAsync(CreateUpdateLawyerCase input)
        {
            return await _lawyerCaseAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<CreateUpdateLawyerCase> UpdateAsync(int id, CreateUpdateLawyerCase input)
        {
            return await _lawyerCaseAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<Response<bool>> DeleteAsync(Guid id)
        {
            return await _lawyerCaseAppService.DeleteAsync(id);
        }
    }
}
