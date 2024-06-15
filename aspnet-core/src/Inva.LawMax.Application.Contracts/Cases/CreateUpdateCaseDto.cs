using System;using System.Collections.Generic;using System.ComponentModel.DataAnnotations;namespace Inva.LawMax.Cases{    public class CreateUpdateCaseDto    {        [Required]        public int Number { get; set; }        [Required]        public DateTime Year { get; set; }        [Required]        public string LitigationDegree { get; set; }        [Required]        public string FinalVerdict { get; set; }
        [Required]
        public List<Guid> LawyerIds { get; set; }


    }}