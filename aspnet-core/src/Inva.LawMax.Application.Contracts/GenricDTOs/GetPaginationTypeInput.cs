using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Inva.LawMax.GenricDTOs
{
    public class GetPaginationTypeInput : PagedAndSortedResultRequestDto
    {
        public int SkipCount { get; set; }
        public int MaxResultCount { get; set; }


        public GetPaginationTypeInput()
        {

        }
    }
}
