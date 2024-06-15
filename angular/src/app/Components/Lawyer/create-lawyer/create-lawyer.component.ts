import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { Lawyer, LawyerCreate } from '../lawyers/lawyer';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CaseService } from '../../Cases/case.service';

@Component({
  selector: 'app-create-lawyer',
  standalone: false,
  templateUrl: './create-lawyer.component.html',
  styleUrl: './create-lawyer.component.scss'
})
export class CreateLawyerComponent implements OnChanges,OnInit{
  @Input() lawyerData: Lawyer | null = null;
  @Output() formSubmit: EventEmitter<LawyerCreate> = new EventEmitter<LawyerCreate>();
  @Output() formClose: EventEmitter<void> = new EventEmitter<void>();
  lawyerForm: FormGroup;
  Cases=[]
  constructor(private fb: FormBuilder,private caseService:CaseService) {
   
  }
  ngOnInit(): void {
    this.lawyerForm = this.fb.group({
      name: ['', Validators.required],
      specialization: ['', Validators.required],
      Position:['', Validators.required],
      Mobile:['', Validators.required],
      Address:['', Validators.required],
      casesIds:[[]]
    });
    this.loadCases()
  }

  ngOnChanges(): void {
    if (this.lawyerData) {
      this.lawyerForm.patchValue(this.lawyerData);
    } else {
      this.lawyerForm.reset();
    }
  }
  onSubmit(): void {
    if (this.lawyerForm.invalid) {
      return;
    }
    this.formSubmit.emit(this.lawyerForm.value);
  }
  onClose(): void {
    this.formClose.emit();
  }
  loadCases() {
    this.caseService.getAllCases().subscribe(data => {
      this.Cases = data.data;
      console.log(this.Cases)
    });
  }
}
