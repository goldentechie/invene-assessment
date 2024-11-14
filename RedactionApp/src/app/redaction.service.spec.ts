import { TestBed } from '@angular/core/testing';

import { RedactionService } from './redaction.service';

describe('RedactionService', () => {
  let service: RedactionService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RedactionService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
