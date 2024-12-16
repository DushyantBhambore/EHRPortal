import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProviderAppoinmetComponent } from './provider-appoinmet.component';

describe('ProviderAppoinmetComponent', () => {
  let component: ProviderAppoinmetComponent;
  let fixture: ComponentFixture<ProviderAppoinmetComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProviderAppoinmetComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ProviderAppoinmetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
