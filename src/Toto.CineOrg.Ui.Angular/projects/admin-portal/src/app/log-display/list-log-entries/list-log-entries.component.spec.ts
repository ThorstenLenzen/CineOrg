import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListLogEntriesComponent } from './list-log-entries.component';

describe('ListLogEntriesComponent', () => {
  let component: ListLogEntriesComponent;
  let fixture: ComponentFixture<ListLogEntriesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListLogEntriesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListLogEntriesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
