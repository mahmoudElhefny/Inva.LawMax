using System;
using System.ComponentModel.DataAnnotations.Schema;
using Inva.LawMax.Cases;
using Volo.Abp.Domain.Entities.Auditing;

namespace Inva.LawMax.Hearings
{
    public class Hearing : FullAuditedAggregateRoot<Guid>
    {
        public DateTime Date { get; set; }
        public string Decision { get; set; }

       // [ForeignKey(nameof(CaseId))]
        public Guid CaseId { get; set; }
        public Case Case { get; set; }
    }
}
