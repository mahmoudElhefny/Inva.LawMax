using AutoMapper;
using Inva.LawMax.GenricDTOs;
using Inva.LawMax.LawyerCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Inva.LawMax.lawyerCases
{

    public class LawyerCaseAppService : ApplicationService, ILawyerCaseAppService
    {
        private readonly ILawyerCaseRepository _lawyerCaseRepository;
        private readonly IMapper _mapper;
        public LawyerCaseAppService(ILawyerCaseRepository lawyerCaseRepository, IMapper mapper)
        {
            _lawyerCaseRepository = lawyerCaseRepository;
            _mapper = mapper;
        }
        //[Authorize(LawMaxPermissions.LookupTypes.Create)]
        public async Task<Response<LawyerCaseDTO>> CreateAsync(CreateUpdateLawyerCase input)
        {
            Response<LawyerCaseDTO> response = new();
            try
            {
                var newLawyercase = _mapper.Map<CreateUpdateLawyerCase, LawyerCase>(input);
                newLawyercase = await _lawyerCaseRepository.InsertAsync(newLawyercase, autoSave: true);
                return response.CreateSuccess(_mapper.Map<LawyerCase, LawyerCaseDTO>(newLawyercase));
            }
            catch (Exception ex) { return response.CreateFailure(new() { new() { ErrorMessage = ex.Message } }); };
        }
        public async Task<Response<bool>> DeleteAsync(Guid id)
        {
            Response<bool> response = new();
            try
            {
                var caseDBContext = await _lawyerCaseRepository.GetAsync(id);
                caseDBContext.IsDeleted = true;
                await _lawyerCaseRepository.UpdateAsync(caseDBContext);
                return response.CreateSuccess(true);
            }
            catch (Exception ex) { return response.CreateFailure(new() { new() { ErrorMessage = ex.Message } }); };
        }

        public async Task<Response<LawyerCaseDTO>> GetAsync(Guid id)
        {
            Response<LawyerCaseDTO> response = new();
            try
            {
                return response.CreateSuccess(_mapper.Map<LawyerCase, LawyerCaseDTO>(await _lawyerCaseRepository.GetAsync(id)));
            }
            catch (Exception ex) { return response.CreateFailure(new() { new() { ErrorMessage = ex.Message } }); };
        }

        public async Task<Response<PagedResultDto<LawyerCaseDTO>>> GetListAsync(GetPaginationTypeInput input)
        {
            Response<PagedResultDto<LawyerCaseDTO>> responseVm = new();
            try
            {
                var totalCount = await _lawyerCaseRepository.CountAsync();
                var cases = await _lawyerCaseRepository.GetPagedListAsync(
                skipCount: input.SkipCount, sorting: "",
                maxResultCount: input.MaxResultCount
                );

                var LawyerCaseDTOs = _mapper.Map<List<LawyerCase>, List<LawyerCaseDTO>>(cases);
                return responseVm.CreateSuccess(new PagedResultDto<LawyerCaseDTO>(totalCount, LawyerCaseDTOs));
            }
            catch (Exception ex) { return responseVm.CreateFailure(new() { new() { ErrorMessage = ex.Message } }); };

        }

        public Task<CreateUpdateLawyerCase> UpdateAsync(int id, CreateUpdateLawyerCase input)
        {
            throw new NotImplementedException();
        }
    }
}
