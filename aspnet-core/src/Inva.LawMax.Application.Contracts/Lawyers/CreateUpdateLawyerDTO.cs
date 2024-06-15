using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inva.LawMax.Lawyers
{
    public class CreateUpdateLawyerDTO
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public List<Guid> CasesIds { get; set; }

    }
}
