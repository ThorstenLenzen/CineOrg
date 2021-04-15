import { TestBed } from '@angular/core/testing';

import { ExMovieService } from './ex-movie.service';

describe('ExMovieService', () => {
  let service: ExMovieService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ExMovieService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
