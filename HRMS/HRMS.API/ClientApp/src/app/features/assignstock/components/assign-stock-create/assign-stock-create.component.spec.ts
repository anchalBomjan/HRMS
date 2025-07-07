import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssignStockCreateComponent } from './assign-stock-create.component';

describe('AssignStockCreateComponent', () => {
  let component: AssignStockCreateComponent;
  let fixture: ComponentFixture<AssignStockCreateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AssignStockCreateComponent]
    });
    fixture = TestBed.createComponent(AssignStockCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
