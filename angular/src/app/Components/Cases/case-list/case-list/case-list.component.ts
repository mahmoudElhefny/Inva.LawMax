import { Component, OnInit } from '@angular/core';
import { CaseService } from '../../case.service';
import { Case, CaseCreate } from '../../case';
import {GetPaginationTypeInput } from './GetCaseTypeInput';
import { MatPaginator } from '@angular/material/paginator';
import { Toaster } from '@abp/ng.theme.shared';
import { MatSnackBar } from '@angular/material/snack-bar';
import Swal from 'sweetalert2';
@Component({
  selector: 'app-case-list',
  standalone: false,
  //imports: [CommonModule, MatTableModule, MatPaginatorModule],
  templateUrl: './case-list.component.html',
  styleUrl: './case-list.component.scss',
})
export class CaseListComponent implements OnInit {
  cases: Case[] = [];
  totalCount: number = 0;
  isPopupVisible: boolean = false;
  currentCase: Case | null = null;
  paginationInput: GetPaginationTypeInput = {
    skipCount: 0,
    maxResultCount: 3,
    sorting: 'number'
  };
  constructor(private caseService: CaseService,private snackBar: MatSnackBar) {

  }
  ngOnInit(): void {
    this.loadCases();
    // this.countCases();
  }

  loadCases(): void {
    this.caseService.getCases(this.paginationInput).subscribe((data) => { 
      this.cases = data.data.items;
      this.totalCount = data.data.totalCount;
    });
    
  }

  openPopup(caseData: Case | null = null): void {
    this.currentCase = caseData;
    this.isPopupVisible = true;
  }
  closePopup(): void {
    this.isPopupVisible = false;
    this.currentCase = null;
  }
  handleFormSubmit(caseData: CaseCreate): void {
    if (this.currentCase) {
      // Update case
      this.caseService.updateCase(caseData).subscribe((response) => {
        Swal.fire({
          title: "Updated succesfully!",
          text: "You clicked the button!",
          icon: "success"
        });
        this.loadCases();
        this.closePopup();
      },
    (error)=>{
      Swal.fire({
        icon: "error",
        title: "Oops...",
        text: "Something went wrong!",
      });
        this.loadCases();
        this.closePopup();
    }
   )
}
   else {
      // Create new case
      this.caseService.createCase(caseData).subscribe(() => {
        Swal.fire({
          title: "saved succesfully!",
          text: "You clicked the button!",
          icon: "success"
        });
        this.loadCases();
        this.closePopup();
      });
    }
  }
  onPageChange(event: any): void {
    this.paginationInput.skipCount = event.pageIndex * event.pageSize;
    this.paginationInput.maxResultCount = event.pageSize;
    this.loadCases();
  }
  onDelete(id:string){
    const swalWithBootstrapButtons = Swal.mixin({
      customClass: {
        confirmButton: "btn btn-success",
        cancelButton: "btn btn-danger"
      },
      buttonsStyling: false
    });
    swalWithBootstrapButtons.fire({
      title: "Are you sure?",
      text: "You won't be able to revert this!",
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Yes, delete it!",
      cancelButtonText: "No, cancel!",
      reverseButtons: true
    }).then((result) => {
      if (result.isConfirmed) {
        this.caseService.deleteCase(id).subscribe(() => {
          this.loadCases();   
          swalWithBootstrapButtons.fire({
            title: "Deleted!",
            text: "Your file has been deleted.",
            icon: "success"
          });    
        });
       
      } else if (
        /* Read more about handling dismissals below */
        result.dismiss === Swal.DismissReason.cancel
      ) {
        swalWithBootstrapButtons.fire({
          title: "Cancelled",
          icon: "error"
        });
      }
    });
   

  }

}
