using System;
using System.Collections.Generic;
using Inva.LawMax.Hearings;
using Inva.LawMax.LawyerCases;
using Volo.Abp.Domain.Entities.Auditing;

namespace Inva.LawMax.Cases
{
    public class Case : FullAuditedAggregateRoot<Guid>
    {
        public int Number { get; set; }
        public DateTime Year { get; set; }
        public string LitigationDegree { get; set; }
        public string FinalVerdict { get; set; }

        public ICollection<LawyerCase> LawyerCases { get; set; }
        public ICollection<Hearing> Hearings { get; set; }

        public Case()
        {
            LawyerCases = new List<LawyerCase>();
            Hearings = new List<Hearing>();
        }
    }
}
