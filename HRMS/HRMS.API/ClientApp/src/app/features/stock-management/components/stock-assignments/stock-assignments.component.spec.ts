import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StockAssignmentsComponent } from './stock-assignments.component';

describe('StockAssignmentsComponent', () => {
  let component: StockAssignmentsComponent;
  let fixture: ComponentFixture<StockAssignmentsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [StockAssignmentsComponent]
    });
    fixture = TestBed.createComponent(StockAssignmentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
