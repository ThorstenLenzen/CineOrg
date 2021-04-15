import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditMovieDialogComponent } from './edit-movie-dialog.component';

describe('EditMovieDialogComponent', () => {
  let component: EditMovieDialogComponent;
  let fixture: ComponentFixture<EditMovieDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditMovieDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditMovieDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
