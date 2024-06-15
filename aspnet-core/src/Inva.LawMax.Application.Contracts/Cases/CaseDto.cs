using System;

namespace Inva.LawMax.Cases
{
    public class CaseDto
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public DateTime YearDate { get; set; }
        public string LitigationDegree { get; set; }
        public string FinalVerdict { get; set; }
    }
}
