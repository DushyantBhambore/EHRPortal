import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GetPatientAppoinmentComponent } from './get-patient-appoinment.component';

describe('GetPatientAppoinmentComponent', () => {
  let component: GetPatientAppoinmentComponent;
  let fixture: ComponentFixture<GetPatientAppoinmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GetPatientAppoinmentComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(GetPatientAppoinmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
