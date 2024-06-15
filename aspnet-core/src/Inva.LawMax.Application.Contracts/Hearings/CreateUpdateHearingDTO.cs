using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inva.LawMax.Hearings
{
    public class CreateUpdateHearingDTO
    {
        public DateTime Date { get; set; }
        public string Decision { get; set; }
        public Guid CaseId { get; set; }
    }
}
