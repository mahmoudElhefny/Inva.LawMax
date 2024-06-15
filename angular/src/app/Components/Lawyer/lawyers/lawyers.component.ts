import { Component, OnInit } from '@angular/core';
import { LawyerService } from './lawyer.service';
import { Lawyer, LawyerCreate } from './lawyer';
import { MatSnackBar } from '@angular/material/snack-bar';
import { GetPaginationTypeInput } from '../../Cases/case-list/case-list/GetCaseTypeInput';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-lawyers',
  standalone: false,
  templateUrl: './lawyers.component.html',
  styleUrl: './lawyers.component.scss'
})
export class LawyersComponent implements OnInit {
  lawyers: Lawyer[] = [];
  totalCount: number = 0;
  isPopupVisible: boolean = false;
  currentLawyer: Lawyer | null = null;
  paginationInput: GetPaginationTypeInput = {
    skipCount: 0,
    maxResultCount: 3,
    sorting: 'number'
  };

  constructor(private lawyerService: LawyerService,private snackBar: MatSnackBar) {

  }
  ngOnInit(): void {
    this.loadLawyers();
  }
  loadLawyers(): void {
    this.lawyerService.getLawyers(this.paginationInput).subscribe((data) => { 
      this.lawyers = data.data.items;
      console.log(this.lawyers)
      this.totalCount = data.data.totalCount;
    });
    
  }

  openPopup(caseData: Lawyer | null = null): void {
    this.currentLawyer = caseData;
    this.isPopupVisible = true;
  }
  closePopup(): void {
    this.isPopupVisible = false;
    this.currentLawyer = null;
  }
  handleFormSubmit(_lawyerData: LawyerCreate): void {
    if (this.currentLawyer) {
      // Update Lawyer
      this.lawyerService.updateLawyer(_lawyerData).subscribe((response) => {
        Swal.fire({
          title: "Updated succesfully!",
          text: "You clicked the button!",
          icon: "success"
        });
        this.loadLawyers();
        this.closePopup();
      },
    (error)=>{
      Swal.fire({
        icon: "error",
        title: "Oops...",
        text: "Something went wrong!",
      });
        this.loadLawyers();
        this.closePopup();
    }
   )
    } else {
      // Create new Lawyer
      this.lawyerService.createLawyer(_lawyerData).subscribe((response) => {
        Swal.fire({
          title: "Added succesfully!",
          text: "You clicked the button!",
          icon: "success"
        });
        this.loadLawyers();
        this.closePopup();
      },
    (error)=>{
      Swal.fire({
        icon: "error",
        title: "Oops...",
        text: "Something went wrong!",
      });
        this.loadLawyers();
        this.closePopup();
    })
    }
}
  onPageChange(event: any): void {
    this.paginationInput.skipCount = event.pageIndex * event.pageSize;
    this.paginationInput.maxResultCount = event.pageSize;
    this.loadLawyers();
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
        this.lawyerService.deleteLawyer(id).subscribe(() => {
          this.loadLawyers();   
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
