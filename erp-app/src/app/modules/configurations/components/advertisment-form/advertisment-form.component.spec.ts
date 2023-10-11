import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdvertismentFormComponent } from './advertisment-form.component';

describe('AdvertismentFormComponent', () => {
  let component: AdvertismentFormComponent;
  let fixture: ComponentFixture<AdvertismentFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdvertismentFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AdvertismentFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
