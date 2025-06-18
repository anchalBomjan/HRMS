import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StockassignmentListComponent } from './stockassignment-list.component';

describe('StockassignmentListComponent', () => {
  let component: StockassignmentListComponent;
  let fixture: ComponentFixture<StockassignmentListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [StockassignmentListComponent]
    });
    fixture = TestBed.createComponent(StockassignmentListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
