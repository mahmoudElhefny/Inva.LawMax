using Inva.LawMax.GenricDTOs;
using Inva.LawMax.Lawyers;
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
    [Route("api/Lawyer")]
    [ApiController]
    public class LawyerController : AbpController
    {
        private readonly IlawyerAppService _ilawyerAppService;

        public LawyerController(IlawyerAppService ilawyerAppService)
        {
            _ilawyerAppService = ilawyerAppService;
        }

        [HttpGet]
        public async Task<Response<PagedResultDto<LawyerDTO>>> GetListAsync([FromQuery] GetPaginationTypeInput input)
        {
            return await _ilawyerAppService.GetListAsync(input);
        }
        [HttpGet("GetAllLawyers")]
        public async Task<Response<List<LawyerDTO>>> GetListAsync()
        {
            return await _ilawyerAppService.GetListAsync();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Response<LawyerDTO>> GetAsync(Guid id)
        {
            return await _ilawyerAppService.GetAsync(id);
        }

        [HttpPost("Create")]
        public async Task<Response<LawyerDTO>> CreateAsync(CreateUpdateLawyerDTO input)
        {
            return await _ilawyerAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<CreateUpdateLawyerDTO> UpdateAsync(int id, CreateUpdateLawyerDTO input)
        {
            return await _ilawyerAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<Response<bool>> DeleteAsync(Guid id)
        {
            return await _ilawyerAppService.DeleteAsync(id);
        }
    }
}
