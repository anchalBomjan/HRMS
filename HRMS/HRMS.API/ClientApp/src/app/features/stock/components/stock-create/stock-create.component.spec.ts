import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StockCreateComponent } from './stock-create.component';

describe('StockCreateComponent', () => {
  let component: StockCreateComponent;
  let fixture: ComponentFixture<StockCreateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [StockCreateComponent]
    });
    fixture = TestBed.createComponent(StockCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
