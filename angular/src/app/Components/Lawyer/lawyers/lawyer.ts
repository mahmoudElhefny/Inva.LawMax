import { Case } from "../../Cases/case";

export interface Lawyer {
    id: string;
    name: string;
    position:string
    mobile:string;
    address:string
    cases: Case[];
  }
      
  export interface LawyerCreate {
       id:string;
       name:string; 
       Position:string;
       Mobile:string;
       Address:string 
  }
  