import { TestBed } from '@angular/core/testing';

import { FiltersResolver } from './filters.resolver';

describe('FiltersResolver', () => {
  let resolver: FiltersResolver;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    resolver = TestBed.inject(FiltersResolver);
  });

  it('should be created', () => {
    expect(resolver).toBeTruthy();
  });
});
