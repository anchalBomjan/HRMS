import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssignStockEditComponent } from './assign-stock-edit.component';

describe('AssignStockEditComponent', () => {
  let component: AssignStockEditComponent;
  let fixture: ComponentFixture<AssignStockEditComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AssignStockEditComponent]
    });
    fixture = TestBed.createComponent(AssignStockEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
