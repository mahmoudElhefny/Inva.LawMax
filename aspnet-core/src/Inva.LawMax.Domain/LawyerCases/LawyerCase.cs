using System;
using System.ComponentModel.DataAnnotations.Schema;
using Inva.LawMax.Cases;
using Inva.LawMax.Lawyers;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace Inva.LawMax.LawyerCases
{
    public class LawyerCase : FullAuditedAggregateRoot<Guid>
    {
        [ForeignKey(nameof(LawyerId))]
        public Guid LawyerId { get; set; }
        public Lawyer Lawyer { get; set; }

        [ForeignKey(nameof(CaseId))]
        public Guid CaseId { get; set; }
        public Case Case { get; set; }

        public override object[] GetKeys()
        {
            return new object[] { LawyerId, CaseId };
        }
    }
}
