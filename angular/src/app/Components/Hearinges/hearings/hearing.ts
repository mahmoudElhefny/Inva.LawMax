import { Case } from "../../Cases/case";
export interface Hearing {
    id: string;
    date: Date;
    caseId: string;
    decision: string;
    case:Case;
  }

  export interface HearingCreate {
    id: string;
    date: Date;
    caseId: string;
    decision: string;
  }
  


  