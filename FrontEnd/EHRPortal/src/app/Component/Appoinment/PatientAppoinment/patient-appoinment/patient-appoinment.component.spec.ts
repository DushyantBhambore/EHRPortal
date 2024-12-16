import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientAppoinmentComponent } from './patient-appoinment.component';

describe('PatientAppoinmentComponent', () => {
  let component: PatientAppoinmentComponent;
  let fixture: ComponentFixture<PatientAppoinmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PatientAppoinmentComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PatientAppoinmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
