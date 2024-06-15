using AutoMapper;
using Inva.LawMax.Lawyers;
using Inva.LawMax.GenricDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Inva.LawMax.LawyerCases;

namespace Inva.LawMax.Lawyers
{
    public class LawyerAppService : ApplicationService, IlawyerAppService
    {
        private readonly IlawyerRepository _ILawyerRepository;
        private readonly IMapper _mapper;
        public LawyerAppService(IlawyerRepository lawyerRepository, IMapper mapper)
        {
            _ILawyerRepository = lawyerRepository;
            _mapper = mapper;
        }

        public async Task<Response<LawyerDTO>> CreateAsync(CreateUpdateLawyerDTO input)
        {
            Response<LawyerDTO> response = new();
            try
            {
                var newLawyer = _mapper.Map<CreateUpdateLawyerDTO, Lawyer>(input);
                newLawyer.LawyerCases = input.CasesIds.Select(caseId => new LawyerCase { CaseId = caseId }).ToList();
                newLawyer = await _ILawyerRepository.InsertAsync(newLawyer, autoSave: true);
                return response.CreateSuccess(_mapper.Map<Lawyer, LawyerDTO>(newLawyer));
            }
            catch (Exception ex) { return response.CreateFailure(new() { new() { ErrorMessage = ex.Message } }); };
        }
        public async Task<Response<bool>> DeleteAsync(Guid id)
        {
            Response<bool> response = new();
            try
            {
                var LawyerDBContext = await _ILawyerRepository.GetAsync(id);
                LawyerDBContext.IsDeleted = true;
                await _ILawyerRepository.UpdateAsync(LawyerDBContext);
                return response.CreateSuccess(true);
            }
            catch (Exception ex) { return response.CreateFailure(new() { new() { ErrorMessage = ex.Message } }); };
        }

        public async Task<Response<LawyerDTO>> GetAsync(Guid id)
        {
            Response<LawyerDTO> response = new();
            try
            {
                return response.CreateSuccess(_mapper.Map<Lawyer, LawyerDTO>(await _ILawyerRepository.GetAsync(id)));
            }
            catch (Exception ex) { return response.CreateFailure(new() { new() { ErrorMessage = ex.Message } }); };
        }

        public async Task<Response<PagedResultDto<LawyerDTO>>> GetListAsync(GetPaginationTypeInput input)
        {
            Response<PagedResultDto<LawyerDTO>> responseVm = new();
            try
            {
                var totalCount = await _ILawyerRepository.CountAsync();
                var Lawyers = await _ILawyerRepository.GetPagedListAsync(
                skipCount: input.SkipCount, sorting: "",
                maxResultCount: input.MaxResultCount
                );

                var LawyerDTOs = _mapper.Map<List<Lawyer>, List<LawyerDTO>>(Lawyers);
                return responseVm.CreateSuccess(new PagedResultDto<LawyerDTO>(totalCount, LawyerDTOs));
            }
            catch (Exception ex) { return responseVm.CreateFailure(new() { new() { ErrorMessage = ex.Message } }); };

        }

        public async Task<Response<List<LawyerDTO>>> GetListAsync()
        {
            Response<List<LawyerDTO>> responseVm = new();
            try
            {
                var totalCount = await _ILawyerRepository.CountAsync();
                var Lawyers = await _ILawyerRepository.GetListAsync();

                var LawyerDTOs = _mapper.Map<List<Lawyer>, List<LawyerDTO>>(Lawyers);
                return responseVm.CreateSuccess(LawyerDTOs);
            }
            catch (Exception ex) { return responseVm.CreateFailure(new() { new() { ErrorMessage = ex.Message } }); };
        }

        public Task<CreateUpdateLawyerDTO> UpdateAsync(int id, CreateUpdateLawyerDTO input)
        {
            throw new NotImplementedException();
        }

    }
}
