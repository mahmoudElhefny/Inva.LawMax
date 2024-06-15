
using AutoMapper;
using Inva.LawMax.GenricDTOs;
using Inva.LawMax.LawyerCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Inva.LawMax.Cases
{
    public class CaseAppService : ApplicationService, ICaseAppService
    {
        private readonly ICasRepository _casRepository;
        private readonly IMapper _mapper;
        public CaseAppService(ICasRepository casRepository, IMapper mapper)
        {
            _casRepository = casRepository;
            _mapper = mapper;
        }
        //[Authorize(LawMaxPermissions.LookupTypes.Create)]
        public async Task<Response<CaseDto>> CreateAsync(CreateUpdateCaseDto input)
        {
            Response<CaseDto> response = new();
            try
            { 
                var newcase = _mapper.Map<CreateUpdateCaseDto, Case>(input);
                newcase.LawyerCases = input.LawyerIds.Select(lawyerId => new LawyerCase { LawyerId = lawyerId }).ToList();
                newcase = await _casRepository.InsertAsync(newcase, autoSave: true);
                return response.CreateSuccess(_mapper.Map<Case, CaseDto>(newcase));
            }
            catch (Exception ex) { return response.CreateFailure(new() { new() { ErrorMessage = ex.Message } }); };
        }
        public async Task<Response<bool>> DeleteAsync(Guid id)
        {
            Response<bool> response = new();
            try
            {
                var caseDBContext = await _casRepository.GetAsync(id);
                caseDBContext.IsDeleted = true;
                await _casRepository.UpdateAsync(caseDBContext);
                return response.CreateSuccess(true);
            }
            catch (Exception ex) { return response.CreateFailure(new() { new() { ErrorMessage = ex.Message } }); };
        }

        public async Task<Response<CaseDto>> GetAsync(Guid id)
        {
            Response<CaseDto> response = new();
            try
            {
                return response.CreateSuccess(_mapper.Map<Case, CaseDto>(await _casRepository.GetAsync(id)));
            }
            catch (Exception ex) { return response.CreateFailure(new() { new() { ErrorMessage = ex.Message } }); };
        }

        public async Task<Response<PagedResultDto<CaseDto>>> GetListAsync(GenricDTOs.GetPaginationTypeInput input)
        {
            Response<PagedResultDto<CaseDto>> responseVm = new();
            try
            {
                var totalCount = await _casRepository.CountAsync();
                var cases = await _casRepository.GetPagedListAsync(
                skipCount: input.SkipCount, sorting: "",
                maxResultCount: input.MaxResultCount
                );

                var caseDtos = _mapper.Map<List<Case>, List<CaseDto>>(cases);
                return responseVm.CreateSuccess(new PagedResultDto<CaseDto>(totalCount, caseDtos));
            } catch (Exception ex) { return responseVm.CreateFailure(new() { new() { ErrorMessage = ex.Message } }); };
            
        }

        public async Task<Response<List<CaseDto>>> GetListAsync()
        {
            Response<List<CaseDto>> responseVm = new();
            try
            {
                var cases = await _casRepository.GetListAsync();

                var caseDtos = _mapper.Map<List<Case>, List<CaseDto>>(cases);
                return responseVm.CreateSuccess(caseDtos);
            }
            catch (Exception ex) { return responseVm.CreateFailure(new() { new() { ErrorMessage = ex.Message } }); };


        }

        public Task<CreateUpdateCaseDto> UpdateAsync(int id, CreateUpdateCaseDto input)
        {
            throw new NotImplementedException();
        }
    }
}
