import { Hearing } from "../Hearinges/hearings/hearing";
import { Lawyer } from "../Lawyer/lawyers/lawyer";

export interface Case {
    id: string;
    number: number;
    YearDate: Date;
    litigationDegree: string;
    finalVerdict: string;
    lawyers: Lawyer[];
    hearings: Hearing[];
  }

  export interface CaseCreate {
    id: string;
    number: number;
    YearDate: Date;
    litigationDegree: string;
    finalVerdict: string;
    LawyerIds:string[]
   CaseLawyers:CaseLawyer[]
  }
export interface CaseLawyer
{
    
}

  