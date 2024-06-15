import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HearingCreateComponent } from './hearing-create.component';

describe('HearingCreateComponent', () => {
  let component: HearingCreateComponent;
  let fixture: ComponentFixture<HearingCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HearingCreateComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(HearingCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
