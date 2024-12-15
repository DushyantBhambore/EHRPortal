import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GetProviderAppoinmentComponent } from './get-provider-appoinment.component';

describe('GetProviderAppoinmentComponent', () => {
  let component: GetProviderAppoinmentComponent;
  let fixture: ComponentFixture<GetProviderAppoinmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GetProviderAppoinmentComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(GetProviderAppoinmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
