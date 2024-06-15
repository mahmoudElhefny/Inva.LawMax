using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inva.LawMax.LawyerCases
{
    public class CreateUpdateLawyerCase
    {
        public Guid LawyerId { get; set; }
        public Guid CaseId { get; set; }
        
    }
}
