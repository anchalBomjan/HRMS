import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssignStockListComponent } from './assign-stock-list.component';

describe('AssignStockListComponent', () => {
  let component: AssignStockListComponent;
  let fixture: ComponentFixture<AssignStockListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AssignStockListComponent]
    });
    fixture = TestBed.createComponent(AssignStockListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
