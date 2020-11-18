import { TestBed } from '@angular/core/testing';

import { UserNotLogguedGuard } from './user-not-loggued.guard';

describe('UserNotLogguedGuard', () => {
  let guard: UserNotLogguedGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(UserNotLogguedGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
