import { TestBed } from '@angular/core/testing';

import { SpotNotExistsGuard } from './spot-not-exists.guard';

describe('SpotNotExistsGuard', () => {
  let guard: SpotNotExistsGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(SpotNotExistsGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
