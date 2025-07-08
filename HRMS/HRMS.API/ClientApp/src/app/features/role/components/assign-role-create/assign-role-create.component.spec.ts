import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssignRoleCreateComponent } from './assign-role-create.component';

describe('AssignRoleCreateComponent', () => {
  let component: AssignRoleCreateComponent;
  let fixture: ComponentFixture<AssignRoleCreateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AssignRoleCreateComponent]
    });
    fixture = TestBed.createComponent(AssignRoleCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
