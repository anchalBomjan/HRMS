import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssignRoleEditComponent } from './assign-role-edit.component';

describe('AssignRoleEditComponent', () => {
  let component: AssignRoleEditComponent;
  let fixture: ComponentFixture<AssignRoleEditComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AssignRoleEditComponent]
    });
    fixture = TestBed.createComponent(AssignRoleEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
