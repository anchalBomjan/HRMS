import { TestBed } from '@angular/core/testing';

import { EmployeeApiServiceService } from './employee-api-service.service';

describe('EmployeeApiServiceService', () => {
  let service: EmployeeApiServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EmployeeApiServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
