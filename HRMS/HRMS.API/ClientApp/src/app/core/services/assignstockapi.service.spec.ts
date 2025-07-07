import { TestBed } from '@angular/core/testing';

import { AssignstockapiService } from './assignstockapi.service';

describe('AssignstockapiService', () => {
  let service: AssignstockapiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AssignstockapiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
