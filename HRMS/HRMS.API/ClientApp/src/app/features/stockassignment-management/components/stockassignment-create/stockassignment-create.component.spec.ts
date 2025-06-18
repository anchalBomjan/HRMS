import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StockassignmentCreateComponent } from './stockassignment-create.component';

describe('StockassignmentCreateComponent', () => {
  let component: StockassignmentCreateComponent;
  let fixture: ComponentFixture<StockassignmentCreateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [StockassignmentCreateComponent]
    });
    fixture = TestBed.createComponent(StockassignmentCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
