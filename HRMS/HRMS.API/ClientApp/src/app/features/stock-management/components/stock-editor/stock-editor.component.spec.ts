import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StockEditorComponent } from './stock-editor.component';

describe('StockEditorComponent', () => {
  let component: StockEditorComponent;
  let fixture: ComponentFixture<StockEditorComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [StockEditorComponent]
    });
    fixture = TestBed.createComponent(StockEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
