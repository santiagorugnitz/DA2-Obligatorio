import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccommodationCommentsComponent } from './accommodation-comments.component';

describe('AccommodationCommentsComponent', () => {
  let component: AccommodationCommentsComponent;
  let fixture: ComponentFixture<AccommodationCommentsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AccommodationCommentsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AccommodationCommentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
