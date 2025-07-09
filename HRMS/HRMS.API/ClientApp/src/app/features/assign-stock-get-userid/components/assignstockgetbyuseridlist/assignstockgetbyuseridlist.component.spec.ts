import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssignstockgetbyuseridlistComponent } from './assignstockgetbyuseridlist.component';

describe('AssignstockgetbyuseridlistComponent', () => {
  let component: AssignstockgetbyuseridlistComponent;
  let fixture: ComponentFixture<AssignstockgetbyuseridlistComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AssignstockgetbyuseridlistComponent]
    });
    fixture = TestBed.createComponent(AssignstockgetbyuseridlistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
