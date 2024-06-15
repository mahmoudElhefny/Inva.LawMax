import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Hearing, HearingCreate } from '../hearings/hearing';
import { MatSnackBar } from '@angular/material/snack-bar';
import { GetPaginationTypeInput } from '../../Cases/case-list/case-list/GetCaseTypeInput';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CaseService } from '../../Cases/case.service';
import { Case } from '../../Cases/case';

@Component({
  selector: 'app-hearing-create',
  standalone: false,
  templateUrl: './hearing-create.component.html',
  styleUrl: './hearing-create.component.scss'
})
export class HearingCreateComponent implements OnInit{
  @Input() hearingData: Hearing | null = null;
  @Output() formSubmit: EventEmitter<HearingCreate> = new EventEmitter<HearingCreate>();
  @Output() formClose: EventEmitter<void> = new EventEmitter<void>();
  cases:Case[]=[]
  hearingForm: FormGroup;

  constructor(private fb: FormBuilder,private caseService:CaseService) {
    this.hearingForm = this.fb.group({
      decision: ['', Validators.required],
      caseId: ['', Validators.required],
      date: ['', Validators.required],
    });
  }
  ngOnInit(): void {
      this.loadCases()
}

  ngOnChanges(): void {
    if (this.hearingData) {
      this.hearingForm.patchValue(this.hearingData);
    } else {
      this.hearingForm.reset();
    }
  }

  onSubmit(): void {
    if (this.hearingForm.invalid) {
      return;
    }
    const heaingData =this.hearingForm.value; 
    const formvalue={
      ...heaingData,
    }
    this.formSubmit.emit(formvalue);
  }

  onClose(): void {
    this.formClose.emit();
  }
  loadCases(){
    this.caseService.getAllCases().subscribe((response) => {
      this.cases = response.data;
    },
    (error) => {
      console.error('Error loading cases:', error);
    });
  }
}



