import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StockassignmentDetailsComponent } from './stockassignment-details.component';

describe('StockassignmentDetailsComponent', () => {
  let component: StockassignmentDetailsComponent;
  let fixture: ComponentFixture<StockassignmentDetailsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [StockassignmentDetailsComponent]
    });
    fixture = TestBed.createComponent(StockassignmentDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
