import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssignroleListComponent } from './assignrole-list.component';

describe('AssignroleListComponent', () => {
  let component: AssignroleListComponent;
  let fixture: ComponentFixture<AssignroleListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AssignroleListComponent]
    });
    fixture = TestBed.createComponent(AssignroleListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
