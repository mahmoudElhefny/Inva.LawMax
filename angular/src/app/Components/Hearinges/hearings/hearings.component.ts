import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Hearing, HearingCreate } from './hearing';
import { GetPaginationTypeInput } from '../../Cases/case-list/case-list/GetCaseTypeInput';
import { HearingService } from './hearing.service';
import { CaseService } from '../../Cases/case.service';
import { Case } from '../../Cases/case';
import Swal from 'sweetalert2';


@Component({
  selector: 'app-hearings',
  standalone: false,
  templateUrl: './hearings.component.html',
  styleUrl: './hearings.component.scss'
})
export class HearingsComponent {
  
  Hearings: Hearing[] = [];
  totalCount: number = 0;
  isPopupVisible: boolean = false;
  currentHearing: Hearing | null = null;
  paginationInput: GetPaginationTypeInput = {
    skipCount: 0,
    maxResultCount: 3,
    sorting: 'number'
  };
  constructor(private snackBar: MatSnackBar,private _hearingService:HearingService,
    ) {

  }
  ngOnInit(): void {
    this.loadHearings();
    // this.countHearings();
  }
  loadHearings(): void {
    this._hearingService.getHearings(this.paginationInput).subscribe((data) => { 
      this.Hearings = data.data.items;
      this.totalCount = data.data.totalCount;
    });
  }
  openPopup(HearingData: Hearing | null = null): void {
    this.currentHearing = HearingData;
    this.isPopupVisible = true;
  }
  closePopup(): void {
    this.isPopupVisible = false;
    this.currentHearing = null;
  }
  handleFormSubmit(HearingData: HearingCreate): void {
    if (this.currentHearing) {
      // Update Hearing
      this._hearingService.updateHearing(HearingData).subscribe(
        (response) => {
        Swal.fire({
          title: "Updated succesfully!",
          text: "You clicked the button!",
          icon: "success"
        });
        this.loadHearings();
        this.closePopup();
      },
    (error)=>{
      Swal.fire({
        icon: "error",
        title: "Oops...",
        text: "Something went wrong!",
      });
        this.loadHearings();
        this.closePopup();
    }
  );
} else {
      // Create new Hearing
      this._hearingService.createHearing(HearingData).subscribe(() => {
        Swal.fire({
          title: "saved succesfully!",
          text: "You clicked the button!",
          icon: "success"
        });
        this.loadHearings();
        this.closePopup();
      });
    }
  }
  onPageChange(event: any): void {
    this.paginationInput.skipCount = event.pageIndex * event.pageSize;
    this.paginationInput.maxResultCount = event.pageSize;
    this.loadHearings();
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
        this._hearingService.deleteHearing(id).subscribe(() => {
          this.loadHearings();   
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

