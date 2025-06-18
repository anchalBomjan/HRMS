import { TestBed } from '@angular/core/testing';

import { StockassignmentService } from './stockassignment.service';

describe('StockassignmentService', () => {
  let service: StockassignmentService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(StockassignmentService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
