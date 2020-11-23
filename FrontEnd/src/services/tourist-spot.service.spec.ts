import { TestBed } from '@angular/core/testing';

import { TouristSpotService } from './tourist-spot.service';

describe('TouristSpotService', () => {
  let service: TouristSpotService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TouristSpotService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
