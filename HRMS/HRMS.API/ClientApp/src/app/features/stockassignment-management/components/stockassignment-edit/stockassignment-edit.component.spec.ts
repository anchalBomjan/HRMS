import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StockassignmentEditComponent } from './stockassignment-edit.component';

describe('StockassignmentEditComponent', () => {
  let component: StockassignmentEditComponent;
  let fixture: ComponentFixture<StockassignmentEditComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [StockassignmentEditComponent]
    });
    fixture = TestBed.createComponent(StockassignmentEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
