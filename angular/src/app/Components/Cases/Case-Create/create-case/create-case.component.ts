import { Component, EventEmitter, Input, OnChanges, OnInit, Output } from '@angular/core';
import { Case, CaseCreate } from '../../case';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CaseService } from '../../case.service';
import { LawyerService } from 'src/app/Components/Lawyer/lawyers/lawyer.service';

//import { NgxMatDatetimePickerModule } from '@angular-material-components/datetime-picker';
@Component({
  selector: 'app-create-case',
  standalone: false,
  //imports: [],
  templateUrl: './create-case.component.html',
  styleUrl: './create-case.component.scss'
})
export class CreateCaseComponent implements OnChanges,OnInit{

  @Input() caseData: Case | null = null;
  @Output() formSubmit: EventEmitter<CaseCreate> = new EventEmitter<CaseCreate>();
  @Output() formClose: EventEmitter<void> = new EventEmitter<void>();

  caseForm: FormGroup;
  lawyers = [];

  constructor(private fb: FormBuilder,private lawyerService: LawyerService) {
   
  }
  ngOnInit(): void {
    this.caseForm = this.fb.group({
      number: ['', Validators.required],
      YearDate: ['', Validators.required],
      litigationDegree: ['', Validators.required],
      finalVerdict: ['', Validators.required],
      lawyerIds:[[]]
    });
    this.loadLawyers()
  }

  ngOnChanges(): void {
    if (this.caseData) {
      this.caseForm.patchValue(this.caseData);
    } else {
      this.caseForm.reset();
    }
  }

  onSubmit(): void {
    if (this.caseForm.invalid) {
      return;
    }
    const caseData =this.caseForm.value; 
    const formvalue={
      ...caseData,
    }
    this.formSubmit.emit(formvalue);
  }

  loadLawyers() {
    this.lawyerService.getAllLawyers().subscribe(data => {
      this.lawyers = data.data;
      console.log(this.lawyers)
    });
  }

  onClose(): void {
    this.formClose.emit();
  }
}
