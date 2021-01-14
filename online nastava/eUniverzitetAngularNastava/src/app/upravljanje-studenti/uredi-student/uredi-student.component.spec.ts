import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UrediStudentComponent } from './uredi-student.component';

describe('UrediStudentComponent', () => {
  let component: UrediStudentComponent;
  let fixture: ComponentFixture<UrediStudentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UrediStudentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UrediStudentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
