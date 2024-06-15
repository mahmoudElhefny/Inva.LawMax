using AutoMapper;
using Inva.LawMax.GenricDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Inva.LawMax.Hearings
{
    public class HearingAppService : ApplicationService, IHearingService
    {
        private readonly IHearingRepository _hearingRepository;
        private readonly IMapper _mapper;
        public HearingAppService(IHearingRepository hearingRepository, IMapper mapper)
        {
            _hearingRepository = hearingRepository;
            _mapper = mapper;
        }
        //[Authorize(LawMaxPermissions.LookupTypes.Create)]
        public async Task<Response<HearingDTO>> CreateAsync(CreateUpdateHearingDTO input)
        {
            Response<HearingDTO> response = new();
            try
            {
                var newHearing = _mapper.Map<CreateUpdateHearingDTO, Hearing>(input);
                newHearing = await _hearingRepository.InsertAsync(newHearing, autoSave: true);
                return response.CreateSuccess(_mapper.Map<Hearing, HearingDTO>(newHearing));
            }
            catch (Exception ex) { return response.CreateFailure(new() { new() { ErrorMessage = ex.Message } }); };
        }
        public async Task<Response<bool>> DeleteAsync(Guid id)
        {
            Response<bool> response = new();
            try
            {
                var HearingDBContext = await _hearingRepository.GetAsync(id);
                HearingDBContext.IsDeleted = true;
                await _hearingRepository.UpdateAsync(HearingDBContext);
                return response.CreateSuccess(true);
            }
            catch (Exception ex) { return response.CreateFailure(new() { new() { ErrorMessage = ex.Message } }); };
        }

        public async Task<Response<HearingDTO>> GetAsync(Guid id)
        {
            Response<HearingDTO> response = new();
            try
            {
                return response.CreateSuccess(_mapper.Map<Hearing, HearingDTO>(await _hearingRepository.GetAsync(id)));
            }
            catch (Exception ex) { return response.CreateFailure(new() { new() { ErrorMessage = ex.Message } }); };
        }

        public async Task<Response<PagedResultDto<HearingDTO>>> GetListAsync(GetPaginationTypeInput input)
        {
            Response<PagedResultDto<HearingDTO>> responseVm = new();
            try
            {
                var totalCount = await _hearingRepository.CountAsync();
                var Hearings = await _hearingRepository.GetPagedListAsync(
                skipCount: input.SkipCount, sorting: "",
                maxResultCount: input.MaxResultCount
                );
                var HearingDTOs = _mapper.Map<List<Hearing>, List<HearingDTO>>(Hearings);
                return responseVm.CreateSuccess(new PagedResultDto<HearingDTO>(totalCount, HearingDTOs));
            }
            catch (Exception ex) { return responseVm.CreateFailure(new() { new() { ErrorMessage = ex.Message } }); };

        }

        public async Task<Response<HearingDTO>> UpdateAsync(CreateUpdateHearingDTO input)
        {
            Response<HearingDTO> response = new();
            try
            {
                var newHearing = _mapper.Map<CreateUpdateHearingDTO, Hearing>(input);
                newHearing = await _hearingRepository.UpdateAsync(newHearing, autoSave: true);
                return response.CreateSuccess(_mapper.Map<Hearing, HearingDTO>(newHearing));
            }
            catch (Exception ex) { return response.CreateFailure(new() { new() { ErrorMessage = ex.Message } }); };
        }
    }
}
